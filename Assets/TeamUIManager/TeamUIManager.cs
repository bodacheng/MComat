using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        public TeamMode TeamMode;
        
        public MultiDict<int, int, Data_Center> TeamMembers = new MultiDict<int, int, Data_Center>();
        public TeamConfig teamConfig;
        public RectTransform sideIconsContainer;
        public Canvas _targetCanvas;
        public SideCharIcon button_prefab;
        public Text HitCombo;
        
        [HideInInspector]
        public Transform[] TeamStandPoints;
        
        protected IDictionary<Data_Center, SideCharIcon> UnitIconDic = new Dictionary<Data_Center, SideCharIcon>();
        
        public SideCharIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic.ContainsKey(d) ? UnitIconDic[d]: null;
        }

        public IEnumerator UnitsLoad(MultiDict<int, int, UnitInfo> MembersSets)
        {
            foreach (KeyValuePair<(int, int), UnitInfo> kv in MembersSets.mDict)
            {
                UnitInfo _one = kv.Value;
                Data_Center dcenter = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (dcenter == null)
                {
                    IEnumerator char_DC = UnitCreator.CreateUnit(_one);
                    yield return char_DC;
                    dcenter = (Data_Center)char_DC.Current;
                }
                TeamMembers.Set(kv.Key.Item1, kv.Key.Item2, dcenter);
                DicAdd<Data_Center, UnitInfo>.Add(RTFightManager.target.UnitInfoRef, dcenter, _one);
            }
        }
        
        public void Clear()
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    MultiClear();
                    break;
                case TeamMode.rotation:
                    RotateClear();
                    break;
            }
        }

        public void localFightingUpdate()
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    MultiRaid_LocalFightingUpdate();
                    break;
                case TeamMode.rotation:
                    Rotation_LocalFightingUpdate();
                    break;
            }
        }
        
        public List<Transform> TeamMemberTransforms()
        {
            List<Transform> transforms = new List<Transform>();
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    foreach (Data_Center a_char in TeamMembers.GetValues())
                    {
                        if (a_char._MyBehaviorRunner.GetNowState().StateKey != "Death")
                        {
                            transforms.Add(a_char.WholeT.transform);
                        }
                    }
                    return transforms;
                case TeamMode.rotation:
                    transforms = new List<Transform>
                    {
                        RotationMode_fightingMember.transform
                    };
                    return transforms;
            }
            return null;
        }

        protected SideCharIcon _tempSI;
        public void BarsPosUpdate()
        {
            foreach (Data_Center _one in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_one, out _tempSI);
                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
            }
        }

        public void InsTeamUI()
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    InsTeamUI_Multi();
                    break;
                case TeamMode.rotation:
                    InsTeamUI_Rotate();
                    break;
            }
        }
        
        public void TeamsFightInitialize(float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    TeamsFightInitialize_Multi(TeamHpRate, teamCGMode);
                    break;
                case TeamMode.rotation:
                    TeamsFightInitialize_Rotate(TeamHpRate, teamCGMode);
                    break;
            }
        }

        // for multiRaid
        public void ToStartPos(MultiDict<int, int, Data_Center> heromultiDictionary)
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    ToStartPos_Multi(heromultiDictionary);
                    break;
                case TeamMode.rotation:
                    ToStartPos_Rotate(heromultiDictionary);
                    break;
            }
        }
        
        // 全队无敌
        public void TurnAllMembersInvincible(bool _Invincible)
        {
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                a_char.FightDataRef.Invincible = _Invincible;
            }
        }
        
        protected void RefreshResistanceBar(Data_Center data_Center)
        {
            UnitIconDic.TryGetValue(data_Center, out _tempSI);
            _tempSI.RefreshResistanceBar();
        }
        
        protected void RefreshHPBar(Data_Center data_Center, float current_hp, float wholeHP)
        {
            UnitIconDic.TryGetValue(data_Center, out _tempSI);
            _tempSI.RefreshHpBar(current_hp, wholeHP);
        }
        protected void RefreshExBar(Data_Center data_Center, int current_ex, int wholeex)
        {
            UnitIconDic.TryGetValue(data_Center, out _tempSI);
            _tempSI.RefreshExBar(current_ex, wholeex);
        }
        
        public void Refresh()
        {
            foreach (Data_Center _datacenter in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_datacenter, out _tempSI);
                if (teamConfig.myTeam == RTFightManager.playerTeam)
                {
                    _tempSI.transform.localScale = _datacenter != RTFightManager.focusingChar ? Vector3.one : Vector3.one * 1.2f;
                    _tempSI.transform.SetParent(sideIconsContainer.transform);
                    _tempSI.focusingCharIcon.gameObject.SetActive(true);
                    _tempSI.ExBar.gameObject.SetActive(true);
                    _tempSI.ExBar.transform.SetSiblingIndex(4);
                    _tempSI.RecallBars();
                }
                else
                {
                    _tempSI.focusingCharIcon.gameObject.SetActive(false);
                    _tempSI.ExBar.gameObject.SetActive(false);
                    _tempSI.transform.SetParent(_targetCanvas.transform);
                }
            }
            
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    foreach (Data_Center _datacenter in TeamMembers.GetValues())
                    {
                        if (multiRaidHitComboDic.ContainsKey(_datacenter))
                        {
                            multiRaidHitComboDic[_datacenter].color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                            multiRaidHitComboDic[_datacenter].gameObject.SetActive(true);
                            if (multiRaidHitComboDic[_datacenter].gameObject.transform.parent != _targetCanvas)
                            {
                                multiRaidHitComboDic[_datacenter].gameObject.transform.SetParent(_targetCanvas.transform);
                            }
                            multiRaidHitComboDic[_datacenter].transform.localScale = Vector3.one;
                            multiRaidHitComboDic[_datacenter].fontSize = 30;
                        }
                    }
                    break;
                case TeamMode.rotation:
                    rotationModeHitCombo.color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                    rotationModeHitCombo.gameObject.SetActive(true);
                    if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvas)
                    {
                        rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvas.transform);
                    }
                    rotationModeHitCombo.transform.localScale = Vector3.one;
                    rotationModeHitCombo.fontSize = 30;
                    break;
            }
        }

        public bool IfAllCharsPreparedForBattle()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                if (!oneMember.IfPreparedForBattle())
                    return false;
            }
            return true;
        }
        
        public void AllUnitsStartOff()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                oneMember._MyBehaviorRunner.controller.TestMode = false;
                RTFightManager.AddOrRemoveFightingMember(oneMember, this.teamConfig.myTeam, true);
                oneMember._MyBehaviorRunner.ChangeToWaitingState();
            }
        }
        
        public void LetAllCharactersChangeToTestMode()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                oneMember._MyBehaviorRunner.controller.TestMode = true;
                RTFightManager.AddOrRemoveFightingMember(oneMember, this.teamConfig.myTeam, true);
                oneMember._MyBehaviorRunner.ChangeToTestMode();
            }
        }
        
        // 队伍模式对应行为运行第一步。
        public void ModeStart()
        {
            switch (TeamMode)
            {
                case TeamMode.multiraid:
                    AllUnitsStartOff();
                    break;
                case TeamMode.rotation:
                    RTFightManager.AddOrRemoveFightingMember(RotationMode_fightingMember, this.teamConfig.myTeam, true);
                    RotationMode_fightingMember._MyBehaviorRunner.ChangeToWaitingState();
                    break;
            }
        }
        
        // 获取该队伍所有账户技能石id（只有在这个队伍是玩家账户队员组成情况下有效）
        public List<string> GetAllUsingStoneOfAcc()
        {
            List<string> stones = new List<string>();
            foreach (KeyValuePair<Data_Center, UnitInfo> keyValuePair in RTFightManager.target.UnitInfoRef)
            {
                List<StoneOfPlayerInfo> mystones = Stones.GetEquipingStones(keyValuePair.Value.id);
                for (int i = 0; i < mystones.Count; i++)
                {
                    stones.Add(mystones[i].InstanceId);
                }
            }
            return stones;
        }
    }
}