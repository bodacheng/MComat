using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        void TeamsIni_Rotate(MultiDict<int, int, Data_Center> TeamMembers, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                center.Step3Initialize(teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.UnitInfoRef[center].set.SkillEntityList()), teamCGMode);
                
                var maxHp = center.FightDataRef.CurrentHp.Value;
                center.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(center, x, maxHp);
                });
                
                center.FightDataRef.CriticalGauge = new ReactiveProperty<int>();
                center.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(center, x, 120);
                });
                
                center.IsDead = new ReactiveProperty<bool>(false);
                center.IsDead.Subscribe(x => {
                    if (x) 
                    {
                        UnitIconDic[center].focusingCharIcon.CooldownCurtainUpdate(1);
                        RTFightManager.AddOrRemoveFightingMember(center, teamConfig.myTeam, false);
                        
                        var team = teamConfig.myTeam == Team.player1 ? RTFightManager.target.Team1Members : RTFightManager.target.Team2Members;
                        ToNewUnit(team);
                    }
                });
                
                center._ResistanceManager.Resistance = new ReactiveProperty<int>
                {
                    Value = 0
                };
                center._ResistanceManager.OpenResistRender();
                center._ResistanceManager.Resistance.Subscribe(x =>
                {
                    center._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                    RefreshResistanceBar(center);
                });
            }
        }

        async void ToNewUnit(MultiDict<int, int, Data_Center> team)
        {
            await UniTask.DelayFrame(100);
            RandomToAliveUnit(team);
        }
        
        void InsTeamUI_Rotate(MultiDict<int, int, Data_Center> TeamMembers)//这个环节应该能够同时把HP bar也适配好。
        {
            RefreshTimeDic.Clear();
            foreach (var center in TeamMembers.GetValues())
            {
                //  时间刷新整备
                if (!RefreshTimeDic.ContainsKey(center))
                {
                    RefreshTimeDic.Add(center, 0);
                }
                //  SideCharIcon整备
                SideCharIcon _SideCharIcon;
                if (!(UnitIconDic.ContainsKey(center) && UnitIconDic[center] != null))
                {
                    _SideCharIcon = Instantiate(button_prefab);
                    _SideCharIcon.name = center.name + " ICon";
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    void action1()
                    {
                        ReadyForNextMember(center);
                    }
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                    UnitInfo charDInfo = RTFightManager.target.UnitInfoRef[center];
                    UnitConfig unitConfig = Units.GetUnitConfig(charDInfo.r_id);
                    _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Get(charDInfo.r_id), unitConfig._zokusei);
                    _SideCharIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideCharIcon = UnitIconDic[center];
                }
                
                _SideCharIcon.INIHPShow(center, center.FightDataRef.CurrentHp.Value);
                _SideCharIcon.focusingCharIcon.CooldownCurtainUpdate(0);
                
                if (teamConfig.myTeam == RTFightManager.playerTeam)
                {
                    _SideCharIcon.transform.SetParent(sideIconsContainer.transform);
                    _SideCharIcon.transform.localScale = Vector3.one;
                }
                else
                {
                    _SideCharIcon.transform.SetParent(_targetCanvasT.transform);
                    _SideCharIcon.transform.localScale = Vector3.one;
                }
                DicAdd<Data_Center, SideCharIcon>.Add(UnitIconDic, center, _SideCharIcon);
                
                // hitCombo整备
                if (rotationModeHitCombo == null)
                {
                    rotationModeHitCombo = Instantiate(HitCombo);
                    rotationModeHitCombo.name = teamConfig.myTeam + "HitCombo";
                }
                
                // 魔法按键
                MobileInputsManager.target.ZokuseiButtonRegister(center.zokusei);
            }
        }
    }
}
