using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StoryInfo : ScriptableObject
{
    [SerializeField] private List<StoryScene> _storyScenes;
    
    public List<StoryScene> StoryScenes
    {
        get => _storyScenes;
        set => _storyScenes = value;
    }

    [Serializable]
    public class StoryScene
    {
        public Sprite Pic;
        public List<string> Lines;
    }
    
#if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetTeam"></param>
    /// <param name="path">"Assets/" 开头</param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static StoryInfo CreateStoryAsset(string path, string fileName)
    {
        var StoryInfo = CreateInstance<StoryInfo>();
        if (!Directory.Exists(path))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(path);
        }
        
        AssetDatabase.CreateAsset(StoryInfo, path + "/" + fileName + ".asset");
        Debug.Log("Generated：" + path + "/" + fileName + ".asset");
        AssetDatabase.Refresh();
        return StoryInfo;
    }
#endif
}
