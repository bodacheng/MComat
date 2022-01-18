using UnityEngine;
using mainMenu;
using dataAccess;

public class UnitListPage : MainSceneProcess
{
    public bool loadFinished;

    private UnitsLayer layer;
    private UnitOptionLayer unitOptionLayer;
    
    public UnitListPage()
    {
        Step = MainSceneStep.MonsterList;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        loadFinished = false;
        switch (Account._AccInfo.accountprogress)
        {
            case PlayerAccountProgressStep.Freedom:
                break;
            case PlayerAccountProgressStep.justCreated:
                break;
            case PlayerAccountProgressStep.Tutorial:
                MyMonsters.LoadTutorial();
                break;
        }
        
        layer = UnitsLayer.Open();
        unitOptionLayer = UnitOptionLayer.Open();
        layer.DisplayUnitIcons(true);
        
        void UnitIconBtn(string instanceId)
        {
            UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
            unitsLayer.Select(instanceId);
            PreScene.target.SetFocusingUnit(instanceId);
            unitOptionLayer.RefreshMemberDetailPageByFocusingChar();
        }
        
        UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
        unitsLayer.SetUnitsIconOnClick(UnitIconBtn);
        
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        unitOptionLayer.RefreshMemberDetailPageByFocusingChar();
        loadFinished = true;
    }
    
    public override void ProcessEnd()
    {
        UnitOptionLayer.Close();
        UnitsLayer.Close();
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, 10);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
