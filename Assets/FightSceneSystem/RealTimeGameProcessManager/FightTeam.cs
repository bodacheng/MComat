using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public partial class FightTeam : MonoBehaviour
{
    public TeamMode TeamMode;
    
    public MultiDictionary<int, int, Data_Center> teamMembers = new MultiDictionary<int, int, Data_Center>();
    public IDictionary<Data_Center, CharacterDataInfo> CharacterDataInfoReference = new Dictionary<Data_Center, CharacterDataInfo>();
    public TeamConfig teamConfig;
    
    public RectTransform sideIconsContainer;
    public Canvas _targetCanvas;
    public SideCharIcon button_prefab;
    public GameObject selectedFrame;
    public Text HitCombo;
    public RealTimeGameProcessManager realTimeGameProcessManager;
    public MobileInputsManager _mobileInputsManager;
    public CharsManager _CharSetManager;

    IDictionary<Data_Center, SideCharIcon> datacenterCharIconDic = new Dictionary<Data_Center, SideCharIcon>();

    public void Clear()
    {
        datacenterCharIconDic.Clear();
        multiRaidHitComboDic.Clear();
        rotationModeHitCombo.text = "";
    }
    
    public List<Transform> TeamMemberTransforms()
    {
        List<Transform> transforms = new List<Transform>();
        foreach(Data_Center _one in teamMembers.values)
        {
            transforms.Add(_one.WholeT);
        }
        return transforms;
    }
    
    SideCharIcon _tempSideCharIcon;
    public void BarsPositionUpdate()
    {
        foreach(Data_Center _one in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_one,out _tempSideCharIcon);
            _tempSideCharIcon.transform.position = Vector3.Lerp(_tempSideCharIcon.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f),Time.deltaTime * 20f);
        }
    }
    
    // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
    public IEnumerator Instantiate(MultiDictionary<int, int, CharacterDataInfo> ChracterSets,float HP,Color combohitcolor)
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
                yield return CharacterResourceLoad(ChracterSets);
                InstantiateCharsIconsAndFloatHPBar_multiRaid();
                TeamsFightMultiRaidInitialize(HP,combohitcolor);
            break;
            case TeamMode.rotation:
                yield return CharacterResourceLoad(ChracterSets);
                InstantiateCharsIconsAndFloatHPBar_turnMode();
                TeamsFightRotationModeInitialize(HP,combohitcolor);
            break;
            case TeamMode.test:
                yield return CharacterResourceLoadTestMode(ChracterSets);
                InstantiateCharsIconsAndFloatHPBar_turnMode();
                break;
        }
        
        yield return null;
    }
    
    void RefreshResistanceBar(Data_Center data_Center)
    {
        datacenterCharIconDic.TryGetValue(data_Center, out _tempSideCharIcon);
        DOTween.To(() => _tempSideCharIcon.ResistBar.value, (x) => _tempSideCharIcon.ResistBar.value = x, data_Center._ResistanceManager.Resistance.Value / 10f, 0.2f);
        _tempSideCharIcon.ResistBarFillImage.color = data_Center._ResistanceManager.Resistance.Value > 0 ? Color.yellow : Color.clear;
    }
      
    void RefreshHPBar(Data_Center data_Center,float current_hp,float wholeHP)
    {
        datacenterCharIconDic.TryGetValue(data_Center,out _tempSideCharIcon);
        DOTween.To(() => _tempSideCharIcon.HpBar.value, (x) => _tempSideCharIcon.HpBar.value = x, current_hp / wholeHP, 0.2f);
    }
   
    //这个刷新是倾向于画面制御
    public void Refresh()
    {
        foreach (Data_Center _datacenter in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_datacenter, out _tempSideCharIcon);
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                _tempSideCharIcon.transform.localScale = _datacenter != RealTimeGameProcessManager.focusingChar ? Vector3.one : Vector3.one * 1.2f;
                _tempSideCharIcon.transform.SetParent(sideIconsContainer.transform);
                _tempSideCharIcon.focusingCharIcon.gameObject.SetActive(true);
                _tempSideCharIcon.RecallBars();
            }else{
                _tempSideCharIcon.focusingCharIcon.gameObject.SetActive(false);
                _tempSideCharIcon.transform.SetParent(_targetCanvas.transform);
            }

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
    
    public void LocalFightingUpdate()
    {
        switch (TeamMode)
        {
            case TeamMode.multiraid:
            break;
            case TeamMode.rotation:
                WaitToTriggerMemberChange();
                if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                {
                    TurnModeEnemySideAutoMemberShaft();
                }
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
            oneMember._MyBehaviorRunner.StartToGo();
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
            case TeamMode.test:
                MultiRaid_mode_start();
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
    
    public IEnumerator CharacterResourceLoadTestMode(MultiDictionary<int, int, CharacterDataInfo> MembersSets)
    {
        foreach (KeyValuePair<int,List<int>> keys in MembersSets.GetAllUnNullKeys())
        {
            foreach (int key in keys.Value)
            {
                CharacterDataInfo _one = MembersSets.Get(keys.Key,key);
                _one._NineAndTwo = new NineAndTwo();
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
