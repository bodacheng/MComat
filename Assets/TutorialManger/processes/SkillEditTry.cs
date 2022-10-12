using mainMenu;

public class SkillEditTry : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private SkillEditLayer _skillEditLayer;

    private bool showEditConfirmIndicator = false;
    
    public override void ProcessEnter()
    {
        
    }
    
    public override void ProcessEnd()
    {
        _returnLayer.ForceBackMode(true);
    }
    
    public override bool CanEnterOtherProcess()
    {
        var unitInfo = UnitInfo.GetUnitInfo(PreScene.target._focusing);
        if (unitInfo != null)
        {
            return unitInfo.set.CheckEdit() == SkillSet.SkillEditError.Perfect;
        }
        return false;
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
    }
}
