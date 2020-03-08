using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public partial class FightTeam : MonoBehaviour
{
    Data_Center RotationMode_fightingMember;
    Data_Center waitingToChangeMember; // 不能任何时候点击切换角色按钮都切换，那样就乱了。
    IDictionary<Data_Center, float> RefreshTimeDic = new Dictionary<Data_Center, float>();
    Text rotationModeHitCombo;
    
    void RefreshComboHitRotationMode(Data_Center _datacenter,Color comboTextColor)
    {
        if (_datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value > 1)
        {
            rotationModeHitCombo.text = _datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value.ToString() + "Hits!";
            rotationModeHitCombo.color = comboTextColor;
            rotationModeHitCombo.transform.localScale = Vector3.one;
            rotationModeHitCombo.fontSize = 30;
            rotationModeHitCombo.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f),0.2f);
        }
        else
        {
            rotationModeHitCombo.color = Color.clear;
        }
    }
    
    void TeamsFightRotationModeInitialize(float wholeHP,Color comboTextColor)
    {
        foreach (Data_Center a_char in teamMembers.values)
        {
            a_char._FightAttriCalReference.CurrentHp.Value = wholeHP;
            a_char._FightAttriCalReference.CurrentHp.Subscribe(x => 
            {
                RefreshHPBar(a_char, x, wholeHP);
            });
            a_char._ResistanceManager.Resistance.Value = 0;
            a_char._ResistanceManager.Resistance.Subscribe(x => 
            {
                a_char._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10); 
                RefreshResistanceBar(a_char); 
            });            
        }
        RotationMode_fightingMember._FightAttriCalReference._ComboHitCount.HitCount.Subscribe(x => 
        {
            RefreshComboHitRotationMode(RotationMode_fightingMember,comboTextColor);
        });
    }

    public void Rotation_mode_start()
    {
        ChangeFightingMember(teamMembers.values[0]);
    }
    
    void WaitToTriggerMemberChange()
    {
        for (int i = 0; i < teamMembers.values.Count; i++)
        {
            if (RefreshTimeDic[teamMembers.values[i]] > 0)
            {
                RefreshTimeDic[teamMembers.values[i]] -= Time.deltaTime; // 角色切换倒计时;
                datacenterCharIconDic[teamMembers.values[i]].focusingCharIcon.CooldownCurtainUpdate(RefreshTimeDic[teamMembers.values[i]]/20);
            }
        }
        
        if (waitingToChangeMember != null && CanChangeToThisMember(waitingToChangeMember))
        {
            RefreshTimeDic[RotationMode_fightingMember] = 20f;
            ChangeFightingMember(waitingToChangeMember);
            waitingToChangeMember = null;
        }
    }

    bool CanChangeToThisMember(Data_Center targetMember)
    {
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
            charIcon.Seletedfeature(null,selectedFrame,100f);
        }else{
            datacenterCharIconDic.TryGetValue(waitingToChangeMember,out _tempSideCharIcon);
            charIcon.Seletedfeature(_tempSideCharIcon?.focusingCharIcon,selectedFrame,100f);
        }
    }

    public void InstantiateCharsIconsAndFloatHPBar_turnMode()//这个环节应该能够同时把HP bar也适配好。
    {
        SideCharIcon _SideCharIcon;    
        foreach(Data_Center a_char in teamMembers.values)
        {
            if (!RefreshTimeDic.ContainsKey(a_char))
            {
                RefreshTimeDic.Add(a_char,0);
            }
            _SideCharIcon = Instantiate(button_prefab);
            _SideCharIcon.name = a_char.name + " ICon";
            _SideCharIcon.IniHPShow(a_char);
            _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
            void action1()
            {
                ReadyForNextMemberOnTheShow(a_char);
            }
            _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);           
            CharacterDataInfo characterDataInfo = CharacterDataInfoReference[a_char];
            if (characterDataInfo == null)
            {
                Debug.Log("角色信息字典严重错误");
                continue;
            }
            CharacterResourceInfo characterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(characterDataInfo.ResourceName);
            _SideCharIcon.focusingCharIcon.ChangeIcon(monsterIconsDic.Instance.GetMonsterIconSyn(characterDataInfo.ResourceName),characterResourceInfo._zokusei);
            _SideCharIcon.focusingCharIcon.CooldownCurtainUpdate(0);
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                _SideCharIcon.gameObject.SetActive(true);
                _SideCharIcon.transform.SetParent(sideIconsContainer.transform);
                _SideCharIcon.transform.localScale = Vector3.one;
            }
            else
            {
                _SideCharIcon.gameObject.SetActive(false);
            }
            datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));
            _mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
        }
        rotationModeHitCombo = Instantiate(HitCombo);
        rotationModeHitCombo.name = teamConfig.myTeam + "HitCombo";
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
            if (teamMembers.values.Count > 0)
            {
                for (int i = 0; i < teamMembers.values.Count; i++)
                {
                    ReadyForNextMemberOnTheShow(teamMembers.values[i]);
                }
            }
        }
        if (time_counter > 6f)
        {
            if (teamMembers.values.Count > 0)
            {
                for (int i = 0; i < teamMembers.values.Count; i++)
                {
                    if (RefreshTimeDic[teamMembers.values[i]] <= 0)
                    {
                        ReadyForNextMemberOnTheShow(teamMembers.values[i]);
                    }
                    time_counter = 0f;
                }
            }
            time_counter = 0f;
        }
    }
    
    public bool ChangeFightingMember(Data_Center _changeTo)
    {
        if (!(teamMembers.values.Count > 1))
            return false;
        if (RotationMode_fightingMember == _changeTo)
            return false;

        bool memberchanged = false;
        Vector3 targetposition = Vector3.zero;
        if (RotationMode_fightingMember != null)
        {
            targetposition = RotationMode_fightingMember.transform.position;
        }
        foreach (Data_Center data_Center in teamMembers.values)
        {
            if (_changeTo == data_Center && !data_Center.IsDead.Value)
            {
                RotationMode_fightingMember = _changeTo;
                RotationMode_fightingMember._MyBehaviorRunner.StartToGo();
                RotationMode_fightingMember.WholeT.transform.position = targetposition;

                CharacterDataInfo characterDataInfo = CharacterDataInfoReference[_changeTo];
                CharacterResourceInfo characterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(characterDataInfo.ResourceName);
                string personalEffectsPath = FightGlobalSetting.EffectPathDefine(characterResourceInfo._zokusei);
                EffectAndHurtObjectLoading.Instance.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, null);
                memberchanged = true;
            } else {
                data_Center._MyBehaviorRunner.ChangeState("Empty");
                data_Center.WholeT.transform.position = new Vector3(400, 200, 400);
            }
        }
        if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
        {
            realTimeGameProcessManager.SwitchToCMode(RotationMode_fightingMember, teamConfig.myTeam, MobileInputsManager.playerMode);
        }
        realTimeGameProcessManager.Refresh();
        return memberchanged;
    }
}
