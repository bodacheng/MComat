using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SelfFightPage : MSceneProcess
{
    public SelfFightPage()
    {
        Step = MainSceneStep.SelfFightFront;
    }

    SelfFightLayer selfFightLayer;

    public override void ProcessEnter()
    {
        SetUp().Forget();
    }

    async UniTask SetUp()
    {
        await BattleFieldInitialize();
        var layer = UILayerLoader.Load<UnitsLayer>();
        layer.DisplayUnitIcons(dataAccess.Units.Dic, true, true);

        selfFightLayer = UILayerLoader.Load<SelfFightLayer>();
        selfFightLayer.INI(SwitchBattleField, BattleFieldName);
        selfFightLayer.AddUnitIconFeaturesToBox();
        SetLoaded(true);
    }

    private readonly List<string> _battleFieldKeys = new List<string>();
    private int _choosingBattleFieldId = -1;

    private int BattleFieldIndex
    {
        get => _choosingBattleFieldId;
        set
        {
            if (value > _battleFieldKeys.Count - 1)
            {
                _choosingBattleFieldId = 0;
            }
            else if (value < 0)
            {
                _choosingBattleFieldId = _battleFieldKeys.Count - 1;
            }
            else
            {
                _choosingBattleFieldId = value;
            }
        }
    }

    int SwitchBattleField(bool plusIndex)
    {
        _choosingBattleFieldId = _battleFieldKeys.IndexOf("battleGround/" + _choosingBattleFieldId);
        Debug.Log(_choosingBattleFieldId);
        if (plusIndex)
            BattleFieldIndex++;
        else
            BattleFieldIndex--;
        return BattleFieldIndex;
    }

    string BattleFieldName()
    {
        return Translate.Get("battleGround/"+BattleFieldIndex);
    }
    
    async UniTask BattleFieldInitialize()
    {
        _battleFieldKeys.Clear();
        var locationHandle = Addressables.LoadResourceLocationsAsync("battle_ground");
        await locationHandle.Task;
        if (locationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var stageLocation in locationHandle.Result)
            {
                _battleFieldKeys.Add(stageLocation.PrimaryKey);
            }
        }
        Addressables.Release(locationHandle);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UnitsLayer>();
        UILayerLoader.Remove<SelfFightLayer>();
    }
}
