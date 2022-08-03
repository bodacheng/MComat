using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

public partial class PopupLayer : UILayer
{
    [SerializeField] Slider progressBar;
    
    static readonly CompositeDisposable disposables = new ();
    float counter;
    
    public static void Loading(string description, GameObject hook, float curtainAlpha = 0.8f)
    {
        //RemoteWaitTooLongProcess(waitRemoteExtendTime);
        var layer = Open(hook);
        layer.DarkOff(curtainAlpha,0.5f);
        layer.info.text = description;
        layer.loadingIcon.SetActive(true);
    }

    public static void LoadingPercent(string description, GameObject hook, float progress)
    {
        Loading(description, hook, 1f);
        var layer = Get();
        layer.progressBar.gameObject.SetActive(true);
        DOTween.To
        (
            () => layer.progressBar.value,
            (x) => layer.progressBar.value = x,
            progress,
            1f
        );
    }
}
