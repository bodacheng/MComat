using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ArcadeFrontPage : MSceneProcess
{
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
    }
    
    ArcadeTop _arcadeTop;
    StageModeTable stageModeTable;
    private readonly IDictionary<string, IResourceLocation> locationKeyDic = new Dictionary<string, IResourceLocation>();

    int _maxStageNum = -999; 
    async UniTask CheckExistedKey()
    {
        var locationHandle = Addressables.LoadResourceLocationsAsync("quest");
        await locationHandle.Task;
        if (locationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var stageLocation in locationHandle.Result)
            {
                DicAdd<string, IResourceLocation>.Add(locationKeyDic, stageLocation.PrimaryKey, stageLocation);
                int id = Int32.Parse(stageLocation.PrimaryKey);
                if (id > _maxStageNum)
                {
                    _maxStageNum = id;
                }
            }
        }
        Addressables.Release(locationHandle);
    }

    async UniTask<FightInfo> LoadStage(int stageNo)
    {
        locationKeyDic.TryGetValue(stageNo.ToString(), out var location);
        if (location == null)
            return null;
        var one = await AddressablesLogic.LoadT<FightInfo>(location);
        return one;
    }
    
    public override void ProcessEnter()
    {
        PlayFabReadClient.GetStageRewardInfo(Enter);
    }
    
    void Enter()
    {
        stageModeTable = new StageModeTable();
        _arcadeTop = UILayerLoader.Load<ArcadeTop>();
        Load().Forget();
    }

    async UniTask Load()
    {
        await CheckExistedKey();
        _arcadeTop.Setup(stageModeTable, LoadStage, _maxStageNum);
        await stageModeTable.LoadStageMode();
        var stages = _arcadeTop.NewStages(PlayerAccountInfo.Me.arcadeProcess);
        await _arcadeTop.ShowStages(stages);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArcadeTop>();
        locationKeyDic.Clear();
    }
}

public class StageAward
{
    public string stageKey;
    public Award award;
}

public class Award
{
    public int g;
    public int d;
}