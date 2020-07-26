using System.Collections;
using mainMenu;
using System.Collections.Generic;
using Api.Dto.Model;
using UnityEngine;

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
        if (results.Count == 1)
        {
            yield return GachaManager.target.NineForShow.ShowStones
            (
                "-1", "-1", "-1",
                "-1", results[0] != null ? results[0].skillId : "-1", "-1",
                "-1", "-1", "-1"
            );
        }
        else if (results.Count == 9)
        {
            yield return GachaManager.target.NineForShow.ShowStones
            (
                results[0] != null ? results[0].skillId : "-1", 
                results[1] != null ? results[1].skillId : "-1",
                results[2] != null ? results[2].skillId : "-1",
                results[3] != null ? results[3].skillId : "-1",
                results[4] != null ? results[4].skillId : "-1",
                results[5] != null ? results[5].skillId : "-1",
                results[6] != null ? results[6].skillId : "-1",
                results[7] != null ? results[7].skillId : "-1",
                results[8] != null ? results[8].skillId : "-1"
            );
        }
        GachaManager.target._skillStoneDetail._T.SetParent(GachaManager.target.GotchaResultT);
        GachaManager.target._skillStoneDetail._T.localScale = Vector3.one;
        GachaManager.target._skillStoneDetail._T.localPosition = Vector3.zero;
        GachaManager.target.NineForShow.LoadShowDetailFeature();
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
        GachaManager.target.NineForShow.Clear();
        PreScene.target._SkillStonesBox_NineSlot._skillStoneDetail._T.SetParent(GachaManager.target.SKillEditStoneBoxT);
        PreScene.target._SkillStonesBox_NineSlot._skillStoneDetail._T.localScale = Vector3.one;
        PreScene.target._SkillStonesBox_NineSlot._skillStoneDetail._T.localPosition = Vector3.zero;
    }
}
