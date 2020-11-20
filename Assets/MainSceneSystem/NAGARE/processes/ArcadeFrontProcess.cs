using System.Collections;
using mainMenu;
using UnityEngine;
using DG.Tweening;
using dataAccess;

public class ArcadeFrontProcess : MainSceneProcess
{
    public bool loadFinished;
    
    public IEnumerator EnterProcess()
    {
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.5f, 0.1f);
        
        ArcadeManager.target.INIPagingSystem(2);
        
        if (ArcadeManager.ArcadeStages.ContainsKey(AccountSet._AccInfo.ArcadeProcess))
        {
            StageInfo StageInfo = ArcadeManager.ArcadeStages[AccountSet._AccInfo.ArcadeProcess];
            ArcadeManager.target.IconButtonFeature(StageInfo.MemberIcons[0]);
        }else{
            Debug.Log("巨大错误。玩家关卡进度值不对应任何关卡");
            yield return ModelShower.target.ShowMyModel(null);
        }
        
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(true);
        loadFinished = true;
    }
    
    public ArcadeFrontProcess()
    {
        Step = MainSceneStep.ArcadeFront;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        loadFinished = false;
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        ArcadeManager.target._ArcadeCanvas.gameObject.SetActive(false);
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