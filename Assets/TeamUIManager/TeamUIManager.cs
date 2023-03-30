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
        [SerializeField] SideUnitIcon button_prefab;
        [SerializeField] Text hitCombo;
        [SerializeField] AutoSwitch TeamAuto;

        public TeamMode teamMode;
        public TeamConfig teamConfig;
        public MultiDic<int, int, Data_Center> teamMembers;
        public readonly IDictionary<Data_Center, SideUnitIcon> UnitIconDic = new Dictionary<Data_Center, SideUnitIcon>();
        private IDisposable barPosUpdate;
        private int barPosUpdateInterval = 3;
        
        public SideUnitIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic[d];
        }
        
        public void Clear()
        {
            barPosUpdate?.Dispose();
            switch (teamMode)
            {
                case TeamMode.MultiRaid:
                    MultiClear();
                    break;
                case TeamMode.Rotation:
                    RotateClear();
                    break;
            }
        }
        
        public void InsTeamUI(Action<Data_Center> changeUnit, Func<bool> currentAutoState, Action<bool> switchTeamAuto, ReactiveProperty<Data_Center> rModeUnit)
        {
            TeamAuto.Initialize(currentAutoState, switchTeamAuto);
            if (teamConfig.myTeam != RTFightManager.playerTeam)
            {
                TeamAuto.gameObject.SetActive(CommonSetting.DevMode || FightScene.Fight.EventType == FightEventType.Self);
            }
            switch (teamMode)
            {
                case TeamMode.MultiRaid:
                    InsTeamUI_Multi(switchTeamAuto, currentAutoState);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                        {
                            foreach (var one in teamMembers.GetValues())
                            {
                                UnitIconDic.TryGetValue(one, out var _tempSI);
                                if (_tempSI != null)
                                    _tempSI.transform.DOMove(CameraManager._camera.WorldToScreenPoint(one.transform.position + Vector3.up * 2.5f), 0.5f);
                            }
                        }).AddTo(gameObject);
                        Refresh();
                    }
                    break;
                case TeamMode.Rotation:
                    IniTeamUI_Rotate(changeUnit);
                    IniComboHit(rModeUnit);
                    rModeUnit.Subscribe(Refresh).AddTo(gameObject);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                            {
                                if (teamConfig.myTeam != RTFightManager.playerTeam)
                                {
                                    if (rModeUnit.Value == null)
                                        return;
                                    UnitIconDic.TryGetValue(rModeUnit.Value, out var tempSi);
                                    if (tempSi != null)
                                        tempSi.transform.DOMove(CameraManager._camera.WorldToScreenPoint(rModeUnit.Value.transform.position + Vector3.up * 2.5f), 0.5f);
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
        
        void RefreshResistanceBar(Data_Center dataCenter, int value)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            if (tempSi != null)
                tempSi.RefreshResistanceBar(value);
        }
        void RefreshHPBar(Data_Center dataCenter, float currentHp, float wholeHP)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            tempSi.RefreshHpBar(currentHp, wholeHP);
        }
        void RefreshExBar(Data_Center dataCenter, int currentEx, int wholeEx)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            tempSi.RefreshExBar(currentEx, wholeEx);
        }
        
        void Refresh(Data_Center fighting = null)
        {
            foreach (var _dt in teamMembers.GetValues())
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
                    _tempSI.gameObject.SetActive(true);
                    _tempSI.focusingCharIcon.gameObject.SetActive(false);
                    _tempSI.HpBar.gameObject.SetActive(true);
                    _tempSI.ExBar.gameObject.SetActive(false);
                    _tempSI.transform.SetParent(_targetCanvasT.transform);
                }
            }
        }
    }
}