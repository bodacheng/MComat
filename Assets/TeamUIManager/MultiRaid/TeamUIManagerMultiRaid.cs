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
        
        void InsTeamUI_Multi()//这个环节应该能够同时把HP bar也适配好。
        {
            foreach (var center in teamMembers.GetValues())
            {
                // SideIcon整备
                void Action1(Data_Center c)
                {
                    if (teamConfig.myTeam == RTFightManager.playerTeam)
                    {
                        _inputsManager.FocusUnit(c);
                    }
                }
                
                SideUnitIcon sideIcon;
                if (!(UnitIconDic.ContainsKey(center) && UnitIconDic[center] != null))
                {
                    sideIcon = Instantiate(button_prefab);
                    sideIcon.name = center.name + " ICon";
                    sideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    sideIcon.focusingCharIcon.iconButton.onClick.AddListener(() =>
                    {
                        Action1(center);
                    });
                    var unitInfo = RTFightManager.Target.UnitInfoRef[center];
                    sideIcon.focusingCharIcon.ChangeIcon(unitInfo);
                    sideIcon.gameObject.SetActive(true);
                }
                else
                {
                    sideIcon = UnitIconDic[center];
                }
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
        }
    }
}