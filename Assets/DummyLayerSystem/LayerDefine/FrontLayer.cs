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

    public DedicatedCameraConnector _connector;
    
    static FrontLayer Get()
    {
        var l = UILayerLoader.Get("FrontLayer");
        FrontLayer returnValue = null;
        if (l != null)
        {
            returnValue = l as FrontLayer;
        }
        return returnValue;
    }
    
    public static FrontLayer Open(PreScene pre)
    {
        var returnValue = Get();
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
        MemberBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.MonsterList, true));
        TrainBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SelfFightFront, true));
        StonesBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList, true));
        GotchaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront, true));
        
        SkillTestRBtn.onClick.AddListener(pre.BeginSkillTest_Rotation);
        SkillTestMBtn.onClick.AddListener(pre.BeginSkillTest_Multi);
    }
}
