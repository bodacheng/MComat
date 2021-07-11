using mainMenu;
using UnityEngine;
using DG.Tweening;
using dataAccess;

public class ArcadeFrontPage : MainSceneProcess
{
    public bool loadFinished;
    
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        loadFinished = false;
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.5f, 0.1f);
        if (ArcadeManager.ArcadeStages.Count == 0)
        {
            Debug.Log("无有效关卡");
            return;
        }
        
        ArcadeManager.target.INIPagingSystem(2);
        if (ArcadeManager.ArcadeStages.ContainsKey(Account._AccInfo.ArcadeProcess))
        {
            StageInfo StageInfo = ArcadeManager.ArcadeStages[Account._AccInfo.ArcadeProcess];
            ArcadeManager.target.IconButtonFeature(StageInfo.stageButton.MemberIcons[0]);
        }else{
            Debug.Log("巨大错误。玩家关卡进度值不对应任何关卡");
        }
        PageTo.Go(MainSceneStep.ArcadeFront);
        loadFinished = true;
    }
    
    public override void ProcessEnd()
    {
    }
    
    readonly Vector3 screenPos = new Vector3(0.3f, 0.23f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}