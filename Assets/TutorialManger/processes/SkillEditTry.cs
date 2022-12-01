using mainMenu;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab.ClientModels;

public class SkillEditTry : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private SkillEditLayer _skillEditLayer;
    
    private bool skillEditFinished = false;

    private string tutorialFlag;

    public SkillEditTry(string tutorialFlag)
    {
        this.tutorialFlag = tutorialFlag;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return skillEditFinished;
    }
    
    public override void LocalUpdate()
    {
        if (_returnLayer == null)
        {
            _returnLayer = UILayerLoader.Get<ReturnLayer>();
        }
        if (_returnLayer != null)
        {
            _returnLayer.gameObject.SetActive(false);
        }
        
        if (!skillEditFinished)
        {
            if (_skillEditLayer != null)
            {
                var validate = _skillEditLayer.NineSlot.ValidateWarn();
                _skillEditLayer.NineSlot.confirmBtnIndicator.SetActive(validate == SkillSet.SkillEditError.Perfect);
            }
        }
        
        if (_skillEditLayer == null)
        {
            _skillEditLayer = UILayerLoader.Get<SkillEditLayer>();
            if (_skillEditLayer != null)
            {
                if (this.tutorialFlag == "openInstruction")
                {
                    _skillEditLayer.OpenTutorial();
                }
                
                _skillEditLayer.NineSlot.SetExtraSkillEditSuccess(
                    () =>
                    {
                        var TutorialProgressLabel = PreScene.target.Focusing.r_id == "1" ? "SkillEditFinished" : "SkillEditFinished2";
                        PlayFabReadClient.UpdateUserData(
                            new UpdateUserDataRequest()
                            {
                                Data = new Dictionary<string, string>()
                                {
                                    { "TutorialProgress", TutorialProgressLabel }
                                }
                            },
                            (x) =>
                            {
                                PlayerAccountInfo.Me.tutorialProgress = TutorialProgressLabel;
                                skillEditFinished = true;
                                _skillEditLayer.NineSlot.confirmBtnIndicator.SetActive(false);
                            }
                        );
                    }
                );
            }
        }
    }
}
