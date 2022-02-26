using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        [SerializeField] RectTransform sideIconsContainer;
        [SerializeField] RectTransform _targetCanvasT;
        [SerializeField] SideCharIcon button_prefab;
        [SerializeField] Text HitCombo;
        
        public TeamMode TeamMode;
        public TeamConfig teamConfig;
        [HideInInspector]
        public Transform[] TeamStandPoints;
        
        public readonly IDictionary<Data_Center, SideCharIcon> UnitIconDic = new Dictionary<Data_Center, SideCharIcon>();
        
        public SideCharIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic.ContainsKey(d) ? UnitIconDic[d]: null;
        }
        
        public void Clear()
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    MultiClear();
                    break;
                case TeamMode.rotation:
                    RotateClear();
                    break;
            }
        }

        public void localUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    MultiRaid_LocalUpdate(TeamMembers);
                    break;
                case TeamMode.rotation:
                    Rotation_LocalUpdate(TeamMembers);
                    break;
            }
        }
        
        public List<Transform> GetFightingUnitTs(MultiDict<int, int, Data_Center> TeamMembers)
        {
            var transforms = new List<Transform>();
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    foreach (var a_char in TeamMembers.GetValues())
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
                        RMode_Unit.transform
                    };
                    return transforms;
            }
            return null;
        }
        
        void BarsPosUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (var _one in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_one, out var _tempSI);
                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
            }
        }
        
        public void InsTeamUI(MultiDict<int, int, Data_Center> TeamMembers)
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    InsTeamUI_Multi(TeamMembers);
                    break;
                case TeamMode.rotation:
                    InsTeamUI_Rotate(TeamMembers);
                    break;
            }
        }
        
        public void TeamsInit(MultiDict<int, int, Data_Center> TeamMembers, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    Initialize_Multi(TeamMembers, TeamHpRate, teamCGMode);
                    break;
                case TeamMode.rotation:
                    TeamsIni_Rotate(TeamMembers, TeamHpRate, teamCGMode);
                    break;
            }
        }
        
        void RefreshResistanceBar(Data_Center data_Center)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshResistanceBar();
        }
        void RefreshHPBar(Data_Center data_Center, float current_hp, float wholeHP)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshHpBar(current_hp, wholeHP);
        }
        void RefreshExBar(Data_Center data_Center, int current_ex, int wholeex)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshExBar(current_ex, wholeex);
        }
        
        public void Refresh(MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (var _dt in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_dt, out var _tempSI);
                if (teamConfig.myTeam == RTFightManager.playerTeam)
                {
                    _tempSI.transform.localScale = _dt != RTFightManager.focusingUnit ? Vector3.one : Vector3.one * 1.2f;
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
                    _tempSI.transform.SetParent(_targetCanvasT.transform);
                }
            }
            
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    foreach (var _datacenter in TeamMembers.GetValues())
                    {
                        if (multiRaidHitComboDic.ContainsKey(_datacenter))
                        {
                            multiRaidHitComboDic[_datacenter].color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                            multiRaidHitComboDic[_datacenter].gameObject.SetActive(true);
                            if (multiRaidHitComboDic[_datacenter].gameObject.transform.parent != _targetCanvasT)
                            {
                                multiRaidHitComboDic[_datacenter].gameObject.transform.SetParent(_targetCanvasT.transform);
                            }
                            multiRaidHitComboDic[_datacenter].transform.localScale = Vector3.one;
                            multiRaidHitComboDic[_datacenter].fontSize = 30;
                        }
                    }
                    break;
                case TeamMode.rotation:
                    rotationModeHitCombo.color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                    rotationModeHitCombo.gameObject.SetActive(true);
                    if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvasT)
                    {
                        rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvasT.transform);
                    }
                    rotationModeHitCombo.transform.localScale = Vector3.one;
                    rotationModeHitCombo.fontSize = 30;
                    break;
            }
        }
        
        // 获取该队伍所有账户技能石id（只有在这个队伍是玩家账户队员组成情况下有效）
        public List<string> GetAllUsingStoneOfAcc()
        {
            var stones = new List<string>();
            foreach (var keyValuePair in RTFightManager.target.UnitInfoRef)
            {
                var myStones = Stones.GetEquipingStones(keyValuePair.Value.id);
                for (var i = 0; i < myStones.Count; i++)
                {
                    stones.Add(myStones[i].InstanceId);
                }
            }
            return stones;
        }
    }
}