using UnityEngine;
using UnityEngine.UI;
using FightScene;

// 战斗暂停相关。从暂停界面可以跳转至Setting界面，因此两个模块靠OptionsButton连接在一起
public class FightScenePauseSupport : MonoBehaviour
{
    [Space(11)]
    [Header("Settings")]
    public SettingLayer settingLayer;
    
    [Space(11)]
    [Header("暂停菜单里的Resume")]
    public Button ResumeButton;
    
    [Space(11)]
    [Header("暂停菜单里的Return")]
    public Button ReturnButton;
    
    [Space(11)]
    [Header("暂停菜单里的Options")]
    public Button OptionsButton;
    
    [Space(11)]
    [Header("战斗场景下点击暂停时弹出菜单的RectTransform")]
    public Canvas PauseMenu;
    
    [Space(11)]
    [Header("战斗界面canvas")]
    public Canvas FightCanvas, ControlCanvas;
    
    public static FightScenePauseSupport target;

    void Awake()
    {
        target = this;
    }

    void Start()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(FightOverControl.target.ReturnToFront);
        
        ResumeButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.AddListener(ResumeScene);
        
        //OptionsButton.onClick.RemoveAllListeners();
        //OptionsButton.onClick.AddListener(settingLayer.Open);
        OptionsButton.onClick.AddListener(JumpToOptions);
    }
    
    // 按钮函数，至于战斗界面暂停按钮之上
    public void PauseScene()
    {
        PauseMenu.gameObject.SetActive(true);
        FightCanvas.gameObject.SetActive(false);
        ControlCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    // 本地系函数 而且目前有逻辑问题
    public void ResumeScene()
    {
        PauseMenu.gameObject.SetActive(false);
        FightCanvas.gameObject.SetActive(true);
        ControlCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    
    void JumpToOptions()
    {
        PauseMenu.gameObject.SetActive(false);
    }
}
