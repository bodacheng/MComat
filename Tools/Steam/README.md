# Windows Steam packaging

After a successful Windows x64 Release build, create the Steam upload ZIP with Windows PowerShell 5.1 or newer:

```powershell
& .\Tools\Steam\Package-SteamBuild.ps1 `
    -PlayerPath .\Builds\Steam\QA-20261015 `
    -ArchivePath .\Builds\Steam\mcombat-win-release.zip
```

Use a new archive name outside the player directory. The script refuses to overwrite an existing ZIP and does not modify the Unity build output. It packages the contents of the player directory at the ZIP root, without an extra enclosing folder.

Paths containing `DoNotShip`, all `.pdb` files, and every `steam_appid.txt` are excluded (case insensitive). Keep those diagnostics outside the upload ZIP for local investigations. The actual Burst runtime `mcombat_Data/Plugins/x86_64/lib_burst_generated.dll` is required and retained, along with the executable, Unity player, managed assembly, and Steam native library.

Before reporting success, the script checks entry names, count and sizes, required runtime entries, and the archived Burst DLL SHA-256 against its source. It reports the final archive SHA-256 and size. Source directories containing reparse points are rejected. An incomplete ZIP created by this invocation is removed on failure.

This script only prepares a package; it does not upload to Steam or alter any Steam branch.
