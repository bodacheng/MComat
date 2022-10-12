using mainMenu;
using UnityEngine;

public class OpenSkillEdit : TutorialProcess
{
    UnitListPage unitListPage;
    UnitOptionLayer UnitOptionLayer;

    public override void ProcessEnter()
    {
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
                UnitOptionLayer = UnitOptionLayer.Get();
            
            if (unitListPage.GetLoaded() && UnitOptionLayer != null)
            {
                UnitOptionLayer.PlsClickSkillEdit();
                HighLightLayer.HighLightRect(PreScene.target.T,UnitOptionLayer._NineForShow.GetComponent<RectTransform>());
                Loaded = true;
            }
        }
    }
}