using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Soul;

public partial class FightTeam : MonoBehaviour
{
    public void instantiateCharsIconsAndFloatHPBar_multiRaid()//这个环节应该能够同时把HP bar也适配好。
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
                    realTimeGameProcessManager.SwitchToCMode(a_char,RealTimeGameProcessManager.combatFightPlayerMode);
                    realTimeGameProcessManager.refresh();
                };//点角色icon是设置focusingChar，点icon旁边的C按钮才是进入控制
                _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);
                
                CharacterDataInfo characterDataInfo = CharacterDataInfoReference[a_char];
                if (characterDataInfo == null)
                {
                    Debug.Log("角色信息字典严重错误");continue;
                }
                CharacterResourceInfo characterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(characterDataInfo.monsterId);
                _SideCharIcon.focusingCharIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(characterDataInfo.monsterId),characterResourceInfo._zokusei);
    
                _SideCharIcon.gameObject.SetActive(true);
                _SideCharIcon.transform.SetParent(sideIconsContainer.transform);
                _SideCharIcon.transform.localScale = Vector3.one;
                datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));
                datacenterHitComboDic.Add(new KeyValuePair<Data_Center, TextMeshProUGUI>(a_char, hitCombo));
                this._mobileInputsManager.zokuseiButtonRegister(a_char.Zokusei);
            }
        }
    }
    
    public void MultiRaid_mode_start()
    {
        letAllCharactersStartOff();
    }
}
