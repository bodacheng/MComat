using mainMenu;

// Tutorial
public class ALineConfirm : TutorialProcess
{    
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(TheNineSlot.target.ConfirmSkillChangeButton.transform);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return 
        (
            MainSceneLogger.Logs[MainSceneLogger.Logs.Count - 1].step == MainSceneStep.UnitSkillEdit 
            &&
            MainSceneLogger.Logs[MainSceneLogger.Logs.Count - 1].description == "success"
        );
    }
}