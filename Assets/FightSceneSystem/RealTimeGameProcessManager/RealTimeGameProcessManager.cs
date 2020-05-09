using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
public class RealTimeGameProcessManager : MonoBehaviour
{
    [Header("Basic Element")]
    public CameraManager _CameraManager;
    public MobileInputsManager _mobileInputsManager;
    
    [Header("Watch Mode")]
    [Space(6)]
    public Button WatchModeButton;
    
    [Header("Auto BUtton")]
    [Space(6)]
    public Button autoBUtton;
    public Image _C_button;
    public Image _AI_button;
    
    public FightTeam FightTeam1, FightTeam2;
    public FightTeam_MultiRaid FightTeam1_multi, FightTeam2_multi;
    public FightTeam_RotationMode FightTeam1_rotation, FightTeam2_rotation;
    
    public TeamConfig heroTeamConfig = new TeamConfig(Team.player1, new List<Team>() { Team.player2 });
    public TeamConfig EnemyTeamConfig = new TeamConfig(Team.player2, new List<Team>() { Team.player1 });
    
    public static RealTimeGameProcessManager target;
    
    public static bool Auto;
    public static Data_Center focusingChar;
    public static Team playerTeam = Team.player1;

    void Awake()
    {
        target = this;
    }

    public void FightGUIProcess()
    {
        if (FightTeam1.teamConfig.myTeam == playerTeam)
            FightTeam2.BarsPositionUpdate();
        if (FightTeam2.teamConfig.myTeam == playerTeam)
            FightTeam1.BarsPositionUpdate();
    }
    
    public void SwitchToWatchMode() // button behaviour
    {
        SwitchToCMode(null, false);
        CameraParaAdjustment(playerTeam);
    }
    
	public void Refresh()//这个刷新是倾向于画面制御
	{
        if (!Auto && focusingChar != null)
        {
            _C_button.gameObject.SetActive(true);
            _AI_button.gameObject.SetActive(false);
        }
        else
        {
            _C_button.gameObject.SetActive(false);
            _AI_button.gameObject.SetActive(true);
        }
        void SwitchAutoMode()
        {
            Auto = !Auto;
            SwitchToCMode(focusingChar, Auto);
        }
        autoBUtton.onClick.RemoveAllListeners();
        autoBUtton.onClick.AddListener(SwitchAutoMode);
        
        FightTeam1.Refresh();
        FightTeam2.Refresh();
        
        if (focusingChar == null)
        {
            _mobileInputsManager.TurnOffButtons();
        }
        else
        {
            _mobileInputsManager.FocusCharInputs(focusingChar._MyBehaviorRunner,focusingChar.Zokusei);
            _mobileInputsManager.TurnOnButtons();
        }
	}
    
	public void SwitchToCMode(Data_Center _char, bool playerControll) //要转成控制模式的是哪个角色，如果括号里是null，意味着走向AI模式    
    {
        if (_char != null)
        {
            MobileInputsManager.SetPlayerMode(playerControll);
        }
        else
        {
            MobileInputsManager.SetPlayerMode(false);
        }
        focusingChar = _char;
        Refresh();
    }
    
    public IEnumerator LoadGame(StageScriptableObject stage)
    {
        FightTeam1.TeamMode = stage.Team1Mode;        
        switch (FightTeam1.TeamMode)
        {
            case TeamMode.multiraid:
            target.FightTeam1 = FightTeam1_multi;
            break;
            case TeamMode.rotation:
            target.FightTeam1 = FightTeam1_rotation;
            break;
            case TeamMode.test:
            FightTeam1 = FightTeam1_multi;
            break;
        }
        
        FightTeam2.TeamMode = stage.Team2Mode;
        switch (FightTeam2.TeamMode)
        {
            case TeamMode.multiraid:
            FightTeam2 = FightTeam2_multi;
            break;
            case TeamMode.rotation:
            FightTeam2 = FightTeam2_rotation;
            break;
            case TeamMode.test:
            target.FightTeam2 = target.FightTeam2_multi;
            break;
        }
        
        FightTeam1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        FightTeam2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        
        FightTeam1.teamConfig = heroTeamConfig;
        FightTeam2.teamConfig = EnemyTeamConfig;
        
        yield return FightTeam1.Instantiate (stage.localFight.HeroSets,stage.team1_ExtraHP);
        yield return FightTeam2.Instantiate (stage.localFight.EnemySets,stage.team2_ExtraHP);
        
        FightTeam1.ArrangeAllTeamMembersToPosition(FightTeam1.TeamMembers);
        FightTeam2.ArrangeAllTeamMembersToPosition(FightTeam2.TeamMembers);
        
        switch (playerTeam)
        {
            case Team.player1:
                SwitchToCMode(FightTeam1.TeamMembers.values[0],false);
                break;
            case Team.player2:
                SwitchToCMode(FightTeam2.TeamMembers.values[0],false);
                break;
        }
        NetFightScene.target.LoadStageFinished.Value = true;
    }
    
    // 战斗模式相机。根据选择队伍做相应调整。
    public void CameraParaAdjustment(Team myTeam)
    {
        if (focusingChar != null)
        {
            if (myTeam == Team.player1)
            {
                _CameraManager.Assign_Camera(C_Mode.CertainYAntiVibration, FightTeam2.TeamMemberTransforms());
            }
            else
            {
                _CameraManager.Assign_Camera(C_Mode.CertainYAntiVibration, FightTeam1.TeamMemberTransforms());
            }
            _CameraManager.CurrentMode.SetMeCenter(focusingChar.WholeT);
        }else{
            _CameraManager.Assign_Camera(C_Mode.TopDown,null);
        }
    }
               
    public void Clear()// 这个我们还没有添加在合理的地方。
    {
        FightTeam1.Clear();
        FightTeam2.Clear();
        _mobileInputsManager.Clear();
    }

    public void FightingStepProcess()
    {
        FightTeam1.LocalFightingUpdate();
        FightTeam2.LocalFightingUpdate();
        FightGUIProcess();
        
        if (focusingChar != null)
        {
            _CameraManager.CurrentMode.SetMeCenter(focusingChar.WholeT);//!>>!>!>!!??!
        }        
    }
    
    void OnGUI()
    {
        if (GUI.Button(new Rect(40, 40, 60, 30), "切换队伍"))
        {
            switch(playerTeam)
            {
                case Team.player1:
                    playerTeam = Team.player2;
                    SwitchToCMode(null,Auto);
                    
                break;
                case Team.player2:
                    playerTeam = Team.player1;
                    SwitchToCMode(null,Auto);
                break;
            }
            CameraParaAdjustment(playerTeam);
        }
    }
}

public enum InputKey
{
    Null = -1,
    Attack1 = 0,
    Attack2 = 1,
    Attack3 = 2,
    Acc = 5,
    Defend = 3,
    Defend_Cancel = 4,
    Any = 6
}