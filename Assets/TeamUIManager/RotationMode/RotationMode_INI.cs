using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using DG.Tweening;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        Text rotationModeHitCombo;
        
        void IniTeamUI_Rotate(Action<Data_Center> ChangeUnit)
        {
            foreach (var center in teamMembers.GetValues())
            {
                var sideIcon = Instantiate(button_prefab);
                sideIcon.name = center.name + " ICon";
                sideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                sideIcon.focusingCharIcon.iconButton.onClick.AddListener(() => { ChangeUnit(center); });
                var info = RTFightManager.Target.UnitInfoRef[center];
                sideIcon.focusingCharIcon.ChangeIcon(info);
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
                DicAdd<Data_Center, SideCharIcon>.Add(UnitIconDic, center, sideIcon);
                
                RTFightManager.Target.RefreshTimeDic[center].Subscribe((x) =>
                {
                    UnitIconDic[center].focusingCharIcon.CooldownCurtainUpdate(x/10);
                }).AddTo(RTFightManager.Target.Disposables);
                
                var maxHp = center.FightDataRef.CurrentHp.Value;
                center.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(center, x, maxHp);
                }).AddTo(RTFightManager.Target.Disposables);
                
                center.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(center, x, FightGlobalSetting._EXMax);
                }).AddTo(RTFightManager.Target.Disposables);
                
                center.FightDataRef.Resistance.Subscribe(x =>
                {
                    RefreshResistanceBar(center, x);
                }).AddTo(RTFightManager.Target.Disposables);
                
                center.FightDataRef.IsDead.Subscribe(x => {
                    if (x)
                    {
                        UnitIconDic[center].focusingCharIcon.CooldownCurtainUpdate(1);
                    }
                }).AddTo(RTFightManager.Target.Disposables);
            }
        }
        
        void RotateClear()
        {
            UnitIconDic.Clear();
            rotationModeHitCombo.text = "";
        }
        
        void IniComboHit(ReactiveProperty<Data_Center> RMode_Unit)
        {
            RMode_Unit.Subscribe(x =>
            {
                if (rotationModeHitCombo != null)
                {
                    Destroy(rotationModeHitCombo.gameObject);
                }
                
                if (x != null)
                {
                    rotationModeHitCombo = Instantiate(hitCombo);
                    rotationModeHitCombo.name = teamConfig.myTeam + "HitCombo";
                    rotationModeHitCombo.color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                    rotationModeHitCombo.gameObject.SetActive(true);
                    if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvasT)
                    {
                        rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvasT.transform);
                    }
                    rotationModeHitCombo.transform.localScale = Vector3.one;
                    rotationModeHitCombo.fontSize = 30;
                    
                    x.FightDataRef._comboHitCount.HitCount.Subscribe(h =>
                    {
                        if (h > 1)
                        {
                            rotationModeHitCombo.text = h + "Hits!";
                            rotationModeHitCombo.transform.DOMove(CameraManager._camera.WorldToScreenPoint(x.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 1);
                        }
                        else
                        {
                            rotationModeHitCombo.text = null;
                            switch (teamConfig.myTeam)
                            {
                                case Team.player1:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 1);
                                    break;
                                case Team.player2:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 1);
                                    break;
                                default:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-100, -100), 1);
                                    break;
                            }
                        }
                    }).AddTo(rotationModeHitCombo.gameObject);
                }
            }).AddTo(gameObject);
        }
    }
}
