using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public partial class NineForShow : MonoBehaviour
{
    public Button A1T, A2T, A3T, B1T, B2T, B3T, C1T, C2T, C3T;
    
    public IEnumerator ShowStones(string A1skillid, string A2skillid, string A3skillid,
                                    string B1skillid, string B2skillid, string B3skillid,
                                        string C1skillid, string C2skillid, string C3skillid)
    {
        IEnumerator getA1 = SkillStonesBox.GenerateStoneMode(A1skillid);
        IEnumerator getA2 = SkillStonesBox.GenerateStoneMode(A2skillid);
        IEnumerator getA3 = SkillStonesBox.GenerateStoneMode(A3skillid);
        IEnumerator getB1 = SkillStonesBox.GenerateStoneMode(B1skillid);
        IEnumerator getB2 = SkillStonesBox.GenerateStoneMode(B2skillid);
        IEnumerator getB3 = SkillStonesBox.GenerateStoneMode(B3skillid);
        IEnumerator getC1 = SkillStonesBox.GenerateStoneMode(C1skillid);
        IEnumerator getC2 = SkillStonesBox.GenerateStoneMode(C2skillid);
        IEnumerator getC3 = SkillStonesBox.GenerateStoneMode(C3skillid);
        
        yield return getA1;
        yield return getA2;
        yield return getA3;
        yield return getB1;
        yield return getB2;
        yield return getB3;
        yield return getC1;
        yield return getC2;
        yield return getC3;
        
        SKStoneItem A1S = getA1.Current != null ? (SKStoneItem)getA1.Current : null;
        SKStoneItem A2S = getA2.Current != null ? (SKStoneItem)getA2.Current : null;
        SKStoneItem A3S = getA3.Current != null ? (SKStoneItem)getA3.Current : null;
        SKStoneItem B1S = getB1.Current != null ? (SKStoneItem)getB1.Current : null;
        SKStoneItem B2S = getB2.Current != null ? (SKStoneItem)getB2.Current : null;
        SKStoneItem B3S = getB3.Current != null ? (SKStoneItem)getB3.Current : null;
        SKStoneItem C1S = getC1.Current != null ? (SKStoneItem)getC1.Current : null;
        SKStoneItem C2S = getC2.Current != null ? (SKStoneItem)getC2.Current : null;
        SKStoneItem C3S = getC3.Current != null ? (SKStoneItem)getC3.Current : null;

        Parent(A1S,A2S,A3S,B1S,B2S,B3S,C1S,C2S,C3S);
    }
        
    void Parent(SKStoneItem A1S, SKStoneItem A2S, SKStoneItem A3S, SKStoneItem B1S, SKStoneItem B2S, SKStoneItem B3S, SKStoneItem C1S, SKStoneItem C2S, SKStoneItem C3S)
    {
        if (A1S != null)
        {
            A1S.transform.SetParent(A1T.transform);
            A1S.transform.localPosition = Vector3.zero;
            A1S.transform.localScale = Vector3.one;
            A1S.gameObject.SetActive(true);
        }
        if (A2S != null)
        {
            A2S.transform.SetParent(A2T.transform);
            A2S.transform.localPosition = Vector3.zero;
            A2S.transform.localScale = Vector3.one;
            A2S.gameObject.SetActive(true);
        }
        if (A3S != null)
        {
            A3S.transform.SetParent(A3T.transform);
            A3S.transform.localPosition = Vector3.zero;
            A3S.transform.localScale = Vector3.one;
            A3S.gameObject.SetActive(true);
        }
        if (B1S != null)
        {
            B1S.transform.SetParent(B1T.transform);
            B1S.transform.localPosition = Vector3.zero;
            B1S.transform.localScale = Vector3.one;
            B1S.gameObject.SetActive(true);
        }
        if (B2S != null)
        {
            B2S.transform.SetParent(B2T.transform);
            B2S.transform.localPosition = Vector3.zero;
            B2S.transform.localScale = Vector3.one;
            B2S.gameObject.SetActive(true);
        }
        if (B3S != null)
        {
            B3S.transform.SetParent(B3T.transform);
            B3S.transform.localPosition = Vector3.zero;
            B3S.transform.localScale = Vector3.one;
            B3S.gameObject.SetActive(true);
        }
        if (C1S != null)
        {
            C1S.transform.SetParent(C1T.transform);
            C1S.transform.localPosition = Vector3.zero;
            C1S.transform.localScale = Vector3.one;
            C1S.gameObject.SetActive(true);
        }
        if (C2S != null)
        {
            C2S.transform.SetParent(C2T.transform);
            C2S.transform.localPosition = Vector3.zero;
            C2S.transform.localScale = Vector3.one;
            C2S.gameObject.SetActive(true);
        }
        if (C3S != null)
        {
            C3S.transform.SetParent(C3T.transform);
            C3S.transform.localPosition = Vector3.zero;
            C3S.transform.localScale = Vector3.one;
            C3S.gameObject.SetActive(true);
        }
    }
}
