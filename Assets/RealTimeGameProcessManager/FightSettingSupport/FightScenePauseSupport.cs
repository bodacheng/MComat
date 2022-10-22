using UnityEngine;
using UnityEngine.UI;
using FightScene;
using DummyLayerSystem;

// 战斗暂停相关。从暂停界面可以跳转至Setting界面，因此两个模块靠OptionsButton连接在一起
public class FightScenePauseSupport : UILayer
{
    [Header("暂停菜单里的Resume")]
    public Button ResumeButton;
    
    [Header("暂停菜单里的Return")]
    public Button ReturnButton;
    
    [Header("暂停菜单里的Options")]
    public Button OptionsButton;
    
    public static FightScenePauseSupport target;

    void Awake()
    {
        target = this;
    }

    void Start()
    {
        ReturnButton.onClick.RemoveAllListeners();
        ReturnButton.onClick.AddListener(NetFightScene.target.ReturnToFront);
        
        ResumeButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.AddListener(Close);
        
        //OptionsButton.onClick.RemoveAllListeners();
        //OptionsButton.onClick.AddListener(settingLayer.Open);
        //OptionsButton.onClick.AddListener(JumpToOptions);
    }
    
    public static FightScenePauseSupport Open(GameObject T)
    {
        var returnValue = UILayerLoader.Get<FightScenePauseSupport>();
        if (returnValue != null)
        {
            return returnValue;
        }
        Time.timeScale = 0;
        returnValue = UILayerLoader.Load(T,"FightScenePauseSupport") as FightScenePauseSupport;
        return returnValue;
    }

    public static void Close()
    {
        UILayerLoader.Remove("FightScenePauseSupport");
    }
    
    public override void OnDestroy()
    {
        Time.timeScale = 1;
        base.OnDestroy();
    }
}
