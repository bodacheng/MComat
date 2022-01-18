using UnityEngine;
using UniRx;
using NoSuchStudio.UI.Highlight;

public partial class PopupLayer : UILayer {

    // canvasSortOrder = 100，这个数字无非是想让黑幕变成最上层，
    // 黑幕是靠缕空来实现空区域可点击。
    // PopupLayer 的 sortingOrder为更上一层101，
    // 因为popup出来的东西原则都是高亮
    
    #region 高亮显示

    public static void HighLightRect(GameObject T, RectTransform r, Options options = null)
    {
        var popupLayer = Open(T);
        popupLayer.HighLightRect(r, options);
    }
    
    async void HighLightRect(RectTransform r, Options options = null)
    {
        bigCurtain.raycastTarget = true;// 防止下面那点间隔里有点击画面的空间
        await Observable.TimerFrame(2);// 没有这个间隔ShowForUI的计算可能出错
        bigCurtain.raycastTarget = false;
        HighlightUI.ShowForUI( r, 
            options ?? new Options()
            {
                padding = new Padding(0,0,0,0),
                fadeDuration = 0.5f,
                dismissOnClick = false,
                color = new Color(0,0,0,0.7f),
                canvasSortOrder = 100
            }
        );
    }
    #endregion
    
    #region 黑幕

    public static void DarkOff(GameObject T, float darkness, float duration)
    {
        var popupLayer = Open(T);
        popupLayer.DarkOff(darkness, duration);
    }
    
    async void DarkOff(float darkness, float duration)
    {
        bigCurtain.raycastTarget = true;
        await Observable.TimerFrame(2);// 没有这个间隔ShowForUI的计算可能出错
        HighLightRect(empty.GetComponent<RectTransform>(), new Options()
        {
            padding = new Padding(0,0,0,0),
            fadeDuration = duration,
            dismissOnClick = false,
            color = new Color(0,0,0,darkness),
            canvasSortOrder = 100
        });
    }
    #endregion
}
