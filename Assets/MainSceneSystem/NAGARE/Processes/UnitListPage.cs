using UnityEngine;
using mainMenu;
using dataAccess;

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
        layer.DisplayUnitIcons(true, (x) =>
        {
            void UnitIconBtn(string instanceId)
            {
                Debug.Log("onclick instanceId :"+ instanceId);
                x.Select(instanceId);
                PreScene.target.SetFocusingUnit(instanceId);
                unitOptionLayer.RefreshMemberDetailPageByFocusingChar();
            }
            x.SetUnitsIconOnClick(UnitIconBtn);
        });
        
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        unitOptionLayer.RefreshMemberDetailPageByFocusingChar();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UnitOptionLayer.Close();
        UnitsLayer.Close();
    }
}
