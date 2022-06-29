using UnityEditor;
using UnityEditor.SearchService;

namespace Cocone.ProjectP3
{
    /**
     * Configuration.assetを変更するためのクラス
     */
    public static class ConfigurationSetter
    {
        private const string configrationAssetPath = "Assets/App/Config/Configuration.asset";
        private const string vivoxAssetPath = "Assets/App/Config/VivoxSettings.asset";
        private const string appCenterAssetPath = "Assets/Plugins/AppCenter/AppCenterSettings.asset";

        /**
         * Configurationアセットの値を書き換え
         * NOTE:引数だいぶ増えるようならリファクタ検討
         */
        public static void SetConfigurationValues(int buildNumber, SceneType sceneType)
        {
            SetBuildNumber(buildNumber);
            SetSceneType(sceneType);
            
            // アセット保存
            AssetDatabase.SaveAssets();
        }
        
        /**
         * デフォルトシーンタイプを変更する
         */
        private static void SetSceneType(SceneType sceneType)
        {
            // NOTE:あまりにも同じアセット読むようだったら、一度読むだけにするか要検討
            var config = AssetDatabase.LoadAssetAtPath<Configuration>(configrationAssetPath);
            config.DefaultSceneType = sceneType;
            EditorUtility.SetDirty(config);
        }

        /**
         * ビルド番号の設定
         */
        private static void SetBuildNumber(int buildNumber)
        {
            var config = AssetDatabase.LoadAssetAtPath<Configuration>(configrationAssetPath);
            config.BuildNo = buildNumber.ToString();
            EditorUtility.SetDirty(config);
        }

        public static void SetVivoxSetting(string server, string domain, string issuer, string key)
        {
            var vivoxSettings = AssetDatabase.LoadAssetAtPath<VivoxSettings>(vivoxAssetPath);
            vivoxSettings.VivoxServerURL = server;
            vivoxSettings.VivoxDomain = domain;
            vivoxSettings.TokenIssuer = issuer;
            vivoxSettings.TokenKey = key;
            EditorUtility.SetDirty(vivoxSettings);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// AppCenterの設定を環境別に変更する
        /// </summary>
        /// <param name="distribute"></param>
        public static void SetAppCenterParam(bool distribute)
        {
            var appCenterSettings = AssetDatabase.LoadAssetAtPath<AppCenterSettings>(appCenterAssetPath);

            appCenterSettings.UseDistribute = distribute;
            appCenterSettings.EnableDistributeForDebuggableBuild = distribute;

            EditorUtility.SetDirty(appCenterSettings);
        }
    }
}