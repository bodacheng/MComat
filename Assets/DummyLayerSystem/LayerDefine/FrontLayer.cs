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
    [SerializeField] DedicatedCameraConnector camConnector;
    
    public DedicatedCameraConnector CamConnector => camConnector;
    
    public void Initialise(PreScene pre)
    {
        ArcadeBtn.onClick.AddListener(()=> pre.trySwitchToStep(MainSceneStep.ArcadeFront));
        ArenaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.Arena));
        MemberBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.UnitList));
        TrainBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SelfFightFront));
        StonesBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList));
        GotchaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront));
        
        SkillTestRBtn.onClick.AddListener(pre.BeginSkillTest_Rotation);
        SkillTestMBtn.onClick.AddListener(pre.BeginSkillTest_Multi);
    }

    #region 教程
    [SerializeField] private GameObject indicator;

    public void PlsClickBtn(string btnCode)
    {
        ArcadeBtn.interactable = btnCode == "arcade";
        ArenaBtn.interactable = btnCode == "arena";
        MemberBtn.interactable = btnCode == "unit";
        TrainBtn.interactable = btnCode == "train";
        StonesBtn.interactable = btnCode == "stones";
        GotchaBtn.interactable = btnCode == "gotcha";

        Vector3 localPos = Vector3.zero;
        switch (btnCode)
        {
            case "arcade":
                localPos = ArcadeBtn.transform.localPosition;
                break;
            case "arena":
                localPos = ArenaBtn.transform.localPosition;
                break;
            case "unit":
                localPos = MemberBtn.transform.localPosition;
                break;
            case "train":
                localPos = TrainBtn.transform.localPosition;
                break;
            case "stones":
                localPos = StonesBtn.transform.localPosition;
                break;
            case "gotcha":
                localPos = GotchaBtn.transform.localPosition;
                break;
        }

        indicator.transform.localPosition = localPos;
        indicator.SetActive(true);
    }
    #endregion
}
