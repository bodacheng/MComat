using UnityEngine;
using UnityEngine.UI;
using FightScene;

public class FightSceneSettingSupport : MonoBehaviour
{
    [Space(11)]
    [Header("Settings")]
    public Setting setting;

    [Space(11)]
    [Header("ResumeButton")]
    public Button ResumeButton;
    
    [Space(11)]
    [Header("ReturnButton")]
    public Button ReturnButton;
    
    [Space(11)]
    [Header("OptionsButton")]
    public Button OptionsButton;
    
    [Space(11)]
    [Header("战斗场景下点击暂停时弹出菜单的RectTransform")]
    public Canvas PauseMenu;
    
    [Space(11)]
    [Header("战斗界面canvas")]
    public Canvas FightCanvas, ControlCanvas;
    
    void Start()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(NetFightScene.target.ReturnToFront);
        
        ResumeButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.AddListener(ResumeScene);
        
        OptionsButton.onClick.RemoveAllListeners();
        OptionsButton.onClick.AddListener(setting.Open);
        OptionsButton.onClick.AddListener(JumpToOptions);
    }

    // 本地系函数 而且目前有逻辑问题
    public void ResumeScene()
    {
        PauseMenu.gameObject.SetActive(false);
        FightCanvas.gameObject.SetActive(true);
        ControlCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    
    // 按钮函数，至于战斗界面暂停按钮之上
    public void PauseScene()
    {
        PauseMenu.gameObject.SetActive(true);
        FightCanvas.gameObject.SetActive(false);
        ControlCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    
    void JumpToOptions()
    {
        PauseMenu.gameObject.SetActive(false);
    }
}
