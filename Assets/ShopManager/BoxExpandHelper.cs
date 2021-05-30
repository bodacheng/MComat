using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Collections;
using dataAccess;
using System;

// 贩卖过多的技能石？ 扩张技能石盒？
public class BoxExpandHelper : MonoBehaviour
{    
    #region 扩张    
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
            LoadingCanvas.target.ArrangeConfirmWindow(ChooseFive, "来他5个技能石格子？？");
        }
        Five.onClick.RemoveAllListeners();
        Five.onClick.AddListener(expandFive);
        
        void expandTen()
        {
            LoadingCanvas.target.ArrangeConfirmWindow(ChooseTen, "来他10个技能石格子？？");
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
    
    void ChooseFive()
    {
        PreScene.target.mainProcessRunner.RunAsQueued(BoxExpansion(5));
    }
    
    void ChooseTen()
    {
        PreScene.target.mainProcessRunner.RunAsQueued(BoxExpansion(10));
    }
    
    void ShowResult()
    {
        OptionT.gameObject.SetActive(false);
        ResultConfirmT.gameObject.SetActive(true);
    }
    
    // 扩张了格子之后没个单独的process，有一个成功提示按说就可以
    public IEnumerator BoxExpansion(int ExpandCount)
    {
        OptionT.gameObject.SetActive(false);
        switch (AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                AccountSet._AccInfo.Stoneboxsize = AccountSet._AccInfo.Stoneboxsize + ExpandCount;
            break;
            case PlayerInfoRefMode.remoteTestPlayer:
            break;
            case PlayerInfoRefMode.formalVersion:
            break;
        }
        
        //try 
        //{
        //    if ((bool)expansionProcess.Current)
        //    {
        //        Result.text = " 成功扩张技能石盒 ";
        //    }else{
        //        Result.text = " 失败 ";
        //    }            
        //}
        //catch(Exception e)
        //{
        //    Result.text = " 失败 ";
        //    Debug.Log(e);
        //}
        ShowResult();
        yield break;
    }
    #endregion
    
    #region 贩卖    
    void GoToStoneSell()
    {
        PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList,true); // 没有单独的技能石贩卖画面所以只能送到这里
    }
    #endregion
}