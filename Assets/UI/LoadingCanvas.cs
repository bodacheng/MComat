using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 该模块的最大待解决问题：
// Loading_Canvas 会在不同方面的功能下被打开或关闭，这会让对该画布内容的显示产生很多混乱

public class LoadingCanvas : MonoBehaviour {

    public static LoadingCanvas target;
    
    public HollowOutMask hollowOutMask;
    public Canvas Loading_Canvas;
    public Slider loadingBar;
    public Text processingDescrition;
    public Image LoadingCanvasBigCurtain;
    
    [Space(11)]
    [Header("Validation")]
    public RectTransform ValidationWindow;
    public RectTransform ValidationWindow_PosForMask;
    public Text ValidationIntro;
    public Button YesButton;
    public Button NoButton;
    
    [Space(7)]
    [Header("主进程处理器")]
    public SingleThreadProcesser mainProcessRunner;
    
    void Awake()
    {
        target = this;
    }
    
    public void NowProcess(string desription,float percent)
    {
        processingDescrition.text = desription;
        loadingBar.value = percent;
    }
    
    public void TurnOnProcessDescription(bool _b)
    {
        loadingBar.gameObject.SetActive(_b);
        processingDescrition.gameObject.SetActive(_b);
    }

    #region 高亮显示
    /// <summary>
    // 注：参数列表内的第一个(Ts[0])元素是会高亮显示，而其他transform对应的区域并不会高亮显示，但也会缕空
    // 我们曾经尝试让复数个对象区域都高亮度显示，但失败了。
    /// </summary>
    public void HigtLightRect(List<Transform> Ts)
    {
        hollowOutMask.gameObject.SetActive(true);
        if (LoadingCanvasBigCurtain != null)
        {
            LoadingCanvasBigCurtain.color = Color.clear;
        }
        //Loading_Canvas.sortingOrder = 1;
        hollowOutMask.raycastTarget = true;
        List<RectTransform> rectTransforms = new List<RectTransform>();
        for (int i = 0; i < Ts.Count; i++)
        {
            rectTransforms.Add(Ts[i].GetComponent<RectTransform>());
        }
        hollowOutMask.SetTarget(rectTransforms);
        hollowOutMask.color = new Color(0, 0, 0, 0.6f);
    }
    
    public void HigtLightRect(Transform _Transform)
    {
        hollowOutMask.gameObject.SetActive(true);
        if (LoadingCanvasBigCurtain != null)
            LoadingCanvasBigCurtain.color = Color.clear;
        //Loading_Canvas.sortingOrder = 1;
        hollowOutMask.raycastTarget = true;
        hollowOutMask.SetTarget(new List<RectTransform>{ _Transform.GetComponent<RectTransform>() });
        hollowOutMask.color = new Color(0, 0, 0, 0.6f);
    }
    
    public void ClearHigtLight()
    {
        hollowOutMask.SetTarget(null);
        hollowOutMask.color = Color.clear;
        hollowOutMask.gameObject.SetActive(false);
    }
    #endregion

    #region 黑幕
    public void LightUp()
    {
        mainProcessRunner.Run(LightUpCanvas());
    }
    
    public void DarkOff(float darkness)
    {
        mainProcessRunner.Run(DarkOffCanvas(darkness));
    }
    
    public void DarkOffDirectly(float darkness)
    {
        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, darkness);
    }
    
    IEnumerator LightUpCanvas()
    {
        float a = 1;
        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
        while (a > 0)
        {
            a -= 0.05f;
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
            yield return null;
        }
        Debug.Log(LoadingCanvasBigCurtain.color);
    }
    
    IEnumerator DarkOffCanvas(float toAlpha)
    {
        float a = 0;
        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
        while (a < toAlpha)
        {
            a += Time.deltaTime;
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
            yield return null;
        }
        yield break;
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
        
        IEnumerator closeWindow()
        {
            yield return new WaitForSeconds(1f);
            CloseValidationWindow();
        }
        mainProcessRunner.Run(closeWindow());
    }
    
    void CloseValidationWindow()
    {
        YesButton.onClick.RemoveAllListeners();
        NoButton.onClick.RemoveAllListeners();
        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0);
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
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0);
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
