using UnityEngine;
using UnityEngine.UI;
using System;

public class ArcadeFightResult : UILayer
{
    [SerializeField] private Text RewardGold;
    [SerializeField] private Text RewardDiamond;
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Button AgainBtn;
    [SerializeField] private Button NextBtn;
    
    public void Initialise(Action R, Action A, int gold, int diamond)
    {
        ReturnBtn.onClick.AddListener(R.Invoke);
        AgainBtn.onClick.AddListener(A.Invoke);
        
        // if (ArcadeTop.ArcadeStages.ContainsKey(NetFightScene.Fight.ID + 1))
        // {
        //     NextBtn.onClick.AddListener(N.Invoke);
        //     NextBtn.gameObject.SetActive(true);
        // }else{
        //     NextBtn.gameObject.SetActive(false);
        // }

        RewardGold.text = gold.ToString();
        RewardDiamond.text = diamond.ToString();
    }
}
