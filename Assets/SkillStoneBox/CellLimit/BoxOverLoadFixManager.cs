using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class BoxOverLoadFixManager : MonoBehaviour
{
    public static BoxOverLoadFixManager target;
    
    public Canvas T;
    public Button SELL, Expand;

    void Awake()
    {
        target = this;
    }

    public void ArrangeButtonsFeature()
    {
        void ChooseToExpand()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxExpansion,true);
        }
        Expand.onClick.RemoveAllListeners();
        Expand.onClick.AddListener(ChooseToExpand);
        
        void ChooseToSell()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.SkillStones,true);
        }
        SELL.onClick.RemoveAllListeners();
        SELL.onClick.AddListener(ChooseToSell);
    }
}
