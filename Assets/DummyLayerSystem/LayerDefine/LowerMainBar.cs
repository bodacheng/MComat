using DummyLayerSystem;
using UnityEngine;
using mainMenu;

public class LowerMainBar : UILayer
{
    [SerializeField] private BOButton playTab;
    [SerializeField] private BOButton fighterTab;
    [SerializeField] private BOButton stoneTab;
    [SerializeField] private BOButton gotchaTab;
    
    [SerializeField] private BOButton backBtn;
    
    public void Initialise(PreScene pre)
    {
        playTab.SetListener(() =>
        {
            ReturnLayer.ReturnMissionList.Clear();
            pre.trySwitchToStep(MainSceneStep.FrontPage);
        });
        fighterTab.SetListener(() =>
        {
            ReturnLayer.ReturnMissionList.Clear();
            pre.trySwitchToStep(MainSceneStep.UnitList);
        });
        stoneTab.SetListener(() =>
        {
            ReturnLayer.ReturnMissionList.Clear();
            pre.trySwitchToStep(MainSceneStep.SkillStoneList);
        });
        gotchaTab.SetListener(() =>
        {
            ReturnLayer.ReturnMissionList.Clear();
            pre.trySwitchToStep(MainSceneStep.GotchaFront);
        });
    }

    public static void Open()
    {
        var lowerMainBar = UILayerLoader.Load<LowerMainBar>();
        lowerMainBar.Initialise(PreScene.target);
        lowerMainBar.backBtn.gameObject.SetActive(ReturnLayer.ReturnMissionList.Count > 0);
        if (ReturnLayer.ReturnMissionList.Count > 0)
        {
            lowerMainBar.backBtn.SetListener(ReturnLayer.POP);
        }
    }
}
