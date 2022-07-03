using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Pipeline.Utilities;
#endif

namespace Cocone.ProjectP3
{
    public static class BuildAddressableAssets
    {
#if UNITY_EDITOR

        // バッチモード用一括ビルド
        public static void BatchBuild()
        {
            string[] args = System.Environment.GetCommandLineArgs();
            BatchBuildInternal(args);
        }

        public static bool IsItemGroup(AddressableAssetGroup group)
        {
            var groupName = group.Name;
            if (groupName.Length != 10) return false;
            var category = groupName[0];
            var theme = groupName[1];
            if (category != 'F' && category != 'I' && category != 'P') return false;
            if (theme != 'G' && theme != 'N') return false;
            return true;
        }

        private static void BatchBuildInternal(string[] args)
        {
            // 引数取得
            string assetProfile = "P3Dev";
            bool useReleaseList = false;
            BuildTarget buildTarget = BuildTarget.iOS;
            BuildTargetGroup buildTargetGroup = BuildTargetGroup.iOS;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-assetVersion":
                        i++; // ignore assetVersion
                        break;
                    
                    case "-buildTarget":
                        buildTarget = (BuildTarget) System.Enum.Parse(typeof(BuildTarget), args[i + 1]);
                        buildTargetGroup = (BuildTargetGroup) System.Enum.Parse(typeof(BuildTargetGroup), args[i + 1]);
                        i++;
                        break;

                    case "-assetProfile":
                        assetProfile = args[i + 1];
                        i++;
                        break;

                    case "-useReleaseList":
                        useReleaseList = true;
                        break;
                }
            }

            var settings = GetSettings();
            var profileId = settings.profileSettings.GetProfileId(assetProfile);
            settings.activeProfileId = profileId;

            if (useReleaseList)
            {
                var path = $"Assets/ExternalAssets/ReleaseTheme.yaml";
                if (File.Exists(path))
                {
                    var themeList = ReleaseThemeList.Deserialize(path);
                    var itemGroupList = settings.groups.Where(IsItemGroup).ToList();
                    itemGroupList.ForEach(x =>
                    {
                        if (!themeList.IsReleasedItem(x.Name)) settings.RemoveGroup(x);
                    });
                }
            }

            // save addressable setting
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            CleanBuild(); // TODO: TargetPlatform設定は要らない？
        }

        [MenuItem("P3/Build/Addressable(テスト用)/iOS/Alpha")]
        public static void BuildCommandAddressableIOSBuildAlpha()
        {
            var workspace = ".";
            var unityMethod = "Cocone.ProjectP3.BuildAddressableAssets.BatchBuild";
            var buildTarget = "iOS";

            string[] args =
            {
                "-projectPath", workspace,
                "-quit", "-batchmode",
                "-executeMethod", unityMethod,
                "-buildTarget", buildTarget,
                "-assetProfile", "P3Dev"
            };
            BatchBuildInternal(args);
        }

        [MenuItem("P3/Build/Addressable(テスト用)/iOS/Test")]
        public static void BuildCommandAddressableIOSBuildTest()
        {
            var workspace = ".";
            var unityMethod = "Cocone.ProjectP3.BuildAddressableAssets.BatchBuild";
            var buildTarget = "iOS";

            string[] args =
            {
                "-projectPath", workspace,
                "-quit", "-batchmode",
                "-executeMethod", unityMethod,
                "-buildTarget", buildTarget,
                "-assetProfile", "P3Test",
                "-useReleaseList"
            };
            BatchBuildInternal(args);
        }

        [MenuItem("P3/Build/Addressable(テスト用)/Android/Alpha")]
        public static void BuildCommandAddressableAndroidBuildAlpha()
        {
            var workspace = ".";
            var unityMethod = "Cocone.ProjectP3.BuildAddressableAssets.BatchBuild";
            var buildTarget = "Android";

            string[] args =
            {
                "-projectPath", workspace,
                "-quit", "-batchmode",
                "-executeMethod", unityMethod,
                "-buildTarget", buildTarget,
                "-assetProfile", "P3Dev"
            };
            BatchBuildInternal(args);
        }

        [MenuItem("P3/Build/Addressable(テスト用)/Android/Test")]
        public static void BuildCommandAddressableAndroidBuildTest()
        {
            var workspace = ".";
            var unityMethod = "Cocone.ProjectP3.BuildAddressableAssets.BatchBuild";
            var buildTarget = "Android";

            string[] args =
            {
                "-projectPath", workspace,
                "-quit", "-batchmode",
                "-executeMethod", unityMethod,
                "-buildTarget", buildTarget,
                "-assetProfile", "P3Test"
            };
            BatchBuildInternal(args);
        }

        // アセットバンドルをクリーンビルドします
        [MenuItem("Tools/Asset/CleanBuild")]
        public static void CleanBuild()
        {
            AddressableAssetSettings.CleanPlayerContent();
            BuildCache.PurgeCache(false);
            AddressableAssetSettings.BuildPlayerContent();
        }

/*
    [MenuItem("Tools/Asset/UpdateRemotePath")]
    public static void UpdateRemotePath()
    {
        var list = AssetDatabase
                .FindAssets( "t:BundledAssetGroupSchema" )
                .Select( c => AssetDatabase.GUIDToAssetPath( c ) )
                .Select( c => AssetDatabase.LoadAssetAtPath<BundledAssetGroupSchema>( c ) );

        var settings = GetSettings();
        foreach ( var schema in list )
        {
            if (schema.Group.name == "Default Local Group")
            {
                schema.BuildPath.SetVariableByName( settings, "LocalBuildPath" );
                schema.LoadPath.SetVariableByName( settings, "LocalLoadPath" );
            }
            else
            {
                schema.BuildPath.SetVariableByName( settings, "RemoteBuildPath" );
                schema.LoadPath.SetVariableByName( settings, "RemoteLoadPath" );
            }
        }
    }
*/
        // AddressableAssetSettings を取得します
        public static AddressableAssetSettings GetSettings()
        {
            var guidList = AssetDatabase.FindAssets("t:AddressableAssetSettings");
            var guid = guidList.FirstOrDefault();
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var settings = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(path);

            return settings;
        }
#endif
    }
}