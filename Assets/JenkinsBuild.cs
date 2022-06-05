#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEngine;

using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;

public static class JenkinsBuild
{
    static void KeystoreSet() {
        // keystoreの指定。アプリに含めないようにEditorフォルダに入れてます。
        PlayerSettings.Android.keystoreName = Directory.GetCurrentDirectory() + "/user.keystore";
        PlayerSettings.Android.minifyRelease = true;
        PlayerSettings.Android.minifyDebug = false;
        // keystore作成時に設定したkestoreのパスワード
        PlayerSettings.Android.keystorePass = "890710gxY";
        // keystore作成時に設定したalias名
        PlayerSettings.Android.keyaliasName = "bodacheng";
        // keystore作成時に設定したaliasのパスワード
        PlayerSettings.Android.keyaliasPass = "890710gxY";
    }
    
    [MenuItem("Build/ApplicationBuild/Android")]
    public static void BuildAndroid()
    {
        //AndroidにSwitch Platform
        //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        
        Debug.Log("开始构建AAB");
        
        var scene_name_array = CreateBuildTargetScenes().ToArray();
        PlayerSettings.applicationIdentifier = "com.BO.MCombat";
        PlayerSettings.productName = "MCombat";
        PlayerSettings.companyName = "BO";
        KeystoreSet();

        PlayerSettings.defaultScreenWidth = 1920;
        PlayerSettings.defaultScreenHeight = 1080;
        
        //AppBundleは使用しない（本番ビルドのときだけ使うイメージ）
        EditorUserBuildSettings.buildAppBundle = true;
        EditorUserBuildSettings.development = false;

        AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
        
        KeystoreSet();

        BuildOptions options = BuildOptions.CompressWithLz4;
        
        var result = BuildPipeline.BuildPlayer(scene_name_array,"Android_Build/AppBundle" , BuildTarget.Android, options);
        
        if (result.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("成功：" +result.summary);
        }
        else
        {
            Debug.Log("失败？：" +result.summary.result);
        }
    }
    
    public static string build_script 
            = "Assets/AddressableAssetsData/DataBuilders/BuildScriptPackedMode.asset";
        public static string settings_asset 
            = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
        public static string profile_name = "Default";
        private static AddressableAssetSettings settings;

        static void getSettingsObject(string settingsAsset) {
            // This step is optional, you can also use the default settings:
            //settings = AddressableAssetSettingsDefaultObject.Settings;

            settings
                = AssetDatabase.LoadAssetAtPath<ScriptableObject>(settingsAsset)
                    as AddressableAssetSettings;

            if (settings == null)
                Debug.LogError($"{settingsAsset} couldn't be found or isn't " +
                               $"a settings object.");
        }

        static void setProfile(string profile) {
            string profileId = settings.profileSettings.GetProfileId(profile);
            if (String.IsNullOrEmpty(profileId))
                Debug.LogWarning($"Couldn't find a profile named, {profile}, " +
                                 $"using current profile instead.");
            else
                settings.activeProfileId = profileId;
        }

        static void setBuilder(IDataBuilder builder) {
            int index = settings.DataBuilders.IndexOf((ScriptableObject)builder);

            if (index > 0)
                settings.ActivePlayerDataBuilderIndex = index;
            else
                Debug.LogWarning($"{builder} must be added to the " +
                                 $"DataBuilders list before it can be made " +
                                 $"active. Using last run builder instead.");
        }

        static bool buildAddressableContent() {
            AddressableAssetSettings
                .BuildPlayerContent(out AddressablesPlayerBuildResult result);
            bool success = string.IsNullOrEmpty(result.Error);

            if (!success) {
                Debug.LogError("Addressables build error encountered: " + result.Error);
            }
            return success;
        }
    
    
    [MenuItem("Window/Asset Management/Addressables/Build Addressables only")]
    public static bool BuildAddressables() {
        getSettingsObject(settings_asset);
        setProfile(profile_name);
        IDataBuilder builderScript
            = AssetDatabase.LoadAssetAtPath<ScriptableObject>(build_script) as IDataBuilder;

        if (builderScript == null) {
            Debug.LogError(build_script + " couldn't be found or isn't a build script.");
            return false;
        }

        setBuilder(builderScript);

        return buildAddressableContent();
    }
    
    public static void BuildIOS()
    {
        Debug.Log ("[ScriptLog] Start Build iOS");

        // リリースビルドではない場合Profiler等に繋げるようにする
        //BuildOptions opt = BuildOptions.SymlinkLibraries;
        string[] scenes = CreateBuildTargetScenes().ToArray();
        BuildPipeline.BuildPlayer(scenes,"BuildiOS" , BuildTarget.iOS, BuildOptions.Development);
    }

    #region Util
  
    private static IEnumerable<string> CreateBuildTargetScenes()
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                yield return scene.path;
        }
    }
    #endregion
}
#endif