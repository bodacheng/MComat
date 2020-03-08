using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public partial class FightTeam : MonoBehaviour
{
    IDictionary<Data_Center, Text> multiRaidHitComboDic = new Dictionary<Data_Center, Text>();//这个应该是多人模式的ComboHit，每个角色分属一个。轮流上场式应该是另一种

    void TeamsFightMultiRaidInitialize(float wholeHP,Color comboTextColor)
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
            a_char._FightAttriCalReference._ComboHitCount.HitCount.Value = 0;
            a_char._FightAttriCalReference._ComboHitCount.HitCount.Subscribe(x => 
            {
                RefreshComboHitMultiRaid(a_char,comboTextColor);
            });
        }
    }
    
    Text _hitcomboText;
    void RefreshComboHitMultiRaid(Data_Center _datacenter,Color comboTextColor)
    {
        _hitcomboText = multiRaidHitComboDic[_datacenter];
        if (_datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value > 1)
        {
            _hitcomboText.text = _datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value.ToString() + "Hits!";
            _hitcomboText.color = comboTextColor;
            _hitcomboText.transform.localScale = Vector3.one;
            _hitcomboText.fontSize = 30;
            _hitcomboText.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f),0.2f);
        }
        else
        {
            _hitcomboText.color = Color.clear;
        }
    }

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
            multiRaidHitComboDic.Add(new KeyValuePair<Data_Center, Text>(a_char, hitCombo));
            _mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
        }
    }
    
    public void MultiRaid_mode_start()
    {
        LetAllCharactersStartOff();
    }
}