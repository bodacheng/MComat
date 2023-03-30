using System;
using UnityEngine;
using UniRx;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        void MultiClear()
        {
            UnitIconDic.Clear();
        }
        
        void InsTeamUI_Multi(Action<bool> switchTeamAuto, Func<bool> currentAutoState)//这个环节应该能够同时把HP bar也适配好。
        {
            foreach (var center in teamMembers.GetValues())
            {
                // SideIcon整备
                void OnClickUnitIcon(Data_Center c)
                {
                    if (teamConfig.myTeam == RTFightManager.playerTeam)
                    {
                        if (_inputsManager.CurrentFocus.Value == c)
                        {
                            _inputsManager.FocusUnit(null);
                            RTFightManager.Target._CameraManager.SetCurrentCameraParams(null, null);
                        }
                        else
                        {
                            _inputsManager.FocusUnit(c);
                            RTFightManager.Target._CameraManager.SetCurrentCameraParams(c.WholeT, null);
                        }
                    }
                    switchTeamAuto(currentAutoState());
                }

                var sideIcon = Instantiate(button_prefab);
                sideIcon.name = center.UnitInfo.r_id + "_icon";
                sideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                sideIcon.focusingCharIcon.iconButton.onClick.AddListener(() =>
                {
                    OnClickUnitIcon(center);
                });
                var unitInfo = RTFightManager.Target.UnitInfoRef[center];
                sideIcon.focusingCharIcon.ChangeIcon(unitInfo);
                sideIcon.gameObject.SetActive(true);
                sideIcon.focusingCharIcon.CooldownCurtainUpdate(0);
                
                if (teamConfig.myTeam == RTFightManager.playerTeam)
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
                    RefreshExBar(center, x, FightGlobalSetting._EXMax);
                }).AddTo(gameObject);
                
                center.FightDataRef.Resistance.Subscribe(x =>
                {
                    RefreshResistanceBar(center, x);
                }).AddTo(gameObject);
            }

            _inputsManager.CurrentFocus.Subscribe(
                (x) =>
                {
                    if (x != null)
                    {
                        UnitIconDic.TryGetValue(x, out var targetIcon);
                        if (targetIcon != null)
                        {
                            selectedFrame.SetParent(targetIcon.transform);
                            selectedFrame.transform.localPosition = Vector3.zero;
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
            
            _inputsManager.FocusUnit(null);
        }
    }
}