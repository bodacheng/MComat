using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

// 该模块的最大待解决问题：
// Loading_Canvas 会在不同方面的功能下被打开或关闭，这会让对该画布内容的显示产生很多混乱

public partial class LoadingCanvas : MonoBehaviour {
    
    public static LoadingCanvas target;
    
    [SerializeField] HollowOutMask hollowOutMask;
    [SerializeField] Slider loadingBar;
    [SerializeField] Text processingDescrition;
    [SerializeField] Image bigCurtain;
    
    [Space(11)]
    [Header("Validation")]
    [SerializeField] RectTransform ValidationWindow;
    [SerializeField] RectTransform ValidationWindow_PosForMask;
    [SerializeField] Text ValidationIntro;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    
    void Awake()
    {
        target = this;
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
            bigCurtain.color = new Color(bigCurtain.color.r, bigCurtain.color.g, bigCurtain.color.b, a);
        }
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
