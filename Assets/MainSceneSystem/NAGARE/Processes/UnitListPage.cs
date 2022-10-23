using System;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;
using mainMenu;
using UniRx;

public class UnitListPage : MSceneProcess
{
    private UnitsLayer layer;
    private UnitOptionLayer unitOptionLayer;
    
    public UnitListPage()
    {
        Step = MainSceneStep.UnitList;
        Inherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        layer = UILayerLoader.Load<UnitsLayer>();
        unitOptionLayer = UILayerLoader.Load<UnitOptionLayer>();
        layer.SetDisplayUnitIconsAfterAction(
            () =>
            {
                void UnitIconBtn(string instanceId)
                {
                    Debug.Log("onclick instanceId :"+ instanceId);
                    layer.Select(instanceId);
                    PreScene.target.SetFocusingUnit(instanceId);
                    unitOptionLayer.RefreshMemberDetailPageByFocusingUnit();
                }
                layer.SetUnitsIconOnClick(UnitIconBtn);
            }
        );
        layer.DisplayUnitIcons(dataAccess.Units.Dic, true).Forget();
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ => unitOptionLayer.RefreshMemberDetailPageByFocusingUnit()).AddTo(layer);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UnitOptionLayer>();
        UILayerLoader.Remove<UnitsLayer>();
    }
}
