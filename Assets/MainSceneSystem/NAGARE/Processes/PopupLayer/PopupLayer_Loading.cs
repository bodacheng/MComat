using System;

public partial class PopupLayer : UILayer {
    
    public void Loading(string desription)
    {
        DarkOff(0.8f,0.5f);
        info.text = desription;
        loadingIcon.SetActive(true);
        _canvas.sortingOrder = 101;// 比黑幕的100多1，确保loadingIcon高亮显示
    }
    
    public void LoadingFinished()
    {
        bigCurtain.raycastTarget = false;
        loadingIcon.SetActive(false);
        info.text = String.Empty;
        ClearHighLight();
        Close();
    }
}
