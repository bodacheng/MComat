using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;

public partial class PopupLayer : UILayer {
    
    [Header("Validation")]
    [SerializeField] RectTransform ValidationWindow;
    [SerializeField] Text ValidationIntro;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;

    private static readonly Color windowBgColor = new Color(0,0,0,0.5f);
    
    /// <summary>
    /// 闪一下就关闭的提示窗口
    /// </summary>
    /// <param name="intro"></param>
    public static void ArrangeWarnWindow(string intro)
    {
        var layer = UILayerLoader.Load<PopupLayer>(true);
        
        layer.bigCurtain.color = windowBgColor;
        layer.ValidationWindow.gameObject.SetActive(true);
        
        layer.YesButton.gameObject.SetActive(true);
        layer.NoButton.gameObject.SetActive(false);
        layer.ValidationIntro.text = intro;
        layer.YesButton.onClick.AddListener(Close);
    }
    
    public static void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        var layer = UILayerLoader.Load<PopupLayer>(true);
        
        layer.bigCurtain.color = windowBgColor;
        layer.ValidationWindow.gameObject.SetActive(true);

        layer.YesButton.gameObject.SetActive(true);
        layer.NoButton.gameObject.SetActive(true);
        
        layer.YesButton.onClick.RemoveAllListeners();
        layer.YesButton.onClick.AddListener(Close);
        layer.YesButton.onClick.AddListener(action);
        
        layer.NoButton.onClick.RemoveAllListeners();
        layer.NoButton.onClick.AddListener(Close);
        
        layer.ValidationIntro.text = intro;
    }
    
    public static void ArrangeConfirmWindow(UnityEngine.Events.UnityAction action, UnityEngine.Events.UnityAction cancel_action, string intro)
    {
        var layer = UILayerLoader.Load<PopupLayer>(true);
        
        layer.bigCurtain.color = windowBgColor;
        layer.ValidationWindow.gameObject.SetActive(true);

        layer.YesButton.gameObject.SetActive(true);
        layer.NoButton.gameObject.SetActive(true);
        
        layer.YesButton.onClick.RemoveAllListeners();
        layer.YesButton.onClick.AddListener(Close);
        layer.YesButton.onClick.AddListener(action);
        
        layer.NoButton.onClick.RemoveAllListeners();
        layer.NoButton.onClick.AddListener(Close);
        layer.NoButton.onClick.AddListener(cancel_action);
        
        layer.ValidationIntro.text = intro;
    }
}
