using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 抽卡技能石细节显示
public partial class NineForShow : MonoBehaviour
{
    public void ShowStones_DataInfo(UnitInfo unitInfo)
    {
        ShowStones(
            unitInfo.set.a1, unitInfo.set.a2, unitInfo.set.a3,
            unitInfo.set.b1, unitInfo.set.b2, unitInfo.set.b3,
            unitInfo.set.c1, unitInfo.set.c2, unitInfo.set.c3
        );
    }

    public void LoadShowDetailFeature()
    {
        A1T.onClick.RemoveAllListeners();
        A1T.onClick.AddListener(A1SlotDetail);
        A2T.onClick.RemoveAllListeners();
        A2T.onClick.AddListener(A2SlotDetail);
        A3T.onClick.RemoveAllListeners();
        A3T.onClick.AddListener(A3SlotDetail);
        
        B1T.onClick.RemoveAllListeners();
        B1T.onClick.AddListener(B1SlotDetail);
        B2T.onClick.RemoveAllListeners();
        B2T.onClick.AddListener(B2SlotDetail);
        B3T.onClick.RemoveAllListeners();
        B3T.onClick.AddListener(B3SlotDetail);
        
        C1T.onClick.RemoveAllListeners();
        C1T.onClick.AddListener(C1SlotDetail);
        C2T.onClick.RemoveAllListeners();
        C2T.onClick.AddListener(C2SlotDetail);
        C3T.onClick.RemoveAllListeners();
        C3T.onClick.AddListener(C3SlotDetail);
    }
    
    void A1SlotDetail()
    {
        ShowDetailOfStone(A1T);
    }
    void A2SlotDetail()
    {
        ShowDetailOfStone(A2T);
    }    
    void A3SlotDetail()
    {
        ShowDetailOfStone(A3T);
    }
    void B1SlotDetail()
    {
        ShowDetailOfStone(B1T);
    }
    void B2SlotDetail()
    {
        ShowDetailOfStone(B2T);
    }    
    void B3SlotDetail()
    {
        ShowDetailOfStone(B3T);
    }
    void C1SlotDetail()
    {
        ShowDetailOfStone(C1T);
    }
    void C2SlotDetail()
    {
        ShowDetailOfStone(C2T);
    }    
    void C3SlotDetail()
    {
        ShowDetailOfStone(C3T);
    }
    
    void ShowDetailOfStone(Button targetButton)
    {
        SKStoneItem item = targetButton.transform.GetComponentInChildren<SKStoneItem>();
        if (item != null)
        {
            //GotchaLayer.target._skillStoneDetail.RefreshInfo(item._SkillConfig);
        }
    }
}
