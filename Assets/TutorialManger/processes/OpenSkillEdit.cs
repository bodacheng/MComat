using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class OpenSkillEdit : TutorialProcess
{
    UnitListPage unitListPage;
    UnitsLayer _unitsLayer;
    UnitOptionLayer UnitOptionLayer;

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
        if (!Loaded)
        {
            if (UnitOptionLayer == null)
                UnitOptionLayer = UILayerLoader.Get<UnitOptionLayer>();
            
            if (unitListPage.GetLoaded() && UnitOptionLayer != null)
            {
                UnitOptionLayer.PlsClickSkillEdit();
                HighLightLayer.HighLightRect(PreScene.target.T,UnitOptionLayer._NineForShow.GetComponent<RectTransform>());
                Loaded = true;
            }
        }

        if (_unitsLayer == null)
        {
            _unitsLayer = UILayerLoader.Get<UnitsLayer>();
            _unitsLayer.ForceClickUnit(_focusUnitRId);
        }
    }
}