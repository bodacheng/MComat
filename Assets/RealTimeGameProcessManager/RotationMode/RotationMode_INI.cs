using UnityEngine;
using UniRx;

namespace FightScene
{
    public partial class FightTeam_RotationMode : FightTeam
    {
        protected override void TeamsFightInitialize(float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (Data_Center a_char in TeamMembers.values)
            {
                a_char.Step3Initialize(teamConfig, TeamHpRate * NineAndTwo.INI_Hp(CharDataInfoRef[a_char]._NineAndTwo.SkillEntityList()), teamCGMode);

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
                    if (x == true) 
                    {
                        Invoke("RandomChangeAliveFightingMember", 2f);
                        CharIconDic[a_char].focusingCharIcon.CooldownCurtainUpdate(1);
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
        
        protected override void InstantiateCharsIconsAndFloatHPBar()//这个环节应该能够同时把HP bar也适配好。
        {
            SideCharIcon _SideCharIcon;
            RefreshTimeDic.Clear();
            foreach (Data_Center a_char in TeamMembers.values)
            {
                //  时间刷新整备
                if (!RefreshTimeDic.ContainsKey(a_char))
                {
                    RefreshTimeDic.Add(a_char, 0);
                }
                //  SideCharIcon整备
                if (!(CharIconDic.ContainsKey(a_char) && CharIconDic[a_char] != null))
                {
                    _SideCharIcon = Instantiate(button_prefab);
                    _SideCharIcon.name = a_char.name + " ICon";
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    void action1()
                    {
                        ReadyForNextMember(a_char);
                    }
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                    CharDataInfo charDInfo = CharDataInfoRef[a_char];
                    CharConfig _charConfig = MonstersConfigTable.GetCharConfig(charDInfo.ResourceID);
                    _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Instance.GetMonsterIconSyn(charDInfo.ResourceID), _charConfig._zokusei);
                    _SideCharIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideCharIcon = CharIconDic[a_char];
                }
                
                _SideCharIcon.INIHPShow(a_char, a_char.FightDataRef.CurrentHp.Value);
                _SideCharIcon.focusingCharIcon.CooldownCurtainUpdate(0);
                
                if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
                {
                    _SideCharIcon.transform.SetParent(sideIconsContainer.transform);
                    _SideCharIcon.transform.localScale = Vector3.one;
                }
                else
                {
                    _SideCharIcon.transform.SetParent(_targetCanvas.transform);
                    _SideCharIcon.transform.localScale = Vector3.one;
                }
                DicAdd<Data_Center, SideCharIcon>.Add(CharIconDic, a_char, _SideCharIcon);

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
