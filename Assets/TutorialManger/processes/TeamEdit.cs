using dataAccess;
using mainMenu;
using DummyLayerSystem;
using UnityEngine;

public class TeamEdit : TutorialProcess
{
    private ArcadeTop _arcadeTop;
    private TeamEditLayer _teamEditLayer;
    private ReturnLayer _returnLayer;
    private FightPrepareLayer _fightPrepareLayer;
    private TeamEditPage _teamEditPage;

    private bool teamEditFinished = false;
    private readonly string tutorialStep;
    public TeamEdit(string TutorialStep)
    {
        tutorialStep = TutorialStep;
    }
    
    public override void ProcessEnter()
    {
        _teamEditPage = (TeamEditPage)ProcessesRunner.Main.GetProcess(MainSceneStep.TeamEditFront);
        _teamEditPage.SetExtraArcadeTeamEditSuccess(
            () =>
            {
                teamEditFinished = true;
                if (_returnLayer != null)
                    _returnLayer.gameObject.SetActive(true);
            }
        );
    }
    
    private bool TutorialLegal(string teamMode)
    {
        bool qualified = false;
        int unitCount = 0;

        PosKeySet targetTeamSet = null;
        switch (teamMode)
        {
            case "arena":
                targetTeamSet = TeamSet.Arena3V3;
                break;
            case "arcade":
                targetTeamSet = TeamSet.Default;
                break;
        }
        
        foreach (var set in targetTeamSet.PosNumsWithLocalKeys)
        {
            if (set.instanceID != null && dataAccess.Units.Get(set.instanceID) != null)
            {
                qualified = qualified && (Stones.GetEquippingStones(set.instanceID).Count == 9);
                unitCount += 1;
            }
            else
            {
                qualified = false;
            }
            if (!qualified)
                break;
        }
        
        switch (teamMode)
        {
            case "arena":
                qualified = qualified && unitCount == 3;
                break;
            case "arcade":
                if (tutorialStep == "teamEdit1")
                {
                    qualified = qualified && unitCount > 0;
                }
                else if (tutorialStep == "teamEdit2")
                {
                    qualified = qualified && unitCount > 1;
                }
                break;
        }
        return qualified;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return teamEditFinished;
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
        {
            _teamEditLayer = UILayerLoader.Get<TeamEditLayer>();
            if (_teamEditLayer != null)
            {
                _teamEditLayer.SetTeamLegalCheck(TutorialLegal);
            }
        }
        
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