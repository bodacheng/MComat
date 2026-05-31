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
        [SerializeField] Text rotationModeHitCombo;
        [SerializeField] Animation comboTextAnim;
        [SerializeField] AutoSwitch teamAutoSwitch;
        [SerializeField] RectTransform selectedFrame;
        [SerializeField] int barPosUpdateInterval = 2;
        [SerializeField] int teamIndicatorCloseDelay = 5;
        
        public AutoSwitch AutoSwitch => teamAutoSwitch;
        public FightMode FightMode { get; set; }
        public TeamConfig TeamConfig { get; set; }
        public readonly IDictionary<Data_Center, SideUnitIcon> UnitIconDic = new Dictionary<Data_Center, SideUnitIcon>();
        private TweenTextScaleManager _textScaleManager = new TweenTextScaleManager();
        private static readonly Vector3 EnemyStatusOffset = Vector3.up * 2.5f;
        private static readonly Vector3 TeamIndicatorOffset = Vector3.up * 1.5f;
        
        private IDisposable _barPosUpdate;
        private IDisposable _teamIndicatorCloseDisposable;

        
        MultiDic<int, int, Data_Center> _teamMembers;
        public MultiDic<int, int, Data_Center> TeamMembers
        {
            get => _teamMembers;
            set => _teamMembers = value;
        }

        void Awake()
        {
            EnsureStatusWidgetsHierarchy();
        }

        bool TryGetTeamMembers(out Dictionary<(int, int), Data_Center>.ValueCollection teamMembers)
        {
            if (_teamMembers?.mDict == null)
            {
                teamMembers = null;
                return false;
            }

            teamMembers = _teamMembers.mDict.Values;
            return true;
        }

        void UpdateTrackedUiPosition(Transform target, Vector3 worldPosition)
        {
            if (target == null || CameraManager._camera == null)
            {
                return;
            }

            target.position = CameraManager._camera.WorldToScreenPoint(worldPosition);
        }
        
        public void Clear()
        {
            _barPosUpdate?.Dispose();
            _teamIndicatorCloseDisposable?.Dispose();
            
            switch (FightMode)
            {
                case FightMode.Multi:
                case FightMode.Group:
                    MultiClear();
                    break;
                case FightMode.Rotate:
                case FightMode.Evolve:
                    RotateClear();
                    break;
            }
            _textScaleManager.Clear();
            HideSelectedFrame();
        }
        
        public void InsTeamUI(Action<Data_Center> changeUnit, Func<bool> currentAutoState, Action<bool> switchTeamAuto, ReactiveProperty<Data_Center> rModeUnit)
        {
            teamAutoSwitch.Initialize(currentAutoState, switchTeamAuto);
            var allowsManualUnitControl = FightLoad.Fight.AllowsManualUnitControl;
            if (TeamConfig.myTeam != RTFightManager.playerTeam)
            {
                teamAutoSwitch.gameObject.SetActive((CommonSetting.DevMode || FightLoad.Fight.EventType == FightEventType.Self)
                                                    && allowsManualUnitControl);
            }
            else
            {
                teamAutoSwitch.gameObject.SetActive(allowsManualUnitControl);
            }
            switch (FightMode)
            {
                case FightMode.Multi:
                case FightMode.Group:
                    InsTeamUI_Multi(switchTeamAuto, currentAutoState);
                    break;
                case FightMode.Rotate:
                case FightMode.Evolve:
                    IniTeamUI_Rotate(changeUnit);
                    IniComboHit(rModeUnit);
                    rModeUnit?.Subscribe(Refresh).AddTo(gameObject);
                    SetupRotateSelectedFrameBinding(rModeUnit);
                    break;
            }

            EnsureStatusWidgetsHierarchy();
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
        
        void RefreshSuperComboFlg(Data_Center dataCenter, bool on)
        {
            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
            tempSi?.DreamComboFlg.SetActive(on);
        }
        
        public void Refresh(Data_Center fighting = null)
        {
            if (!TryGetTeamMembers(out var teamMembers))
            {
                return;
            }

            foreach (var dataCenter in teamMembers)
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
            
            _barPosUpdate?.Dispose();
            _teamIndicatorCloseDisposable?.Dispose();
            switch (FightMode)
            {
                case FightMode.Rotate:
                case FightMode.Evolve:
                    foreach (var dataCenter in teamMembers)
                    {
                        UnitIconDic.TryGetValue(dataCenter, out var tempSi);
                        if (tempSi == null)
                            continue;
                        tempSi.TeamIndicator.gameObject.SetActive(false);
                    }
                    
                    if (TeamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        _barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                            {
                                if (fighting == null)
                                    return;
                                UnitIconDic.TryGetValue(fighting, out var tempSi);
                                UpdateTrackedUiPosition(tempSi.transform, fighting.transform.position + EnemyStatusOffset);
                            }
                        ).AddTo(gameObject);
                    }
                    else
                    {
                        if (fighting != null)
                        {
                            UnitIconDic.TryGetValue(fighting, out var tempSi);
                            tempSi.TeamIndicator.gameObject.SetActive(true);
                        }
                        _barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                            {
                                if (fighting == null)
                                    return;
                                UnitIconDic.TryGetValue(fighting, out var tempSi);
                                UpdateTrackedUiPosition(tempSi.TeamIndicator.transform, fighting.transform.position + TeamIndicatorOffset);
                            }
                        ).AddTo(gameObject);
                        
                        _teamIndicatorCloseDisposable = Observable.Timer(TimeSpan.FromSeconds(teamIndicatorCloseDelay)).Subscribe(_ =>
                        {
                            _barPosUpdate.Dispose();
                            if (fighting == null)
                                return;
                            UnitIconDic.TryGetValue(fighting, out var tempSi);
                            tempSi.TeamIndicator.gameObject.SetActive(false);
                            // Add your code here to execute after disposing barPosUpdate
                            _teamIndicatorCloseDisposable.Dispose();
                        }).AddTo(gameObject);
                    }
                    break;
                case FightMode.Multi:
                case FightMode.Group:
                    if (TeamConfig.myTeam != RTFightManager.playerTeam)
                    {
                        foreach (var dataCenter in teamMembers)
                        {
                            UnitIconDic.TryGetValue(dataCenter, out var tempSi);
                            if (tempSi == null)
                                continue;
                            tempSi.TeamIndicator.gameObject.SetActive(false);
                        }
                        _barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                        {
                            foreach (var one in teamMembers)
                            {
                                UnitIconDic.TryGetValue(one, out var tempSi);
                                if (tempSi != null)
                                    UpdateTrackedUiPosition(tempSi.transform, one.transform.position + EnemyStatusOffset);
                            }
                        }).AddTo(gameObject);
                    }
                    else
                    {
                        foreach (var one in teamMembers)
                        {
                            UnitIconDic.TryGetValue(one, out var tempSi);
                            if (inputsManager.CurrentFocus.Value == null)
                                tempSi.TeamIndicator.gameObject.SetActive(true);
                            else
                                tempSi.TeamIndicator.gameObject.SetActive(one == inputsManager.CurrentFocus.Value);
                        }
                        
                        if (inputsManager.CurrentFocus.Value == null)
                        {
                            _barPosUpdate = Observable.IntervalFrame(barPosUpdateInterval).Subscribe(_ =>
                            {
                                foreach (var one in teamMembers)
                                {
                                    UnitIconDic.TryGetValue(one, out var tempSi);
                                    UpdateTrackedUiPosition(tempSi.TeamIndicator.transform, one.transform.position + TeamIndicatorOffset);
                                }
                            }).AddTo(gameObject);
                        
                            _teamIndicatorCloseDisposable = Observable.Timer(TimeSpan.FromSeconds(teamIndicatorCloseDelay)).Subscribe(_ =>
                            {
                                _barPosUpdate.Dispose();
                                foreach (var one in teamMembers)
                                {
                                    UnitIconDic.TryGetValue(one, out var tempSi);
                                    tempSi.TeamIndicator.gameObject.SetActive(false);
                                }
                                _teamIndicatorCloseDisposable.Dispose();
                            }).AddTo(gameObject);
                        }
                        else
                        {
                            inputsManager.CurrentFocus.SetValueAndForceNotify(inputsManager.CurrentFocus.Value); // for refresh
                        }
                    }
                    break;
            }
            
            if (!FightLoad.Fight.AllowsManualUnitControl)
            {
                sideIconsContainer.gameObject.SetActive(false);
            }

            EnsureStatusWidgetsHierarchy();
        }

        public SideUnitIcon GetSideIcon(Data_Center d)
        {
            return UnitIconDic[d];
        }

        void SetupRotateSelectedFrameBinding(ReactiveProperty<Data_Center> rModeUnit)
        {
            if (selectedFrame == null || rModeUnit == null)
                return;

            if (TeamConfig.myTeam != RTFightManager.playerTeam)
            {
                HideSelectedFrame();
                return;
            }

            rModeUnit.Subscribe(UpdateSelectedFrame).AddTo(gameObject);
        }

        void UpdateSelectedFrame(Data_Center center)
        {
            if (selectedFrame == null)
                return;

            if (center != null && UnitIconDic.TryGetValue(center, out var icon) && icon != null)
            {
                HeroIcon.SelectedFeature(icon.Icon.transform, selectedFrame.gameObject, 1f);
            }
            else
            {
                HideSelectedFrame();
            }
        }

        void HideSelectedFrame()
        {
            if (selectedFrame == null)
                return;

            selectedFrame.SetParent(transform);
            selectedFrame.gameObject.SetActive(false);
        }

        void EnsureStatusWidgetsHierarchy()
        {
            if (_targetCanvasT == null)
                return;

            var parent = _targetCanvasT.transform;
            var insertionIndex = 0;
            insertionIndex = MoveStatusWidgetToIndex(sideIconsContainer, parent, insertionIndex);

            foreach (var icon in UnitIconDic.Values)
            {
                if (icon == null)
                    continue;
                var rect = icon.transform as RectTransform;
                insertionIndex = MoveStatusWidgetToIndex(rect, parent, insertionIndex);
            }

            RectTransform comboRect = rotationModeHitCombo != null ? rotationModeHitCombo.rectTransform : null;
            MoveStatusWidgetToIndex(comboRect, parent, insertionIndex);
        }

        int MoveStatusWidgetToIndex(RectTransform target, Transform expectedParent, int desiredIndex)
        {
            if (target == null || expectedParent == null)
                return desiredIndex;

            if (target.parent != expectedParent)
                return desiredIndex;

            var siblingCount = expectedParent.childCount;
            desiredIndex = Mathf.Clamp(desiredIndex, 0, siblingCount - 1);
            if (target.GetSiblingIndex() != desiredIndex)
            {
                target.SetSiblingIndex(desiredIndex);
            }
            return desiredIndex + 1;
        }
    }
}
