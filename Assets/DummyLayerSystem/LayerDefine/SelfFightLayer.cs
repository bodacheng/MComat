using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DummyLayerSystem;

namespace mainMenu
{
    public class SelfFightLayer : UILayer
    {
        [Header("Mode Buttons")] 
        [SerializeField] Button RotationModeBtn, MultiModeBtn;
        
        [SerializeField] Toggle testMode;
        
        [Header("模式选中框")]
        [SerializeField] GameObject ModeFrame;
        
        [Header("选中框")]
        [SerializeField] GameObject selectedFrame;
        
        [Header("共同战斗式按钮")]
        [SerializeField] RectTransform multiRaidTeam1T, multiRaidTeam2T;
        [SerializeField] HeroIcon team1back, team1front, team1left, team1right;
        [SerializeField] HeroIcon team2back, team2front, team2left, team2right;
        
        [Header("轮番战斗式按钮")]
        [SerializeField] Transform RotationTeam1T, RotationTeam2T;
        [SerializeField] HeroIcon team11_R, team12_R, team13_R;
        [SerializeField] HeroIcon team21_R, team22_R, team23_R;
        
        readonly MultiDic<Team, int, HeroIcon> teamButtonDic_M = new ();
        readonly MultiDic<Team, int, HeroIcon> teamButtonDic_R = new ();
        readonly IDictionary<HeroIcon, int> IconNumCheck = new Dictionary<HeroIcon, int>();
        readonly FightMembers _selfFight = new() { };
        FightInfo _stage;
        Team _focusingTeam;
        int _focusingPosNum = -1;
        
        PosKeySet _team1PosKeySet_M = new ();
        PosKeySet _team2PosKeySet_M = new ();
        PosKeySet _team1PosKeySet_R = new ();
        PosKeySet _team2PosKeySet_R = new ();
        
        public void INI()
        {
            _stage = ScriptableObject.CreateInstance<FightInfo>();
            _stage.BattleGroundID = 0;
            
            IniMultiRaidModeUnitIcons(new List<HeroIcon> { team1back, team1left, team1front, team1right }, Team.player1);
            IniMultiRaidModeUnitIcons(new List<HeroIcon> { team2back, team2left, team2front, team2right }, Team.player2);
            IniRotationModeUnitIcons(new List<HeroIcon> { team11_R, team12_R, team13_R }, Team.player1);
            IniRotationModeUnitIcons(new List<HeroIcon> { team21_R, team22_R, team23_R }, Team.player2);
            
            RotationModeBtn.onClick.AddListener(SwitchToRotationMode);
            MultiModeBtn.onClick.AddListener(SwitchToMultiRaidMode);
        }
        
        public void Clear()
        {
            foreach (var Icon in teamButtonDic_M.GetValues())
            {
                Icon.Clear();
            }
            foreach (var Icon in teamButtonDic_R.GetValues())
            {
                Icon.Clear();
            }
            
            _team1PosKeySet_M = new PosKeySet();
            _team2PosKeySet_M = new PosKeySet();
            _team1PosKeySet_R = new PosKeySet();
            _team2PosKeySet_R = new PosKeySet();
        }
        
        void CancelSelect()
        {
            _focusingPosNum = -1;
            HeroIcon.SelectedFeature(null, selectedFrame, 1.1f);
        }

        void FrameRefresh(Transform t)
        {
            ModeFrame.transform.SetParent(t);
            ModeFrame.transform.localPosition = Vector3.zero;
        }

        public void SwitchToMultiRaidMode()
        {
            multiRaidTeam1T.gameObject.SetActive(true);
            multiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            _stage.EventType= FightEventType.Self;
            _stage.Team1Mode = TeamMode.multiRaid;
            _stage.Team2Mode = TeamMode.multiRaid;
            FrameRefresh(MultiModeBtn.transform);
        }
        
        public void SwitchToRotationMode()
        {
            multiRaidTeam1T.gameObject.SetActive(false);
            multiRaidTeam2T.gameObject.SetActive(false);
            RotationTeam1T.gameObject.SetActive(true);
            RotationTeam2T.gameObject.SetActive(true);
            _stage.EventType = FightEventType.Self;
            _stage.Team1Mode = TeamMode.rotation;
            _stage.Team2Mode = TeamMode.rotation;
            FrameRefresh(RotationModeBtn.transform);
        }
        
        public void FightStart()
        {
            switch (_stage.Team1Mode)
            {
                case TeamMode.multiRaid:
                    _selfFight.HeroSets = _team1PosKeySet_M.LoadTeamDic();
                    _selfFight.EnemySets = _team2PosKeySet_M.LoadTeamDic();
                    break;
                case TeamMode.rotation:
                    _selfFight.HeroSets = _team1PosKeySet_R.LoadTeamDic();
                    _selfFight.EnemySets = _team2PosKeySet_R.LoadTeamDic();
                    break;
            }
            _stage.FightMembers = _selfFight;
            _stage.team1ID = PlayerAccountInfo.Me.PlayFabId;
            _stage.team2ID = PlayerAccountInfo.Me.PlayFabId + "_2";
            FightLoad.Go(_stage);
        }
        
        #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
        public void AddHeroIconFeaturesToMonsterBox()
        {
            void MonsterIconButton(string instanceID)
            {
                var unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
                unitsLayer.Select(instanceID);
                UnitIconBtn(instanceID);
            }
            void Trigger(string instanceID)
            {
                MonsterIconButton(instanceID);
            }
            
            var unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
            unitsLayer.SetUnitsIconOnClick(Trigger);
        }
        #endregion
        
        void UnitIconBtn(string instanceID)
        {
            var unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
            if (_focusingPosNum == -1)
            {
                unitsLayer.Select(instanceID);
            }
            else
            {
                switch (_stage.Team1Mode)
                {
                    case TeamMode.multiRaid:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySet_M.SetPosMemInfoByLocalID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, teamButtonDic_M, _team1PosKeySet_M);
                                break;
                            case Team.player2:
                                _team2PosKeySet_M.SetPosMemInfoByLocalID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, teamButtonDic_M, _team2PosKeySet_M);
                                break;
                        }
                        break;
                    case TeamMode.rotation:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySet_R.SetPosMemInfoByLocalID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, teamButtonDic_R, _team1PosKeySet_R);
                                break;
                            case Team.player2:
                                _team2PosKeySet_R.SetPosMemInfoByLocalID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, teamButtonDic_R, _team2PosKeySet_R);
                                break;
                        }
                        break;
                }
                CancelSelect();
                unitsLayer.CancelSelect();
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

        async void ChangeIconOnPos(int posNum, MultiDic<Team, int, HeroIcon> teamButtonDic, PosKeySet posKeySet)
        {
            if (posNum == -1)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
            }
            HeroIcon tar = teamButtonDic.Get(_focusingTeam, posNum);
            if (tar == null)
            {
                Debug.Log("严重错误");
            }
            
            var PosInstanceId = posKeySet.GetInstanceIdOnPos(posNum);
            if (PosInstanceId != null)
            {
                var _one = dataAccess.Units.Get(PosInstanceId);
                tar.ChangeIcon(_one);
            }
            else
            {
                tar.Clear();
            }
        }
        
        void IniMultiRaidModeUnitIcons(List<HeroIcon> icons, Team team)
        {
            for (var i = 0; i < icons.Count; i++)
            {
                var heroIcon = icons[i];
                teamButtonDic_M.Set(team, i, heroIcon);
                DicAdd<HeroIcon, int>.Add(IconNumCheck, heroIcon, i);
                heroIcon.Clear();
                
                void SelectedRender()
                {
                    HeroIcon.SelectedFeature(heroIcon, selectedFrame, 1.1f);
                }
                void A()
                {
                    OnTeamPosBtn(team, IconNumCheck[heroIcon]);
                }
                heroIcon.iconButton.onClick.RemoveAllListeners();
                heroIcon.iconButton.onClick.AddListener(A);
                heroIcon.iconButton.onClick.AddListener(SelectedRender);
            }
        }
        
        void IniRotationModeUnitIcons(List<HeroIcon> icons, Team team)
        {
            for (var i = 0; i < icons.Count; i++)
            {
                var heroIcon = icons[i];
                teamButtonDic_R.Set(team, i, heroIcon);
                DicAdd<HeroIcon, int>.Add(IconNumCheck, heroIcon, i);
                heroIcon.Clear();
                heroIcon.iconButton.onClick.RemoveAllListeners();
                heroIcon.iconButton.onClick.AddListener(() => {OnTeamPosBtn(team, IconNumCheck[heroIcon]);});
                heroIcon.iconButton.onClick.AddListener(() => HeroIcon.SelectedFeature(heroIcon, selectedFrame, 1.1f));
            }
        }
        
        void OnTeamPosBtn(Team team, int pos)
        {
            _focusingTeam = team;
            _focusingPosNum = pos;
            var unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
            var unitsBoxSelect = unitsLayer.GetSelect();
            
            if (unitsBoxSelect != null)
            {
                UnitIconBtn(unitsBoxSelect);
            }
        }
    }
}