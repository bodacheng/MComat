using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        [SerializeField] Text liveUnitCount;
        public Text LiveUnitCount => liveUnitCount;
        
        void SetLiveUnitCount()
        {
            int liveCount = 0;
            foreach (var dc in _teamMembers.GetValues())
            {
                if (!dc.FightDataRef.IsDead.Value)
                {
                    liveCount++;
                }
            }
            liveUnitCount.text = (TeamConfig.myTeam == RTFightManager.playerTeam ? "Player:":"Enemy:") +
                liveCount +  "/" + _teamMembers.GetValues().Count;
        }
        
        void MultiClear()
        {
            UnitIconDic.Clear();
        }
        
        void InsTeamUI_Multi(Action<bool> switchTeamAuto, Func<bool> currentAutoState)//这个环节应该能够同时把HP bar也适配好。
        {
            foreach (var center in _teamMembers.GetValues())
            {
                // SideIcon整备
                void OnClickUnitIcon(Data_Center c)
                {
                    if (TeamConfig.myTeam == RTFightManager.playerTeam)
                    {
                        if (inputsManager.CurrentFocus.Value == c)
                        {
                            inputsManager.FocusUnit(null);
                            RTFightManager.Target._CameraManager.SetCurrentCameraParams(null, null);
                        }
                        else
                        {
                            inputsManager.FocusUnit(c);
                            RTFightManager.Target._CameraManager.SetCurrentCameraParams(c.geometryCenter, null);
                        }
                    }
                    switchTeamAuto(currentAutoState());
                }

                var sideIcon = Instantiate(unitIconPrefab);
                sideIcon.name = center.UnitInfo.r_id + "_icon";
                sideIcon.Icon.iconButton.onClick.RemoveAllListeners();
                sideIcon.Icon.iconButton.onClick.AddListener(() =>
                {
                    OnClickUnitIcon(center);
                });
                var unitInfo = RTFightManager.Target.UnitInfoRef[center];
                sideIcon.Icon.ChangeIcon(unitInfo);
                sideIcon.gameObject.SetActive(true);
                sideIcon.Icon.CooldownCurtainUpdate(0);
                
                if (TeamConfig.myTeam == RTFightManager.playerTeam)
                {
                    sideIcon.transform.SetParent(sideIconsContainer.transform);
                    sideIcon.transform.localScale = Vector3.one;
                }
                else
                {
                    sideIcon.transform.SetParent(_targetCanvasT.transform);
                    sideIcon.transform.localScale = Vector3.one;
                }
                DicAdd<Data_Center, SideUnitIcon>.Add(UnitIconDic, center, sideIcon);
                
                var maxHp = center.FightDataRef.CurrentHp.Value;
                center.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(center, x, maxHp);
                }).AddTo(gameObject);
                
                center.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(center, x);
                }).AddTo(gameObject);
                
                center.FightDataRef.Resistance.Subscribe(x =>
                    {
                        RefreshResistanceBar(center, x);
                    }
                ).AddTo(gameObject);
                
                center.FightDataRef.IsDead.Subscribe(x =>
                    {
                        if (x)
                        {
                            center.FightDataRef.Resistance.Value = 0;
                            center.FightDataRef.CriticalGauge.Value = 0;
                            sideIcon.GreyOut();
                            SetLiveUnitCount();
                        }
                    }
                ).AddTo(sideIcon.gameObject);
            }

            inputsManager.CurrentFocus.Subscribe(
                (x) =>
                {
                    if (x != null)
                    {
                        UnitIconDic.TryGetValue(x, out var targetIcon);
                        if (targetIcon != null)
                        {
                            selectedFrame.SetParent(targetIcon.transform);
                            selectedFrame.transform.localPosition = new Vector3(0,4.5f,0);
                            selectedFrame.transform.localScale = Vector3.one;
                            selectedFrame.gameObject.SetActive(true);
                            selectedFrame.SetAsFirstSibling();
                        }
                    }
                    else
                    {
                        selectedFrame.SetParent(transform);
                        selectedFrame.gameObject.SetActive(false);
                    }
                }
            ).AddTo(this.gameObject);
            
            inputsManager.FocusUnit(null);
            SetLiveUnitCount();
        }
    }
}