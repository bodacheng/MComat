using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        protected void TeamsIni_Rotate(MultiDict<int, int, Data_Center> TeamMembers, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                a_char.Step3Initialize(teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.UnitInfoRef[a_char].set.SkillEntityList()), teamCGMode);

                float maxHp = a_char.FightDataRef.CurrentHp.Value;
                a_char.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(a_char, x, maxHp);
                });
                
                a_char.FightDataRef.CriticalGauge = new ReactiveProperty<int>();
                a_char.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(a_char, x, 120);
                });
                
                a_char.IsDead = new ReactiveProperty<bool>(false);
                a_char.IsDead.Subscribe(x => {
                    if (x) 
                    {
                        UnitIconDic[a_char].focusingCharIcon.CooldownCurtainUpdate(1);
                        RTFightManager.AddOrRemoveFightingMember(a_char, teamConfig.myTeam, false);
                        
                        MultiDict<int, int, Data_Center> tteam = teamConfig.myTeam == Team.player1
                            ? RTFightManager.target.Team1Members
                            : RTFightManager.target.Team2Members;
                        ToNewUnit(tteam);
                    }
                });
                
                a_char._ResistanceManager.Resistance = new ReactiveProperty<int>
                {
                    Value = 0
                };
                a_char._ResistanceManager.OpenResistRender();
                a_char._ResistanceManager.Resistance.Subscribe(x =>
                {
                    a_char._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                    RefreshResistanceBar(a_char);
                });
            }
        }

        async void ToNewUnit(MultiDict<int, int, Data_Center> tteam)
        {
            await UniTask.DelayFrame(100);
            RandomToAliveUnit(tteam);
        }
        
        protected void InsTeamUI_Rotate(MultiDict<int, int, Data_Center> TeamMembers)//这个环节应该能够同时把HP bar也适配好。
        {
            SideCharIcon _SideCharIcon;
            RefreshTimeDic.Clear();
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                //  时间刷新整备
                if (!RefreshTimeDic.ContainsKey(a_char))
                {
                    RefreshTimeDic.Add(a_char, 0);
                }
                //  SideCharIcon整备
                if (!(UnitIconDic.ContainsKey(a_char) && UnitIconDic[a_char] != null))
                {
                    _SideCharIcon = Instantiate(button_prefab);
                    _SideCharIcon.name = a_char.name + " ICon";
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    void action1()
                    {
                        ReadyForNextMember(a_char);
                    }
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                    UnitInfo charDInfo = RTFightManager.target.UnitInfoRef[a_char];
                    CharConfig _charConfig = MonstersConfigTable.GetCharConfig(charDInfo.r_id);
                    _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Get(charDInfo.r_id), _charConfig._zokusei);
                    _SideCharIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideCharIcon = UnitIconDic[a_char];
                }
                
                _SideCharIcon.INIHPShow(a_char, a_char.FightDataRef.CurrentHp.Value);
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
                DicAdd<Data_Center, SideCharIcon>.Add(UnitIconDic, a_char, _SideCharIcon);
                
                // hitCombo整备
                if (rotationModeHitCombo == null)
                {
                    rotationModeHitCombo = Instantiate(HitCombo);
                    rotationModeHitCombo.name = teamConfig.myTeam + "HitCombo";
                }
                
                // 魔法按键
                MobileInputsManager.target.ZokuseiButtonRegister(a_char.Zokusei);
            }
        }
    }
}
