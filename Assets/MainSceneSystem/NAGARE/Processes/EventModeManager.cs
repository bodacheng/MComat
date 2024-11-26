using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine;

public class EventModeManager
{
    private IResourceLocation easyModePath, normalModePath, hardModePath;
    private FightInfo easyMode, normalMode, hardMode;
    private List<string> completedLevels;

    public FightInfo EasyMode => easyMode;
    public FightInfo NormalMode => normalMode;
    public FightInfo HardMode => hardMode;
    
    public static readonly EventModeManager Instance = new EventModeManager();

    public List<string> CompletedLevels
    {
        get
        {
            if (completedLevels != null)
                return completedLevels;
            else
            {
                completedLevels = new List<string>();
                return completedLevels;
            }
        }
        set => completedLevels = value;
    }
    
    /// <summary>
    /// PrimaryKey是用来定位到底哪个战斗文件是哪个难度级别战斗的，
    /// boss关卡进度的实际索引靠的是战斗定义文件本身。所以PrimaryKey可以常年不变
    /// </summary>
    public async UniTask Initialize()
    {
        var locationHandle = Addressables.LoadResourceLocationsAsync("event_stage");
        await locationHandle.Task;
        if (locationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var stageLocation in locationHandle.Result)
            {
                if (stageLocation.PrimaryKey.Contains("easy"))
                {
                    easyModePath = stageLocation;
                }
                if (stageLocation.PrimaryKey.Contains("normal"))
                {
                    normalModePath = stageLocation;
                }
                if (stageLocation.PrimaryKey.Contains("hard"))
                {
                    hardModePath = stageLocation;
                }
            }
        }
        Addressables.Release(locationHandle);
        
        if (easyModePath != null)
            easyMode = await LoadStage(easyModePath);
        if (normalModePath != null)
            normalMode = await LoadStage(normalModePath);
        if (hardModePath != null)
            hardMode = await LoadStage(hardModePath);
    }
    
    public void InitializeRandomMode(string uniqueId)
    {
        Debug.Log("日期字符串："+ uniqueId);
        easyMode = LoadRandomStage();
        easyMode.stageRefLevel = 2;
        easyMode.ID = "easy_"+ uniqueId;
        normalMode = LoadRandomStage(1);
        normalMode.stageRefLevel = 5;
        normalMode.ID = "normal_"+ uniqueId;
        hardMode = LoadRandomStage(2);
        hardMode.stageRefLevel = 10;
        hardMode.ID = "hard_"+ uniqueId;
    }

    public UnitInfo GetRepresentativeUnit()
    {
        var unit1 = hardMode?.UnitsData.FirstOrDefault();
        if (unit1 != null) return unit1;
        var unit2 = normalMode?.UnitsData.FirstOrDefault();
        if (unit2 != null) return unit2;
        var unit3 = easyMode?.UnitsData.FirstOrDefault();
        if (unit3 != null) return unit3;
        return null;
    }
    
    async UniTask<FightInfo> LoadStage(IResourceLocation location)
    {
        var fightInfo = await AddressablesLogic.LoadT<FightInfo>(location);
        fightInfo.EventType = FightEventType.Event;
        fightInfo.SetUnitLevelByRefLevel();
        return fightInfo;
    }
    
    FightInfo LoadRandomStage(int mode = 0)
    {
        FightInfo fightInfo = FightInfo.RandomStage(mode);
        fightInfo.EventType = FightEventType.Event;
        fightInfo.SetUnitLevelByRefLevel();
        fightInfo.SaveDicToData();
        switch (mode)
        {
            case 1:
                fightInfo.team2CGMode = CriticalGaugeMode.DoubleGain;
                break;
            case 2:
                fightInfo.team2CGMode = CriticalGaugeMode.Unlimited;
                break;
            default:
                fightInfo.team2CGMode = CriticalGaugeMode.Normal;
                break;
        }
        return fightInfo;
    }
    
    public void OnCloudScriptSuccess(ExecuteCloudScriptResult result)
    {
        if (result.Error != null) {
            Debug.LogError("Cloud Script Error: " + result.Error.Message);
            return;
        }
        
        Debug.Log("Cloud Script Success: " + result.FunctionResult);
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        if (jsonResult.TryGetValue("completedEventBattles", out var completedBattlesObject))
        {
            var objects = (List<object>)completedBattlesObject;
            CompletedLevels.Clear();
            foreach (var o in objects)
            {
                CompletedLevels.Add(o.ToString());
            }
        }
        else
        {
            Debug.Log("No completed event battles found.");
        }
    }
}