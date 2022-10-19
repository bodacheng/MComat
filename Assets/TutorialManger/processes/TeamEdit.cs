using mainMenu;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class TeamEdit : TutorialProcess
{
    private ArcadeTop _arcadeTop;
    private TeamEditLayer _teamEditLayer;
    private ReturnLayer _returnLayer;
    private FightPrepareLayer _fightPrepareLayer;
    private TeamEditPage _teamEditPage;
    private FrontLayer _frontLayer;
    public override void ProcessEnter()
    {
        _teamEditPage = (TeamEditPage)ProcessesRunner.Main.GetProcess(MainSceneStep.TeamEditFront);
        _teamEditPage.SetExtraArcadeTeamEditSuccess(
            () =>
            {
                if (_returnLayer != null)
                    _returnLayer.gameObject.SetActive(true);
                PlayerAccountInfo.Me.TutorialProgress = "TeamEditFinished";
                PlayFabReadClient.UpdateUserData(
                    new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            { "TutorialProgress", "TeamEditFinished" }
                        }
                    },
                    (x) =>
                    { }
                );
            }
        );
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.TeamEditFront && 
               PlayerAccountInfo.Me.TutorialProgress == "TeamEditFinished";
    }
    
    public override void LocalUpdate()
    {
        if (_frontLayer == null)
        {
            _frontLayer = FrontLayer.Get();
            if (_frontLayer != null)
            {
                _frontLayer.PlsClickBtn("arcade");
                Loaded = true;
            }
        }
        
        if (_fightPrepareLayer == null)
        {
            _fightPrepareLayer = FightPrepareLayer.Get();
            if (_fightPrepareLayer != null)
            {
                _fightPrepareLayer.ForcePressTeamEdit();
            }
        }
        
        if (_teamEditLayer == null)
            _teamEditLayer = TeamEditLayer.Get();
        
        if (_arcadeTop == null)
            _arcadeTop = ArcadeTop.Get();
        
        if (!_returnLayer)
        {
            _returnLayer = ReturnLayer.Get();
            if (_returnLayer != null)
                _returnLayer.gameObject.SetActive(false);
        }
    }
}