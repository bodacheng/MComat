using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class SelfFightManager : MonoBehaviour
    {
        [Space(7)]
        [Header("SelfFightCanvas")]
        public Canvas SelfFightCanvas;
        
        [Space(7)]
        [Header("基本UI元素")]
        public InputField HPinput;
        public Button FightStartBUtton;
        
        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;
        
        [Space(7)]
        [Header("共同战斗式按钮")]
        public RectTransform MuitiRaidTeam1T,MuitiRaidTeam2T;
        public HeroIcon team1back, team1front, team1left, team1right, team1_1, team1_2,team1_3,team1_4,team1_5,team1_6;
        public HeroIcon team2back, team2front, team2left, team2right, team2_1, team2_2,team2_3,team2_4,team2_5,team2_6;
        
        [Space(7)]
        [Header("轮番战斗式按钮")]
        public Transform RotationTeam1T,RotationTeam2T;
        public HeroIcon team11_R, team12_R, team13_R;
        public HeroIcon team21_R, team22_R, team23_R;

        readonly IDictionary<int, HeroIcon> team1ButtonDic_M = new Dictionary<int, HeroIcon>();
        readonly IDictionary<int, HeroIcon> team2ButtonDic_M = new Dictionary<int, HeroIcon>();
        readonly IDictionary<int, HeroIcon> team1ButtonDic_R = new Dictionary<int, HeroIcon>();
        readonly IDictionary<int, HeroIcon> team2ButtonDic_R = new Dictionary<int, HeroIcon>();

        LocalFight _selfFight = new LocalFight { };
        StageScriptableObject stage;
        Team focusingTeam;
        int focusingPosition;
        readonly HeroIcon focusingPosButton;
        
        PositionLocalCharKeySet _team1positionLocalCharKeySet_M = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team2positionLocalCharKeySet_M = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team1positionLocalCharKeySet_R = new PositionLocalCharKeySet();
        PositionLocalCharKeySet _team2positionLocalCharKeySet_R = new PositionLocalCharKeySet();

        void Start()
        {
            HPinput.text = "500";
            stage = new StageScriptableObject
            {
                BattleGroundID = 2
            };
            SwitchToMultiRaidMode();
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, HeroIcon> keyValuePair in team1ButtonDic_M)
            {
                keyValuePair.Value.ChangeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, HeroIcon> keyValuePair in team2ButtonDic_M)
            {
                keyValuePair.Value.ChangeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, HeroIcon> keyValuePair in team1ButtonDic_R)
            {
                keyValuePair.Value.ChangeIcon(null, Zokusei.Null);
            }
            foreach (KeyValuePair<int, HeroIcon> keyValuePair in team2ButtonDic_R)
            {
                keyValuePair.Value.ChangeIcon(null, Zokusei.Null);
            }
            _team1positionLocalCharKeySet_M = new PositionLocalCharKeySet();
            _team2positionLocalCharKeySet_M = new PositionLocalCharKeySet();
            _team1positionLocalCharKeySet_R = new PositionLocalCharKeySet();
            _team2positionLocalCharKeySet_R = new PositionLocalCharKeySet();
        }
        
        public void SwitchToMultiRaidMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(true);
            MuitiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            stage._fightEventType = FightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.multiraid;
        }
        
        public void SwitchToRotationMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(false);
            MuitiRaidTeam2T.gameObject.SetActive(false);
            RotationTeam1T.gameObject.SetActive(true);
            RotationTeam2T.gameObject.SetActive(true);
            stage._fightEventType = FightEventType.Self;
            stage.Team1Mode = TeamMode.rotation;
            stage.Team2Mode = TeamMode.rotation;
        }
        
        public void SwitchToTestMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(true);
            MuitiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            stage._fightEventType = FightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.test;
        }

        public IEnumerator FightStart(float HP)
        {
            MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
            switch (stage.Team1Mode)
            {
                case TeamMode.multiraid:
                    IEnumerator enumerator1 = _team1positionLocalCharKeySet_M.ConvertToMultiDictionary();
                    yield return enumerator1;
                    _selfFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)enumerator1.Current;
                    IEnumerator enumerator2 = _team2positionLocalCharKeySet_M.ConvertToMultiDictionary();
                    yield return enumerator2;
                    _selfFight.EnemySets = (MultiDictionary<int, int, CharDataInfo>)enumerator2.Current;
                    break;
                case TeamMode.rotation:
                    IEnumerator enumerator3 = _team1positionLocalCharKeySet_R.ConvertToMultiDictionary();
                    yield return enumerator3;
                    _selfFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)enumerator3.Current;
                    IEnumerator enumerator4 = _team2positionLocalCharKeySet_R.ConvertToMultiDictionary();
                    yield return enumerator4;
                    _selfFight.EnemySets = (MultiDictionary<int, int, CharDataInfo>)enumerator4.Current;
                    break;
            }
            stage.team1_ExtraHP = HP;
            stage.team2_ExtraHP = HP;
            stage.localFight = _selfFight;
            yield return QuestPreparePage.Instance.GetReadyToBattle(stage, SceneMode.MyPetsFight);
            yield break;
        }

        public IEnumerator MonsterIConButton(string localID)
        {
            if (focusingTeam == Team.none || focusingPosition < 0)
                yield break;
            IEnumerator getchar = AccountCharsSet.Instance.GetAccountCharInfo(localID);
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
                    Debug.Log("dsadaf");
                    switch (focusingTeam)
                    {
                        case Team.player1:
                            _team1positionLocalCharKeySet_M.SetPosMemInfoByLocalID(focusingPosition, localID);
                            yield return ChangeIconOnPos(focusingPosition, team1ButtonDic_M,_team1positionLocalCharKeySet_M);
                            break;
                        case Team.player2:
                            _team2positionLocalCharKeySet_M.SetPosMemInfoByLocalID(focusingPosition, localID);
                            yield return ChangeIconOnPos(focusingPosition, team2ButtonDic_M,_team2positionLocalCharKeySet_M);
                            break;
                    }
                    break;
                case TeamMode.rotation:
                    switch (focusingTeam)
                    {
                        case Team.player1:
                            _team1positionLocalCharKeySet_R.SetPosMemInfoByLocalID(focusingPosition, localID);
                            yield return ChangeIconOnPos(focusingPosition, team1ButtonDic_R,_team1positionLocalCharKeySet_R);
                            break;
                        case Team.player2:
                            _team2positionLocalCharKeySet_R.SetPosMemInfoByLocalID(focusingPosition, localID);
                            yield return ChangeIconOnPos(focusingPosition, team2ButtonDic_R,_team2positionLocalCharKeySet_R);
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

        IEnumerator ChangeIconOnPos(int posNum, IDictionary<int, HeroIcon> teamButtonDic, PositionLocalCharKeySet positionLocalCharKey)
        {
            if (posNum == -1)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
                yield break;
            }
            HeroIcon tar = null;
            if (teamButtonDic.ContainsKey(posNum))
            {
                teamButtonDic.TryGetValue(posNum, out tar);
            }
            else
            {
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
                CharConfig characterResourceInfo = null;
                IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(PositionMonsterOfPlayerId);
                yield return getchar;
                if (getchar.Current == null)
                    yield break;
                _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
                characterResourceInfo = MonstersConfigTable.GetCharConfig(_one.monsterId);
                tar.ChangeIcon(characterResourceInfo == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(characterResourceInfo.RECORD_ID),
                    characterResourceInfo == null ? Zokusei.Null : characterResourceInfo._zokusei);
            }
            else
            {
                tar.ChangeIcon(null, Zokusei.Null);
            }
            yield break;
        }

        public void IniMutiRaidModeCharIcons(List<HeroIcon> icons,Team team)
        {
            IDictionary<int, HeroIcon> targetTeamIcons;
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
            if (targetTeamIcons == null) targetTeamIcons = new Dictionary<int, HeroIcon>();
            else targetTeamIcons.Clear();
            for (int i = 0; i < icons.Count; i++)
            {
                HeroIcon charIcon = icons[i];
                targetTeamIcons.Add(i,charIcon);
                charIcon.ChangeIcon(null, Zokusei.Null);
                charIcon.iconButton.onClick.RemoveAllListeners();
                
                void SelectedRender()
                {
                    HeroIcon.Seletedfeature(charIcon,selectedFrame,110f);
                }

                string pos = i.ToString().Clone().ToString();
                void A()
                {
                    OneTeamPosButtonBehaviour(team,pos);
                }
                charIcon.iconButton.onClick.AddListener(A);
                charIcon.iconButton.onClick.AddListener(SelectedRender);
            }
        }
        
        void IniRotationModeCharIcons(List<HeroIcon> icons,Team team)
        {
            IDictionary<int, HeroIcon> targetTeamIcons;
            switch (team)
            {
                case Team.player1:
                    targetTeamIcons = team1ButtonDic_R;
                break;
                case Team.player2:
                    targetTeamIcons = team2ButtonDic_R;
                break;
                default:
                    Debug.Log("logic error");
                    return;
            }
            if (targetTeamIcons == null) targetTeamIcons = new Dictionary<int, HeroIcon>();
            else targetTeamIcons.Clear();
            for (int i = 0; i < icons.Count; i++)
            {
                HeroIcon charIcon = icons[i];
                targetTeamIcons.Add(i,charIcon);
                charIcon.ChangeIcon(null, Zokusei.Null);
                charIcon.iconButton.onClick.RemoveAllListeners();
                
                void SelectedRender()
                {
                    HeroIcon.Seletedfeature(charIcon,selectedFrame,110f);
                }
                
                string pos = i.ToString().Clone().ToString();
                void A()
                {
                    OneTeamPosButtonBehaviour(team,pos);
                }
                charIcon.iconButton.onClick.AddListener(A);
                charIcon.iconButton.onClick.AddListener(SelectedRender);
            }
        }

        public IEnumerator INITeamPosButtons()
        {
            IniMutiRaidModeCharIcons(
                new List<HeroIcon> { team1back, team1left,team1front,team1right,team1_1,team1_2,team1_3,team1_4,team1_5,team1_6},
                Team.player1
            );
            IniMutiRaidModeCharIcons(
                new List<HeroIcon> { team2back, team2left,team2front,team2right,team2_1,team2_2,team2_3,team2_4,team2_5,team2_6},
                Team.player2
            );
            
            IniRotationModeCharIcons(
                new List<HeroIcon> { team11_R, team12_R, team13_R },
                Team.player1
            );
            
            IniRotationModeCharIcons(
                new List<HeroIcon> { team21_R, team22_R, team23_R },
                Team.player2
            );
                       
            FightStartBUtton.onClick.RemoveAllListeners();
            void AskStartFight()
            {
                PreScene.Instance.mainProcessRunner.Run(FightStart(float.Parse(HPinput.text)));
            }
            FightStartBUtton.onClick.AddListener(AskStartFight);
            yield break;
        }

        void OneTeamPosButtonBehaviour(Team team, string pos)
        {
            focusingTeam = team;
            focusingPosition = int.Parse(pos);
            //Debug.Log("点击了队伍" + team + "的位置" + pos);
        }
    }
}