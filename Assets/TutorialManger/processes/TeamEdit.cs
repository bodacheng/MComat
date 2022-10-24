using mainMenu;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab.ClientModels;

public class TeamEdit : TutorialProcess
{
    private ArcadeTop _arcadeTop;
    private TeamEditLayer _teamEditLayer;
    private ReturnLayer _returnLayer;
    private FightPrepareLayer _fightPrepareLayer;
    private TeamEditPage _teamEditPage;
    
    public override void ProcessEnter()
    {
        _teamEditPage = (TeamEditPage)ProcessesRunner.Main.GetProcess(MainSceneStep.TeamEditFront);
        _teamEditPage.SetExtraArcadeTeamEditSuccess(
            () =>
            {
                PlayFabReadClient.UpdateUserData(
                    new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            { "TutorialProgress", "TeamEditFinished" }
                        }
                    },
                    (x) =>
                    {
                        if (_returnLayer != null)
                            _returnLayer.gameObject.SetActive(true);
                        PlayerAccountInfo.Me.TutorialProgress = "TeamEditFinished";
                    }
                );
            }
        );
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return PlayerAccountInfo.Me.TutorialProgress == "TeamEditFinished";
    }
    
    public override void LocalUpdate()
    {
        if (_fightPrepareLayer == null)
        {
            _fightPrepareLayer = UILayerLoader.Get<FightPrepareLayer>();
            if (_fightPrepareLayer != null)
            {
                _fightPrepareLayer.ForcePressTeamEdit();
            }
        }
        
        if (_teamEditLayer == null)
            _teamEditLayer = UILayerLoader.Get<TeamEditLayer>();
        
        if (_arcadeTop == null)
            _arcadeTop = UILayerLoader.Get<ArcadeTop>();
        
        if (_returnLayer == null)
        {
            _returnLayer = UILayerLoader.Get<ReturnLayer>();
            if (_returnLayer != null)
                _returnLayer.gameObject.SetActive(false);
        }
    }
}