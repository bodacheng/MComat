#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using dataAccess;
using System;

[CustomEditor(typeof(ConfigFileManager))]
public class ConfigFileManagerGUI : Editor {

    ConfigFileManager _ConfigFileManager;
   
    public override void OnInspectorGUI()
    {
        _ConfigFileManager = (ConfigFileManager)target;
        DrawDefaultInspector();

        //_ConfigFileManager.SkillConfigTextFile = EditorGUILayout.ObjectField("SkillConfigFile", _ConfigFileManager.SkillConfigTextFile, typeof(TextAsset), true) as TextAsset;

        if (GUILayout.Button("根据Resource文件夹生成所有角色配置文件"))
        {
            _ConfigFileManager.CharsConfigFileGenerate(_ConfigFileManager.MonstersConfigFilePath,_ConfigFileManager.CharacterConfigTextFile);
        }
        
        if (GUILayout.Button("根据Resource文件夹生成,更新技能配置文件"))
        {
            _ConfigFileManager.SkillConfigFileUpdate(_ConfigFileManager.SkillConfigFilePath, _ConfigFileManager.SkillConfigTextFile);
        }

        if (GUILayout.Button("本地测试存档获得所有技能石"))
        {
            Debug.Log("该功能过于危险暂时停用");
            //IEnumerator loadskillconfigs()
            //{
            //    yield return SkillsConfigInfos.Instance.loadAllSkillConfigs();
            //    List<SkillStoneOfPlayerInfoModel> mystones = new List<SkillStoneOfPlayerInfoModel>();
            //    int i = 1;
            //    foreach (KeyValuePair<string,SkillConfig> _pair in SkillsConfigInfos.SkillConfigDicForReference)
            //    {
            //        Debug.Log("查找技能石头："+_pair.Value.keyName);
            //        mystones.Add(new SkillStoneOfPlayerInfoModel(String.Format("{0:D20}",i),_pair.Value.id));
            //        i++;
            //    }
            //    MySkillStonesReader.Instance.overrideMySkillStoneInfosOnLocalFile(mystones);
            //};
            //_ConfigFileManager.StartCoroutine(loadskillconfigs());
        }

        if (GUILayout.Button("全项目所有贴图转换iphone格式"))
        {
            var guids = AssetDatabase.FindAssets("t:texture2D",  null);
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                if (textureImporter != null)
                {
                    TextureImporterPlatformSettings iPhone_png = new TextureImporterPlatformSettings ();
                    iPhone_png.overridden = true;
                    iPhone_png.name = "iPhone";
                    iPhone_png.maxTextureSize = 2048;
                    iPhone_png.format = TextureImporterFormat.ASTC_RGBA_4x4;
                    iPhone_png.compressionQuality = 50;
                    iPhone_png.allowsAlphaSplitting = false;
            
                    TextureImporterPlatformSettings iPhone_jpeg = new TextureImporterPlatformSettings ();
                    iPhone_jpeg.overridden = true;
                    iPhone_jpeg.name = "iPhone";
                    iPhone_jpeg.maxTextureSize = 2048;
                    iPhone_jpeg.format = TextureImporterFormat.ASTC_RGB_4x4;
                    iPhone_jpeg.compressionQuality = 50;
                    iPhone_jpeg.allowsAlphaSplitting = false;
            
                    TextureImporterPlatformSettings Android_png = new TextureImporterPlatformSettings ();
                    Android_png.overridden = true;
                    Android_png.name = "Android";
                    Android_png.maxTextureSize = 2048;
                    Android_png.format = TextureImporterFormat.DXT5;
                    Android_png.compressionQuality = 50;
                    Android_png.allowsAlphaSplitting = false;
            
                    TextureImporterPlatformSettings Android_jpeg = new TextureImporterPlatformSettings ();
                    Android_jpeg.overridden = true;
                    Android_jpeg.name = "Android";
                    Android_jpeg.maxTextureSize = 2048;
                    Android_jpeg.format = TextureImporterFormat.DXT1;
                    Android_jpeg.compressionQuality = 50;
                    Android_jpeg.allowsAlphaSplitting = false;

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
            _ConfigFileManager.GenerateTutorialCharacterFiles();
        }        
    }
}
#endif