using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using mainMenu;
using dataAccess;
using Skill;
using System.Collections;

public class GachaManager : MonoBehaviour
{
    public Canvas GotchaCanvas;
    public RectTransform GotchaFrontT;
    public RectTransform GotchaResultT;
    public NineForShow NineForShow;
    
    List<SkillStoneOfPlayerInfoModel> Result;
    
    public static GachaManager target;
    
    void Awake()
    {
        target = this;
    }
    
    public void SetResult(List<SkillStoneOfPlayerInfoModel> results)
    {
        Result = results;
    }
    
    public List<SkillStoneOfPlayerInfoModel> GetResult()
    {
        return Result;
    }
    
    public void TenTimes()
    {
        PreScene.Instance.trySwitchToStep(MainSceneStep.GotchaAnim,true);
    }
    
    public IEnumerator Gacha()
    {
        List<SkillStoneOfPlayerInfoModel> Results = null;
        switch (AccountSet._playerinfoReferenceMode)
        {
            case playerInfoRefMode.localTestSaveData:
                Results = TenTimesGotcha("human");
                break;
            case playerInfoRefMode.remoteTestPlayer:
                break;
            case playerInfoRefMode.formalVersion:
                break;
        }
        SetResult(Results);
        yield break;
    }
    
    public static List<SkillStoneOfPlayerInfoModel> TenTimesGotcha(string type)
    {
        List<SkillStoneOfPlayerInfoModel> Geted = new List<SkillStoneOfPlayerInfoModel>();
        
        List<SkillConfig> skillConfigs = SkillConfigTable.GetSkillConfigsOfType(type);
        for (int i = 0; i < 10; i++)
        {
            int random_index = Random.Range(0,skillConfigs.Count);
            SkillConfig skillConfig = skillConfigs[random_index];
            SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
            {
                skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
                skillId = skillConfig.REAL_NAME,
                exp = "0",
                Inherent = "false",
                inUsingMonsterOfPlayerId = i.ToString(),
                inUsingSkillSlot = null
            };
            Geted.Add(stoneInfo);
        }
        return Geted;
    }
    
}