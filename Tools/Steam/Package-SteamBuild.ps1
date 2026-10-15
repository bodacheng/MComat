# Windows Steam release packaging. Keep the Unity build and its diagnostics intact.
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$PlayerPath,
    [Parameter(Mandatory = $true)]
    [string]$ArchivePath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

function Test-ExcludedEntry([string]$RelativePath) {
    return $RelativePath -match '(^|/)[^/]*DoNotShip[^/]*(/|$)' -or
        $RelativePath -match '\.pdb$' -or
        $RelativePath -match '(^|/)steam_appid\.txt$'
}

function Assert-NoReparseAncestor([System.IO.DirectoryInfo]$Directory) {
    $ancestor = $Directory
    while ($null -ne $ancestor) {
        if ($ancestor.Attributes -band [System.IO.FileAttributes]::ReparsePoint) {
            throw 'Player and archive paths must not pass through reparse points.'
        }
        $ancestor = $ancestor.Parent
    }
}

$player = Get-Item -LiteralPath $PlayerPath
if ($player -isnot [System.IO.DirectoryInfo]) { throw 'PlayerPath must be a directory.' }
Assert-NoReparseAncestor $player
$playerRoot = $player.FullName.TrimEnd([char]'\')
$playerPrefix = $playerRoot + '\'
$zipPath = [System.IO.Path]::GetFullPath($ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($ArchivePath))
if ($zipPath.Equals($playerRoot, [System.StringComparison]::OrdinalIgnoreCase) -or
    $zipPath.StartsWith($playerPrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
    throw 'ArchivePath must be outside PlayerPath.'
}
if ([System.IO.Path]::GetExtension($zipPath) -ine '.zip') { throw 'ArchivePath must end in .zip.' }
if (Test-Path -LiteralPath $zipPath) { throw 'Refusing to overwrite an existing upload archive.' }
if (!(Test-Path -LiteralPath ([System.IO.Path]::GetDirectoryName($zipPath)) -PathType Container)) {
    throw 'The archive parent directory must already exist.'
}

Assert-NoReparseAncestor (Get-Item -LiteralPath ([System.IO.Path]::GetDirectoryName($zipPath)))

$sourceEntries = @($player) + @(Get-ChildItem -LiteralPath $playerRoot -Recurse -Force)
if (@($sourceEntries | Where-Object { $_.Attributes -band [System.IO.FileAttributes]::ReparsePoint }).Count -gt 0) {
    throw 'PlayerPath must not contain symbolic links or other reparse points.'
}
$files = @($sourceEntries | Where-Object { $_ -is [System.IO.FileInfo] } | ForEach-Object {
    $relative = $_.FullName.Substring($playerPrefix.Length).Replace('\', '/')
    if (!(Test-ExcludedEntry $relative)) {
        [pscustomobject]@{ Source = $_; RelativePath = $relative }
    }
} | Sort-Object RelativePath)
$requiredPaths = @(
    'mcombat.exe',
    'UnityPlayer.dll',
    'mcombat_Data/globalgamemanagers',
    'mcombat_Data/Managed/Assembly-CSharp.dll',
    'mcombat_Data/Plugins/x86_64/steam_api64.dll',
    'mcombat_Data/Plugins/x86_64/lib_burst_generated.dll'
)
$selected = @{}
foreach ($file in $files) { $selected.Add($file.RelativePath, $file) }
foreach ($required in $requiredPaths) {
    if (!$selected.ContainsKey($required)) { throw "Missing required runtime file: $required" }
}
$burstPath = 'mcombat_Data/Plugins/x86_64/lib_burst_generated.dll'
$burstHash = (Get-FileHash -LiteralPath $selected[$burstPath].Source.FullName -Algorithm SHA256).Hash

$createdArchive = $false
try {
    $zipStream = [System.IO.File]::Open($zipPath, [System.IO.FileMode]::CreateNew, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    $createdArchive = $true
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($zipStream, [System.IO.Compression.ZipArchiveMode]::Create, $true)
        try {
            foreach ($file in $files) {
                [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile(
                    $archive, $file.Source.FullName, $file.RelativePath, [System.IO.Compression.CompressionLevel]::Optimal) | Out-Null
            }
        } finally { $archive.Dispose() }
    } finally { $zipStream.Dispose() }

    $verify = [System.IO.Compression.ZipFile]::OpenRead($zipPath)
    try {
        if ($verify.Entries.Count -ne $files.Count) { throw 'ZIP entry count does not match the source file list.' }
        foreach ($entry in $verify.Entries) {
            if ((Test-ExcludedEntry $entry.FullName) -or !$selected.ContainsKey($entry.FullName) -or
                $entry.Length -ne $selected[$entry.FullName].Source.Length) {
                throw "Unexpected or incomplete ZIP entry: $($entry.FullName)"
            }
        }
        foreach ($required in $requiredPaths) {
            if (!$verify.GetEntry($selected[$required].RelativePath)) { throw "Required runtime missing from ZIP: $required" }
        }
        $burstStream = $verify.GetEntry($selected[$burstPath].RelativePath).Open()
        $sha256 = [System.Security.Cryptography.SHA256]::Create()
        try {
            $archivedBurstHash = ([System.BitConverter]::ToString($sha256.ComputeHash($burstStream))).Replace('-', '')
        } finally { $sha256.Dispose(); $burstStream.Dispose() }
        if ($archivedBurstHash -ne $burstHash) { throw 'Archived Burst runtime does not match the source DLL.' }
    } finally { $verify.Dispose() }

    [pscustomobject]@{
        ArchivePath = $zipPath
        Files = $files.Count
        SourceBytes = ($files | ForEach-Object { $_.Source.Length } | Measure-Object -Sum).Sum
        ArchiveBytes = (Get-Item -LiteralPath $zipPath).Length
        ArchiveSha256 = (Get-FileHash -LiteralPath $zipPath -Algorithm SHA256).Hash
    }
} catch {
    if ($createdArchive -and (Test-Path -LiteralPath $zipPath)) { Remove-Item -LiteralPath $zipPath -Force }
    throw
}
