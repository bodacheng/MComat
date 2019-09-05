using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Soul;

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
    public IDictionary<Data_Center, SideCharIcon> datacenterCharIconDic = new Dictionary<Data_Center, SideCharIcon>();
    public IDictionary<Data_Center, TextMeshProUGUI> datacenterHitComboDic = new Dictionary<Data_Center, TextMeshProUGUI>();
        
    public TeamMode TeamMode;

    SideCharIcon _tempSideCharIcon = null;
    public void refreshHPAndResistBar()
    {
        foreach(KeyValuePair<int,List<int>> keys in teamMembers.getAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center _one = teamMembers.Get(keys.Key,key);
                datacenterCharIconDic.TryGetValue(_one,out _tempSideCharIcon);            
                _tempSideCharIcon.hpBarPrefab.value = Mathf.Lerp(_tempSideCharIcon.hpBarPrefab.value, (float)_one.BO_Health._health / 500,Time.deltaTime);
                if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                {
                    _tempSideCharIcon.transform.position = 
                        Vector3.Lerp(_tempSideCharIcon.transform.position,
                        CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f),Time.deltaTime * 20f);
                }
                _tempSideCharIcon.resistBarPrefab.value = 
                Mathf.Lerp(_tempSideCharIcon.resistBarPrefab.value, (float)_one._ResistanceManager.Resistance / 10f, Time.deltaTime);//抵抗槽最大10格   
            }
        }
    }

    public void refreshComboHit()
    {
        foreach(KeyValuePair<int,List<int>> keys in teamMembers.getAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center _datacenter = teamMembers.Get(keys.Key,key);
                TextMeshProUGUI _hitcomboText = datacenterHitComboDic[_datacenter];
                if (_datacenter.BO_Health.getHitCount() > 1)
                {
                    _hitcomboText.color = Color.yellow;
                    _hitcomboText.text = _datacenter.BO_Health.getHitCount().ToString() + "Hits!";
                    _hitcomboText.transform.localScale = Vector3.one;
                    _hitcomboText.fontSizeMax = 30f;
                    _hitcomboText.transform.position = 
                    Vector3.Lerp(_hitcomboText.transform.position, 
                    CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 2.5f),Time.deltaTime * 20f);
                }
                else
                    _hitcomboText.color = Color.clear;
            }
        }
    }
    
    public void refresh()//这个刷新是倾向于画面制御
    {
        foreach (KeyValuePair<int,List<int>> keys in teamMembers.getAllUnNullKeys())
        {
            foreach (int key in keys.Value)
            {
                Data_Center _datacenter = teamMembers.Get(keys.Key,key);
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
                    if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
                        datacenterHitComboDic[_datacenter].color = Color.yellow;
                    else
                        datacenterHitComboDic[_datacenter].color = Color.blue;
                    datacenterHitComboDic[_datacenter].gameObject.SetActive(true);
                    if (datacenterHitComboDic[_datacenter].gameObject.transform.parent != _targetCanvas)
                        datacenterHitComboDic[_datacenter].gameObject.transform.SetParent(_targetCanvas.transform);
                    datacenterHitComboDic[_datacenter].transform.localScale = Vector3.one;
                    datacenterHitComboDic[_datacenter].fontSizeMax = 30f;
                }
            }
        }
    }
    
    public void localFightingUpdate()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
            break;
            case TeamMode.rotation:
                if (this.teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                    turnModeEnemySideAutoMemberShaft();
            break;
        }
    }
    
    // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
    public void instantiateCharsIconsAndFloatHPBar()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
                instantiateCharsIconsAndFloatHPBar_multiRaid();
            break;
            case TeamMode.rotation:
                instantiateCharsIconsAndFloatHPBar_turnMode();
            break;
        }
    }

    public bool ifAllCharsPreparedForBattle()
    {
        foreach (Data_Center oneMember in teamMembers.values)
        {
            if (!oneMember.ifPreparedForBattle())
                return false;
        }
        return true;
    }
    
    public void letAllCharactersStartOff()
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
                if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                    Rotation_mode_start();
            break;
        }
    }
    
    public IEnumerator CharacterResourceLoad(MultiDictionary<int, int, CharacterDataInfo> MembersSets)
    {
        foreach (KeyValuePair<int,List<int>> keys in MembersSets.getAllUnNullKeys())
        {
            foreach (int key in keys.Value)
            {
                CharacterDataInfo _one = MembersSets.Get(keys.Key,key);
                IEnumerator character_datacenter = _CharSetManager.CreateCharacter(_one);
                yield return character_datacenter;
                Data_Center data_Center = (Data_Center)character_datacenter.Current;
                data_Center.step3Initialize(teamConfig);
                teamMembers.Set(keys.Key,key,data_Center);
                CharacterDataInfoReference.Add(teamMembers.Get(keys.Key,key),_one);
            }
        }
    }
}
