#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Linq;

public class LocalMasterDataToolGUI : EditorWindow {

    bool Initialized;
    MasterDataTool tool;
    
    [SerializeField] string[] unitTypes = {"human"};
    [SerializeField] TextAsset unitConfigFile;
    [SerializeField] string unitConfigFilePath;
    [SerializeField] TextAsset SkillConfigFile;
    [SerializeField] string SkillConfigPath;
    
    [SerializeField] Shader ChangeShader;
    [SerializeField] Shader ToShader;
    
    void OnGUI()
    {
        if (!Initialized)
        {
            Initialized = true;
        }
        
        unitConfigFile = EditorGUILayout.ObjectField("角色定义文件", unitConfigFile, typeof(TextAsset), true) as TextAsset;
        unitConfigFilePath = EditorGUILayout.TextField("角色定义文件路径",unitConfigFilePath);
        SkillConfigFile = EditorGUILayout.ObjectField("技能定义文件", SkillConfigFile, typeof(TextAsset), true) as TextAsset;
        SkillConfigPath = EditorGUILayout.TextField("技能定义文件",SkillConfigPath);
        ChangeShader = EditorGUILayout.ObjectField("Change Shader", ChangeShader, typeof(Shader), true) as Shader;
        ToShader = EditorGUILayout.ObjectField("To Shader", ToShader, typeof(Shader), true) as Shader;

        if (GUILayout.Button("根据Resource文件夹生成所有角色配置文件"))
        {
            tool.UnitsConfigFileGenerate(unitConfigFilePath, unitConfigFile, unitTypes);
        }
        
        if (GUILayout.Button("根据Resource文件夹生成,更新技能配置文件"))
        {
            tool.SkillConfigFileUpdate(SkillConfigPath, SkillConfigFile, unitTypes);
        }

        if (GUILayout.Button("(playFab)输出Json格式技能石定义文件(只能在程序启动状态下正常运行)"))
        {
            tool.OutputSKStonesCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式技能石商店文件(只能在程序启动状态下正常运行)"))
        {
            tool.OutputSKStonesStore();
        }

        if (GUILayout.Button("(playFab)输出获取全部技能石的测试用cloudscript)"))
        {
            tool.OutputCloudScriptPart_GetAllStones();
        }

        if (GUILayout.Button("(playFab)输出Json格式角色定义文件(只能在程序启动状态下正常运行)"))
        {
            tool.OutputMonstersCatalog();
        }

        if (GUILayout.Button("(playFab)输出Json格式角色商店文件(只能在程序启动状态下正常运行)"))
        {
            tool.OutputMonsterStore();
        }

        if (GUILayout.Button("(playFab)输出获取全部角色的测试用cloudscript)"))
        {
            tool.OutputCloudScriptPart_GetAllMonsters();
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
                        format = TextureImporterFormat.ASTC_4x4,
                        compressionQuality = 50,
                        allowsAlphaSplitting = false
                    };

                    TextureImporterPlatformSettings iPhone_jpeg = new TextureImporterPlatformSettings
                    {
                        overridden = true,
                        name = "iPhone",
                        maxTextureSize = 2048,
                        format = TextureImporterFormat.ASTC_4x4,
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
        
        
        if (GUILayout.Button("置换一切材质的shader"))
        {
            var list = AssetDatabase
                .FindAssets( "t:Material" )
                .Select( AssetDatabase.GUIDToAssetPath )
                .Select( AssetDatabase.LoadAssetAtPath<Material> )
                .Where( c => c != null );
            foreach (var m in list)
            {
                if (m.shader == ChangeShader)
                {
                    m.shader = ToShader;
                }
            }
        }
    }
}
#endif