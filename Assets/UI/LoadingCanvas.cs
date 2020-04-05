using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    
    public void HigtLightRect(Transform _Transform)
    {
        Loading_Canvas.gameObject.SetActive(true);
        if (LoadingCanvasBigCurtain != null)
            LoadingCanvasBigCurtain.color = Color.clear;
        //Loading_Canvas.sortingOrder = 1;
        hollowOutMask.SetTarget(_Transform.GetComponent<RectTransform>());
    }
    
    public void ClearHigtLight()
    {
        hollowOutMask.SetTarget(null);
        Loading_Canvas.gameObject.SetActive(false);
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

        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0.5f);

        void closeValidationWindow()
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();

            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
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

        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0.5f);

        void closeValidationWindow()
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();

            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
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
