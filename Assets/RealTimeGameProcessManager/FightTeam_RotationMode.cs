using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace FightScene
{
    public class FightTeam_RotationMode : FightTeam
    {
        Data_Center RotationMode_fightingMember;
        Data_Center waitingToChangeMember; // 不能任何时候点击切换角色按钮都切换，那样就乱了。
        IDictionary<Data_Center, float> RefreshTimeDic = new Dictionary<Data_Center, float>();
        Text rotationModeHitCombo;
        
        public override List<Transform> TeamMemberTransforms()
        {
            List<Transform> transforms = new List<Transform>
            {
                RotationMode_fightingMember.transform
            };
            return transforms;
        }

        public override void Refresh()
        {
            base.Refresh();
            rotationModeHitCombo.color = teamConfig.myTeam == RealTimeGameProcessManager.playerTeam ? Color.yellow : Color.blue;
            rotationModeHitCombo.gameObject.SetActive(true);
            if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvas)
            {
                rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvas.transform);
            }
            rotationModeHitCombo.transform.localScale = Vector3.one;
            rotationModeHitCombo.fontSize = 30;
        }

        public override void Clear()
        {
            CharIconDic.Clear();
            rotationModeHitCombo.text = "";
        }

        public override void ArrangeAllTeamMembersToPosition(MultiDictionary<int, int, Data_Center> heromultiDictionary)
        {
            foreach (KeyValuePair<int, List<int>> keys in heromultiDictionary.GetAllUnNullKeys())
            {
                foreach (int key in keys.Value)
                {
                    Data_Center character_data_Center = heromultiDictionary.Get(keys.Key, key);
                    if (character_data_Center == null)
                    {
                        continue;
                    }
                    character_data_Center.WholeT.parent = null;
                    character_data_Center.WholeT.gameObject.SetActive(true);
                }
            }
            ChangeFightingMember_ReadyToGo(heromultiDictionary.values[0], TeamStandPoints[0]);
        }

        void RefreshComboHitRotationMode(Data_Center _datacenter)
        {
            if (_datacenter.FightDataRef._ComboHitCount.HitCount.Value > 1)
            {
                rotationModeHitCombo.text = _datacenter.FightDataRef._ComboHitCount.HitCount.Value.ToString() + "Hits!";
                rotationModeHitCombo.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 0.2f);
            }
            else
            {
                switch (teamConfig.myTeam)
                {
                    case Team.player1:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 0.2f);
                        break;
                    case Team.player2:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 0.2f);
                        break;
                    default:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-100, -100), 0.2f);
                        break;
                }
            }
        }

        public override void LocalFightingUpdate()
        {
            WaitToTriggerMemberChange();
            if (RotationMode_fightingMember != null)
            {
                RefreshComboHitRotationMode(RotationMode_fightingMember);
            }
            if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
            {
                BarsPositionUpdate();
            }
        }

        protected override void TeamsFightInitialize(float extraHP)
        {
            foreach (Data_Center a_char in TeamMembers.values)
            {
                a_char.FightDataRef.CurrentHp = new ReactiveProperty<float>();
                a_char.Step3Initialize(teamConfig, NineAndTwo.INI_Hp(CharDataInfoRef[a_char]._NineAndTwo.SkillEntityList()));
                a_char.FightDataRef.CurrentHp.Value += extraHP;
                float maxHp = a_char.FightDataRef.CurrentHp.Value;
                a_char.FightDataRef.CurrentHp.Subscribe(x =>
                {
                    RefreshHPBar(a_char, x, maxHp);
                });

                a_char._ResistanceManager.Resistance = new ReactiveProperty<int>();
                a_char._ResistanceManager.Resistance.Value = 0;
                a_char._ResistanceManager.Resistance.Subscribe(x =>
                {
                    a_char._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                    RefreshResistanceBar(a_char);
                });
            }
        }

        public override void ModeStart()
        {
            RotationMode_fightingMember._MyBehaviorRunner.ChangeToWaitingState();
        }

        void WaitToTriggerMemberChange()
        {
            for (int i = 0; i < TeamMembers.values.Count; i++)
            {
                if (RefreshTimeDic[TeamMembers.values[i]] > 0)
                {
                    RefreshTimeDic[TeamMembers.values[i]] -= Time.deltaTime; // 角色切换倒计时;
                    CharIconDic[TeamMembers.values[i]].focusingCharIcon.CooldownCurtainUpdate(RefreshTimeDic[TeamMembers.values[i]] / 10);
                }
            }

            if (waitingToChangeMember != null && CanChangeToThisMember(waitingToChangeMember))
            {
                RefreshTimeDic[RotationMode_fightingMember] = 10f;
                ChangeFightingMember(waitingToChangeMember);
                waitingToChangeMember = null;
            }
        }

        bool CanChangeToThisMember(Data_Center targetMember)
        {
            if (targetMember.IsDead.Value)
            {
                return false;
            }
            if (RefreshTimeDic[targetMember] > 0)
            {
                return false;
            }
            if (targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.KnockOff)
            {
                return false;
            }
            if (targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GI || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GM || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GR)
            {
                if (!targetMember._SkillCancelFlag.Cancel_Flag)
                    return true;
            }
            return true;
        }

        void ReadyForNextMemberOnTheShow(Data_Center nextOne)
        {
            waitingToChangeMember = waitingToChangeMember != nextOne ? nextOne : null;
            if (waitingToChangeMember == null)
            {
                HeroIcon.Seletedfeature(null, selectedFrame, 100f);
            }
            else
            {
                CharIconDic.TryGetValue(waitingToChangeMember, out _tempSI);
                HeroIcon.Seletedfeature(_tempSI?.focusingCharIcon, selectedFrame, 100f);
            }
        }

        protected override void InstantiateCharsIconsAndFloatHPBar()//这个环节应该能够同时把HP bar也适配好。
        {
            SideCharIcon _SideCharIcon;
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
                        ReadyForNextMemberOnTheShow(a_char);
                    }
                    _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                    CharDataInfo characterDataInfo = CharDataInfoRef[a_char];
                    CharConfig _charConfig = MonstersConfigTable.GetCharConfig(characterDataInfo.ResourceID);
                    _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Instance.GetMonsterIconSyn(characterDataInfo.ResourceID), _charConfig._zokusei);
                    _SideCharIcon.gameObject.SetActive(true);
                }
                else
                {
                    _SideCharIcon = CharIconDic[a_char];
                }
                _SideCharIcon.IniHPShow(a_char, a_char.FightDataRef.CurrentHp.Value);
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
                _mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
            }

        }

        /// <summary>
        /// 本质上这个函数是AI。。。而AI按理说应该和其他东西是分层的。。
        /// </summary>
        float time_counter;
        public void TurnModeEnemySideAutoMemberShaft()
        {
            time_counter += Time.deltaTime;
            if (RotationMode_fightingMember != null && RotationMode_fightingMember.IsDead.Value)
            {
                if (TeamMembers.values.Count > 0)
                {
                    for (int i = 0; i < TeamMembers.values.Count; i++)
                    {
                        ReadyForNextMemberOnTheShow(TeamMembers.values[i]);
                    }
                }
            }
            if (time_counter > 6f)
            {
                if (TeamMembers.values.Count > 0)
                {
                    for (int i = 0; i < TeamMembers.values.Count; i++)
                    {
                        if (RefreshTimeDic[TeamMembers.values[i]] <= 0)
                        {
                            ReadyForNextMemberOnTheShow(TeamMembers.values[i]);
                        }
                        time_counter = 0f;
                    }
                }
                time_counter = 0f;
            }
        }

        public bool ChangeFightingMember(Data_Center _changeTo)
        {
            if (!(TeamMembers.values.Count > 1) || RotationMode_fightingMember == _changeTo)
            {
                return false;
            }
            if (_changeTo.IsDead.Value)
            {
                return false;
            }
            bool memberchanged = false;
            Vector3 targetposition = Vector3.zero;
            if (RotationMode_fightingMember != null)
            {
                targetposition = RotationMode_fightingMember.transform.position;
            }
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (_changeTo == data_Center)
                {
                    if (RotationMode_fightingMember != null && _changeTo != null)//继承hit数
                    {
                        _changeTo.FightDataRef._ComboHitCount.HitCount.Value = RotationMode_fightingMember.FightDataRef._ComboHitCount.HitCount.Value;
                    }
                    RotationMode_fightingMember = _changeTo;
                    RotationMode_fightingMember.IsDead.Subscribe(x => { if (x == true) { Invoke("RandomChangeAliveFightingMember", 2f); } });
                    RotationMode_fightingMember._MyBehaviorRunner.ChangeToWaitingState();
                    RotationMode_fightingMember.WholeT.transform.position = targetposition;
                    EffectsManager.GenerateEffect("membershift", null, RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, RotationMode_fightingMember.geometryCenter);
                    memberchanged = true;
                }
                else
                {
                    data_Center._MyBehaviorRunner.ChangeState("Empty");
                    data_Center.WholeT.transform.position = new Vector3(9999, -200, 9999);
                }
            }
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                RealTimeGameProcessManager.target.SwitchToCMode(RotationMode_fightingMember, MobileInputsManager.playerMode);
            }
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            RealTimeGameProcessManager.target.Refresh();
            return memberchanged;
        }

        public bool RandomChangeAliveFightingMember()
        {
            if (waitingToChangeMember != null && waitingToChangeMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingToChangeMember.IsDead.Value)
                {
                    if (ChangeFightingMember(waitingToChangeMember))
                    {
                        return true;
                    }
                }
            }
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (!data_Center.IsDead.Value)
                {
                    if (ChangeFightingMember(data_Center))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ChangeFightingMember_ReadyToGo(Data_Center _changeTo, Transform IniStandPoint)
        {
            bool memberchanged = false;
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (_changeTo == data_Center)
                {
                    RotationMode_fightingMember = _changeTo;
                    RotationMode_fightingMember.IsDead.Subscribe(x => { if (x == true) { Invoke("RandomChangeAliveFightingMember", 2f); } });
                    RotationMode_fightingMember.WholeT.transform.position = IniStandPoint.position;
                    RotationMode_fightingMember.WholeT.rotation = IniStandPoint.rotation;
                    EffectsManager.GenerateEffect("membershift", null, RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, RotationMode_fightingMember.geometryCenter);
                    memberchanged = true;
                }
                else
                {
                    data_Center._MyBehaviorRunner.ChangeState("Empty");
                    data_Center.WholeT.transform.position = new Vector3(9999, -200, 9999);
                }
            }
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            RealTimeGameProcessManager.target.Refresh();
            return memberchanged;
        }
    }
}