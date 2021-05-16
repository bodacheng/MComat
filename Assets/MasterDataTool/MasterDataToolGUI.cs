#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MasterDataTool))]
public class LocalMasterDataToolGUI : Editor {

    MasterDataTool _ConfigManager;
    
    public override void OnInspectorGUI()
    {
        _ConfigManager = (MasterDataTool)target;
        DrawDefaultInspector();
        
        //_ConfigFileManager.SkillConfigTextFile = EditorGUILayout.ObjectField("SkillConfigFile", _ConfigFileManager.SkillConfigTextFile, typeof(TextAsset), true) as TextAsset;
        
        if (GUILayout.Button("根据Resource文件夹生成所有角色配置文件"))
        {
            _ConfigManager.UnitsConfigFileGenerate(_ConfigManager.CharConfigFilePath,_ConfigManager.CharConfigFile);
        }
        
        if (GUILayout.Button("根据Resource文件夹生成,更新技能配置文件"))
        {
            _ConfigManager.SkillConfigFileUpdate(_ConfigManager.SkillConfigPath, _ConfigManager.SkillConfigFile);
        }

        if (GUILayout.Button("(playFab)输出Json格式技能石定义文件(只能在程序启动状态下正常运行)"))
        {
            _ConfigManager.OutputSKStonesCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式技能石商店文件(只能在程序启动状态下正常运行)"))
        {
            _ConfigManager.OutputSKStonesStore();
        }

        if (GUILayout.Button("(playFab)输出获取全部技能石的测试用cloudscript)"))
        {
            _ConfigManager.OutputCloudScriptPart_GetAllStones();
        }

        if (GUILayout.Button("(playFab)输出Json格式角色定义文件(只能在程序启动状态下正常运行)"))
        {
            _ConfigManager.OutputMonstersCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式角色商店文件(只能在程序启动状态下正常运行)"))
        {
            _ConfigManager.OutputMonsterStore();
        }

        if (GUILayout.Button("(playFab)输出获取全部角色的测试用cloudscript)"))
        {
            _ConfigManager.OutputCloudScriptPart_GetAllMonsters();
        }

        if (GUILayout.Button("全项目所有贴图转换iphone格式"))
        {
            Debug.Log("危险，已经弃用");
            return;
            var guids = AssetDatabase.FindAssets("t:texture2D",  null);
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                if (textureImporter != null)
                {
                    TextureImporterPlatformSettings iPhone_png = new TextureImporterPlatformSettings
                    {
                        overridden = true,
                        name = "iPhone",
                        maxTextureSize = 2048,
                        format = TextureImporterFormat.ASTC_RGBA_4x4,
                        compressionQuality = 50,
                        allowsAlphaSplitting = false
                    };

                    TextureImporterPlatformSettings iPhone_jpeg = new TextureImporterPlatformSettings
                    {
                        overridden = true,
                        name = "iPhone",
                        maxTextureSize = 2048,
                        format = TextureImporterFormat.ASTC_RGB_4x4,
                        compressionQuality = 50,
                        allowsAlphaSplitting = false
                    };

                    TextureImporterPlatformSettings Android_png = new TextureImporterPlatformSettings
                    {
                        overridden = true,
                        name = "Android",
                        maxTextureSize = 2048,
                        format = TextureImporterFormat.DXT5,
                        compressionQuality = 50,
                        allowsAlphaSplitting = false
                    };

                    TextureImporterPlatformSettings Android_jpeg = new TextureImporterPlatformSettings
                    {
                        overridden = true,
                        name = "Android",
                        maxTextureSize = 2048,
                        format = TextureImporterFormat.DXT1,
                        compressionQuality = 50,
                        allowsAlphaSplitting = false
                    };

                    if (textureImporter.DoesSourceTextureHaveAlpha ()) {
                        //Alphaチャンネルあある場合
                        textureImporter.SetPlatformTextureSettings (iPhone_png);
                        textureImporter.SetPlatformTextureSettings (Android_png);
                    } else {
                        //Alphaチャンネルがない場合
                        textureImporter.SetPlatformTextureSettings (iPhone_jpeg);
                        textureImporter.SetPlatformTextureSettings (Android_jpeg);
                    }
                }
            }
        }
        
        if (GUILayout.Button("生成剧情用临时角色存档文件"))
        {
            _ConfigManager.GenerateTutorialUnitsFiles();
        }        
    }
}
#endif