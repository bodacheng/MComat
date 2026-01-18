using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ArcadeModeManager
{
    private readonly IDictionary<string, IResourceLocation> locationKeyDic = new Dictionary<string, IResourceLocation>();
    int _maxStageNum = -999;
    public int MaxStageNum => _maxStageNum;

    public static readonly ArcadeModeManager Instance = new ArcadeModeManager();

    private static bool DemoQuestLimitEnabled =>
        CommonSetting.DemoMode && CommonSetting.DemoMaxQuestStage > 0;

    public static int ClampQuestStage(int stageNo)
    {
        if (!DemoQuestLimitEnabled)
        {
            return stageNo;
        }
        return Math.Min(stageNo, CommonSetting.DemoMaxQuestStage);
    }

    public static int ClampQuestProgress(int progress)
    {
        if (!DemoQuestLimitEnabled)
        {
            return progress;
        }
        return Math.Min(progress, CommonSetting.DemoMaxQuestStage);
    }
    
    public async UniTask Initialize()
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
        if (DemoQuestLimitEnabled && _maxStageNum > CommonSetting.DemoMaxQuestStage)
        {
            _maxStageNum = CommonSetting.DemoMaxQuestStage;
        }
    }
    
    public async UniTask<FightInfo> LoadStage(int stageNo)
    {
        if (DemoQuestLimitEnabled && stageNo > CommonSetting.DemoMaxQuestStage)
        {
            return null;
        }
        locationKeyDic.TryGetValue(stageNo.ToString(), out var location);
        if (location == null)
            return null;
        var fightInfo = await AddressablesLogic.LoadT<FightInfo>(location);
        fightInfo.EventType = FightEventType.Quest;
        fightInfo.FightMembers.EnemySets.ConvertSerializableArrayToDictionary();
        fightInfo.SetUnitLevelByRefLevel();
        return fightInfo;
    }

    public async void DirectToArcadeStage(int stageNo, bool forward)
    {
        stageNo = ClampQuestStage(stageNo);
        var stage = await LoadStage(stageNo);
        if (stage == null)
        {
            stage = await LoadStage(stageNo - 1);
        }
        if (stage != null)
        {
            stage.EventType = FightEventType.Quest;
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, stage, forward);
        }
        else
        {
            PreScene.target.trySwitchToStep(MainSceneStep.ArcadeFront, forward);
        }
    }
}
