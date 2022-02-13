using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public static class JenkinsBuild
{
    [MenuItem("Build/ApplicationBuild/Android")]
    public static void BuildAndroid()
    {
        //AndroidにSwitch Platform
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        var scene_name_array = CreateBuildTargetScenes().ToArray();
        PlayerSettings.applicationIdentifier = "com.hogehoge.fugafuga";
        PlayerSettings.productName = "MCombat";
        PlayerSettings.companyName = "BO";
        
        //Splash Screenをオフにする(Personalだと動かないよ）
        PlayerSettings.SplashScreen.show = true;
        PlayerSettings.SplashScreen.showUnityLogo = false;

        PlayerSettings.defaultScreenWidth = 1920;
        PlayerSettings.defaultScreenHeight = 1080;
        
        //AppBundleは使用しない（本番ビルドのときだけ使うイメージ）
        EditorUserBuildSettings.buildAppBundle = false;

        BuildPipeline.BuildPlayer(scene_name_array,"Build.apk" , BuildTarget.Android, BuildOptions.Development);
    }

    public static void BuildIOS()
    {
        Debug.Log ("[ScriptLog] Start Build iOS");

        // リリースビルドではない場合Profiler等に繋げるようにする
        BuildOptions opt = BuildOptions.SymlinkLibraries;
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