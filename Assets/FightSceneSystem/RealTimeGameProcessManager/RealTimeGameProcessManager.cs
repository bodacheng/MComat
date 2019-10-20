using System.Collections.Generic;
using UnityEngine;
using Soul;
using UnityEngine.UI;
using TMPro;

//角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
public class RealTimeGameProcessManager : MonoBehaviour
{
    [Header("Basic Element")]
	public CameraManager _CameraManager;
	public mobileInputsManager _mobileInputsManager;
    
    [Header("Auto BUtton")]
    [Space(6)]
    public Button autoBUtton;
    public Image _C_button;
    public Image _AI_button;

    [Header("Auto BUtton")]
    [Space(6)]
    public FightTeam FightTeam1, FightTeam2;
    
    public TeamConfig heroTeamConfig = new TeamConfig(Team.player1, new List<Team>() { Team.player2 });
    public TeamConfig EnemyTeamConfig = new TeamConfig(Team.player2, new List<Team>() { Team.player1 });
    
    public static bool combatFightPlayerMode;
    public static Data_Center focusingChar;
    public static Team playerTeam = Team.player1;
    
    public void FightGUIProcess()
    {
        if (FightTeam1.teamConfig.myTeam == playerTeam)
            FightTeam2.BarsPositionUpdate();
        if (FightTeam2.teamConfig.myTeam == playerTeam)
            FightTeam1.BarsPositionUpdate();
    }
    
	public void Refresh()//这个刷新是倾向于画面制御
	{
        if (combatFightPlayerMode && focusingChar != null)
        {
            _C_button.gameObject.SetActive(true);
            _AI_button.gameObject.SetActive(false);
        }
        else
        {
            _C_button.gameObject.SetActive(false);
            _AI_button.gameObject.SetActive(true);
        }
        void SwitchAUtoMOde()
        {
            combatFightPlayerMode = !combatFightPlayerMode;
            SwitchToCMode(focusingChar, combatFightPlayerMode);
            Refresh();
        }
        autoBUtton.onClick.RemoveAllListeners();
        autoBUtton.onClick.AddListener(SwitchAUtoMOde);

        FightTeam1.Refresh();
        FightTeam2.Refresh();
        
        if (focusingChar == null)
        {
            _mobileInputsManager.TurnOffButtons();
        }
        else
        {
            _mobileInputsManager.FocusCharInputs(focusingChar.AIStateRunner.getInputManager(),focusingChar.Zokusei);
            _mobileInputsManager.TurnOnButtons();
        }
	}
    
	public void SwitchToCMode(Data_Center _char,bool playerControll) //要转成控制模式的是哪个角色，如果括号里是null，意味着走向AI模式    
    {
        if (focusingChar != null)
            focusingChar.AIStateRunner.SetPlayerMode(false);
        focusingChar = _char;
        if (focusingChar != null)
            focusingChar.AIStateRunner.SetPlayerMode(playerControll);
    }

    public void Clear()// 这个我们还没有添加在合理的地方。
    {
        FightTeam1.Clear();
        FightTeam2.Clear();
        _mobileInputsManager.Clear();
    }
    
    private List<Transform> outter_watchetargets = new List<Transform>();
    private List<Transform> inner_watchetargets = new List<Transform>();
    public void FightingStepProcess()
    {
        FightTeam1.LocalFightingUpdate();
        FightTeam2.LocalFightingUpdate();
        FightGUIProcess();
        
        outter_watchetargets.Clear();
        inner_watchetargets.Clear();
        if (focusingChar != null)
        {
            _CameraManager.current_Camera_Mode.setMeCenter(focusingChar.WholeT);
            foreach (Collider _G in focusingChar.Sensor.getInnerEnemiesColliders())
            {
                inner_watchetargets.Add(_G.transform);
            }
            foreach (Collider _G in focusingChar.Sensor.getMidEnemiesColliders())
            {
                outter_watchetargets.Add(_G.transform);
            }
            if (inner_watchetargets.Count == 0 && outter_watchetargets.Count > 0)
                _CameraManager.current_Camera_Mode.targets = outter_watchetargets;
            if (inner_watchetargets.Count > 0)
                _CameraManager.current_Camera_Mode.targets = inner_watchetargets;
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
                    SwitchToCMode(null,combatFightPlayerMode);
                break;
                case Team.player2:
                    playerTeam = Team.player1;
                     SwitchToCMode(null,combatFightPlayerMode);
                break;
            }
            Refresh();
        }
    }
}

//    switch (this.hPBarDisplayMode)
//{
//    case HPBarDisplayMode.allEnemies:
//        break;
//    case HPBarDisplayMode.onlyNearEnemies:
//        if (focusingChar == null)
//            continue;
//        if (Vector3.Distance(_one.transform.position, focusingChar.gameObject.transform.position) > 11f)
//        {
//            oneHpBar.gameObject.SetActive(false);// 试着变颜色
//            continue;
//        }else{
//            oneHpBar.gameObject.SetActive(true);// 试着变颜色
//        }
//        break;                    
//}