using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Collections;
using dataAccess;
using System;

// 贩卖过多的技能石？ 扩张技能石盒？
public class BoxExpandHelper : MonoBehaviour
{
    public Canvas ExpansionT;

    public RectTransform OptionT;
    public RectTransform ResultConfirmT;
    
    public Button Five; // 扩张5个格
    public Button Ten; // 扩张10个格
    public Button ResultConfirmButton;
    public Text Result;

    public static BoxExpandHelper target;

    void Awake()
    {
        target = this;
    }

    public void ArrangeButtonsFeature()
    {
        void expandFive()
        {
            //LoadingCanvas.target.ArrangeConfirmWindow(ChooseFive, "来他5个技能石格子？？");
        }
        Five.onClick.RemoveAllListeners();
        Five.onClick.AddListener(expandFive);
        
        void expandTen()
        {
            PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow(ChooseTen, "来他10个技能石格子？？");
        }
        Ten.onClick.RemoveAllListeners();
        Ten.onClick.AddListener(expandTen);
        
        void ConfirmButton()
        {
            OptionT.gameObject.SetActive(true);
            ResultConfirmT.gameObject.SetActive(false);
        }
        ResultConfirmButton.onClick.RemoveAllListeners();
        ResultConfirmButton.onClick.AddListener(ConfirmButton);
    }
    
    void ChooseTen()
    {
        CloudScript.ExpandBox10();
    }
    
    void ShowResult()
    {
        OptionT.gameObject.SetActive(false);
        ResultConfirmT.gameObject.SetActive(true);
    }
       
    void GoToStoneSell()
    {
        PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList,true); // 没有单独的技能石贩卖画面所以只能送到这里
    }
}