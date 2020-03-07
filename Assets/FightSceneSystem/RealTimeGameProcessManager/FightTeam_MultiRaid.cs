using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class FightTeam : MonoBehaviour
{
    public void InstantiateCharsIconsAndFloatHPBar_multiRaid()//这个环节应该能够同时把HP bar也适配好。
    {
        SideCharIcon _SideCharIcon;
        Text hitCombo;
        foreach(Data_Center a_char in teamMembers.values)
        {
            hitCombo = Instantiate(HitCombo);
            hitCombo.name = a_char.WholeT.name + "HitCombo";
            _SideCharIcon = Instantiate(button_prefab);
            _SideCharIcon.name = a_char.name + " ICon";            
            _SideCharIcon.IniHPShow(a_char);
            _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
            void Action1()
            {
                realTimeGameProcessManager.SwitchToCMode(a_char, teamConfig.myTeam, RealTimeGameProcessManager.Auto);
            }
            _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
            CharacterDataInfo characterDataInfo = CharacterDataInfoReference[a_char];
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
            datacenterHitComboDic.Add(new KeyValuePair<Data_Center, Text>(a_char, hitCombo));
            _mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
        }
    }
    
    public void MultiRaid_mode_start()
    {
        LetAllCharactersStartOff();
    }
}