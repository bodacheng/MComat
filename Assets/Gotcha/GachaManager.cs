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
            case PlayerInfoRefMode.localTestSaveData:
                IEnumerator GET = Gotcha("human", count);
                yield return GET;
                Results = (List<SkillStoneOfPlayerInfoModel>)GET.Current;
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
        }
        Result = Results;
        yield break;
    }
    
    public static IEnumerator Gotcha(string type, int stoneCount)
    {
        List<SkillStoneOfPlayerInfoModel> Geted = new List<SkillStoneOfPlayerInfoModel>();
        for (int i = 0; i < stoneCount; i++)
        {
            IEnumerator GET = Gocha(type);
            yield return GET;
            Geted.Add((SkillStoneOfPlayerInfoModel)GET.Current);
        }
        yield return Geted;
    }
    
    // Gocha对象技能石应该有个更明确的范围，因为开启不同的gocha项目，出的东西还不一样，这应该是比较普遍的。更何况还有个不同获得概率的问题。
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