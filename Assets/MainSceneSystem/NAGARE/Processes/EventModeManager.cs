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
    private IResourceLocation _easyModePath, _normalModePath, _hardModePath;
    private FightInfo _easyMode, _normalMode, _hardMode;
    private List<string> _completedLevels;
    
    public FightInfo EasyMode => _easyMode;
    public FightInfo NormalMode => _normalMode;
    public FightInfo HardMode => _hardMode;
    
    public static readonly EventModeManager Instance = new EventModeManager();

    public List<string> CompletedLevels
    {
        get
        {
            if (_completedLevels != null)
                return _completedLevels;
            else
            {
                _completedLevels = new List<string>();
                return _completedLevels;
            }
        }
        set => _completedLevels = value;
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
                    _easyModePath = stageLocation;
                }
                if (stageLocation.PrimaryKey.Contains("normal"))
                {
                    _normalModePath = stageLocation;
                }
                if (stageLocation.PrimaryKey.Contains("hard"))
                {
                    _hardModePath = stageLocation;
                }
            }
        }
        Addressables.Release(locationHandle);
        
        if (_easyModePath != null)
            _easyMode = await LoadStage(_easyModePath);
        if (_normalModePath != null)
            _normalMode = await LoadStage(_normalModePath);
        if (_hardModePath != null)
            _hardMode = await LoadStage(_hardModePath);
    }
    
    public void InitializeRandomMode(string uniqueId)
    {
        _easyMode = LoadRandomStage(CriticalGaugeMode.Normal, 3);
        _easyMode.stageRefLevel = 2;
        _easyMode.ID = "easy_"+ uniqueId;
        _normalMode = LoadRandomStage(CriticalGaugeMode.DoubleGain, 2);
        _normalMode.stageRefLevel = 5;
        _normalMode.ID = "normal_"+ uniqueId;
        _hardMode = LoadRandomStage(CriticalGaugeMode.Unlimited, 1);
        _hardMode.stageRefLevel = 10;
        _hardMode.ID = "hard_"+ uniqueId;
    }

    public UnitInfo GetRepresentativeUnit()
    {
        var unit1 = _hardMode?.UnitsData.FirstOrDefault();
        if (unit1 != null) return unit1;
        var unit2 = _normalMode?.UnitsData.FirstOrDefault();
        if (unit2 != null) return unit2;
        var unit3 = _easyMode?.UnitsData.FirstOrDefault();
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

    FightInfo LoadRandomStage(CriticalGaugeMode mode = CriticalGaugeMode.Normal, int unitCount = 3)
    {
        var fightInfo = FightInfo.RandomStage(mode, unitCount);
        fightInfo.EventType = FightEventType.Event;
        fightInfo.SetUnitLevelByRefLevel();
        fightInfo.SaveDicToData();
        fightInfo.team2CGMode = mode;
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