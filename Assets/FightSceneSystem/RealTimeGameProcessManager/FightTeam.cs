using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public partial class FightTeam : MonoBehaviour
{
    public MultiDictionary<int, int, Data_Center> teamMembers = new MultiDictionary<int, int, Data_Center>();
    public IDictionary<Data_Center, CharacterDataInfo> CharacterDataInfoReference = new Dictionary<Data_Center, CharacterDataInfo>();
    
    public TeamConfig teamConfig;
    public RectTransform sideIconsContainer;
    public Canvas _targetCanvas;
    public RectTransform controllingCharT;
    
    public SideCharIcon button_prefab;
    public TextMeshProUGUI HitCombo;
    public RealTimeGameProcessManager realTimeGameProcessManager;
    public mobileInputsManager _mobileInputsManager;
    public CharsManager _CharSetManager;
    public TeamMode TeamMode;
    
    private IDictionary<Data_Center, SideCharIcon> datacenterCharIconDic = new Dictionary<Data_Center, SideCharIcon>();
    private IDictionary<Data_Center, TextMeshProUGUI> datacenterHitComboDic = new Dictionary<Data_Center, TextMeshProUGUI>();
    
    private SideCharIcon _tempSideCharIcon;
    
    public void Clear()
    {
        datacenterCharIconDic.Clear();
        datacenterHitComboDic.Clear();
    }
    
    public void BarsPositionUpdate()
    {
        foreach(Data_Center _one in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_one,out _tempSideCharIcon);
            _tempSideCharIcon.transform.position = Vector3.Lerp(_tempSideCharIcon.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f),Time.deltaTime * 20f);
        }
    }
    
    // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
    public void Instantiate()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
                InstantiateCharsIconsAndFloatHPBar_multiRaid();
            break;
            case TeamMode.rotation:
                InstantiateCharsIconsAndFloatHPBar_turnMode();
            break;
        }
        TeamsFightInitialize();
    }
    
    private void TeamsFightInitialize()
    {
        foreach(Data_Center a_char in teamMembers.values)
        {
            a_char._FightAttriCalReference.CurrentHp.Value = 300f;
            a_char._FightAttriCalReference.CurrentHp.Subscribe(x => {RefreshHPBar(a_char,x);});            
            a_char._ResistanceManager.Resistance.Value = 0;
            a_char._ResistanceManager.Resistance.Subscribe(x => { a_char._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10); RefreshResistanceBar(a_char); });
            a_char._FightAttriCalReference._ComboHitCount.HitCount.Value = 0;
            a_char._FightAttriCalReference._ComboHitCount.HitCount.Subscribe(x => { RefreshComboHit(a_char); });
        }
    }
    
    public void RefreshResistanceBar(Data_Center data_Center)
    {
        datacenterCharIconDic.TryGetValue(data_Center,out _tempSideCharIcon);
        _tempSideCharIcon.ResistBar.value = (float)data_Center._ResistanceManager.Resistance.Value / 10f;//抵抗槽最大10格   
    }
  
    public void RefreshHPBar(Data_Center data_Center,float current_hp)
    {
        datacenterCharIconDic.TryGetValue(data_Center,out _tempSideCharIcon);
        _tempSideCharIcon.HpBar.value = current_hp / 500; 
    }

    private TextMeshProUGUI _hitcomboText;
    public void RefreshComboHit(Data_Center _datacenter)
    {
        _hitcomboText = datacenterHitComboDic[_datacenter];
        if (_datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value > 1)
        {
            _hitcomboText.color = Color.yellow;
            _hitcomboText.text = _datacenter._FightAttriCalReference._ComboHitCount.HitCount.Value.ToString() + "Hits!";
            _hitcomboText.transform.localScale = Vector3.one;
            _hitcomboText.fontSizeMax = 30f;
            _hitcomboText.transform.position = Vector3.Lerp(_hitcomboText.transform.position, CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 2.5f),Time.deltaTime * 20f);
        }
        else
            _hitcomboText.color = Color.clear;
    }

    public void Refresh()//这个刷新是倾向于画面制御
    {
        foreach (Data_Center _datacenter in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_datacenter, out _tempSideCharIcon);
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                if (_datacenter != RealTimeGameProcessManager.focusingChar)
                {
                    _tempSideCharIcon.transform.SetParent(sideIconsContainer);
                }
                else
                {
                    _tempSideCharIcon.transform.SetParent(controllingCharT);
                    _tempSideCharIcon.transform.localPosition = Vector3.zero;
                    _tempSideCharIcon.transform.localScale = Vector3.one;
                }
                _tempSideCharIcon.focusingCharIcon.gameObject.SetActive(true);
                _tempSideCharIcon.recallBars();
            }else{
                _tempSideCharIcon.focusingCharIcon.gameObject.SetActive(false);
                _tempSideCharIcon.transform.SetParent(_targetCanvas.transform);
            }
            
            if (datacenterHitComboDic.ContainsKey(_datacenter))
            {
                datacenterHitComboDic[_datacenter].color = teamConfig.myTeam == RealTimeGameProcessManager.playerTeam ? Color.yellow : Color.blue;
                datacenterHitComboDic[_datacenter].gameObject.SetActive(true);
                if (datacenterHitComboDic[_datacenter].gameObject.transform.parent != _targetCanvas)
                    datacenterHitComboDic[_datacenter].gameObject.transform.SetParent(_targetCanvas.transform);
                datacenterHitComboDic[_datacenter].transform.localScale = Vector3.one;
                datacenterHitComboDic[_datacenter].fontSizeMax = 30f;
            }
        }
    }
    
    public void LocalFightingUpdate()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
            break;
            case TeamMode.rotation:
                if (this.teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                    TurnModeEnemySideAutoMemberShaft();
            break;
        }
    }

    public bool IfAllCharsPreparedForBattle()
    {
        foreach (Data_Center oneMember in teamMembers.values)
        {
            if (!oneMember.IfPreparedForBattle())
                return false;
        }
        return true;
    }
    
    public void LetAllCharactersStartOff()
    {
        foreach (Data_Center oneMember in teamMembers.values)
        {
            oneMember.AIStateRunner.StartToGo();
        }
    }
    
    // 队伍模式对应行为运行第一步。
    public void ModeStart()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
                MultiRaid_mode_start();
            break;
            case TeamMode.rotation:
                Rotation_mode_start();
            break;
        }
    }
    
    public IEnumerator CharacterResourceLoad(MultiDictionary<int, int, CharacterDataInfo> MembersSets)
    {
        foreach (KeyValuePair<int,List<int>> keys in MembersSets.GetAllUnNullKeys())
        {
            foreach (int key in keys.Value)
            {
                CharacterDataInfo _one = MembersSets.Get(keys.Key,key);
                IEnumerator character_datacenter = _CharSetManager.CreateCharacter(_one);
                yield return character_datacenter;
                Data_Center data_Center = (Data_Center)character_datacenter.Current;
                data_Center.Step3Initialize(teamConfig);
                teamMembers.Set(keys.Key,key,data_Center);
                CharacterDataInfoReference.Add(teamMembers.Get(keys.Key,key),_one);
            }
        }
    }
}
