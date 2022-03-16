using UnityEngine;
using UniRx;
using System;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        void IniTeamUI_Rotate(Action<Data_Center> ChangeUnit)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                var sideIcon = Instantiate(button_prefab);
                sideIcon.name = center.name + " ICon";
                sideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                sideIcon.focusingCharIcon.iconButton.onClick.AddListener(() => { ChangeUnit(center); });
                var info = RTFightManager.target.UnitInfoRef[center];
                var unitConfig = Units.GetUnitConfig(info.r_id);
                sideIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Get(info.r_id), unitConfig._zokusei);
                sideIcon.gameObject.SetActive(true);
                sideIcon.INIHPShow(center, center.FightDataRef.CurrentHp.Value);
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
                
                // 魔法按键
                MobileInputsManager.target.ZokuseiButtonRegister(center.zokusei);
                
                RTFightManager.RefreshTimeDic[center].Subscribe((x) =>
                {
                    UnitIconDic[center].focusingCharIcon.CooldownCurtainUpdate(x/10);
                }).AddTo(gameObject);
                
                var maxHp = center.FightDataRef.CurrentHp.Value;
                center.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(center, x, maxHp);
                }).AddTo(gameObject);
                
                center.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(center, x, 120);
                }).AddTo(gameObject);
                
                center.IsDead.Subscribe(x => {
                    if (x)
                    {
                        UnitIconDic[center].focusingCharIcon.CooldownCurtainUpdate(1);
                    }
                }).AddTo(gameObject);
                
                center._ResistanceManager.Resistance.Subscribe(x =>
                {
                    RefreshResistanceBar(center);
                }).AddTo(gameObject);
            }
        }
    }
}
