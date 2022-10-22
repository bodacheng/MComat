using DG.Tweening;
using DummyLayerSystem;
using UnityEngine;
using NoSuchStudio.UI.Highlight;
using UniRx;
using UnityEngine.UI;

public class HighLightLayer : UILayer
{
    [SerializeField] Image bigCurtain;
    
    // canvasSortOrder = 100，这个数字无非是想让黑幕变成最上层，
    // 黑幕是靠缕空来实现空区域可点击。
    // PopupLayer 的 sortingOrder为更上一层101，
    // 因为popup出来的东西原则都是高亮
    
    // 这个模块的黑幕系统分两种，一种是部分区域高亮显示，一种是全屏颜色
    // 之所以不得不分两种是因为那么傻逼插件在计算高亮区域的时候不得不延迟两帧，否则计算不对
    // 从而有些需要立刻让全屏变色的地方我们要准备另外一套
    
    static HighLightLayer Get()
    {
        HighLightLayer returnValue = null;
        var l = UILayerLoader.Get<HighLightLayer>();
        if (l != null)
        {
            returnValue = l as HighLightLayer;
        }
        return returnValue;
    }
    
    public static HighLightLayer Open(GameObject T)
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(T,"HighLightLayer") as HighLightLayer;
        return returnValue;
    }
    
    #region 高亮显示
    public static void HighLightRect(GameObject T, RectTransform r, Options options = null)
    {
        var HighLightLayer = Open(T);
        HighLightLayer.HighLightRect(r, options);
    }
    
    async void HighLightRect(RectTransform r, Options options = null)
    {
        bigCurtain.raycastTarget = true; // 防止下面那点间隔里有点击画面的空间
        await Observable.TimerFrame(2); // 没有这个间隔ShowForUI的计算可能出错
        bigCurtain.raycastTarget = false;
        
        if (r != null)
        {
            HighlightUI.ShowForUI(r,
                options ?? new Options()
                {
                    padding = new Padding(0,0,0,0),
                    fadeDuration = 0.5f,
                    dismissOnClick = false,
                    color = new Color(1,1,1,0f),
                    canvasSortOrder = 100
                }
            );
        }
    }
    #endregion
    
    #region 黑幕
    public static void DarkOff(GameObject T, float darkness, float duration)
    {
        var HighLightLayer = Open(T);
        HighLightLayer.DarkOff(darkness, duration);
    }
    
    void DarkOff(float darkness, float duration)
    {
        bigCurtain.raycastTarget = true;
        bigCurtain.DOColor(new Color(0,0,0, darkness), duration);
    }
    
    public static void LightUp(float duration)
    {
        var HighLightLayer = Get();
        if (HighLightLayer != null)
        {
            HighLightLayer.bigCurtain.DOColor(new Color(0,0,0, 0), duration).OnComplete(() =>
            {
                HighLightLayer.bigCurtain.raycastTarget = false;
                Close();
            });
        }
    }
    
    #endregion
    
    public static void Close()
    {
        HighlightUI.Dismiss();
        UILayerLoader.Remove("HighLightLayer");
    }
}
