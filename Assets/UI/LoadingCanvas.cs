using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour {
    public Canvas Loading_Canvas;
    public Slider loadingBar;
    public Text processingDescrition;
    public Image LoadingCanvasBigCurtain;
    private IEnumerator loadingCanvasProcess;

    [Space(11)]
    [Header("Validation")]
    public RectTransform ValidationWindow;
    public Text ValidationIntro;
    public Button YesButton;
    public Button NoButton;

    [Space(11)]
    [Header("Mails")]
    public RectTransform MailWindow;
    public Button NewsButton;
    public Button PresentButton;
    
    [Space(11)]
    [Header("monsterboxfilter")]
    public RectTransform monsterboxfilter;

    void Start()
    {
        if (ValidationWindow)
        ValidationWindow.gameObject.SetActive(false);
        if (MailWindow)
        MailWindow.gameObject.SetActive(false);
    }

    public void OpenMailBox()
    {
        if (loadingCanvasProcess != null)
            StopCoroutine(loadingCanvasProcess);

        loadingCanvasProcess = LoadMailBox();
        StartCoroutine(loadingCanvasProcess);
    }

    public void CloseMailBox()
    {
        this.MailWindow.gameObject.SetActive(false);
        LightUp();
    }
    
    public void OpenMonsterBoxFilters()
    {
        if (loadingCanvasProcess != null)
            StopCoroutine(loadingCanvasProcess);
        loadingCanvasProcess = OpenMonsterBoxFilter();
        StartCoroutine(loadingCanvasProcess);
    }
    
    public void CloseMonsterBoxFilters()
    {
        this.monsterboxfilter.gameObject.SetActive(false);
        LightUp();
    }
    
    private IEnumerator OpenMonsterBoxFilter()
    {
        monsterboxfilter.gameObject.SetActive(true);
        monsterboxfilter.transform.SetSiblingIndex(4);
        yield return darkOffCanvas(0.5f);
    }

    private IEnumerator LoadMailBox()//实际读取邮件应该也是这个函数内部？
    {
        MailWindow.gameObject.SetActive(true);
        MailWindow.transform.SetSiblingIndex(4);
        yield return darkOffCanvas(0.5f);
    }

    public void nowProcess(string desription,float percent)
    {
        this.processingDescrition.text = desription;
        this.loadingBar.value = percent;
    }

    public void turnOnProcessDescription(bool _b)
    {
        this.loadingBar.gameObject.SetActive(_b);
        this.processingDescrition.gameObject.SetActive(_b);
    }

    public void LightUp()
    {
        if (loadingCanvasProcess != null)
            StopCoroutine(loadingCanvasProcess);

        loadingCanvasProcess = lightUpCanvas();
        StartCoroutine(loadingCanvasProcess);
    }

    public void DarkOff()
    {
        if (loadingCanvasProcess != null)
            StopCoroutine(loadingCanvasProcess);

        loadingCanvasProcess = darkOffCanvas(1);
        StartCoroutine(loadingCanvasProcess);
    }

    private IEnumerator lightUpCanvas()
    {
        Loading_Canvas.gameObject.SetActive(true);
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

    private IEnumerator darkOffCanvas(float toAlpha)
    {
        this.Loading_Canvas.gameObject.SetActive(true);
        this.Loading_Canvas.transform.SetSiblingIndex(3);
        float a = 0;
        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
        while (a < toAlpha)
        {
            a += 0.05f;
            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, a);
            yield return null;
        }
        yield break;
    }

    public void arrangeValiationWindow(UnityEngine.Events.UnityAction action, string intro)
    {
        this.Loading_Canvas.gameObject.SetActive(true);
        this.Loading_Canvas.transform.SetSiblingIndex(3);

        this.ValidationWindow.gameObject.SetActive(true);
        this.ValidationWindow.transform.SetSiblingIndex(4);

        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0.5f);

        UnityEngine.Events.UnityAction closeValidationWindow = () =>
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();

            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
        };

        this.YesButton.onClick.RemoveAllListeners();
        this.YesButton.onClick.AddListener(action);
        this.YesButton.onClick.AddListener(closeValidationWindow);

        this.NoButton.onClick.RemoveAllListeners();
        this.NoButton.onClick.AddListener(closeValidationWindow);
        ValidationIntro.text = intro;
    }

    public void arrangeValiationWindow(UnityEngine.Events.UnityAction action, UnityEngine.Events.UnityAction cancel_action, string intro)
    {
        this.Loading_Canvas.gameObject.SetActive(true);
        this.ValidationWindow.gameObject.SetActive(true);

        LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 0.5f);

        UnityEngine.Events.UnityAction closeValidationWindow = () =>
        {
            this.YesButton.onClick.RemoveAllListeners();
            this.NoButton.onClick.RemoveAllListeners();

            LoadingCanvasBigCurtain.color = new Color(LoadingCanvasBigCurtain.color.r, LoadingCanvasBigCurtain.color.g, LoadingCanvasBigCurtain.color.b, 1);
            this.ValidationWindow.gameObject.SetActive(false);
            this.Loading_Canvas.gameObject.SetActive(false);
        };

        this.YesButton.onClick.RemoveAllListeners();
        this.YesButton.onClick.AddListener(action);
        this.YesButton.onClick.AddListener(closeValidationWindow);

        this.NoButton.onClick.RemoveAllListeners();
        this.NoButton.onClick.AddListener(cancel_action);
        this.NoButton.onClick.AddListener(closeValidationWindow);
        ValidationIntro.text = intro;
    }
}
