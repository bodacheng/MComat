using mainMenu;
using dataAccess;
using UnityEngine.UI;
using System.Collections.Generic;
using DummyLayerSystem;
using UnityEngine;

public class GotchaLayer : UILayer
{
    [SerializeField] private Button Gotcha1;
    [SerializeField] private Button Gotcha9;
    [SerializeField] private Button GetAllSKBtn;
    [SerializeField] private Button GetAllMBtn;
    [SerializeField] private Button Remove25StonesBtn;
    
    public static GotchaLayer Open()
    {
        var layer = UILayerLoader.Load(PreScene.target.T, "GotchaLayer") as GotchaLayer;
        layer.Gotcha1.onClick.AddListener(OneTime);
        layer.Gotcha9.onClick.AddListener(NineTimes);
        
        layer.GetAllSKBtn.gameObject.SetActive(true);
        layer.GetAllMBtn.gameObject.SetActive(true);
        layer.Remove25StonesBtn.gameObject.SetActive(true);

        layer.GetAllSKBtn.onClick.AddListener(GetAllSK);
        layer.GetAllMBtn.onClick.AddListener(GetAllM);
        layer.Remove25StonesBtn.onClick.AddListener(Remove25Stones);
        
        return layer;
    }

    public static void Close()
    {
        UILayerLoader.Remove("GotchaLayer");
    }
    
    static void OneTime()
    {
    }
    
    /// <summary>
    /// 缺少消费关联处理
    /// </summary>
    static void NineTimes()
    {
        Close();// 点击按钮瞬间关闭layer。
        CloudScript.GotchaX9(temp);
        void temp(List<StoneOfPlayerInfo> stones)
        {
            GotchaResult.Result = stones;
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, true);
        }
    }
    
    static void GetAllSK()
    {
        CloudScript.GrantStonesTest();
    }

    static void GetAllM()
    {
        CloudScript.GrantMonsterTest();
    }

    static void Remove25Stones()
    {
        CloudScript.Remove25Stones();
    }
}