using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public class BoxOverLoadFixLayer : UILayer
{
    [SerializeField] private Button SELL;
    [SerializeField] private Button delete25;
    
    public void INI()
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
