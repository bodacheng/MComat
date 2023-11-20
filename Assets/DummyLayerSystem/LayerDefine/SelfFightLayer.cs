using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DummyLayerSystem;

namespace mainMenu
{
    public class SelfFightLayer : UILayer
    {
        [Header("选中框")]
        [SerializeField] GameObject selectedFrame;

        [Header("删除")]
        [SerializeField] Button removeBtn;
        
        [Header("Unit slots")]
        [SerializeField] HeroIcon team11_R, team12_R, team13_R;
        [SerializeField] HeroIcon team21_R, team22_R, team23_R;
        
        [Header("选中角色的技能显示")]
        [SerializeField] NineForShow nineForShow;

        [Header("Start")]
        [SerializeField] FightModeSwitch _fightModeSwitch;
        [SerializeField] FightBeginBtn fightStartBtn;

        [Header("战场选择")] 
        [SerializeField] private Text battleFieldName;
        [SerializeField] private BOButton leftSwitchBattleField;
        [SerializeField] private BOButton rightSwitchBattleField;
        
        readonly MultiDic<Team, int, HeroIcon> _teamButtonDicR = new MultiDic<Team, int, HeroIcon>();
        readonly IDictionary<HeroIcon, int> _iconNumCheck = new Dictionary<HeroIcon, int>();
        private readonly FightMembers _selfFight = new FightMembers();
        FightInfo _stage;
        Team _focusingTeam;
        int _focusingPosNum = -1;
        private Func<bool, int> switchBattleGround;
        private Func<string> getBattleFieldName;
        
        PosKeySet _team1PosKeySetR = new PosKeySet();
        PosKeySet _team2PosKeySetR = new PosKeySet();
        
        public void INI(Func<bool, int> switchBattleGround, Func<string> getBattleFieldName)
        {
            _stage = ScriptableObject.CreateInstance<FightInfo>();
            _stage.EventType = FightEventType.Self;
            
            IniRotationModeUnitIcons(new List<HeroIcon> { team11_R, team12_R, team13_R }, Team.player1);
            IniRotationModeUnitIcons(new List<HeroIcon> { team21_R, team22_R, team23_R }, Team.player2);
            
            _fightModeSwitch.Setup(0,PlayerPrefs.GetInt("preferAdventureMode",  PlayerPrefs.GetInt("preferAdventureMode", 2)));
            fightStartBtn.SetAction(FightStart);

            this.switchBattleGround = switchBattleGround;
            this.getBattleFieldName = getBattleFieldName;
            
            this.switchBattleGround(true);
            leftSwitchBattleField.SetListener(()=> SwitchBattleGround(false));
            rightSwitchBattleField.SetListener(()=> SwitchBattleGround(true));
        }

        void SwitchBattleGround(bool left)
        {
            _stage.battleGroundID = this.switchBattleGround(left);
            battleFieldName.text = getBattleFieldName();
        }
        
        void FightStart()
        {
            _stage.team1Mode = _fightModeSwitch.TeamMode;
            _stage.team2Mode = _fightModeSwitch.TeamMode;
            FightLoad.Go(_stage);
        }
        
        public void Clear()
        {
            foreach (var icon in _teamButtonDicR.GetValues())
            {
                icon.Clear();
            }
            
            _team1PosKeySetR = new PosKeySet();
            _team2PosKeySetR = new PosKeySet();
        }
        
        void CancelSelect()
        {
            _focusingPosNum = -1;
            HeroIcon.SelectedFeature(null, selectedFrame, 1.1f);
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
                CancelSelect();
                unitsLayer.Selected.Value = null;
            }
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
            switch (_focusingTeam)
            {
                case Team.player1:
                    return _team1PosKeySetR.GetPosMemInfo(pos);
                case Team.player2:
                    return _team2PosKeySetR.GetPosMemInfo(pos);
            }
            return null;
        }
        
        void ArrangeStageInfo()
        {
            _selfFight.HeroSets = _team1PosKeySetR.LoadTeamDic();
            _selfFight.EnemySets = _team2PosKeySetR.LoadTeamDic();
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
            }
        }
    }
}