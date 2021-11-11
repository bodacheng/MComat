using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class BoxOverLoadFixLayer : UILayer
{
    public Button SELL, Expand;
    
    public static BoxOverLoadFixLayer Open()
    {
        return UILayerLoader.Load(PreScene.target.T,"BoxOverLoadFixLayer") as BoxOverLoadFixLayer;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("BoxOverLoadFixLayer");
    }
    
    public void ArrangeButtonsFeature()
    {
        void ChooseToExpand()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxExpansion, true);
        }
        Expand.onClick.RemoveAllListeners();
        Expand.onClick.AddListener(ChooseToExpand);
        
        void ChooseToSell()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.SkillStones_Sell, true);
        }
        SELL.onClick.RemoveAllListeners();
        SELL.onClick.AddListener(ChooseToSell);
    }
}
