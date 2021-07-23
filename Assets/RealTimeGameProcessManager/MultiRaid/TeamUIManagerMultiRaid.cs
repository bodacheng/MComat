using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace FightScene
{
    public class TeamUIManagerMultiRaid : TeamUIManager
    {
        IDictionary<Data_Center, Text> multiRaidHitComboDic = new Dictionary<Data_Center, Text>();
        
        public override void Refresh()
        {
            base.Refresh();
            foreach (Data_Center _datacenter in TeamMembers.GetValues())
            {
                if (multiRaidHitComboDic.ContainsKey(_datacenter))
                {
                    multiRaidHitComboDic[_datacenter].color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                    multiRaidHitComboDic[_datacenter].gameObject.SetActive(true);
                    if (multiRaidHitComboDic[_datacenter].gameObject.transform.parent != _targetCanvas)
                    {
                        multiRaidHitComboDic[_datacenter].gameObject.transform.SetParent(_targetCanvas.transform);
                    }
                    multiRaidHitComboDic[_datacenter].transform.localScale = Vector3.one;
                    multiRaidHitComboDic[_datacenter].fontSize = 30;
                }
            }
        }

        public override void ToStartPos(MultiDict<int, int, Data_Center> heromultiDictionary)
        {
            foreach (KeyValuePair<(int, int), Data_Center> kv in heromultiDictionary.mDict)
            {
                Data_Center _DataCenter = heromultiDictionary.Get(kv.Key.Item1, kv.Key.Item2);
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

        public override void Clear()
        {
            foreach (Data_Center one in TeamMembers.GetValues())
            {
                one.FightDataRef.Clear();
            }
            CharIconDic.Clear();
            multiRaidHitComboDic.Clear();
        }
        
        public void MultiRaid_LocalFightingUpdate()
        {
            if (teamConfig.myTeam != RTFightManager.playerTeam)
            {
                BarsPosUpdate();
            }
        }

        protected override void TeamsFightInitialize(float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                a_char.Step3Initialize
                    (teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.CharDataInfoRef[a_char].set.SkillEntityList()), teamCGMode);

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

                a_char.FightDataRef._ComboHitCount.HitCount.Value = 0;
                a_char.FightDataRef._ComboHitCount.HitCount.Subscribe(x =>
                {
                    RefreshComboHitMultiRaid(a_char);
                });

                a_char.IsDead = new ReactiveProperty<bool>(false);
                a_char.IsDead.Subscribe(x => 
                {
                    if (x == true)
                    {
                        RTFightManager.AddOrRemoveFightingMember(a_char, this.teamConfig.myTeam, false);
                        RTFightManager.target.CameraParaAdjustment(RTFightManager.playerTeam);
                    }
                });
            }

            localFightingUpdate = MultiRaid_LocalFightingUpdate;
        }

        Text _hitcomboText;
        void RefreshComboHitMultiRaid(Data_Center _datacenter)
        {
            _hitcomboText = multiRaidHitComboDic[_datacenter];
            if (_datacenter.FightDataRef._ComboHitCount.HitCount.Value > 1)
            {
                _hitcomboText.text = _datacenter.FightDataRef._ComboHitCount.HitCount.Value.ToString() + "Hits!";
                _hitcomboText.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 0.2f);
            }
            else
            {
                switch (teamConfig.myTeam)
                {
                    case Team.player1:
                        _hitcomboText.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 0.2f);
                        break;
                    case Team.player2:
                        _hitcomboText.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 0.2f);
                        break;
                    default:
                        _hitcomboText.rectTransform.DOAnchorPos(new Vector2(-100, -100), 0.2f);
                        break;
                }
            }
        }

        protected override void InsTeamUI()//这个环节应该能够同时把HP bar也适配好。
        {
            SideCharIcon _SideCharIcon;
            Text hitCombo;
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                //  SideCharIcon整备
                void Action1()
                {
                    RTFightManager.target.SwitchToCMode(a_char, RTFightManager.Auto);
                    RTFightManager.target.CameraParaAdjustment(teamConfig.myTeam);
                }
                
                if (!(CharIconDic.ContainsKey(a_char) && CharIconDic[a_char] != null))
                {
                    _SideCharIcon = Instantiate(button_prefab);
                    _SideCharIcon.name = a_char.name + " ICon";
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
                    CharDataInfo charDInfo = RTFightManager.target.CharDataInfoRef[a_char];
                    CharConfig _charConfig = MonstersConfigTable.GetCharConfig(charDInfo.r_id);
                    _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Get(charDInfo.r_id), _charConfig._zokusei);
                    _SideCharIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideCharIcon = CharIconDic[a_char];
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
                    _SideCharIcon.transform.SetParent(_targetCanvas.transform);
                    _SideCharIcon.transform.localScale = Vector3.one;
                }
                DicAdd<Data_Center, SideCharIcon>.Add(CharIconDic, a_char, _SideCharIcon);

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

        public override void ModeStart()
        {
            AllUnitsStartOff();
        }

        public override List<Transform> TeamMemberTransforms()
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                if (a_char._MyBehaviorRunner.GetNowState().StateKey != "Death")
                {
                    transforms.Add(a_char.WholeT.transform);
                }
            }
            return transforms;
        }
    }
}