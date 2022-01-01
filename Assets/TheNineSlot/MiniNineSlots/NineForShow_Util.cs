using System;
using UnityEngine;
using UnityEngine.UI;

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

    public void LoadShowDetailFeature(Action<string> showDetail)
    {
        A1T.onClick.RemoveAllListeners();
        A1T.onClick.AddListener(() => { ShowDetailOfStone(A1T, showDetail);});
        A2T.onClick.RemoveAllListeners();
        A2T.onClick.AddListener(() => { ShowDetailOfStone(A2T, showDetail);});
        A3T.onClick.RemoveAllListeners();
        A3T.onClick.AddListener(() => { ShowDetailOfStone(A3T, showDetail);});
        
        B1T.onClick.RemoveAllListeners();
        B1T.onClick.AddListener(() => { ShowDetailOfStone(B1T, showDetail);});
        B2T.onClick.RemoveAllListeners();
        B2T.onClick.AddListener(() => { ShowDetailOfStone(B2T, showDetail);});
        B3T.onClick.RemoveAllListeners();
        B3T.onClick.AddListener(() => { ShowDetailOfStone(B3T, showDetail);});
        
        C1T.onClick.RemoveAllListeners();
        C1T.onClick.AddListener(() => { ShowDetailOfStone(C1T, showDetail);});
        C2T.onClick.RemoveAllListeners();
        C2T.onClick.AddListener(() => { ShowDetailOfStone(C2T, showDetail);});
        C3T.onClick.RemoveAllListeners();
        C3T.onClick.AddListener(() => { ShowDetailOfStone(C3T, showDetail);});
    }
    
    void ShowDetailOfStone(Button targetButton, Action<string> showDetail)
    {
        SKStoneItem item = targetButton.transform.GetComponentInChildren<SKStoneItem>();
        if (item != null)
        {
            showDetail(item._SkillConfig.RECORD_ID);
        }
    }
}
