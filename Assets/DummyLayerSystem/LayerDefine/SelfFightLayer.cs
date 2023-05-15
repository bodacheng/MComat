using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DummyLayerSystem;

namespace mainMenu
{
    public class SelfFightLayer : UILayer
    {
        [Header("Mode Buttons")] 
        [SerializeField] Button rotationModeBtn, multiModeBtn;
        
        [Header("模式选中框")]
        [SerializeField] GameObject modeFrame;
        
        [Header("选中框")]
        [SerializeField] GameObject selectedFrame;

        [Header("删除")]
        [SerializeField] Button removeBtn;
        
        [Header("共同战斗式按钮")]
        [SerializeField] RectTransform multiRaidTeam1T, multiRaidTeam2T;
        [SerializeField] HeroIcon team1back, team1front, team1left, team1right;
        [SerializeField] HeroIcon team2back, team2front, team2left, team2right;
        
        [Header("轮番战斗式按钮")]
        [SerializeField] Transform RotationTeam1T, RotationTeam2T;
        [SerializeField] HeroIcon team11_R, team12_R, team13_R;
        [SerializeField] HeroIcon team21_R, team22_R, team23_R;
        
        [Header("选中角色的技能显示")]
        [SerializeField] NineForShow nineForShow;

        [Header("Start")] 
        [SerializeField] FightModeSwitch _fightModeSwitch;
        [SerializeField] FightBeginBtn fightStartBtn;

        private readonly MultiDic<Team, int, HeroIcon> _teamButtonDicM = new MultiDic<Team, int, HeroIcon>();
        readonly MultiDic<Team, int, HeroIcon> _teamButtonDicR = new MultiDic<Team, int, HeroIcon>();
        readonly IDictionary<HeroIcon, int> _iconNumCheck = new Dictionary<HeroIcon, int>();
        private readonly FightMembers _selfFight = new FightMembers();
        FightInfo _stage;
        Team _focusingTeam;
        int _focusingPosNum = -1;

        PosKeySet _team1PosKeySetM = new PosKeySet();
        PosKeySet _team2PosKeySetM = new PosKeySet();
        PosKeySet _team1PosKeySetR = new PosKeySet();
        PosKeySet _team2PosKeySetR = new PosKeySet();
        
        public void INI()
        {
            _stage = ScriptableObject.CreateInstance<FightInfo>();
            _stage.battleGroundID = 2;
            
            IniMultiRaidModeUnitIcons(new List<HeroIcon> { team1back, team1left, team1front, team1right }, Team.player1);
            IniMultiRaidModeUnitIcons(new List<HeroIcon> { team2back, team2left, team2front, team2right }, Team.player2);
            IniRotationModeUnitIcons(new List<HeroIcon> { team11_R, team12_R, team13_R }, Team.player1);
            IniRotationModeUnitIcons(new List<HeroIcon> { team21_R, team22_R, team23_R }, Team.player2);
            
            rotationModeBtn.onClick.AddListener(SwitchToRotationMode);
            multiModeBtn.onClick.AddListener(SwitchToMultiRaidMode);

            _fightModeSwitch.Setup(0,PlayerPrefs.GetInt("preferAdventureMode",  PlayerPrefs.GetInt("preferAdventureMode", 2)));
            fightStartBtn.SetAction(FightStart);
        }
        
        // btn feature
        public void FightStart()
        {
            _stage.team1Mode = _fightModeSwitch.TeamMode;
            _stage.team2Mode = _fightModeSwitch.TeamMode;
            FightLoad.Go(_stage);
        }
        
        public void Clear()
        {
            foreach (var icon in _teamButtonDicM.GetValues())
            {
                icon.Clear();
            }
            foreach (var icon in _teamButtonDicR.GetValues())
            {
                icon.Clear();
            }
            
            _team1PosKeySetM = new PosKeySet();
            _team2PosKeySetM = new PosKeySet();
            _team1PosKeySetR = new PosKeySet();
            _team2PosKeySetR = new PosKeySet();
        }
        
        void CancelSelect()
        {
            _focusingPosNum = -1;
            HeroIcon.SelectedFeature(null, selectedFrame, 1.1f);
        }

        void FrameRefresh(Transform t)
        {
            modeFrame.transform.SetParent(t);
            modeFrame.transform.localPosition = Vector3.zero;
        }

        public void SwitchToMultiRaidMode()
        {
            multiRaidTeam1T.gameObject.SetActive(true);
            multiRaidTeam2T.gameObject.SetActive(true);
            RotationTeam1T.gameObject.SetActive(false);
            RotationTeam2T.gameObject.SetActive(false);
            _stage.EventType= FightEventType.Self;
            _stage.team1Mode = TeamMode.MultiRaid;
            _stage.team2Mode = TeamMode.MultiRaid;
            FrameRefresh(multiModeBtn.transform);
        }
        
        public void SwitchToRotationMode()
        {
            multiRaidTeam1T.gameObject.SetActive(false);
            multiRaidTeam2T.gameObject.SetActive(false);
            RotationTeam1T.gameObject.SetActive(true);
            RotationTeam2T.gameObject.SetActive(true);
            _stage.EventType = FightEventType.Self;
            _stage.team1Mode = TeamMode.Rotation;
            _stage.team2Mode = TeamMode.Rotation;
            FrameRefresh(rotationModeBtn.transform);
        }
        
        #region Icon Feature 必须在unit box生成所有角色头像之后执行
        public void AddUnitIconFeaturesToBox()
        {
            void UnitIconButton(string instanceID)
            {
                var unitsLayer = UILayerLoader.Get<UnitsLayer>();
                unitsLayer.Selected.Value = instanceID;
                UnitIconBtn(instanceID);
                nineForShow.ShowStones_Acc(instanceID);
            }
            var unitsLayer = UILayerLoader.Get<UnitsLayer>();
            unitsLayer.SetUnitsIconOnClick(UnitIconButton);
        }
        #endregion
        
        void UnitIconBtn(string instanceID)
        {
            var unitsLayer = UILayerLoader.Get<UnitsLayer>();
            if (_focusingPosNum == -1)
            {
                unitsLayer.Selected.Value = instanceID;
            }
            else
            {
                switch (_stage.team1Mode)
                {
                    case TeamMode.MultiRaid:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySetM.SetPosMemInfoByInstanceID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, _teamButtonDicM, _team1PosKeySetM);
                                break;
                            case Team.player2:
                                _team2PosKeySetM.SetPosMemInfoByInstanceID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, _teamButtonDicM, _team2PosKeySetM);
                                break;
                        }
                        break;
                    case TeamMode.Rotation:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySetR.SetPosMemInfoByInstanceID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, _teamButtonDicR, _team1PosKeySetR);
                                break;
                            case Team.player2:
                                _team2PosKeySetR.SetPosMemInfoByInstanceID(_focusingPosNum, instanceID);
                                ChangeIconOnPos(_focusingPosNum, _teamButtonDicR, _team2PosKeySetR);
                                break;
                        }
                        break;
                }
                CancelSelect();
                unitsLayer.Selected.Value = null;
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

        void ChangeIconOnPos(int posNum, MultiDic<Team, int, HeroIcon> teamButtonDic, PosKeySet posKeySet)
        {
            if (posNum == -1)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
            }
            var icon = teamButtonDic.Get(_focusingTeam, posNum);
            if (icon == null)
            {
                Debug.Log("严重错误");
            }
            
            var posInstanceId = posKeySet.GetInstanceIdOnPos(posNum);
            if (posInstanceId != null)
            {
                var one = dataAccess.Units.Get(posInstanceId);
                icon.ChangeIcon(one);
            }
            else
            {
                icon.Clear();
            }

            CheckFightLegal();
        }
        
        void IniMultiRaidModeUnitIcons(List<HeroIcon> icons, Team team)
        {
            for (var i = 0; i < icons.Count; i++)
            {
                var heroIcon = icons[i];
                _teamButtonDicM.Set(team, i, heroIcon);
                DicAdd<HeroIcon, int>.Add(_iconNumCheck, heroIcon, i);
                heroIcon.Clear();
                
                void SelectedRender()
                {
                    HeroIcon.SelectedFeature(heroIcon, selectedFrame, 1.1f);
                }
                void A()
                {
                    OnTeamPosBtn(team, _iconNumCheck[heroIcon]);
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
                _teamButtonDicR.Set(team, i, heroIcon);
                DicAdd<HeroIcon, int>.Add(_iconNumCheck, heroIcon, i);
                heroIcon.Clear();
                heroIcon.iconButton.onClick.RemoveAllListeners();
                heroIcon.iconButton.onClick.AddListener(() => { OnTeamPosBtn(team, _iconNumCheck[heroIcon]); });
                heroIcon.iconButton.onClick.AddListener(() => HeroIcon.SelectedFeature(heroIcon, selectedFrame, 1.1f));
            }
        }
        
        void OnTeamPosBtn(Team team, int pos)
        {
            _focusingTeam = team;
            _focusingPosNum = pos;
            var unitsLayer = UILayerLoader.Get<UnitsLayer>();
            var unitsBoxSelect = unitsLayer.Selected.Value;
            if (unitsBoxSelect != null)
            {
                UnitIconBtn(unitsBoxSelect);
            }

            var oneSet = CheckIfHaveUnitOnTeamSlot(_focusingPosNum);
            nineForShow.ShowStones_Acc(oneSet?.instanceID);
            removeBtn.gameObject.SetActive(oneSet != null && oneSet.instanceID != null);
            removeBtn.onClick.RemoveAllListeners();
            removeBtn.onClick.AddListener(() =>
            {
                RemoveSelect(_focusingPosNum);
                removeBtn.gameObject.SetActive(false);
            });
        }

        PosKeySet.OneSet CheckIfHaveUnitOnTeamSlot(int pos)
        {
            switch (_stage.team1Mode)
            {
                case TeamMode.MultiRaid:
                    switch (_focusingTeam)
                    {
                        case Team.player1:
                            return _team1PosKeySetM.GetPosMemInfo(pos);
                        case Team.player2:
                            return _team2PosKeySetM.GetPosMemInfo(pos);
                    }
                    break;
                case TeamMode.Rotation:
                    switch (_focusingTeam)
                    {
                        case Team.player1:
                            return _team1PosKeySetR.GetPosMemInfo(pos);
                        case Team.player2:
                            return _team2PosKeySetR.GetPosMemInfo(pos);
                    }
                    break;
            }
            return null;
        }
        
        void ArrangeStageInfo()
        {
            switch (_stage.team1Mode)
            {
                case TeamMode.MultiRaid:
                    _selfFight.HeroSets = _team1PosKeySetM.LoadTeamDic();
                    _selfFight.EnemySets = _team2PosKeySetM.LoadTeamDic();
                    break;
                case TeamMode.Rotation:
                    _selfFight.HeroSets = _team1PosKeySetR.LoadTeamDic();
                    _selfFight.EnemySets = _team2PosKeySetR.LoadTeamDic();
                    break;
            }
            _stage.FightMembers = _selfFight;
            _stage.Team1ID = PlayerAccountInfo.Me.PlayFabId;
            _stage.Team2ID = PlayerAccountInfo.Me.PlayFabId + "_2";
        }

        void CheckFightLegal()
        {
            ArrangeStageInfo();
            fightStartBtn.Enable(_stage.FightMembers.CheckStonesLegal(FightEventType.Self));
        }

        void RemoveSelect(int pos)
        {
            if (pos != -1)
            {
                switch (_stage.team1Mode)
                {
                    case TeamMode.MultiRaid:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySetM.SetPosMemInfoByInstanceID(pos, null);
                                ChangeIconOnPos(pos, _teamButtonDicM, _team1PosKeySetM);
                                break;
                            case Team.player2:
                                _team2PosKeySetM.SetPosMemInfoByInstanceID(pos, null);
                                ChangeIconOnPos(pos, _teamButtonDicM, _team2PosKeySetM);
                                break;
                        }
                        break;
                    case TeamMode.Rotation:
                        switch (_focusingTeam)
                        {
                            case Team.player1:
                                _team1PosKeySetR.SetPosMemInfoByInstanceID(pos, null);
                                ChangeIconOnPos(pos, _teamButtonDicR, _team1PosKeySetR);
                                break;
                            case Team.player2:
                                _team2PosKeySetR.SetPosMemInfoByInstanceID(pos, null);
                                ChangeIconOnPos(pos, _teamButtonDicR, _team2PosKeySetR);
                                break;
                        }
                        break;
                }
            }
        }
    }
}