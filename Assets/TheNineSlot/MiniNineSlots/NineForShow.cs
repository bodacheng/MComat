using System;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

public partial class NineForShow : MonoBehaviour
{
    public Button A1T, A2T, A3T, B1T, B2T, B3T, C1T, C2T, C3T;
    public Image A1Frame, A2Frame, A3Frame, B1Frame, B2Frame, B3Frame, C1Frame, C2Frame, C3Frame;
    
    SKStoneItem A1S, A2S, A3S, B1S, B2S, B3S, C1S, C2S, C3S;
    
    public void ClearCurrent()
    {
        if (A1S != null)
        {
            Destroy(A1S.gameObject);
            A1S = null;
        }
        if (A2S != null)
        {
            Destroy(A2S.gameObject);
            A2S = null;
        }
        if (A3S != null)
        {
            Destroy(A3S.gameObject);
            A3S = null;
        }
        if (B1S != null)
        {
            Destroy(B1S.gameObject);
            B1S = null;
        }
        if (B2S != null)
        {
            Destroy(B2S.gameObject);
            B2S = null;
        }
        if (B3S != null)
        {
            Destroy(B3S.gameObject);
            B3S = null;
        }
        if (C1S != null)
        {
            Destroy(C1S.gameObject);
            C1S = null;
        }
        if (C2S != null)
        {
            Destroy(C2S.gameObject);
            C2S = null;
        }
        if (C3S != null)
        {
            Destroy(C3S.gameObject);
            C3S = null;
        }
    }
    
    public void ShowStones(string A1skillid, string A2skillid, string A3skillid,
                                    string B1skillid, string B2skillid, string B3skillid,
                                        string C1skillid, string C2skillid, string C3skillid)
    {
        try
        {
            ClearCurrent();

            A1S = Stones.GenerateStoneModel(A1skillid, false);
            A2S = Stones.GenerateStoneModel(A2skillid, false);
            A3S = Stones.GenerateStoneModel(A3skillid, false);
            B1S = Stones.GenerateStoneModel(B1skillid, false);
            B2S = Stones.GenerateStoneModel(B2skillid, false);
            B3S = Stones.GenerateStoneModel(B3skillid, false);
            C1S = Stones.GenerateStoneModel(C1skillid, false);
            C2S = Stones.GenerateStoneModel(C2skillid, false);
            C3S = Stones.GenerateStoneModel(C3skillid, false);

            if (A1S != null)
            {
                A1Frame.color = RefreshFrameColor(A1S._SkillConfig.SP_LEVEL);
            }

            if (A2S != null)
            {
                A2Frame.color = RefreshFrameColor(A2S._SkillConfig.SP_LEVEL);
            }

            if (A3S != null)
            {
                A3Frame.color = RefreshFrameColor(A3S._SkillConfig.SP_LEVEL);
            }

            if (B1S != null)
            {
                B1Frame.color = RefreshFrameColor(B1S._SkillConfig.SP_LEVEL);
            }

            if (B2S != null)
            {
                B2Frame.color = RefreshFrameColor(B2S._SkillConfig.SP_LEVEL);
            }

            if (B3S != null)
            {
                B3Frame.color = RefreshFrameColor(B3S._SkillConfig.SP_LEVEL);
            }

            if (C1S != null)
            {
                C1Frame.color = RefreshFrameColor(C1S._SkillConfig.SP_LEVEL);
            }

            if (C2S != null)
            {
                C2Frame.color = RefreshFrameColor(C2S._SkillConfig.SP_LEVEL);
            }

            if (C3S != null)
            {
                C3Frame.color = RefreshFrameColor(C3S._SkillConfig.SP_LEVEL);
            }

            Parent();
        }
        catch (Exception e)
        {
            Debug.Log("存在本地逻辑顺序问题");
            Debug.Log(e);
        }
    }
    
    Color RefreshFrameColor(int splevel)
    {
        switch(splevel)
        {
            case 1:
                return Color.green;
            case 2:
                return Color.yellow;
            case 3:
                return Color.red;
            default:
                return Color.white;
        }
    }
    
    public void ShowStoneLevel()
    {
        if (A1S != null)
        {
            A1S.ShowStoneLevel();
        }
        if (A2S != null)
        {
            A2S.ShowStoneLevel();
        }
        if (A3S != null)
        {
            A3S.ShowStoneLevel();
        }
        if (B1S != null)
        {
            B1S.ShowStoneLevel();
        }
        if (B2S != null)
        {
            B2S.ShowStoneLevel();
        }
        if (B3S != null)
        {
            B3S.ShowStoneLevel();
        }
        if (C1S != null)
        {
            C1S.ShowStoneLevel();
        }
        if (C2S != null)
        {
            C2S.ShowStoneLevel();
        }
        if (C3S != null)
        {
            C3S.ShowStoneLevel();
        }
    }
    
    public void CloseStoneInfo()
    {
        if (A1S != null)
        {
            A1S.CloseInfo();
        }
        if (A2S != null)
        {
            A2S.CloseInfo();
        }
        if (A3S != null)
        {
            A3S.CloseInfo();
        }
        if (B1S != null)
        {
            B1S.ShowStoneLevel();
        }
        if (B2S != null)
        {
            B2S.CloseInfo();
        }
        if (B3S != null)
        {
            B3S.CloseInfo();
        }
        if (C1S != null)
        {
            C1S.CloseInfo();
        }
        if (C2S != null)
        {
            C2S.CloseInfo();
        }
        if (C3S != null)
        {
            C3S.CloseInfo();
        }
    }
        
    void Parent()
    {
        RectTransform slotRT = A1T.GetComponent<RectTransform>();

        void SS(SKStoneItem SK, Button BT)
        {
            if (SK == null)
                return;
            SK.transform.SetParent(BT.transform);
            SK.transform.localPosition = Vector3.zero;
            SK.transform.localScale = Vector3.one;

            var targetRect = SK.GetComponent<RectTransform>();
            targetRect.anchorMin = new Vector2(0, 0);
            targetRect.anchorMax = new Vector2(1, 1);
            targetRect.offsetMin = new Vector2(0, 0);
            targetRect.offsetMax = new Vector2(0, 0);
            //SK.GetComponent<RectTransform>().rect.Set(0, 0, slotRT.rect.width,slotRT.rect.height);
            SK.gameObject.SetActive(true);
        }

        SS(A1S, A1T);
        SS(A2S, A2T);
        SS(A3S, A3T);
        SS(B1S, B1T);
        SS(B2S, B2T);
        SS(B3S, B3T);
        SS(C1S, C1T);
        SS(C2S, C2T);
        SS(C3S, C3T);
    }
}
