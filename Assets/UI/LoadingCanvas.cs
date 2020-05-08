using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// LoadingCanvas 可以存在很多别的丰富的功能，比如播放视频？用于loading画面？
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
    
    //进程类
    [Space(7)]
    [Header("主进程处理器")]
    public SingleThreadProcesser mainProcessRunner;
    
    void Start()
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
    
    /// <summary>
    // 注：参数列表内的第一个(Ts[0])元素是会高亮显示，而其他transform对应的区域并不会高亮显示，但也会缕空
    // 我们曾经尝试让复数个对象区域都高亮度显示，但失败了。
    /// </summary>
    public void HigtLightRect(List<Transform> Ts)
    {
        hollowOutMask.gameObject.SetActive(true);
        if (LoadingCanvasBigCurtain != null)
            LoadingCanvasBigCurtain.color = Color.clear;
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
        Loading_Canvas.gameObject.SetActive(false);
        hollowOutMask.color = Color.clear;
        hollowOutMask.gameObject.SetActive(false);
    }
    
    public void LightUp()
    {
        mainProcessRunner.Run(LightUpCanvas());
    }
    
    public void DarkOff(float darkness)
    {
        mainProcessRunner.Run(DarkOffCanvas(darkness));
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
        Loading_Canvas.gameObject.SetActive(false);
        yield break;
    }

    IEnumerator DarkOffCanvas(float toAlpha)
    {
        Loading_Canvas.gameObject.SetActive(true);
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

    public void ArrangeValiationWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        this.Loading_Canvas.gameObject.SetActive(true);
        this.ValidationWindow.gameObject.SetActive(true);
        HigtLightRect(this.ValidationWindow_PosForMask.transform);
        
        void closeValidationWindow()
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();
        
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
            ClearHigtLight();
        }
        
        this.YesButton.onClick.RemoveAllListeners();
        this.YesButton.onClick.AddListener(action);
        this.YesButton.onClick.AddListener(closeValidationWindow);
        
        this.NoButton.onClick.RemoveAllListeners();
        this.NoButton.onClick.AddListener(closeValidationWindow);
        ValidationIntro.text = intro;
    }
    
    public void ArrangeValiationWindow(UnityEngine.Events.UnityAction action, UnityEngine.Events.UnityAction cancel_action, string intro)
    {
        this.Loading_Canvas.gameObject.SetActive(true);
        this.ValidationWindow.gameObject.SetActive(true);
        HigtLightRect(this.ValidationWindow_PosForMask.transform);
        
        void closeValidationWindow()
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();
        
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
            ClearHigtLight();
        }
        
        this.YesButton.onClick.RemoveAllListeners();
        this.YesButton.onClick.AddListener(action);
        this.YesButton.onClick.AddListener(closeValidationWindow);
        
        this.NoButton.onClick.RemoveAllListeners();
        this.NoButton.onClick.AddListener(cancel_action);
        this.NoButton.onClick.AddListener(closeValidationWindow);
        ValidationIntro.text = intro;
    }
}
