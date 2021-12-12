using System.Collections;
using mainMenu;
using dataAccess;
using System.Collections.Generic;

public class GachaResult : MainSceneProcess
{
    public static List<StoneOfPlayerInfo> Result;
    
    public IEnumerator EnterProcess()
    {
        GotchaResultLayer gotchaResultLayer = GotchaResultLayer.Open();
        
        CameraManager._camera.gameObject.SetActive(false);
        PreScene.target.starsFall._camera.gameObject.SetActive(true);
        
        //List<SkillStoneOfPlayerInfoModel> results = GachaManager.target.GetResult();
        //if (results.Count == 1)
        //{
        //    yield return GachaManager.target.NineForShow.ShowStones
        //    (
        //        "-1", "-1", "-1",
        //        "-1", results[0] != null ? results[0].skillId : "-1", "-1",
        //        "-1", "-1", "-1"
        //    );
        //}
        //else if (results.Count == 9)
        //{
        //    yield return GachaManager.target.NineForShow.ShowStones
        //    (
        //        results[0] != null ? results[0].skillId : null,
        //        results[1] != null ? results[1].skillId : null,
        //        results[2] != null ? results[2].skillId : null,
        //        results[3] != null ? results[3].skillId : null,
        //        results[4] != null ? results[4].skillId : null,
        //        results[5] != null ? results[5].skillId : null,
        //        results[6] != null ? results[6].skillId : null,
        //        results[7] != null ? results[7].skillId : null,
        //        results[8] != null ? results[8].skillId : null
        //    );
        //}
        
        gotchaResultLayer.NineForShow.LoadShowDetailFeature();
        yield break;
    }
    
    public GachaResult()
    {
        Step = MainSceneStep.GotchaResult;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        CameraManager._camera.gameObject.SetActive(true);
        PreScene.target.starsFall._camera.gameObject.SetActive(false);
        GotchaResultLayer.Close();
    }
}
