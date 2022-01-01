using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public partial class PopupLayer : UILayer {
    
    [SerializeField] HollowOutMask hollowOutMask;
    [SerializeField] Slider loadingBar;
    [SerializeField] Text processingDescrition;
    [SerializeField] Image bigCurtain;
    
    [Header("Validation")]
    [SerializeField] RectTransform ValidationWindow;
    [SerializeField] RectTransform ValidationWindow_PosForMask;
    [SerializeField] Text ValidationIntro;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    
    public static PopupLayer Open(GameObject T)
    {
        PopupLayer returnValue;
        UILayer l = UILayerLoader.Get("PopupLayer");
        if (l != null)
        {
            returnValue = l as PopupLayer;
            return returnValue;
        }
        returnValue = UILayerLoader.Load(T,"PopupLayer") as PopupLayer;
        returnValue.transform.SetSiblingIndex(0);
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("PopupLayer");
    }
    
    #region 进度条
    public void NowProcess(string desription, float percent)
    {
        processingDescrition.text = desription;
        loadingBar.value = percent;
    }
    
    public void TurnOnProcessDescription(bool _b)
    {
        loadingBar.gameObject.SetActive(_b);
        processingDescrition.gameObject.SetActive(_b);
    }
    
    #endregion
    
    #region 黑幕
    public void DarkOffDirectly(float darkness)
    {
        bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, darkness);
    }
    
    public async void LightUp()
    {
        float a = 1;
        bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, a);
        while (a > 0)
        {
            a -= 0.05f;
            await Task.Delay(1);
            if (bigCurtain == null)
            {
                Close();
                return;
            }
            bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, a);
        }
        Close();
    }
    
    public async void DarkOff(float toAlpha)
    {
        float a = 0;
        bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, a);
        while (a < toAlpha)
        {
            a += Time.deltaTime;
            await Task.Delay(1);
        }
    }
    #endregion
    
    #region 浮动窗口
    public void ArrangeWarnWindow(string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HigtLightRect(ValidationWindow_PosForMask.transform);
        
        YesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
        
        ValidationIntro.text = intro;
        
        async void closeWindow()
        {
            await Task.Delay(1000);
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
        ClearHigtLight();
    }
    
    public void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        ValidationWindow.gameObject.SetActive(true);
        HigtLightRect(ValidationWindow_PosForMask.transform);
        
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
        HigtLightRect(ValidationWindow_PosForMask.transform);
        
        void closeValidationWindow()
        {
            YesButton.onClick.RemoveAllListeners();
            NoButton.onClick.RemoveAllListeners();
            bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, 0);
            ValidationWindow.gameObject.SetActive(false);
            ClearHigtLight();
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
    #endregion
}
