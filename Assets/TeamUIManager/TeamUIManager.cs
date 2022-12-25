using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        [SerializeField] MobileInputsManager _inputsManager;
        [SerializeField] RectTransform sideIconsContainer;
        [SerializeField] RectTransform _targetCanvasT;
        [SerializeField] SideCharIcon button_prefab;
        [SerializeField] Text HitCombo;
        
        public TeamMode TeamMode;
        public TeamConfig teamConfig;
        public MultiDic<int, int, Data_Center> TeamMembers;
        public readonly IDictionary<Data_Center, SideCharIcon> UnitIconDic = new Dictionary<Data_Center, SideCharIcon>();
        
        private IDisposable barPosUpdate;
        
        public SideCharIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic[d];
        }
        
        public void Clear()
        {
            switch (TeamMode)
            {
                case TeamMode.MultiRaid:
                    MultiClear();
                    break;
                case TeamMode.Rotation:
                    RotateClear();
                    break;
            }
        }
        
        public void InsTeamUI(Action<Data_Center> changeUnit, ReactiveProperty<Data_Center> RMode_Unit)
        {
            switch (TeamMode)
            {
                case TeamMode.MultiRaid:
                    InsTeamUI_Multi();
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(30).Subscribe(_ =>
                        {
                            foreach (var _one in TeamMembers.GetValues())
                            {
                                UnitIconDic.TryGetValue(_one, out var _tempSI);
                                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
                            }
                        }).AddTo(gameObject);
                    }
                    break;
                case TeamMode.Rotation:
                    IniTeamUI_Rotate(changeUnit);
                    IniComboHit(RMode_Unit);
                    RMode_Unit.Subscribe(Refresh).AddTo(gameObject);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(30).Subscribe(_ =>
                            {
                                if (teamConfig.myTeam != RTFightManager.playerTeam)
                                {
                                    if (RMode_Unit.Value == null)
                                        return;
                                    UnitIconDic.TryGetValue(RMode_Unit.Value, out var _tempSI);
                                    if (_tempSI != null)
                                        _tempSI.transform.DOMove(CameraManager._camera.WorldToScreenPoint(RMode_Unit.Value.transform.position + Vector3.up * 3f), 1);
                                    else
                                    {
                                        Debug.Log("潜在逻辑错误");
                                    }
                                }
                            }
                        ).AddTo(gameObject);
                    }
                    break;
            }
        }
        
        void RefreshResistanceBar(Data_Center data_Center, int value)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshResistanceBar(value);
        }
        void RefreshHPBar(Data_Center data_Center, float current_hp, float wholeHP)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshHpBar(current_hp, wholeHP);
        }
        void RefreshExBar(Data_Center data_Center, int current_ex, int wholeEx)
        {
            UnitIconDic.TryGetValue(data_Center, out var _tempSI);
            _tempSI.RefreshExBar(current_ex, wholeEx);
        }
        
        void Refresh(Data_Center fighting = null)
        {
            foreach (var _dt in TeamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(_dt, out var _tempSI);
                if (_tempSI == null)
                    continue;
                if (teamConfig.myTeam == RTFightManager.playerTeam)
                {
                    _tempSI.transform.localScale = Vector3.one;
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