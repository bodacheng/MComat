using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using mainMenu;
using Skill;

public class FightTeam : MonoBehaviour
{
    public TeamMode TeamMode;
    public MultiDictionary<int, int, Data_Center> teamMembers = new MultiDictionary<int, int, Data_Center>();
    public IDictionary<Data_Center, CharDataInfo> CharDataInfoRef = new Dictionary<Data_Center, CharDataInfo>();
    public TeamConfig teamConfig;
    public RectTransform sideIconsContainer;
    public Canvas _targetCanvas;
    public SideCharIcon button_prefab;
    public GameObject selectedFrame;
    public Text HitCombo;
    public RealTimeGameProcessManager realTimeGameProcessManager;
    public MobileInputsManager _mobileInputsManager;
    public CharsManager _CharSetManager;
    
    [HideInInspector]
    public Transform[] TeamStandPoints;
    protected IDictionary<Data_Center, SideCharIcon> datacenterCharIconDic = new Dictionary<Data_Center, SideCharIcon>();

    public IEnumerator CharsLoad(MultiDictionary<int, int, CharDataInfo> MembersSets)
    {
        foreach (KeyValuePair<int,List<int>> keys in MembersSets.GetAllUnNullKeys())
        {
            foreach (int key in keys.Value)
            {
                CharDataInfo _one = MembersSets.Get(keys.Key,key);
                IEnumerator char_DC = _CharSetManager.CreateCharacter(_one);
                yield return char_DC;
                Data_Center data_Center = (Data_Center)char_DC.Current;
                data_Center.Step3Initialize(teamConfig, NineAndTwo.INI_Hp(_one._NineAndTwo.SkillEntityList()));
                teamMembers.Set(keys.Key,key,data_Center);
                CharDataInfoRef.Add(teamMembers.Get(keys.Key,key),_one);
            }
        }
    }

    public virtual void Clear()
    {
        datacenterCharIconDic.Clear();
    }

    public virtual List<Transform> TeamMemberTransforms()
    {
        return null;
    }

    protected SideCharIcon _tempSI;
    public void BarsPositionUpdate()
    {
        foreach(Data_Center _one in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_one,out _tempSI);
            _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f),Time.deltaTime * 20f);
        }
    }
    
    protected virtual void InstantiateCharsIconsAndFloatHPBar()
    {
    }
    
    protected virtual void TeamsFightInitialize(float extraHP)
    {
    }
    
    // for multiRaid
    public virtual void ArrangeAllTeamMembersToPosition(MultiDictionary<int,int,Data_Center> heromultiDictionary)
    {
    }
        
    // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
    public IEnumerator Instantiate(MultiDictionary<int, int, CharDataInfo> ChracterSets,float extraHP)
    {
        yield return CharsLoad(ChracterSets);
        InstantiateCharsIconsAndFloatHPBar();
        TeamsFightInitialize(extraHP);
    }
    
    protected void RefreshResistanceBar(Data_Center data_Center)
    {
        datacenterCharIconDic.TryGetValue(data_Center, out _tempSI);
        DOTween.To(() => _tempSI.ResistBar.value, (x) => _tempSI.ResistBar.value = x, data_Center._ResistanceManager.Resistance.Value / 10f, 0.2f);
        _tempSI.ResistBarFillImage.color = data_Center._ResistanceManager.Resistance.Value > 0 ? Color.yellow : Color.clear;
    }
    
    protected void RefreshHPBar(Data_Center data_Center,float current_hp,float wholeHP)
    {
        datacenterCharIconDic.TryGetValue(data_Center,out _tempSI);
        _tempSI.HpText.text = current_hp.ToString();
        DOTween.To(() => _tempSI.HpBar.value, (x) => _tempSI.HpBar.value = x, current_hp / wholeHP, 0.2f);
    }
    
    //这个刷新是倾向于画面制御
    public virtual void Refresh()
    {
        foreach (Data_Center _datacenter in teamMembers.values)
        {
            datacenterCharIconDic.TryGetValue(_datacenter, out _tempSI);
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                _tempSI.transform.localScale = _datacenter != RealTimeGameProcessManager.focusingChar ? Vector3.one : Vector3.one * 1.2f;
                _tempSI.transform.SetParent(sideIconsContainer.transform);
                _tempSI.focusingCharIcon.gameObject.SetActive(true);
                _tempSI.RecallBars();
            }else{
                _tempSI.focusingCharIcon.gameObject.SetActive(false);
                _tempSI.transform.SetParent(_targetCanvas.transform);
            }
        }
    }
    
    public virtual void LocalFightingUpdate()
    {
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
    public virtual void ModeStart()
    {
    }
}
