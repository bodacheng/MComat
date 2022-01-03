using UnityEngine;
using UniRx;
using NoSuchStudio.UI.Highlight;

public partial class PopupLayer : UILayer {

    // canvasSortOrder = 100，这个数字无非是想让黑幕变成最上层，
    // 黑幕是靠缕空来实现空区域可点击。
    // PopupLayer 有时候会把自身sortingOrder变成更上一层101，
    // 目的是为了高亮显示那个读取信息中的图标
    
    #region 高亮显示
    public async void HighLightRect(RectTransform r, Options options = null)
    {
        bigCurtain.raycastTarget = true;
        await Observable.TimerFrame(2);// 没有这个间隔ShowForUI的计算可能出错
        bigCurtain.raycastTarget = false;
        HighlightUI.ShowForUI( r, 
            options ?? new Options()
            {
                padding = new Padding(0,0,0,0),
                dismissOnClick = false,
                color = new Color(0,0,0,0.7f),
                canvasSortOrder = 100
            }
        );
    }
    #endregion
    
    #region 黑幕
    public async void DarkOff(float darkness, float duration)
    {
        bigCurtain.raycastTarget = true;
        await Observable.TimerFrame(2);// 没有这个间隔ShowForUI的计算可能出错
        HighLightRect(empty.GetComponent<RectTransform>(), new Options()
        {
            fadeDuration = duration,
            dismissOnClick = false,
            color = new Color(0,0,0,darkness),
            canvasSortOrder = 100
        });
    }
    #endregion
    
    public void ClearHighLight()
    {
        HighlightUI.Dismiss();
    }
}
