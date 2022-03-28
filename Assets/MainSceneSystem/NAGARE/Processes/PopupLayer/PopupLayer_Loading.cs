using System;
using mainMenu;
using UnityEngine;
using UniRx;
public partial class PopupLayer : UILayer
{
    private static float waitRemoteExtendTime = 5f;
    static readonly CompositeDisposable disposables = new CompositeDisposable();
    private float counter;
    
    static void RemoteWaitTooLongProcess(float CountTime)
    {
        Observable.Timer(TimeSpan.FromSeconds(CountTime), Scheduler.MainThreadIgnoreTimeScale).Subscribe(_ =>
        {
            PreScene.ReturnToLobby("通讯错误, 返回主屏幕");
        }).AddTo(disposables);                                 
    }
    
    public static void Loading(string description, GameObject hook)
    {
        RemoteWaitTooLongProcess(waitRemoteExtendTime);
        var layer = Open(hook);
        layer.DarkOff(0.8f,0.5f);
        layer.info.text = description;
        layer.loadingIcon.SetActive(true);
    }
}
