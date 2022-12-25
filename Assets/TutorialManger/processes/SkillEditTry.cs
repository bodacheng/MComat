using mainMenu;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab.ClientModels;

public class SkillEditTry : TutorialProcess
{
    private ReturnLayer _returnLayer;
    private SkillEditLayer _skillEditLayer;
    private bool _skillEditFinished = false;
    private readonly string _tutorialFlag;

    public SkillEditTry(string tutorialFlag)
    {
        this._tutorialFlag = tutorialFlag;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return _skillEditFinished;
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
        
        if (!_skillEditFinished)
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
                if (this._tutorialFlag == "openInstruction1")
                {
                    _skillEditLayer.OpenTutorial1();
                }
                
                if (this._tutorialFlag == "openInstruction2")
                {
                    _skillEditLayer.OpenTutorial2();
                }
                
                _skillEditLayer.NineSlot.SetExtraSkillEditSuccess(
                    () =>
                    {
                        var tutorialProgressLabel = PreScene.target.Focusing.r_id == "1" ? "SkillEditFinished" : "SkillEditFinished2";
                        PlayFabReadClient.UpdateUserData(
                            new UpdateUserDataRequest()
                            {
                                Data = new Dictionary<string, string>()
                                {
                                    { "TutorialProgress", tutorialProgressLabel }
                                }
                            },
                            () =>
                            {
                                PlayerAccountInfo.Me.tutorialProgress = tutorialProgressLabel;
                                _skillEditFinished = true;
                                _skillEditLayer.NineSlot.confirmBtnIndicator.SetActive(false);
                            },
                            PreScene.ReturnToLobby
                        );
                    }
                );
            }
        }
    }
}
