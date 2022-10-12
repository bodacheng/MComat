using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UniRx;
using DummyLayerSystem;
using NoSuchStudio.UI.Highlight;

public class ProgressLayer : UILayer
{
    [SerializeField] TextMeshProUGUI percentageText;
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] Image bigCurtain;
    
    static ProgressLayer Get()
    {
        ProgressLayer returnValue = null;
        var l = UILayerLoader.Get("ProgressLayer");
        if (l != null)
        {
            returnValue = l as ProgressLayer;
        }
        return returnValue;
    }
    
    public static ProgressLayer Open(GameObject T)
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(T,"ProgressLayer") as ProgressLayer;
        return returnValue;
    }
    
    // 「正在读取」画面
    public static void Loading(string description, GameObject hook, float curtainAlpha = 0.8f)
    {
        var layer = Open(hook);
        layer.DarkOff(curtainAlpha,0.5f);
        layer.info.text = description;
    }
    
    #region 黑幕
    void DarkOff(float darkness, float duration)
    {
        bigCurtain.raycastTarget = true;
        bigCurtain.DOColor(new Color(0,0,0, darkness), duration);
    }

    public static void LightUp(float duration)
    {
        var popupLayer = Get();
        if (popupLayer != null)
        {
            popupLayer.bigCurtain.DOColor(new Color(0,0,0, 0), duration).OnComplete(() =>
            {
                popupLayer.bigCurtain.raycastTarget = false;
                Close();
            });
        }
    }
    #endregion
    
    private static IDisposable current;
    // 带进度条的正在读取画面。不会主动打开新的popuplayer
    public static void LoadingPercent(string description, float progress, bool tween = true)
    {
        var layer = Get();
        if (layer == null)
        {
            return;
        }
        layer.info.text = description;
        layer.progressBar.gameObject.SetActive(true);
        layer.percentageText.text = ((int)(progress * 100)).ToString() + "%";
        
        if (current != null)
        {
            current.Dispose();
            current = null;
        }
        
        if (tween)
        {
            current = DOTween.To
            (
                () => layer.progressBar.value,
                (x) => layer.progressBar.value = x,
                progress,
                1f
            ).OnCompleteAsObservable().Subscribe(_ => 
            {
                Debug.Log("Percent Load Completed.");
            }).AddTo(layer.gameObject);
        }
        else
        {
            layer.progressBar.value = progress;
        }
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("ProgressLayer");
    }
}
