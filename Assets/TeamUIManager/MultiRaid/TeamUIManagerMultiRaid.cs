using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        IDictionary<Data_Center, Text> multiRaidHitComboDic = new Dictionary<Data_Center, Text>();
        
        void ToStartPos_Multi(MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (KeyValuePair<(int, int), Data_Center> kv in TeamMembers.mDict)
            {
                Data_Center _DataCenter = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (_DataCenter == null)
                {
                    continue;
                }
                if (TeamStandPoints[kv.Key.Item2] != null)
                {
                    _DataCenter.WholeT.transform.position = TeamStandPoints[kv.Key.Item2].position;
                    _DataCenter.WholeT.transform.rotation = TeamStandPoints[kv.Key.Item2].rotation;
                    _DataCenter.WholeT.parent = null;
                    _DataCenter.WholeT.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("站位逻辑错误。出现了系统未安排的站位点");
                }
            }
        }

        public void MultiClear()
        {
            UnitIconDic.Clear();
            multiRaidHitComboDic.Clear();
        }
        
        void MultiRaid_LocalUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            if (teamConfig.myTeam != RTFightManager.playerTeam)
            {
                BarsPosUpdate(TeamMembers);
            }
        }

        void Initialize_Multi(MultiDict<int, int, Data_Center> TeamMembers, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (Data_Center dc in TeamMembers.GetValues())
            {
                dc.Step3Initialize(teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.UnitInfoRef[dc].set.SkillEntityList()), teamCGMode);
                
                float maxHp = dc.FightDataRef.CurrentHp.Value;
                dc.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(dc, x, maxHp);
                });
                
                dc.FightDataRef.CriticalGauge = new ReactiveProperty<int>();
                dc.FightDataRef.CriticalGauge.Subscribe(x =>
                {
                    RefreshExBar(dc, x, 120);
                });
                
                dc._ResistanceManager.Resistance = new ReactiveProperty<int>
                {
                    Value = 0
                };
                dc._ResistanceManager.OpenResistRender();
                dc._ResistanceManager.Resistance.Subscribe(x =>
                {
                    dc._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                    RefreshResistanceBar(dc);
                });
                
                dc.FightDataRef._ComboHitCount.HitCount.Value = 0;
                dc.FightDataRef._ComboHitCount.HitCount.Subscribe(x =>
                {
                    RefreshComboHitMultiRaid(dc);
                });
                
                dc.IsDead = new ReactiveProperty<bool>(false);
                dc.IsDead.Subscribe(x => 
                {
                    if (x)
                    {
                        RTFightManager.AddOrRemoveFightingMember(dc, this.teamConfig.myTeam, false);
                        RTFightManager.target.ParaAdjustment(RTFightManager.playerTeam);
                    }
                });
            }
        }
        
        void RefreshComboHitMultiRaid(Data_Center _datacenter)
        {
            Text comboText = multiRaidHitComboDic[_datacenter];
            if (_datacenter.FightDataRef._ComboHitCount.HitCount.Value > 1)
            {
                comboText.text = _datacenter.FightDataRef._ComboHitCount.HitCount.Value.ToString() + "Hits!";
                comboText.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 0.2f);
            }
            else
            {
                switch (teamConfig.myTeam)
                {
                    case Team.player1:
                        comboText.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 0.2f);
                        break;
                    case Team.player2:
                        comboText.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 0.2f);
                        break;
                    default:
                        comboText.rectTransform.DOAnchorPos(new Vector2(-100, -100), 0.2f);
                        break;
                }
            }
        }

        void InsTeamUI_Multi(MultiDict<int, int, Data_Center> TeamMembers)//这个环节应该能够同时把HP bar也适配好。
        {
            SideCharIcon _SideCharIcon;
            Text hitCombo;
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                //  SideCharIcon整备
                void Action1()
                {
                    RTFightManager.target.SwitchToCMode(a_char, RTFightManager.Auto);
                    RTFightManager.target.ParaAdjustment(teamConfig.myTeam);
                }
                
                if (!(UnitIconDic.ContainsKey(a_char) && UnitIconDic[a_char] != null))
                {
                    _SideCharIcon = Instantiate(button_prefab);
                    _SideCharIcon.name = a_char.name + " ICon";
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
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
                if (multiRaidHitComboDic.ContainsKey(a_char) && multiRaidHitComboDic[a_char] != null)
                {
                    hitCombo = multiRaidHitComboDic[a_char];
                }
                else
                {
                    hitCombo = Instantiate(HitCombo);
                    hitCombo.name = a_char.WholeT.name + "HitCombo";
                }
                DicAdd<Data_Center, Text>.Add(multiRaidHitComboDic, a_char, hitCombo);

                // 魔法按键
                MobileInputsManager.target.ZokuseiButtonRegister(a_char.Zokusei);
            }
        }
    }
}