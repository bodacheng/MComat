using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

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
            if (teamConfig.myTeam != RTFightManager.playerTeam)
            {
                BarsPosUpdate(TeamMembers);
            }
        }
        
        void BarsPosUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (var _one in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_one, out var _tempSI);
                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
            }
        }
        
        public void InsTeamUI(MultiDict<int, int, Data_Center> TeamMembers, Action<Data_Center> changeUnit, ReactiveProperty<Data_Center> RMode_Unit)
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    InsTeamUI_Multi(TeamMembers);
                    break;
                case TeamMode.rotation:
                    IniTeamUI_Rotate(TeamMembers, changeUnit);
                    IniComboHit(RMode_Unit);
                    RMode_Unit.Subscribe(
                        x =>
                    {
                        FightingStepLayer.target.Refresh();
                    }).AddTo(gameObject);
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
        
        public void Refresh(MultiDict<int, int, Data_Center> TeamMembers, Data_Center fighting = null)
        {
            foreach (var _dt in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_dt, out var _tempSI);
                if (_tempSI == null)
                    continue;
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
                    _tempSI.gameObject.SetActive(fighting == _dt);
                    _tempSI.focusingCharIcon.gameObject.SetActive(false);
                    _tempSI.ExBar.gameObject.SetActive(false);
                    _tempSI.transform.SetParent(_targetCanvasT.transform);
                }
            }
        }
    }
}