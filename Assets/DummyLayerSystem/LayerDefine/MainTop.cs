using UnityEngine;
using UnityEngine.UI;
using mainMenu;

public class MainTop : UILayer
{
    [SerializeField] private Button ArcadeBtn;
    [SerializeField] private Button ArenaBtn;
    [SerializeField] private Button MemberBtn;
    [SerializeField] private Button TrainBtn;
    [SerializeField] private Button StonesBtn;
    [SerializeField] private Button GotchaBtn;
    [SerializeField] private Button SkillTestRBtn;
    [SerializeField] private Button SkillTestMBtn;

    public void Initialise(PreScene pre)
    {
        ArcadeBtn.onClick.AddListener(()=> pre.trySwitchToStep(MainSceneStep.ArcadeFront,true));
        ArenaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.Arena, true));
        MemberBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.MonsterList, true));
        TrainBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SelfFightFront, true));
        StonesBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.SkillStoneList, true));
        GotchaBtn.onClick.AddListener(() => pre.trySwitchToStep(MainSceneStep.GotchaFront, true));

        SkillTestRBtn.onClick.AddListener(pre.BeginSkillTest_Rotatiom);
        SkillTestMBtn.onClick.AddListener(pre.BeginSkillTest_Multi);
    }
}
