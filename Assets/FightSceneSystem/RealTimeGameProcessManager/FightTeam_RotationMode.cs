using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Soul;

public partial class FightTeam : MonoBehaviour
{
    Data_Center RotationMode_fightingMember;
    
    public void Rotation_mode_start()
    {
        changeFightingMember(teamMembers.values[0]);
    }

    public void instantiateCharsIconsAndFloatHPBar_turnMode()//这个环节应该能够同时把HP bar也适配好。
    {
        SideCharIcon _SideCharIcon;
        TextMeshProUGUI hitCombo;
        foreach(KeyValuePair<int,List<int>> keys in teamMembers.getAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center a_char = teamMembers.Get(keys.Key,key);
                hitCombo = Instantiate(HitCombo);
                hitCombo.name = a_char.name + "HitCombo";
                _SideCharIcon = Instantiate(button_prefab);
                _SideCharIcon.name = a_char.name + " ICon";
                _SideCharIcon.iniHPShow(a_char);
                _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
                UnityEngine.Events.UnityAction action1 = () =>
                {
                    this.changeFightingMember(a_char);
                    realTimeGameProcessManager.refresh();
                };
                _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                
                CharacterDataInfo characterDataInfo = CharacterDataInfoReference[a_char];
                if (characterDataInfo == null)
                {
                    Debug.Log("角色信息字典严重错误");continue;
                }
                CharacterResourceInfo characterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(characterDataInfo.monsterId);
                _SideCharIcon.focusingCharIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(characterDataInfo.monsterId),characterResourceInfo._zokusei);
                _SideCharIcon.gameObject.SetActive(true);
                _SideCharIcon.transform.SetParent(sideIconsContainer);
                _SideCharIcon.transform.localScale = Vector3.one;
                datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));
                datacenterHitComboDic.Add(new KeyValuePair<Data_Center, TextMeshProUGUI>(a_char, hitCombo));
                this._mobileInputsManager.zokuseiButtonRegister(a_char.Zokusei);
            }
        }
    }
    
    /// <summary>
    /// 本质上这个函数是AI。。。而AI按理说应该和其他东西是分层的。。
    /// </summary>
    float time_counter = 0;
    public void turnModeEnemySideAutoMemberShaft()
    {
        time_counter += Time.deltaTime;
        if (RotationMode_fightingMember != null && RotationMode_fightingMember.BO_Health._health <= 0)
        {
            if (teamMembers.values.Count > 0)
            {
                for (int i = 0; i < teamMembers.values.Count; i++)
                {
                    if (changeFightingMember(teamMembers.values[i]))
                    {
                        break;
                    }
                }
            }
        }
        if (time_counter > 6f)
        {
            if (teamMembers.values.Count > 0)
            {
                for (int i = 0; i < teamMembers.values.Count; i++)
                {
                    if (changeFightingMember(teamMembers.values[i]))
                    {
                        time_counter = 0f;
                        break;
                    }
                }
            }
            time_counter = 0f;
        }
    }
        
    public bool changeFightingMember(Data_Center _changeTo)
    {
        if (!(teamMembers.values.Count > 1))
            return false;

        if (RotationMode_fightingMember == _changeTo)
            return false;

        bool memberchanged = false;
        Vector3 targetposition = Vector3.zero;
        if (RotationMode_fightingMember != null)
            targetposition = RotationMode_fightingMember.transform.position;
        foreach (Data_Center data_Center in teamMembers.values)
        {
            if (_changeTo == data_Center && data_Center.BO_Health._health > 0)
            {
                RotationMode_fightingMember = _changeTo;
                RotationMode_fightingMember.AIStateRunner.StartToGo();
                RotationMode_fightingMember.WholeT.transform.position = targetposition;
                
                CharacterDataInfo characterDataInfo = CharacterDataInfoReference[_changeTo];
                CharacterResourceInfo characterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(characterDataInfo.monsterId);
                string personalEffectsPath;
                switch (characterResourceInfo._zokusei)
                {
                    case zokusei.darkMagic:
                        personalEffectsPath = "darkMagic";
                        break;
                    case zokusei.blueMagic:
                        personalEffectsPath = "blueMagic";
                        break;
                    case zokusei.greenMagic:
                        personalEffectsPath = "greenMagic";
                        break;
                    case zokusei.lightMagic:
                        personalEffectsPath = "lightMagic";
                        break;
                    case zokusei.redMagic:
                        personalEffectsPath = "redMagic";
                        break;
                    default:
                        personalEffectsPath = "defaultEffects";
                        break;
                }
                EffectAndHurtObjectLoading.Instance.GenerateEffect("skillEditConfirmEffect", personalEffectsPath,RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, null);
                memberchanged = true;
            }
            else{
                data_Center.AIStateRunner.changeState("Empty");
                data_Center.WholeT.transform.position = new Vector3(0,200,0);
            }
        }
        return memberchanged;
    }
}
