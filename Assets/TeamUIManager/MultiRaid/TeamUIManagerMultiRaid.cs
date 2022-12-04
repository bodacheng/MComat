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
            foreach (var center in TeamMembers.GetValues())
            {
                // SideCharIcon整备
                void Action1(Data_Center c)
                {
                    if (teamConfig.myTeam == RTFightManager.playerTeam)
                    {
                        _inputsManager.FocusUnit(c);
                    }
                }
                
                SideCharIcon _SideIcon;
                if (!(UnitIconDic.ContainsKey(center) && UnitIconDic[center] != null))
                {
                    _SideIcon = Instantiate(button_prefab);
                    _SideIcon.name = center.name + " ICon";
                    _SideIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    _SideIcon.focusingCharIcon.iconButton.onClick.AddListener(() =>
                    {
                        Action1(center);
                    });
                    var unitInfo = RTFightManager.Target.UnitInfoRef[center];
                    _SideIcon.focusingCharIcon.ChangeIcon(unitInfo);
                    _SideIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideIcon = UnitIconDic[center];
                }
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
                DicAdd<Data_Center, SideCharIcon>.Add(UnitIconDic, center, _SideIcon);
                
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