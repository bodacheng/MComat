using System;
using Cysharp.Threading.Tasks;
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
        HurtObjectManager.Clear();
        EffectsManager.Clear();
        
        switch (PlayerAccountInfo.Me.progress)
        {
            case PlayerAccountProgressStep.Freedom:
                break;
            case PlayerAccountProgressStep.justCreated:
                break;
            case PlayerAccountProgressStep.Tutorial:
                dataAccess.Units.LoadTutorial();
                break;
        }
        
        layer = UnitsLayer.Open();
        unitOptionLayer = UnitOptionLayer.Open();
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
        UnitOptionLayer.Close();
        UnitsLayer.Close();
    }
}
