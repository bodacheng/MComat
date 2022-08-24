using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UniRx;

public partial class PopupLayer : UILayer
{
    [SerializeField] TextMeshProUGUI percentageText;
    [SerializeField] Slider progressBar;
    
    float counter;
    
    // 「正在读取」画面
    public static void Loading(string description, GameObject hook, float curtainAlpha = 0.8f)
    {
        //RemoteWaitTooLongProcess(waitRemoteExtendTime);
        var layer = Open(hook);
        layer.DarkOff(curtainAlpha,0.5f);
        layer.info.text = description;
        layer.loadingIcon.SetActive(true);
    }

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
        layer.ValidationWindow.gameObject.SetActive(false);
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
}
