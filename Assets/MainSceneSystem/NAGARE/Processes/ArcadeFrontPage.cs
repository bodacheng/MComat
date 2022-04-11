using mainMenu;
using UnityEngine;
using DG.Tweening;

public class ArcadeFrontPage : MainSceneProcess
{
    public bool loadFinished;
    
    public ArcadeFrontPage()
    {
        Step = MainSceneStep.ArcadeFront;
        Inherit(PreScene.target);
    }

    ArcadeTop arcadeTop;
    public override void ProcessEnter()
    {
        loadFinished = false;
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.5f, 0.1f);
        arcadeTop = ArcadeTop.Open();
        if (arcadeTop.ArcadeStages.ContainsKey(PlayerAccountInfo.Me.ArcadeProcess))
        {
            var StageInfo = arcadeTop.ArcadeStages[PlayerAccountInfo.Me.ArcadeProcess];
            arcadeTop.IconButtonFeature(StageInfo.stageButton.MemberIcons[0]);
        }else{
            Debug.Log("巨大错误。玩家关卡进度值不对应任何关卡");
        }
        loadFinished = true;
    }
    
    public override void ProcessEnd()
    {
        ArcadeTop.Close();
    }
}