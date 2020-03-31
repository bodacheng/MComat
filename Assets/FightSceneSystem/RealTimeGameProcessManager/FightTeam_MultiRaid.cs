using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class FightTeam_MultiRaid : FightTeam
{
    IDictionary<Data_Center, Text> multiRaidHitComboDic = new Dictionary<Data_Center, Text>();
    
    public override void Refresh()
    {
        base.Refresh();
        foreach (Data_Center _datacenter in teamMembers.values)
        {
            if (multiRaidHitComboDic.ContainsKey(_datacenter))
            {
                multiRaidHitComboDic[_datacenter].color = teamConfig.myTeam == RealTimeGameProcessManager.playerTeam ? Color.yellow : Color.blue;
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
    
    public override void ArrangeAllTeamMembersToPosition(MultiDictionary<int,int,Data_Center> heromultiDictionary)
    {
        foreach(KeyValuePair<int,List<int>> keys in heromultiDictionary.GetAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center character_data_Center = heromultiDictionary.Get(keys.Key,key);
                if (character_data_Center == null)
                {
                    continue;
                }
                if (TeamStandPoints[key] != null)
                {
                    character_data_Center.WholeT.transform.position = TeamStandPoints[key].position;
                    character_data_Center.WholeT.transform.rotation = TeamStandPoints[key].rotation;
                    character_data_Center.WholeT.parent = null;
                    character_data_Center.WholeT.gameObject.SetActive(true);
                }else{
                    Debug.Log("站位逻辑错误。出现了系统未安排的站位点");
                }
            }
        }
    }
    
    public override void Clear()
    {
        datacenterCharIconDic.Clear();
        multiRaidHitComboDic.Clear();
    }
    
    public override void LocalFightingUpdate()
    { 
    }

    protected override void TeamsFightInitialize(float extraHP)
    {
        foreach (Data_Center a_char in teamMembers.values)
        {
            a_char._FightAttriCalReference.CurrentHp.Value += extraHP;
            a_char._FightAttriCalReference.CurrentHp.Subscribe(x => 
            {
                RefreshHPBar(a_char, x, a_char._FightAttriCalReference.CurrentHp.Value);
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
                RefreshComboHitMultiRaid(a_char);
            });
        }
    }
    
    Text _hitcomboText;
    void RefreshComboHitMultiRaid(Data_Center _datacenter)
    {
        _hitcomboText = multiRaidHitComboDic[_datacenter];
        if (_datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value > 1)
        {
            _hitcomboText.text = _datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value.ToString() + "Hits!";
            _hitcomboText.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f),0.2f);
        }
        else
        {
            switch (teamConfig.myTeam)
            {
                case Team.player1:
                    _hitcomboText.rectTransform.DOAnchorPos(new Vector2(-200,Screen.height + 100), 0.2f);
                    break;
                case Team.player2:
                    _hitcomboText.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100),0.2f);
                    break;
                default:
                    _hitcomboText.rectTransform.DOAnchorPos(new Vector2(-100,-100) ,0.2f);
                    break;
            }
        }
    }

    protected override void InstantiateCharsIconsAndFloatHPBar()//这个环节应该能够同时把HP bar也适配好。
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
                realTimeGameProcessManager.SwitchToCMode(a_char, RealTimeGameProcessManager.Auto);
                realTimeGameProcessManager.CameraParaAdjustment(teamConfig.myTeam);
            }
            _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(Action1);
            CharDataInfo characterDataInfo = CharDataInfoRef[a_char];
            CharConfig characterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(characterDataInfo.ResourceID);
            _SideCharIcon.focusingCharIcon.ChangeIcon(MonsterIconDic.Instance.GetMonsterIconSyn(characterDataInfo.ResourceID),characterResourceInfo._zokusei);
            _SideCharIcon.focusingCharIcon.CooldownCurtainUpdate(0);
            _SideCharIcon.gameObject.SetActive(true);
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
            datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));
            multiRaidHitComboDic.Add(new KeyValuePair<Data_Center, Text>(a_char, hitCombo));
            _mobileInputsManager.ZokuseiButtonRegister(a_char.Zokusei);
        }
    }
    
    public override void ModeStart()
    {
        LetAllCharactersStartOff();
    }

    public override List<Transform> TeamMemberTransforms()
    {
        return null;
    }
}