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
        [SerializeField] MobileInputsManager inputsManager;
        [SerializeField] RectTransform sideIconsContainer;
        [SerializeField] RectTransform _targetCanvasT;
        [SerializeField] SideUnitIcon unitIconPrefab;
        [SerializeField] Text hitCombo;
        [SerializeField] AutoSwitch teamAutoSwitch;
        [SerializeField] RectTransform selectedFrame;

        public AutoSwitch AutoSwitch => teamAutoSwitch;
        public TeamMode TeamMode { get; set; }
        public TeamConfig TeamConfig { get; set; }
        public readonly IDictionary<Data_Center, SideUnitIcon> UnitIconDic = new Dictionary<Data_Center, SideUnitIcon>();
        private IDisposable barPosUpdate;
        private int barPosUpdateInterval = 3;
        
        MultiDic<int, int, Data_Center> _teamMembers;
        public MultiDic<int, int, Data_Center> TeamMembers
        {
            get => _teamMembers;
            set => _teamMembers = value;
        }
        
        public void Clear()
        {
            barPosUpdate?.Dispose();
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
        
        public void InsTeamUI(Action<Data_Center> changeUnit, Func<bool> currentAutoState, Action<bool> switchTeamAuto, ReactiveProperty<Data_Center> rModeUnit)
        {
            teamAutoSwitch.Initialize(currentAutoState, switchTeamAuto);
            if (TeamConfig.myTeam != RTFightManager.playerTeam)
            {
                teamAutoSwitch.gameObject.SetActive(CommonSetting.DevMode || FightScene.Fight.EventType == FightEventType.Self);
            }
            switch (TeamMode)
            {
                case TeamMode.MultiRaid:
                    InsTeamUI_Multi(switchTeamAuto, currentAutoState);
                    if (TeamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                        {
                            foreach (var one in _teamMembers.GetValues())
                            {
                                UnitIconDic.TryGetValue(one, out var tempSi);
                                if (tempSi != null)
                                    tempSi.transform.DOMove(CameraManager._camera.WorldToScreenPoint(one.transform.position + Vector3.up * 2.5f), 0.5f);
                            }
                        }).AddTo(gameObject);
                        Refresh();
                    }
                    break;
                case TeamMode.Rotation:
                    IniTeamUI_Rotate(changeUnit);
                    IniComboHit(rModeUnit);
                    rModeUnit.Subscribe(Refresh).AddTo(gameObject);
                    if (TeamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                            {
                                if (TeamConfig.myTeam != RTFightManager.playerTeam)
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
            tempSi?.RefreshResistanceBar(value);
        }
        void RefreshHPBar(Data_Center dataCenter, float currentHp, float wholeHP)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            tempSi?.RefreshHpBar(currentHp, wholeHP);
        }
        void RefreshExBar(Data_Center dataCenter, int currentEx)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            tempSi?.RefreshExBar(currentEx);
        }
        
        void Refresh(Data_Center fighting = null)
        {
            foreach (var dataCenter in _teamMembers.GetValues())
            {
                UnitIconDic.TryGetValue(dataCenter, out var tempSi);
                if (tempSi == null)
                    continue;
                if (TeamConfig.myTeam == RTFightManager.playerTeam)
                {
                    tempSi.transform.localScale = Vector3.one;
                    tempSi.transform.SetParent(sideIconsContainer.transform);
                    tempSi.Icon.gameObject.SetActive(true);
                    tempSi.RecallBars();
                }
                else
                {
                    tempSi.gameObject.SetActive(true);
                    tempSi.Icon.gameObject.SetActive(false);
                    tempSi.transform.SetParent(_targetCanvasT.transform);
                }
            }
        }
        
        public SideUnitIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic[d];
        }
    }
}