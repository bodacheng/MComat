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
    public RectTransform SKillEditStoneBoxT;
    public NineForShow NineForShow;
    public SkillStoneDetail _skillStoneDetail;
    
    List<SkillStoneOfPlayerInfoModel> Result;
    
    public static GachaManager target;
    
    void Awake()
    {
        target = this;
    }
       
    public List<SkillStoneOfPlayerInfoModel> GetResult()
    {
        return Result;
    }
    
    public void OneTime()
    {
        IEnumerator Go()
        {
            yield return GachaManager.target.Gacha(1);
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
        }
        PreScene.target.mainProcessRunner.Run(Go());
    }
    
    public void TenTimes()
    {
        IEnumerator Go()
        {
            yield return GachaManager.target.Gacha(9);
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
        }
        PreScene.target.mainProcessRunner.Run(Go());
    }
    
    public IEnumerator Gacha(int count)
    {
        List<SkillStoneOfPlayerInfoModel> Results = null;
        switch (AccountSet._playerinfoReferenceMode)
        {
            case playerInfoRefMode.localTestSaveData:
                IEnumerator GET = Gotcha("human", count);
                yield return GET;
                Results = (List<SkillStoneOfPlayerInfoModel>)GET.Current;
                break;
            case playerInfoRefMode.remoteTestPlayer:
                break;
            case playerInfoRefMode.formalVersion:
                break;
        }
        Result = Results;
        yield break;
    }
    
    public static IEnumerator Gotcha(string type, int stoneCount)
    {
        List<SkillStoneOfPlayerInfoModel> Geted = new List<SkillStoneOfPlayerInfoModel>();
        List<SkillConfig> skillConfigs = SkillConfigTable.GetSkillConfigsOfType(type);
        for (int i = 0; i < stoneCount; i++)
        {
            IEnumerator GET = Gocha(type);
            yield return GET;
            Geted.Add((SkillStoneOfPlayerInfoModel)GET.Current);
        }
        yield return Geted;
    }
    
    static IEnumerator Gocha(string type)
    {
        List<SkillConfig> skillConfigs = SkillConfigTable.GetSkillConfigsOfType(type);
        int random_index = Random.Range(0, skillConfigs.Count);
        SkillConfig skillConfig = skillConfigs[random_index];
        SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
        {
            skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
            skillId = skillConfig.RECORD_ID,
            EXP = 0,
            Inherent = "false",
            inUsingMonsterOfPlayerId = "-1",
            inUsingSkillSlot = null
        };
        yield return MySkillStonesReader.Add(stoneInfo);
        yield return stoneInfo;
    }
}