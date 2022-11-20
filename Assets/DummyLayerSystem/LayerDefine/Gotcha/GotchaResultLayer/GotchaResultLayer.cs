using DummyLayerSystem;
using UnityEngine;
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
            Destroy(t.gameObject);
        }
        stoneFallingModels.Clear();
        foreach (var t in stoneStartFlashModels)
        {
            Destroy(t.gameObject);
        }
        stoneStartFlashModels.Clear();
    }
    
    void ClearGotchaEffects()
    {
        foreach (var t in screenStarModels)
        {
            Destroy(t.gameObject);
        }
        foreach (var t in screenStarExplosionModels)
        {
            Destroy(t.gameObject);
        }
        screenStarModels.Clear();
        screenStarExplosionModels.Clear();
    }
}
