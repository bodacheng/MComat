using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using mainMenu;

public class GachaManager : MonoBehaviour
{
    public Canvas GotchaCanvas;
    public RectTransform GotchaFrontT;
    public RectTransform GotchaResultT;
    public NineForShow NineForShow;
    
    List<SkillStoneOfPlayerInfoModel> Result;
    
    public static GachaManager target;
    
    public void SetResult(List<SkillStoneOfPlayerInfoModel> results)
    {
        Result = results;
    }
    
    public List<SkillStoneOfPlayerInfoModel> GetResult()
    {
        return Result;
    }
    
    void Awake()
    {
        target = this;
    }
    
    public void TenTimes()
    {
        SetResult(SkillConfigTable.TenTimesGotcha("human"));
        PreScene.Instance.trySwitchToStep(MainSceneStep.GotchaAnim,true);
    }
}