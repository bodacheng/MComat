using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine.UI;

public partial class GotchaResultLayer : UILayer
{
    public NineForShow NineForShow;
    
    #region 动画的跳过以及加速
    [SerializeField] Button Skip;
    [SerializeField] Button SpeedOnce;
    bool _starFalled;
    bool _oneStarFalled;
    Coroutine starFallAnimWholeProcess;
    Coroutine starFallAnimOneProcess;
    #endregion
    
    public static GotchaResultLayer Open()
    {
        UILayer l = UILayerLoader.Get("GotchaResultLayer");
        if (l != null)
        {
            return l as GotchaResultLayer;
        }
        
        l = UILayerLoader.Load(PreScene.target.T, "GotchaResultLayer");
        GotchaResultLayer layer = l as GotchaResultLayer;
        layer.Skip.onClick.AddListener(layer.SkipStarFallAnim);
        layer.SpeedOnce.onClick.AddListener(layer.SpeedOneGotchaAnim);
        layer.SetWaitPos();
        
        return layer;
    }

    public static void Close()
    {
        UILayerLoader.Remove("GotchaResultLayer");
    }
    
    void SetWaitPos()
    {
        waitPos.Clear();
        waitPos.Add(starWaitPos1);
        waitPos.Add(starWaitPos2);
        waitPos.Add(starWaitPos3);
        waitPos.Add(starWaitPos4);
        waitPos.Add(starWaitPos5);
        waitPos.Add(starWaitPos6);
        waitPos.Add(starWaitPos7);
        waitPos.Add(starWaitPos8);
        waitPos.Add(starWaitPos9);
    }
    
    // 清理相关特效等等
    public void Reset()
    {
        _starFalled = false;
        _oneStarFalled = false;
        foreach (var t in stoneFallingModels)
        {
            t.EnergyRessolve();
        }
        foreach (var t in stoneStartFlashModels)
        {
            t.EnergyRessolve();
        }
        stoneFallingModels.Clear();
        stoneStartFlashModels.Clear();
        ClearGotchaEffects();
    }
    
    void ClearGotchaEffects()
    {
        for (int i = 0; i < screenStarModels.Count; i++)
        {
            screenStarModels[i].EnergyRessolve();
        }
        for (int i = 0; i < screenStarExplosionModels.Count; i++)
        {
            screenStarExplosionModels[i].EnergyRessolve();
        }
        screenStarModels.Clear();
        screenStarExplosionModels.Clear();
    }
    
    // Gotcha总过程 点击画面的话进入下一个星星
    public IEnumerator GotchaAnimProcess(List<StoneOfPlayerInfo> results)
    {
        NineForShow.transform.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        SpeedOnce.gameObject.SetActive(true);
        Skip.gameObject.SetActive(true);
        starFallAnimWholeProcess = StartCoroutine (StarFallAnim(results));
        
        while(!_starFalled)
            yield return new WaitForSeconds(0.1f);
        
        SpeedOnce.gameObject.SetActive(false);
        Reset();
        Skip.gameObject.SetActive(false);
        
        PosDecide();
        StarSortAnim(results);
        yield return new WaitForSeconds(2f);
        
        string A1skillid = null, A2skillid= null, A3skillid= null, 
            B1skillid= null, B2skillid= null, B3skillid= null, 
            C1skillid= null, C2skillid= null, C3skillid= null;
        for (int i = 0; i < results.Count; i++)
        {
            switch(i)
            {
                case 0:
                    A1skillid = results[i].skillId;
                    break;
                case 1:
                    A2skillid = results[i].skillId;
                    break;
                case 2:
                    A3skillid = results[i].skillId;
                    break;
                case 3:
                    B1skillid = results[i].skillId;
                    break;
                case 4:
                    B2skillid = results[i].skillId;
                    break;
                case 5:
                    B3skillid = results[i].skillId;
                    break;
                case 6:
                    C1skillid = results[i].skillId;
                    break;
                case 7:
                    C2skillid = results[i].skillId;
                    break;
                case 8:
                    C3skillid = results[i].skillId;
                    break;
            }
        }
        
        NineForShow.ShowStones(A1skillid, A2skillid, A3skillid, B1skillid, B2skillid, B3skillid, C1skillid, C2skillid, C3skillid);
        NineForShow.transform.gameObject.SetActive(true);
    }
}
