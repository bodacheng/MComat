using mainMenu;

public class SkillEditTry : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private SkillEditLayer _skillEditLayer;

    private bool showEditConfirmIndicator = false;
    private bool skillEditFinished = false;
    
    public override void ProcessEnter()
    {
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.FrontPage;
    }
    
    public override void LocalUpdate()
    {
        if (!Loaded)
        {
            if (_returnLayer == null)
                _returnLayer = ReturnLayer.Get();
            
            if (_skillEditLayer == null)
                _skillEditLayer = SkillEditLayer.Get();
            
            if (_returnLayer != null && _skillEditLayer != null)
            {
                _returnLayer.gameObject.SetActive(false);
                Loaded = true;
            }
        }
        
        if (!showEditConfirmIndicator)
        {
            if (_skillEditLayer != null)
            {
                var validate = _skillEditLayer.NineSlot.ValidateWarn();
                if (validate == SkillSet.SkillEditError.Perfect)
                {
                    //HighLightLayer.HighLightRect(PreScene.target.T,_skillEditLayer.NineSlot.ConfirmSkillChangeButton.GetComponent<RectTransform>());
                    showEditConfirmIndicator = true;
                }
            }
        }
        
        if (!skillEditFinished)
        {
            var unitInfo = UnitInfo.GetUnitInfo(PreScene.target._focusing);
            if (unitInfo != null)
            {
                skillEditFinished = unitInfo.set.CheckEdit() == SkillSet.SkillEditError.Perfect;
                if (skillEditFinished)
                {
                    _returnLayer.gameObject.SetActive(true);
                    _returnLayer.ForceBackMode(true);
                }
            }
        }

        if (skillEditFinished && ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitList)
        {
            _returnLayer.ForceBackMode(true);
        }
    }
}
