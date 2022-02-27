using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        void MultiClear()
        {
            UnitIconDic.Clear();
        }
        
        void InsTeamUI_Multi(MultiDict<int, int, Data_Center> TeamMembers)//这个环节应该能够同时把HP bar也适配好。
        {
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                //  SideCharIcon整备
                void Action1()
                {
                    RTFightManager.target.SwitchToCMode(a_char, RTFightManager.Auto);
                    RTFightManager.target.CameraAdjustment(teamConfig.myTeam);
                }
                
                SideCharIcon _SideIcon;
                if (!(UnitIconDic.ContainsKey(a_char) && UnitIconDic[a_char] != null))
                {
                    _SideIcon = Instantiate(button_prefab);
                    _SideIcon.name = a_char.name + " ICon";
                    _SideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    _SideIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
                    UnitInfo charDInfo = RTFightManager.target.UnitInfoRef[a_char];
                    UnitConfig unitConfig = Units.GetUnitConfig(charDInfo.r_id);
                    _SideIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Get(charDInfo.r_id), unitConfig._zokusei);
                    _SideIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideIcon = UnitIconDic[a_char];
                }
                _SideIcon.INIHPShow(a_char, a_char.FightDataRef.CurrentHp.Value);
                _SideIcon.focusingCharIcon.CooldownCurtainUpdate(0);
                
                if (teamConfig.myTeam == RTFightManager.playerTeam)
                {
                    _SideIcon.transform.SetParent(sideIconsContainer.transform);
                    _SideIcon.transform.localScale = Vector3.one;
                }
                else
                {
                    _SideIcon.transform.SetParent(_targetCanvasT.transform);
                    _SideIcon.transform.localScale = Vector3.one;
                }
                DicAdd<Data_Center, SideCharIcon>.Add(UnitIconDic, a_char, _SideIcon);
                
                // 魔法按键
                MobileInputsManager.target.ZokuseiButtonRegister(a_char.zokusei);
                
                var maxHp = a_char.FightDataRef.CurrentHp.Value;
                a_char.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(a_char, x, maxHp);
                }).AddTo(gameObject);
                
                a_char.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(a_char, x, 120);
                }).AddTo(gameObject);
                
                a_char._ResistanceManager.OpenResistRender();
                a_char._ResistanceManager.Resistance.Subscribe(x =>
                {
                    RefreshResistanceBar(a_char);
                }).AddTo(gameObject);
                
                a_char.IsDead.Subscribe(x => 
                {
                    if (x)
                    {
                        RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
                    }
                }).AddTo(gameObject);
            }
        }
    }
}