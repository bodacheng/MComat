using System.Collections.Generic;
using UnityEngine;

public partial class FightTeam : MonoBehaviour
{
    public void InstantiateCharsIconsAndFloatHPBar_multiRaid()//这个环节应该能够同时把HP bar也适配好。
    {
        SideCharIcon _SideCharIcon;
        TextMesh hitCombo;
        foreach(Data_Center a_char in teamMembers.values)
        {
            hitCombo = Instantiate(HitCombo);
            hitCombo.name = a_char.name + "HitCombo";
            _SideCharIcon = Instantiate(button_prefab);
            _SideCharIcon.name = a_char.name + " ICon";            
            _SideCharIcon.iniHPShow(a_char);

            _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
            void Action1()
            {
                realTimeGameProcessManager.SwitchToCMode(a_char, RealTimeGameProcessManager.Auto);
            }
            _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
            CharacterDataInfo characterDataInfo = CharacterDataInfoReference[a_char];
            CharacterResourceInfo characterResourceInfo = monstersConfigTable.getCharacterResourceInfo(characterDataInfo.monsterId);
            _SideCharIcon.focusingCharIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(characterDataInfo.monsterId),characterResourceInfo._zokusei);
            _SideCharIcon.gameObject.SetActive(true);
            _SideCharIcon.transform.SetParent(sideIconsContainer.transform);
            _SideCharIcon.transform.localScale = Vector3.one;
            datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));
            datacenterHitComboDic.Add(new KeyValuePair<Data_Center, TextMesh>(a_char, hitCombo));
            this._mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
        }
    }
    
    public void MultiRaid_mode_start()
    {
        LetAllCharactersStartOff();
    }
}
