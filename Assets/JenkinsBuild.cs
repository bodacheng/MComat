#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEngine;

using System.IO;
using UnityEditor;
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