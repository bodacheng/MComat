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
        public Button FightStartBUtton;

        [Space(7)]
        [Header("模式选中框")]
        public GameObject RFrame, MFrame, TFrame;

        [Space(7)]
        [Header("选中框")]
        public GameObject selectedFrame;
        
        [Space(7)]
        [Header("共同战斗式按钮")]
        public RectTransform MuitiRaidTeam1T, MuitiRaidTeam2T;
        public HeroIcon team1back, team1front, team1left, team1right;
        public HeroIcon team2back, team2front, team2left, team2right;
        
        [Space(7)]
        [Header("轮番战斗式按钮")]
        public Transform RotationTeam1T, RotationTeam2T;
        public HeroIcon team11_R, team12_R, team13_R;
        public HeroIcon team21_R, team22_R, team23_R;

        public MultiDictionary<Team, int, HeroIcon> teamButtonDic_M = new MultiDictionary<Team, int, HeroIcon>();
        public MultiDictionary<Team, int, HeroIcon> teamButtonDic_R = new MultiDictionary<Team, int, HeroIcon>();

        IDictionary<HeroIcon, int> IconNumCheck = new Dictionary<HeroIcon, int>();

        FightMembers _selfFight = new FightMembers { };
        FightInfo stage;
        Team focusingTeam;
        int focusingPosNum = -1;
        
        PosKeySet _team1positionLocalCharKeySet_M = new PosKeySet();
        PosKeySet _team2positionLocalCharKeySet_M = new PosKeySet();
        PosKeySet _team1positionLocalCharKeySet_R = new PosKeySet();
        PosKeySet _team2positionLocalCharKeySet_R = new PosKeySet();

        void Start()
        {
            stage = ScriptableObject.CreateInstance<FightInfo>();
            stage.BattleGroundID = 0;
        }

        public void Clear()
        {
            foreach (HeroIcon Icon in teamButtonDic_M.values)
            {
                Icon.ChangeIcon(null, Zokusei.Null);
            }
            foreach (HeroIcon Icon in teamButtonDic_R.values)
            {
                Icon.ChangeIcon(null, Zokusei.Null);
            }

            _team1positionLocalCharKeySet_M = new PosKeySet();
            _team2positionLocalCharKeySet_M = new PosKeySet();
            _team1positionLocalCharKeySet_R = new PosKeySet();
            _team2positionLocalCharKeySet_R = new PosKeySet();
        }

        void CancelSelect()
        {
            focusingPosNum = -1;
            HeroIcon.Seletedfeature(null, selectedFrame, 200f);
        }

        public void SwitchToMultiRaidMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(true);
            MuitiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            RFrame.gameObject.SetActive(false);
            MFrame.gameObject.SetActive(true);
            TFrame.gameObject.SetActive(false);
            stage.eventType = FightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.multiraid;
        }
        
        public void SwitchToRotationMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(false);
            MuitiRaidTeam2T.gameObject.SetActive(false);
            RotationTeam1T.gameObject.SetActive(true);
            RotationTeam2T.gameObject.SetActive(true);
            RFrame.gameObject.SetActive(true);
            MFrame.gameObject.SetActive(false);
            TFrame.gameObject.SetActive(false);
            stage.eventType = FightEventType.Self;
            stage.Team1Mode = TeamMode.rotation;
            stage.Team2Mode = TeamMode.rotation;
        }
        
        public void SwitchToTestMode()
        {
            MuitiRaidTeam1T.gameObject.SetActive(true);
            MuitiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            RFrame.gameObject.SetActive(false);
            MFrame.gameObject.SetActive(false);
            TFrame.gameObject.SetActive(true);
            stage.eventType = FightEventType.Test;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.multiraid;
        }
        
        void ArrangeTeamBySelection()
        {
            switch (stage.Team1Mode)
            {
                case TeamMode.multiraid:
                    _selfFight.HeroSets = _team1positionLocalCharKeySet_M.LoadTeamDic();
                    _selfFight.EnemySets = _team2positionLocalCharKeySet_M.LoadTeamDic();
                    break;
                case TeamMode.rotation:
                    _selfFight.HeroSets = _team1positionLocalCharKeySet_R.LoadTeamDic();
                    _selfFight.EnemySets = _team2positionLocalCharKeySet_R.LoadTeamDic();
                    break;
            }
            stage.fightMembers = _selfFight;
        }

        public void FightStart()
        {
            MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
            ArrangeTeamBySelection();
            FightLoad.Go(stage);
        }
        
        #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
        public void AddHeroIconFeaturesToMonsterBox()
        {
            foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
            {
                AddHeroIconFeatureToMonsterBox(keyValuePair.Key,keyValuePair.Value.iconButton);
            }
        }
        
        void AddHeroIconFeatureToMonsterBox(string CharAccID, Button targetButton)
        {
            IEnumerator MonsterIconButton()
            {
                MonsterBox.target.Select(CharAccID);
                yield return MonsterIConButton(CharAccID);
            }
            void Trigger()
            {
                PreScene.target.mainProcessRunner.RunAsQueued(MonsterIconButton());
            }
            targetButton.onClick.AddListener(Trigger);
        }
        #endregion
        
        public IEnumerator MonsterIConButton(string localID)
        {
            if (focusingPosNum == -1)
            {
                MonsterBox.target.Select(localID);
            }
            else
            {
                switch (stage.Team1Mode)
                {
                    case TeamMode.multiraid:
                        switch (focusingTeam)
                        {
                            case Team.player1:
                                _team1positionLocalCharKeySet_M.SetPosMemInfoByLocalID(focusingPosNum, localID);
                                yield return ChangeIconOnPos(focusingPosNum, teamButtonDic_M, _team1positionLocalCharKeySet_M);
                                break;
                            case Team.player2:
                                _team2positionLocalCharKeySet_M.SetPosMemInfoByLocalID(focusingPosNum, localID);
                                yield return ChangeIconOnPos(focusingPosNum, teamButtonDic_M, _team2positionLocalCharKeySet_M);
                                break;
                        }
                        break;
                    case TeamMode.rotation:
                        switch (focusingTeam)
                        {
                            case Team.player1:
                                _team1positionLocalCharKeySet_R.SetPosMemInfoByLocalID(focusingPosNum, localID);
                                yield return ChangeIconOnPos(focusingPosNum, teamButtonDic_R, _team1positionLocalCharKeySet_R);
                                break;
                            case Team.player2:
                                _team2positionLocalCharKeySet_R.SetPosMemInfoByLocalID(focusingPosNum, localID);
                                yield return ChangeIconOnPos(focusingPosNum, teamButtonDic_R, _team2positionLocalCharKeySet_R);
                                break;
                        }
                        break;
                }
                CancelSelect();
                MonsterBox.target.CancelSelect();
            }

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

        IEnumerator ChangeIconOnPos(int posNum, MultiDictionary<Team, int, HeroIcon> teamButtonDic, PosKeySet positionLocalCharKey)
        {
            if (posNum == -1)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
                yield break;
            }
            HeroIcon tar = teamButtonDic.Get(focusingTeam, posNum);
            if (tar == null)
            {
                Debug.Log("严重错误");
                yield break;
            }

            string PositionMonsterOfPlayerId = positionLocalCharKey.GetMonsterOfPlayerIdOnPos(posNum);
            if (PositionMonsterOfPlayerId != null)
            {
                MonsterOfPlayerInfo _one = MyMonsters.Get(PositionMonsterOfPlayerId);
                CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.monsterId);
                tar.ChangeIcon(charConfig == null ? null : MonsterIconDic.GetMonsterIconSyn(charConfig.RECORD_ID),
                    charConfig == null ? Zokusei.Null : charConfig._zokusei);
            }
            else
            {
                tar.ChangeIcon(null, Zokusei.Null);
            }
            yield break;
        }

        public void IniMutiRaidModeCharIcons(List<HeroIcon> icons, Team team)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                HeroIcon charIcon = icons[i];
                teamButtonDic_M.Set(team, i, charIcon);
                DicAdd<HeroIcon, int>.Add(IconNumCheck, charIcon, i);
                charIcon.ChangeIcon(null, Zokusei.Null);
                
                void SelectedRender()
                {
                    HeroIcon.Seletedfeature(charIcon, selectedFrame, 110f);
                }
                void A()
                {
                    OneTeamPosButtonBehaviour(team, IconNumCheck[charIcon]);
                }
                charIcon.iconButton.onClick.RemoveAllListeners();
                charIcon.iconButton.onClick.AddListener(A);
                charIcon.iconButton.onClick.AddListener(SelectedRender);
            }
        }
        
        void IniRotationModeCharIcons(List<HeroIcon> icons, Team team)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                HeroIcon charIcon = icons[i];
                teamButtonDic_R.Set(team, i, charIcon);
                DicAdd<HeroIcon, int>.Add(IconNumCheck, charIcon, i);
                charIcon.ChangeIcon(null, Zokusei.Null);

                void SelectedRender()
                {
                    HeroIcon.Seletedfeature(charIcon, selectedFrame, 110f);
                }
                void A()
                {
                    OneTeamPosButtonBehaviour(team, IconNumCheck[charIcon]);
                }
                charIcon.iconButton.onClick.RemoveAllListeners();
                charIcon.iconButton.onClick.AddListener(A);
                charIcon.iconButton.onClick.AddListener(SelectedRender);
            }
        }

        void SelectedRender(HeroIcon charIcon)
        {
            HeroIcon.Seletedfeature(charIcon, selectedFrame, 110f);
        }

        public void INITeamPosButtons()
        {
            IniMutiRaidModeCharIcons(new List<HeroIcon> { team1back, team1left, team1front, team1right }, Team.player1);
            IniMutiRaidModeCharIcons(new List<HeroIcon> { team2back, team2left, team2front, team2right }, Team.player2);

            IniRotationModeCharIcons(new List<HeroIcon> { team11_R, team12_R, team13_R }, Team.player1);
            IniRotationModeCharIcons(new List<HeroIcon> { team21_R, team22_R, team23_R }, Team.player2);
                       
            FightStartBUtton.onClick.RemoveAllListeners();
            FightStartBUtton.onClick.AddListener(FightStart);
        }

        void OneTeamPosButtonBehaviour(Team team, int pos)
        {
            focusingTeam = team;
            focusingPosNum = pos;

            if (MonsterBox.selectingAccID != null)
            {
                IEnumerator temp()
                {
                    yield return MonsterIConButton(MonsterBox.selectingAccID);
                }
                PreScene.target.mainProcessRunner.RunAsQueued(temp());
            }
        }
    }
}