using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FightScene
{
    //角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
    public class RTFightManager : MonoBehaviour
    {
        [Header("Basic Element")]
        public CameraManager _CameraManager;
        
        [Header("Auto BUtton")]
        [Space(6)]
        public Button autoBUtton;
        public Image _C_button;
        public Image _AI_button;
        
        [Header("Messages")]
        [Space(6)]
        public Text Messages;
        
        public TeamUIManager team1;
        public TeamUIManager team2;
        
        public TeamConfig heroTeamConfig = new TeamConfig("1", Team.player1, new List<Team>() { Team.player2 });
        public TeamConfig EnemyTeamConfig = new TeamConfig("2", Team.player2, new List<Team>() { Team.player1 });
        
        public static RTFightManager target;
        
        public static bool Auto;
        public static Data_Center focusingChar;
        public static Team playerTeam = Team.player1;
        
        public MultiDict<int, int, Data_Center> Team1Members;
        public MultiDict<int, int, Data_Center> Team2Members;
        
        public IDictionary<Data_Center, UnitInfo> UnitInfoRef = new Dictionary<Data_Center, UnitInfo>();
        //public readonly IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();
        public static IDictionary<Team, List<Data_Center>> FightingMembers = new Dictionary<Team, List<Data_Center>>();
        
        FightInfo loadFight;
        
        public static void AddOrRemoveFightingMember(Data_Center member, Team team, bool add) // add:true remove: false
        {
            if (!FightingMembers.ContainsKey(team))
                FightingMembers.Add(team, new List<Data_Center>());
            List<Data_Center> fightingmembers = FightingMembers[team];
            if (add)
            {
                if (!fightingmembers.Contains(member))
                {
                    fightingmembers.Add(member);
                }
            }
            else
            {
                if (fightingmembers.Contains(member))
                {
                    fightingmembers.Remove(member);
                }
            }
            FightingMembers[team] = fightingmembers;
        }

        void Awake()
        {
            target = this;
        }
                
        public void SwitchToWatchMode() // button behaviour
        {
            SwitchToCMode(null, false);
            ParaAdjustment(playerTeam);
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
            
            team1.Refresh();
            team2.Refresh();
            
            if (focusingChar == null)
            {
                MobileInputsManager.target.TurnOffButtons();
            }
            else
            {
                MobileInputsManager.target.FocusCharInputs(focusingChar._MyBehaviorRunner, focusingChar.Zokusei);
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
        
        public IEnumerator LoadGame(FightInfo stage)
        {
            loadFight = stage;
            
            team1.teamConfig = heroTeamConfig;
            team2.teamConfig = EnemyTeamConfig;
            team1.teamConfig.playID = loadFight.team1ID;
            team2.teamConfig.playID = loadFight.team2ID;
            
            yield return team1.UnitsLoad(stage.fightMembers.HeroSets);
            yield return team2.UnitsLoad(stage.fightMembers.EnemySets);

            team1.InsTeamUI();
            team2.InsTeamUI();

            team1.TeamsFightInitialize(stage.Team1HpRate ,stage.team1CGMode);
            team2.TeamsFightInitialize(stage.Team2HpRate ,stage.team2CGMode);
            
            if (stage.GetEventType() == FightEventType.Screensaver)
            {
                team1.TurnAllMembersInvincible(true);
                team2.TurnAllMembersInvincible(true);
            }else{
                team1.TurnAllMembersInvincible(false);
                team2.TurnAllMembersInvincible(false);
            }
            
            team1.ToStartPos(team1.TeamMembers);
            team2.ToStartPos(team2.TeamMembers);
            
            switch (playerTeam)
            {
                case Team.player1:
                    SwitchToCMode(team1.TeamMembers.GetValues()[0], false);
                    break;
                case Team.player2:
                    SwitchToCMode(team2.TeamMembers.GetValues()[0], false);
                    break;
            }
            NetFightScene.target.LoadStageFinished.Value = true;
        }
        
        // 战斗模式相机。根据选择队伍做相应调整。
        public void ParaAdjustment(Team myTeam)
        {
            C_Mode c_Mode;
            if (loadFight.Team1Mode == TeamMode.multiraid)
            {
                c_Mode = C_Mode.CertainYAntiVibration;
            }
            else
            {
                c_Mode = C_Mode.CertainYAntiVibration;
            }
            if (focusingChar != null)
            {
                if (myTeam == Team.player1)
                {
                    _CameraManager.Assign_Camera(c_Mode, focusingChar.WholeT, team2.TeamMemberTransforms());
                }
                else
                {
                    _CameraManager.Assign_Camera(c_Mode, focusingChar.WholeT, team1.TeamMemberTransforms());
                }
            }
            else
            {
                _CameraManager.Assign_Camera(C_Mode.TopDown, null);
            }
        }
        
        // 屏保模式相机。
        public void ScreenSaverC(Team myTeam)
        {
            if (focusingChar != null)
            {
                if (myTeam == Team.player1)
                {
                    _CameraManager.Assign_Camera(C_Mode.ScreenSaver, team2.TeamMemberTransforms());
                }
                else
                {
                    _CameraManager.Assign_Camera(C_Mode.ScreenSaver, team1.TeamMemberTransforms());
                }
                _CameraManager.CurrentMode.SetMeCenter(focusingChar.WholeT);
            }
        }

        public void Clear()// 这个我们还没有添加在合理的地方。
        {
            team1.Clear();
            team2.Clear();
            Team1Members.Clear();
            Team2Members.Clear();
            FightingMembers.Clear();
            MobileInputsManager.target.Clear();
        }

        //void OnGUI()
        //{
        //    if (GUI.Button(new Rect(40, 40, 60, 30), "切换队伍"))
        //    {
        //        switch (playerTeam)
        //        {
        //            case Team.player1:
        //                playerTeam = Team.player2;
        //                SwitchToCMode(null, Auto);

        //                break;
        //            case Team.player2:
        //                playerTeam = Team.player1;
        //                SwitchToCMode(null, Auto);
        //                break;
        //        }
        //        CameraParaAdjustment(playerTeam);
        //    }
        //}
    }
}