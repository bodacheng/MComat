using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public sealed class SteamBuildSafetyCheck : IPreprocessBuildWithReport
{
    const string ExpectedSteamAppId = "4345170";
    const string PlayFabSettingsPath = "Assets/PlayFabSDK/Shared/Public/Resources/PlayFabSharedSettings.asset";

    static readonly string[] ForbiddenDefines =
    {
        "ENABLE_PLAYFABSERVER_API",
        "ENABLE_PLAYFABADMIN_API",
        "ENABLE_PLAYFAB_SECRETKEY"
    };

    public int callbackOrder => -10000;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (BuildPipeline.GetBuildTargetGroup(report.summary.platform) != BuildTargetGroup.Standalone)
        {
            return;
        }

        ValidateStandaloneDefines();
        ValidatePlayFabSecret();
        ValidateSteamAppId();
    }

    static void ValidateStandaloneDefines()
    {
        var defines = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.Standalone)
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        var forbidden = defines.Where(ForbiddenDefines.Contains).ToArray();
        if (forbidden.Length > 0)
        {
            throw new BuildFailedException("Steam build must not enable PlayFab Server/Admin secret-key APIs: " + string.Join(", ", forbidden));
        }
    }

    static void ValidatePlayFabSecret()
    {
        var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), PlayFabSettingsPath);
        if (!File.Exists(absolutePath))
        {
            throw new BuildFailedException("Missing PlayFab shared settings asset: " + PlayFabSettingsPath);
        }

        var secretLine = File.ReadLines(absolutePath)
            .FirstOrDefault(line => line.TrimStart().StartsWith("DeveloperSecretKey:", StringComparison.Ordinal));
        if (secretLine == null)
        {
            return;
        }

        var value = secretLine.Substring(secretLine.IndexOf(':') + 1).Trim();
        if (!string.IsNullOrEmpty(value))
        {
            throw new BuildFailedException("Steam build must not include PlayFab DeveloperSecretKey in Resources.");
        }
    }

    static void ValidateSteamAppId()
    {
        var appIdPath = Path.Combine(Directory.GetCurrentDirectory(), "steam_appid.txt");
        if (!File.Exists(appIdPath))
        {
            throw new BuildFailedException("Missing steam_appid.txt in project root for Steam development build.");
        }

        var appId = File.ReadAllText(appIdPath).Trim();
        if (appId != ExpectedSteamAppId)
        {
            throw new BuildFailedException("steam_appid.txt must contain " + ExpectedSteamAppId + " for this branch.");
        }
    }
}
