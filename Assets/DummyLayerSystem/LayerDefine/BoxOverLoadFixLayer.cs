using DummyLayerSystem;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class BoxOverLoadFixLayer : UILayer
{
    [SerializeField] private Button SELL;
    [SerializeField] private Button delete25;
    
    public static BoxOverLoadFixLayer Open()
    {
        var b = UILayerLoader.Load(PreScene.target.T, "BoxOverLoadFixLayer") as BoxOverLoadFixLayer;
        b.INI();
        return b;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("BoxOverLoadFixLayer");
    }
    
    void INI()
    {
        void ChooseToSell()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.SkillStones_Sell, true);
        }
        SELL.onClick.AddListener(ChooseToSell);
        
        #if Pre
        delete25.gameObject.SetActive(true);
        delete25.onClick.AddListener(CloudScript.Remove25Stones);
        #endif
    }
}
