using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;
using System.Linq;

namespace mainMenu
{
    public class SelfFightManager : MonoBehaviour
    {
        public preparingScene _preparingScene;

        public Transform selfFightUI;
        public Button FightStartBUtton;

        public Transform MuitiRaidModeIconsT;
        public charIcon team1back, team1front, team1left, team1right, team1_1, team1_2,team1_3,team1_4,team1_5,team1_6;
        public charIcon team2back, team2front, team2left, team2right, team2_1, team2_2,team2_3,team2_4,team2_5,team2_6;
        
        public Transform RotationModeIconsT;
        public charIcon team11_R, team12_R, team13_R;
        public charIcon team21_R, team22_R, team23_R;

        public QuestPreparePage _QuestPreparePage;

        readonly IDictionary<int, charIcon> team1ButtonDic_M = new Dictionary<int, charIcon>();
        readonly IDictionary<int, charIcon> team2ButtonDic_M = new Dictionary<int, charIcon>();
        IDictionary<int, charIcon> team1ButtonDic_R = new Dictionary<int, charIcon>();
        IDictionary<int, charIcon> team2ButtonDic_R = new Dictionary<int, charIcon>();

        LocalFight _selfFight = new LocalFight { };
        StageScriptableObject stage;
        Team focusingTeam; //team1或者是team2
        int focusingPosition; // 0到3
        readonly charIcon focusingPosButton;

        PositionLocalCharKeySet _team1positionLocalCharKeySet_M = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team2positionLocalCharKeySet_M = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team1positionLocalCharKeySet_R = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team2positionLocalCharKeySet_R = new PositionLocalCharKeySet();

        void Start()
        {
            stage = new StageScriptableObject
            {
                BattleGroundID = 2
            };
            SwitchToMultiRaidMode();
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, charIcon> keyValuePair in team1ButtonDic_M)
            {
                keyValuePair.Value.changeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, charIcon> keyValuePair in team2ButtonDic_M)
            {
                keyValuePair.Value.changeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, charIcon> keyValuePair in team1ButtonDic_R)
            {
                keyValuePair.Value.changeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, charIcon> keyValuePair in team2ButtonDic_R)
            {
                keyValuePair.Value.changeIcon(null, Zokusei.Null);
            }
            _team1positionLocalCharKeySet_M = new PositionLocalCharKeySet();
            _team2positionLocalCharKeySet_M = new PositionLocalCharKeySet();
            _team1positionLocalCharKeySet_R = new PositionLocalCharKeySet();
            _team2positionLocalCharKeySet_R = new PositionLocalCharKeySet();
        }
        
        public void SwitchToMultiRaidMode()
        {
            MuitiRaidModeIconsT.gameObject.SetActive(true);
            RotationModeIconsT.gameObject.SetActive(false);
            stage.fightModeType = fightModeType.combat;
            stage._fightEventType = fightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.multiraid;
        }
        
        public void SwitchToRotationMode()
        {
            RotationModeIconsT.gameObject.SetActive(true);
            MuitiRaidModeIconsT.gameObject.SetActive(false);
            stage.fightModeType = fightModeType.combat;
            stage._fightEventType = fightEventType.Self;
            stage.Team1Mode = TeamMode.rotation;
            stage.Team2Mode = TeamMode.rotation;
        }
        
        public void SwitchToTestMode()
        {
            MuitiRaidModeIconsT.gameObject.SetActive(true);
            RotationModeIconsT.gameObject.SetActive(false);
            stage.fightModeType = fightModeType.combat;
            stage._fightEventType = fightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.test;
        }

        public IEnumerator FightStart()
        {
            MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
            switch (stage.Team1Mode)
            {
                case TeamMode.multiraid:
                    IEnumerator enumerator1 = _team1positionLocalCharKeySet_M.ConvertToMultiDictionary();
                    yield return enumerator1;
                    _selfFight.HeroSets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator1.Current;
                    IEnumerator enumerator2 = _team2positionLocalCharKeySet_M.ConvertToMultiDictionary();
                    yield return enumerator2;
                    _selfFight.EnemySets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator2.Current;
                    break;
                case TeamMode.rotation:
                    IEnumerator enumerator3 = _team1positionLocalCharKeySet_R.ConvertToMultiDictionary();
                    yield return enumerator3;
                    _selfFight.HeroSets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator3.Current;
                    IEnumerator enumerator4 = _team2positionLocalCharKeySet_R.ConvertToMultiDictionary();
                    yield return enumerator4;
                    _selfFight.EnemySets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator4.Current;
                    break;
            }
            stage.localFight = _selfFight;
            yield return _QuestPreparePage.GetReadyToBattle(stage, SceneMode.MyPetsFight);
            yield break;
        }

        public IEnumerator MonsterIConButton(string localID)
        {
            if (this.focusingTeam == Team.none || this.focusingPosition < 0)
                yield break;
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo(localID);
            yield return getchar;
            GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (myfighter == null)
            {
                Debug.Log("角色存档问题。localid：" + localID);
                yield break;
            }
            
            switch (stage.Team1Mode)
            {
                case TeamMode.multiraid:
                    switch (this.focusingTeam)
                    {
                        case Team.player1:
                            _team1positionLocalCharKeySet_M.SetPosMemInfoByLocalID(this.focusingPosition,localID);
                            yield return ChangeIconOnPos(this.focusingPosition,team1ButtonDic_M,_team1positionLocalCharKeySet_M);
                            break;
                        case Team.player2:
                            _team2positionLocalCharKeySet_M.SetPosMemInfoByLocalID(this.focusingPosition,localID);
                            yield return ChangeIconOnPos(this.focusingPosition,team2ButtonDic_M,_team2positionLocalCharKeySet_M);
                            break;
                    }
                    break;
                case TeamMode.rotation:
                    switch (this.focusingTeam)
                    {
                        case Team.player1:
                            _team1positionLocalCharKeySet_R.SetPosMemInfoByLocalID(this.focusingPosition,localID);
                            yield return ChangeIconOnPos(this.focusingPosition,team1ButtonDic_R,_team1positionLocalCharKeySet_R);
                            break;
                        case Team.player2:
                            _team2positionLocalCharKeySet_R.SetPosMemInfoByLocalID(this.focusingPosition,localID);
                            yield return ChangeIconOnPos(this.focusingPosition,team2ButtonDic_R,_team2positionLocalCharKeySet_R);
                            break;
                    }
                    break;
            }
            yield break;
            
            //以下为防重复版本选人。
            //switch (this.focusingTeam)
            //{
            //    case Team.player1:
            //        PosNumWithLocalKey _PosNumWithLocalKeyfromteam2 = _team2positionLocalCharKeySet.getPosMemInfoByLocalID(localID);
            //        if (_PosNumWithLocalKeyfromteam2 != null)
            //        {
            //            positionLocalCharKeySet.
            //            changePositionBetweenDifferentTeamSets
            //            ((PosNum)this.focusingPosition,_team1positionLocalCharKeySet,_PosNumWithLocalKeyfromteam2.posNum,_team2positionLocalCharKeySet);
            //            yield return changeIconOnPos((PosNum)this.focusingPosition,team1ButtonDic,_team1positionLocalCharKeySet);
            //            yield return changeIconOnPos(_PosNumWithLocalKeyfromteam2.posNum,team2ButtonDic,_team2positionLocalCharKeySet);
            //        }else{
            //            posNums = _team1positionLocalCharKeySet.setPosMemInfoByLocalIDConservationMode((PosNum)this.focusingPosition,localID);
            //            for (int i = 0; i < posNums.Count; i++)
            //            {
            //                yield return changeIconOnPos(posNums[i].posNum,team1ButtonDic,_team1positionLocalCharKeySet);
            //            }
            //        }
            //        break;
            //    case Team.player2:
            //        PosNumWithLocalKey _PosNumWithLocalKeyfromteam1 = _team1positionLocalCharKeySet.getPosMemInfoByLocalID(localID);
            //        if (_PosNumWithLocalKeyfromteam1 != null)
            //        {
            //            positionLocalCharKeySet.
            //            changePositionBetweenDifferentTeamSets
            //            ((PosNum)this.focusingPosition,_team2positionLocalCharKeySet,_PosNumWithLocalKeyfromteam1.posNum,_team1positionLocalCharKeySet);
            //            yield return changeIconOnPos((PosNum)this.focusingPosition,team2ButtonDic,_team2positionLocalCharKeySet);
            //            yield return changeIconOnPos(_PosNumWithLocalKeyfromteam1.posNum,team1ButtonDic,_team1positionLocalCharKeySet);
            //        }else{
            //            posNums = _team2positionLocalCharKeySet.setPosMemInfoByLocalIDConservationMode((PosNum)this.focusingPosition,localID);
            //            for (int i = 0; i < posNums.Count; i++)
            //            {
            //                yield return changeIconOnPos(posNums[i].posNum,team2ButtonDic,_team2positionLocalCharKeySet);
            //            }
            //        }
            //        break;
            //} 
        }
        
        private IEnumerator ChangeIconOnPos(int posNum, IDictionary<int, charIcon> teamButtonDic, PositionLocalCharKeySet positionLocalCharKey)
        {
            if (posNum == -1)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
                yield break;
            }
            charIcon tar = null;
            if (teamButtonDic.ContainsKey(posNum))
            {
                teamButtonDic.TryGetValue(posNum,out tar);
            }else{
                Debug.Log("错误的位置值：" + posNum);
                yield break;
            }        
            if (tar == null)
            {
                Debug.Log("严重错误");
                yield break;
            }
            
            string PositionMonsterOfPlayerId = positionLocalCharKey.GetPositionMonsterOfPlayerId(posNum);
            if (PositionMonsterOfPlayerId != null)
            {
                GetMonsterOfPlayerDetailModel _one;
                CharacterResourceInfo characterResourceInfo = null;
                IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo(PositionMonsterOfPlayerId);
                yield return getchar;
                if (getchar.Current == null)
                    yield break;
                _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
                characterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_one.monsterId);
                tar.changeIcon(characterResourceInfo == null ? null: monsterIconsDic.Instance.getMonsterIconSyn(characterResourceInfo.RECORD_ID),
                    characterResourceInfo == null ? Zokusei.Null : characterResourceInfo._zokusei);
            }else{
                tar.changeIcon(null,Zokusei.Null);
            }
            yield break;
        }
        
        public void IniMutiRaidModeCharIcons(List<charIcon> icons,Team team)
        {
            IDictionary<int, charIcon> targetTeamIcons;
            switch (team)
            {
                case Team.player1:
                    targetTeamIcons = team1ButtonDic_M;
                break;
                case Team.player2:
                    targetTeamIcons = team2ButtonDic_M;
                break;
                default:
                    Debug.Log("logic error");
                    return;
            }
            if (targetTeamIcons == null) targetTeamIcons = new Dictionary<int, charIcon>();
            else targetTeamIcons.Clear();
            for (int i = 0; i < icons.Count; i++)
            {
                targetTeamIcons.Add(i,icons[i]);
                icons[i].changeIcon(null, Zokusei.Null);
                icons[i].iconButton.onClick.RemoveAllListeners();
                string pos = i.ToString().Clone().ToString();
                void A()
                {
                    OneTeamPosButtonBehaviour(team,pos);
                }
                icons[i].iconButton.onClick.AddListener(A);
            }
        }

        public IEnumerator INITeamPosButtons()
        {
            IniMutiRaidModeCharIcons(
                new List<charIcon> { team1back, team1left,team1front,team1right,team1_1,team1_2,team1_3,team1_4,team1_5,team1_6},
                Team.player1
            );
            IniMutiRaidModeCharIcons(
                new List<charIcon> { team2back, team2left,team2front,team2right,team2_1,team2_2,team2_3,team2_4,team2_5,team2_6},
                Team.player2
            );
            
            /////////////////////
            
            team1ButtonDic_R.Clear();
            team2ButtonDic_R.Clear();

            team1ButtonDic_R.Add(0, team11_R);
            team1ButtonDic_R.Add(1, team12_R);
            team1ButtonDic_R.Add(2, team13_R);

            team2ButtonDic_R.Add(0, team21_R);
            team2ButtonDic_R.Add(1, team22_R);
            team2ButtonDic_R.Add(2, team23_R);
  
            team11_R.changeIcon(null, Zokusei.Null);
            team12_R.changeIcon(null, Zokusei.Null);
            team13_R.changeIcon(null, Zokusei.Null);

            team21_R.changeIcon(null, Zokusei.Null);
            team22_R.changeIcon(null, Zokusei.Null);
            team23_R.changeIcon(null, Zokusei.Null);

            team11_R.iconButton.onClick.RemoveAllListeners();
            void pos11()
            {
                OneTeamPosButtonBehaviour(Team.player1, "0");
            }
            team11_R.iconButton.onClick.AddListener(pos11);
            team12_R.iconButton.onClick.RemoveAllListeners();
            void pos12()
            {
                OneTeamPosButtonBehaviour(Team.player1, "1");
            }
            team12_R.iconButton.onClick.AddListener(pos12);
            team13_R.iconButton.onClick.RemoveAllListeners();
            void pos13()
            {
                OneTeamPosButtonBehaviour(Team.player1, "2");
            }
            team13_R.iconButton.onClick.AddListener(pos13);

            team21_R.iconButton.onClick.RemoveAllListeners();
            void pos21()
            {
                OneTeamPosButtonBehaviour(Team.player2, "0");
            }
            team21_R.iconButton.onClick.AddListener(pos21);
            team22_R.iconButton.onClick.RemoveAllListeners();
            void pos22()
            {
                OneTeamPosButtonBehaviour(Team.player2, "1");
            }
            team22_R.iconButton.onClick.AddListener(pos22);
            team23_R.iconButton.onClick.RemoveAllListeners();
            void pos23()
            {
                OneTeamPosButtonBehaviour(Team.player2, "2");
            }
            team23_R.iconButton.onClick.AddListener(pos23);
            
            FightStartBUtton.onClick.RemoveAllListeners();
            void AskStartFight()
            {
                _preparingScene.mainProcessRunner.TriggerMainProcess(FightStart());
            }
            FightStartBUtton.onClick.AddListener(AskStartFight);
            yield break;
        }

        private void OneTeamPosButtonBehaviour(Team team, string pos)
        {
            this.focusingTeam = team;
            this.focusingPosition = int.Parse(pos);
            Debug.Log("点击了队伍"+team+"的位置"+pos);
        }
    }
}