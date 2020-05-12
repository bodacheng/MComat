using System.Collections;
using mainMenu;
using System.Collections.Generic;
using Api.Dto.Model;

public class GachaResult : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        GachaManager.target.GotchaCanvas.gameObject.SetActive(true);
        GachaManager.target.GotchaFrontT.gameObject.SetActive(false);
        GachaManager.target.GotchaResultT.gameObject.SetActive(true);
        CameraManager._camera.gameObject.SetActive(false);
        GachaRender.target.Camera.gameObject.SetActive(true);
        List<SkillStoneOfPlayerInfoModel> results = GachaManager.target.GetResult();
        yield return GachaManager.target.NineForShow.ShowStones
        (
            results[0].skillId, results[1].skillId,results[2].skillId,
            results[3].skillId, results[4].skillId,results[5].skillId,
            results[6].skillId, results[7].skillId,results[8].skillId
        );
        yield break;
    }
    
    public GachaResult()
    {
        Step = MainSceneStep.GotchaResult;
        EelementsInherit(PreScene.target);
    }
       
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        GachaManager.target.GotchaCanvas.gameObject.SetActive(false);
        CameraManager._camera.gameObject.SetActive(true);
        GachaRender.target.Camera.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    }
}
