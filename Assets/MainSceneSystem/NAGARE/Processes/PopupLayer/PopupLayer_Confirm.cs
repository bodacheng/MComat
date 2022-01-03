using UniRx;
using UnityEngine;
using UnityEngine.UI;

public partial class PopupLayer : UILayer {
    
    [Header("Validation")]
    [SerializeField] RectTransform ValidationWindow;
    [SerializeField] Text ValidationIntro;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    
    /// <summary>
    /// 闪一下就关闭的提示窗口
    /// </summary>
    /// <param name="intro"></param>
    public void ArrangeWarnWindow(string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HighLightRect(ValidationWindow.GetComponent<RectTransform>());
        
        YesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
        ValidationIntro.text = intro;
        
        async void closeWindow()
        {
            await Observable.TimerFrame(10);
            Close();
        }
        closeWindow();
    }
    
    public void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HighLightRect(ValidationWindow.GetComponent<RectTransform>());
        
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        
        YesButton.onClick.RemoveAllListeners();
        YesButton.onClick.AddListener(action);
        YesButton.onClick.AddListener(Close);
        
        NoButton.onClick.RemoveAllListeners();
        NoButton.onClick.AddListener(Close);
        ValidationIntro.text = intro;
    }
    
    public void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, UnityEngine.Events.UnityAction cancel_action, string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HighLightRect(ValidationWindow.GetComponent<RectTransform>());
        
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        
        YesButton.onClick.RemoveAllListeners();
        YesButton.onClick.AddListener(action);
        YesButton.onClick.AddListener(Close);
        
        NoButton.onClick.RemoveAllListeners();
        NoButton.onClick.AddListener(cancel_action);
        NoButton.onClick.AddListener(Close);
        
        ValidationIntro.text = intro;
    }
}
