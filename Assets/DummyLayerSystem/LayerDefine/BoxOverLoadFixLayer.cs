using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class BoxOverLoadFixLayer : UILayer
{
    [SerializeField] private Button SELL, Expand, delete25;
    
    public static BoxOverLoadFixLayer Open()
    {
        BoxOverLoadFixLayer b = UILayerLoader.Load(PreScene.target.T, "BoxOverLoadFixLayer") as BoxOverLoadFixLayer;
        b.ArrangeButtonsFeature();
        return b;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("BoxOverLoadFixLayer");
    }
    
    void ArrangeButtonsFeature()
    {
        void ChooseToExpand()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxExpansion, true);
        }
        Expand.onClick.AddListener(ChooseToExpand);
        
        void ChooseToSell()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.SkillStones_Sell, true);
        }
        SELL.onClick.AddListener(ChooseToSell);
        
        delete25.onClick.AddListener(CloudScript.Remove25Stones);
    }
}
