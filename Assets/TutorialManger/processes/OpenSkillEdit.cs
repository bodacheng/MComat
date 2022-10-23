using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class OpenSkillEdit : TutorialProcess
{
    UnitListPage unitListPage;
    UnitsLayer _unitsLayer;
    UnitOptionLayer unitOptionLayer;

    private readonly string _focusUnitRId;
    
    public OpenSkillEdit(string unitRId)
    {
        _focusUnitRId = unitRId;
    }

    public override void ProcessEnter()
    {
        var unitInfo = dataAccess.Units.GetByRId(_focusUnitRId);
        PreScene.target.SetFocusingUnit(unitInfo.id);
        unitListPage = (UnitListPage)ProcessesRunner.Main.GetProcess(MainSceneStep.UnitList);
    }
    
    public override void ProcessEnd()
    {
        HighLightLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitSkillEdit;
    }
    
    public override void LocalUpdate()
    {
        if (unitOptionLayer == null)
            unitOptionLayer = UILayerLoader.Get<UnitOptionLayer>();
        
        if (unitListPage.GetLoaded() && unitOptionLayer != null)
        {
            unitOptionLayer.PlsClickSkillEdit();
            HighLightLayer.HighLightRect(unitOptionLayer._NineForShow.GetComponent<RectTransform>());
        }
    }
}