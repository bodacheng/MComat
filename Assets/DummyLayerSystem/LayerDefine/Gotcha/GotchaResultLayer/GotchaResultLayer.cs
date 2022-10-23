using DummyLayerSystem;
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
    
    public void Setup()
    {
        Skip.onClick.AddListener(SkipStarFallAnim);
        SpeedOnce.onClick.AddListener(SpeedOneGotchaAnim);
        SetWaitPos();
    }

    public static void Close()
    {
        var layer = UILayerLoader.Get<GotchaResultLayer>();
        if (layer != null)
        {
            layer.Reset();
        }
        UILayerLoader.Remove<GotchaResultLayer>();
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
