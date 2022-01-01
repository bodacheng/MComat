using UnityEngine;
using mainMenu;
using UnityEngine.UI;

public partial class GotchaResultLayer : UILayer
{
    public NineForShow NineForShow;
    
    #region 动画的跳过以及加速
    [SerializeField] Button Skip;
    [SerializeField] Button SpeedOnce;
    bool _starFallen;
    bool _oneStarFallen;
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
        var layer = UILayerLoader.Get("GotchaResultLayer");
        if (layer != null)
        {
            GotchaResultLayer gLayer = (GotchaResultLayer)layer;
            gLayer.Reset();
        }
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
        _starFallen = false;
        _oneStarFallen = false;
        ClearFallingStars();
        ClearGotchaEffects();
        ClearDetail();
    }

    void ClearFallingStars()
    {
        foreach (var t in stoneFallingModels)
        {
            t.EnergyRessolve();
        }
        stoneFallingModels.Clear();
        foreach (var t in stoneStartFlashModels)
        {
            t.EnergyRessolve();
        }
        stoneStartFlashModels.Clear();
    }
    
    void ClearGotchaEffects()
    {
        foreach (var t in screenStarModels)
        {
            t.EnergyRessolve();
        }
        foreach (var t in screenStarExplosionModels)
        {
            t.EnergyRessolve();
        }
        screenStarModels.Clear();
        screenStarExplosionModels.Clear();
    }
}
