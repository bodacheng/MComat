using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FightScene;

public class CommonFightResult : UILayer
{
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Button AgainBtn;
    [SerializeField] private RectTransform IconAndSKillShowUISetT;

    [Header("IconWithSkillShow")]
    public IconAndSKillShowUISet IconAndSKillShowUISetPretab;
        
    [Header("NineForShow")]
    public NineForShow NineForShowPretab;
    
    // 战斗结束后统计技能石升级情况时的画面显示
    List<NineForShow> NineForShows = new List<NineForShow>();
    public void ShowSKillSets(TeamUIManager teamUIManager, RectTransform IconAndSKillShowUISetT)
    {
        NineForShows.Clear();
        foreach (Transform child in IconAndSKillShowUISetT) 
        {
            Destroy(child.gameObject);
        }
        
        foreach (Data_Center data in teamUIManager.TeamMembers.mDict.Values)
        {
            IconAndSKillShowUISet iassi = Instantiate(IconAndSKillShowUISetPretab);
            SideCharIcon sideCharIcon = teamUIManager.GetSideIcon(data);
            NineForShow nineForShow = Instantiate(NineForShowPretab);
            NineForShows.Add(nineForShow);
            iassi.Set(sideCharIcon, nineForShow);
            iassi.transform.SetParent(IconAndSKillShowUISetT);
            iassi.transform.localPosition = Vector3.zero;
            iassi.transform.localScale = Vector3.one;
            nineForShow.ShowStones_Acc(RTFightManager.target.UnitInfoRef[data].id);
        }
    }
    
    void Clear()
    {
        foreach(NineForShow nineForShow in NineForShows)
        {
            nineForShow.ClearCurrent();
        }
    }
    
    public override void OnDestroy()
    {
        Clear();
    }
    
    public RectTransform GetIconAndSKillShowUISetT()
    {
        return IconAndSKillShowUISetT;
    }
    
    public void Initialise(Action R, Action A)
    {
        ReturnBtn.onClick.AddListener(R.Invoke);
        AgainBtn.onClick.AddListener(A.Invoke);
    }
}
