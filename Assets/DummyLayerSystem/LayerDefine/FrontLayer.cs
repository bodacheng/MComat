using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using ModelView;

public class FrontLayer : UILayer
{
    [SerializeField] Button ArcadeBtn;
    [SerializeField] Button ArenaBtn;
    [SerializeField] Button MemberBtn;
    [SerializeField] Button TrainBtn;
    [SerializeField] Button StonesBtn;
    [SerializeField] Button GotchaBtn;
    [SerializeField] Button SkillTestRBtn;
    [SerializeField] Button SkillTestMBtn;
    [SerializeField] DedicatedCameraConnector camConnector;
    
    public void Initialise(PreScene pre)
    {
        // Unit View Size Calulate
        var unitViewSize = (PosCal.canvasWidth - 940);
        if (unitViewSize > PosCal.canvasHeight -150)
        {
            unitViewSize = PosCal.canvasHeight -150;
        }
        camConnector.GetComponent<RectTransform>().sizeDelta = new Vector2(unitViewSize,unitViewSize);
        
        ArcadeBtn.onClick.AddListener(()=> pre.trySwitchToStep(MainSceneStep.ArcadeFront));
        ArenaBtn.onClick.AddListener(() =>
        {
            if (PlayerAccountInfo.Me.arcadeProcess >= 5)
                pre.trySwitchToStep(MainSceneStep.Arena);
            else
            {
                PopupLayer.ArrangeWarnWindow(Translate.Get("PlsClearStage5"));
            }
        });
        MemberBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.UnitList));
        TrainBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SelfFightFront));
        StonesBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList));
        GotchaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront));
        
        SkillTestRBtn.onClick.AddListener(pre.BeginSkillTest_Rotation);
        SkillTestMBtn.onClick.AddListener(pre.BeginSkillTest_Multi);
        SkillTestRBtn.gameObject.SetActive(CommonSetting.DevMode); 
        SkillTestMBtn.gameObject.SetActive(CommonSetting.DevMode);
    }
    
    public async UniTask ShowMyModel(string instanceID)
    {
        if (camConnector.TaskRunningCount == 0)
        {
            var info = dataAccess.Units.Get(instanceID);
            await camConnector.ShowModel(info?.r_id);
        }
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
