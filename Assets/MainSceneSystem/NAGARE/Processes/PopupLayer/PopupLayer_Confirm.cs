using UniRx;
using UnityEngine;

public partial class PopupLayer : UILayer {
    
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
            CloseValidationWindow();
            Close();
        }
        closeWindow();
    }
    
    void CloseValidationWindow()
    {
        YesButton.onClick.RemoveAllListeners();
        NoButton.onClick.RemoveAllListeners();
        bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, 0);
        ValidationWindow.gameObject.SetActive(false);
        ClearHighLight();
    }
    
    public void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HighLightRect(ValidationWindow.GetComponent<RectTransform>());
        
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        
        YesButton.onClick.RemoveAllListeners();
        YesButton.onClick.AddListener(action);
        YesButton.onClick.AddListener(CloseValidationWindow);
        
        NoButton.onClick.RemoveAllListeners();
        NoButton.onClick.AddListener(CloseValidationWindow);
        ValidationIntro.text = intro;
    }
    
    public void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, UnityEngine.Events.UnityAction cancel_action, string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HighLightRect(ValidationWindow.GetComponent<RectTransform>());
        
        void closeValidationWindow()
        {
            YesButton.onClick.RemoveAllListeners();
            NoButton.onClick.RemoveAllListeners();
            bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, 0);
            ValidationWindow.gameObject.SetActive(false);
            ClearHighLight();
            Close();
        }
        
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        
        YesButton.onClick.RemoveAllListeners();
        YesButton.onClick.AddListener(action);
        YesButton.onClick.AddListener(closeValidationWindow);
        
        NoButton.onClick.RemoveAllListeners();
        NoButton.onClick.AddListener(cancel_action);
        NoButton.onClick.AddListener(closeValidationWindow);
        ValidationIntro.text = intro;
    }
}
