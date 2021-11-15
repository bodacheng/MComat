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

    ArcadeTop arcadeTop;
    public override void ProcessEnter()
    {
        loadFinished = false;
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.5f, 0.1f);
        arcadeTop = UILayerLoader.Load(PreScene.target.T, "ArcadeTop") as ArcadeTop;
        arcadeTop.INIArcadeStageButtons();

        arcadeTop.INIPagingSystem(2);
        if (ArcadeTop.ArcadeStages.ContainsKey(Account._AccInfo.ArcadeProcess))
        {
            StageInfo StageInfo = ArcadeTop.ArcadeStages[Account._AccInfo.ArcadeProcess];
            arcadeTop.IconButtonFeature(StageInfo.stageButton.MemberIcons[0]);
        }else{
            Debug.Log("巨大错误。玩家关卡进度值不对应任何关卡");
        }
        loadFinished = true;
    }
    
    public override void ProcessEnd()
    {
        if (arcadeTop != null)
            GameObject.Destroy(arcadeTop.gameObject);
    }
    
    readonly Vector3 screenPos = new Vector3(0.3f, 0.23f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}