using DummyLayerSystem;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using ModelView;

public class FrontLayer : UILayer
{
    [SerializeField] private Button ArcadeBtn;
    [SerializeField] private Button ArenaBtn;
    [SerializeField] private Button MemberBtn;
    [SerializeField] private Button TrainBtn;
    [SerializeField] private Button StonesBtn;
    [SerializeField] private Button GotchaBtn;
    [SerializeField] private Button SkillTestRBtn;
    [SerializeField] private Button SkillTestMBtn;

    [SerializeField] private GameObject selectMeIndicator;
    
    public DedicatedCameraConnector _connector;
    
    public static FrontLayer Open(PreScene pre)
    {
        var returnValue = UILayerLoader.Get<FrontLayer>();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T,"FrontLayer") as FrontLayer;
        returnValue.Initialise(pre);
        return returnValue;
    }

    public static void Close()
    {
        UILayerLoader.Remove("FrontLayer");
    }
    
    public void Initialise(PreScene pre)
    {
        ArcadeBtn.onClick.AddListener(()=> pre.trySwitchToStep(MainSceneStep.ArcadeFront,true));
        ArenaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.Arena, true));
        MemberBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.UnitList, true));
        TrainBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SelfFightFront, true));
        StonesBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList, true));
        GotchaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront, true));
        
        SkillTestRBtn.onClick.AddListener(pre.BeginSkillTest_Rotation);
        SkillTestMBtn.onClick.AddListener(pre.BeginSkillTest_Multi);
    }

    #region 教程

    public void PlsClickBtn(string btnCode)
    {
        Debug.Log("this place entered:"+ btnCode);
        
        ArcadeBtn.interactable = btnCode == "arcade";
        ArenaBtn.interactable = btnCode == "arena";
        MemberBtn.interactable = btnCode == "unit";
        TrainBtn.interactable = btnCode == "train";
        StonesBtn.interactable = btnCode == "stones";
        GotchaBtn.interactable = btnCode == "gotcha";

        switch (btnCode)
        {
            case "arcade":
                break;
            case "arena":
                break;
            case "unit":
                break;
            case "train":
                break;
            case "stones":
                break;
            case "gotcha":
                break;
        }
    }
    #endregion
}
