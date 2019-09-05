using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LoadingCanvas 可以存在很多别的丰富的功能，比如播放视频？用于loading画面？
public class LoadingCanvas : MonoBehaviour {

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

    [Space(11)]
    [Header("Mails")]
    public RectTransform MailWindow;
    
    [Space(11)]
    [Header("monsterboxfilter")]
    public RectTransform monsterboxfilter;
    
    [Space(11)]
    [Header("Settings")]
    public RectTransform SettingRectT;
    
    //进程类
    private IEnumerator MenuProcess;
    private bool processEnded = false;
    private float processTime = 0;
    private void setProcessStartEnd(bool a)
    {
        processEnded = a;
    }
    public void triggerMainProcess(IEnumerator _process)
    {
        StartCoroutine(this.MainProcess(_process));
    }
    private IEnumerator giveProcessStartEndFlag(IEnumerator _process)
    {
        setProcessStartEnd(false);
        yield return _process;
        setProcessStartEnd(true);
    }
    private IEnumerator MainProcess(IEnumerator _process)//这个函数是供外界调用的。
    {
        if (MenuProcess != null)
        {
            while (!processEnded)
            {
                processTime += 0.01f;
                if (processTime > 5f)
                {
                    Debug.Log("进程超时.");
                    StopCoroutine(MenuProcess);
                    break;
                }
                yield return null;
            };
        }
        processTime = 0;
        MenuProcess = giveProcessStartEndFlag(_process);
        yield return MenuProcess;
    }
    
    public void turnOnSettings()
    {
        DarkOff(0.5f);
        SettingRectT.transform.SetSiblingIndex(4);
        SettingRectT.gameObject.SetActive(true);
    }
    
    public void turnOffSettings()
    {
        LightUp();
        SettingRectT.gameObject.SetActive(false);
    }
    
    public void OpenMailBox()
    {
        triggerMainProcess(LoadMailBox());
    }

    public void CloseMailBox()
    {
        this.MailWindow.gameObject.SetActive(false);
        LightUp();
    }
    
    public void OpenMonsterBoxFilters()
    {
        triggerMainProcess(OpenMonsterBoxFilter());
    }
    
    public void CloseMonsterBoxFilters()
    {
        this.monsterboxfilter.gameObject.SetActive(false);
        LightUp();
    }
    
    private IEnumerator OpenMonsterBoxFilter()
    {
        monsterboxfilter.gameObject.SetActive(true);
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
        triggerMainProcess(lightUpCanvas());
    }

    public void DarkOff(float darkness)
    {
        triggerMainProcess(darkOffCanvas(darkness));
    }

    private IEnumerator lightUpCanvas()
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
