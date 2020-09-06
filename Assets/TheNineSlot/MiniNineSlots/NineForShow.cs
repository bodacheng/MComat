using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public partial class NineForShow : MonoBehaviour
{
    public Button A1T, A2T, A3T, B1T, B2T, B3T, C1T, C2T, C3T;

    SKStoneItem A1S, A2S, A3S, B1S, B2S, B3S, C1S, C2S, C3S;
    
    void ClearCurrent()
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
    
    public IEnumerator ShowStones(string A1skillid, string A2skillid, string A3skillid,
                                    string B1skillid, string B2skillid, string B3skillid,
                                        string C1skillid, string C2skillid, string C3skillid)
    {
        ClearCurrent();
        
        IEnumerator getA1 = SkillStonesBox.GenerateNewStoneModel(A1skillid, 2);
        IEnumerator getA2 = SkillStonesBox.GenerateNewStoneModel(A2skillid, 2);
        IEnumerator getA3 = SkillStonesBox.GenerateNewStoneModel(A3skillid, 2);
        IEnumerator getB1 = SkillStonesBox.GenerateNewStoneModel(B1skillid, 2);
        IEnumerator getB2 = SkillStonesBox.GenerateNewStoneModel(B2skillid, 2);
        IEnumerator getB3 = SkillStonesBox.GenerateNewStoneModel(B3skillid, 2);
        IEnumerator getC1 = SkillStonesBox.GenerateNewStoneModel(C1skillid, 2);
        IEnumerator getC2 = SkillStonesBox.GenerateNewStoneModel(C2skillid, 2);
        IEnumerator getC3 = SkillStonesBox.GenerateNewStoneModel(C3skillid, 2);
        
        yield return getA1;
        yield return getA2;
        yield return getA3;
        yield return getB1;
        yield return getB2;
        yield return getB3;
        yield return getC1;
        yield return getC2;
        yield return getC3;
        
        A1S = getA1.Current != null ? (SKStoneItem)getA1.Current : null;
        A2S = getA2.Current != null ? (SKStoneItem)getA2.Current : null;
        A3S = getA3.Current != null ? (SKStoneItem)getA3.Current : null;
        B1S = getB1.Current != null ? (SKStoneItem)getB1.Current : null;
        B2S = getB2.Current != null ? (SKStoneItem)getB2.Current : null;
        B3S = getB3.Current != null ? (SKStoneItem)getB3.Current : null;
        C1S = getC1.Current != null ? (SKStoneItem)getC1.Current : null;
        C2S = getC2.Current != null ? (SKStoneItem)getC2.Current : null;
        C3S = getC3.Current != null ? (SKStoneItem)getC3.Current : null;
        
        Parent();
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
    
    public void Clear()
    {
        if (A1S != null)
        {
            A1S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (A2S != null)
        {
            A2S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (A3S != null)
        {
            A3S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (B1S != null)
        {
            B1S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (B2S != null)
        {
            B2S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (B3S != null)
        {
            B3S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (C1S != null)
        {
            C1S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (C2S != null)
        {
            C2S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
        if (C3S != null)
        {
            C3S.transform.SetParent(SkillStonesBox._stonesTempContainer);
        }
    }
    
    void Parent()
    {
        RectTransform slotRT = A1T.GetComponent<RectTransform>();
        if (A1S != null)
        {
            A1S.transform.SetParent(A1T.transform);
            A1S.transform.localPosition = Vector3.zero;
            A1S.transform.localScale = Vector3.one;
            A1S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            A1S.gameObject.SetActive(true);
        }
        if (A2S != null)
        {
            A2S.transform.SetParent(A2T.transform);
            A2S.transform.localPosition = Vector3.zero;
            A2S.transform.localScale = Vector3.one;
            A2S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            A2S.gameObject.SetActive(true);
        }
        if (A3S != null)
        {
            A3S.transform.SetParent(A3T.transform);
            A3S.transform.localPosition = Vector3.zero;
            A3S.transform.localScale = Vector3.one;
            A3S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            A3S.gameObject.SetActive(true);
        }
        if (B1S != null)
        {
            B1S.transform.SetParent(B1T.transform);
            B1S.transform.localPosition = Vector3.zero;
            B1S.transform.localScale = Vector3.one;
            B1S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            B1S.gameObject.SetActive(true);
        }
        if (B2S != null)
        {
            B2S.transform.SetParent(B2T.transform);
            B2S.transform.localPosition = Vector3.zero;
            B2S.transform.localScale = Vector3.one;
            B2S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            B2S.gameObject.SetActive(true);
        }
        if (B3S != null)
        {
            B3S.transform.SetParent(B3T.transform);
            B3S.transform.localPosition = Vector3.zero;
            B3S.transform.localScale = Vector3.one;
            B3S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            B3S.gameObject.SetActive(true);
        }
        if (C1S != null)
        {
            C1S.transform.SetParent(C1T.transform);
            C1S.transform.localPosition = Vector3.zero;
            C1S.transform.localScale = Vector3.one;
            C1S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            C1S.gameObject.SetActive(true);
        }
        if (C2S != null)
        {
            C2S.transform.SetParent(C2T.transform);
            C2S.transform.localPosition = Vector3.zero;
            C2S.transform.localScale = Vector3.one;
            C2S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            C2S.gameObject.SetActive(true);
        }
        if (C3S != null)
        {
            C3S.transform.SetParent(C3T.transform);
            C3S.transform.localPosition = Vector3.zero;
            C3S.transform.localScale = Vector3.one;
            C3S.GetComponent<RectTransform>().sizeDelta = new Vector2(slotRT.sizeDelta.x,slotRT.sizeDelta.y);
            C3S.gameObject.SetActive(true);
        }
    }
}
