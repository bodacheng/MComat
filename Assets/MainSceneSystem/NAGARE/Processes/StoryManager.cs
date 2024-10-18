using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class StoryManager
{
    private readonly IDictionary<string, IResourceLocation> _locationKeyDic = new Dictionary<string, IResourceLocation>();
    public static readonly StoryManager Instance = new StoryManager();
    
    public async UniTask Initialize()
    {
        var locationHandle = Addressables.LoadResourceLocationsAsync("story");
        await locationHandle.Task;
        if (locationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var stageLocation in locationHandle.Result)
            {
                DicAdd<string, IResourceLocation>.Add(_locationKeyDic, stageLocation.PrimaryKey, stageLocation);
            }
        }
        Addressables.Release(locationHandle);
    }
    
    public async UniTask<StoryInfo> LoadStory(string storyKey)
    {
        _locationKeyDic.TryGetValue(storyKey, out var location);
        if (location == null)
            return null;
        var storyInfo = await AddressablesLogic.LoadT<StoryInfo>(location);
        return storyInfo;
    }
}
