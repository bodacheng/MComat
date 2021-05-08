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
    public RectTransform MemberDetailT;
    public RectTransform SKillEditStoneBoxT;
    public NineForShow NineForShow;
    public SkillStoneDetail _skillStoneDetail;
    
    List<StoneOfPlayerInfoModel> Result;
    
    public static GachaManager target;
    
    void Awake()
    {
        target = this;
    }
       
    public List<StoneOfPlayerInfoModel> GetResult()
    {
        return Result;
    }
    
    public void OneTime()
    {
        IEnumerator Go()
        {
            IEnumerator process = target.Gacha("human", 1);
            yield return process;
            if ((bool)process.Current)
                PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
            else{
                Debug.Log("错误返回" + process.Current);
            }
        }
        PreScene.target.mainProcessRunner.RunAsQueued(Go());
    }
    
    public void NineTimes()
    {
        IEnumerator Go()
        {
            IEnumerator process = target.Gacha("human", 9);
            yield return process;
            if (process.Current != null)
                PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
            else{
                Debug.Log("错误返回" + process.Current);
            }
        }
        PreScene.target.mainProcessRunner.RunAsQueued(Go());
    }
    
    public IEnumerator Gacha(string type, int count)
    {
        List<StoneOfPlayerInfoModel> Results = null;
        switch (AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                IEnumerator GET = Gotcha(type, count);
                yield return GET;
                Results = (List<StoneOfPlayerInfoModel>)GET.Current;
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
        }
        Result = Results;
        yield return true;
    }
    
    public static IEnumerator Gotcha(string type, int stoneCount)
    {
        List<StoneOfPlayerInfoModel> Geted = new List<StoneOfPlayerInfoModel>();
        for (int i = 0; i < stoneCount; i++)
        {
            IEnumerator GET = OneGocha(type);
            yield return GET;
            Geted.Add((StoneOfPlayerInfoModel)GET.Current);
        }
        yield return Geted;
    }
    
    // Gocha对象技能石应该有个更明确的范围，因为开启不同的gocha项目，出的东西还不一样，这应该是比较普遍的。更何况还有个不同获得概率的问题。
    // 这个函数摆在这地方本身是违背原则的。
    static IEnumerator OneGocha(string type)
    {
        List<SkillConfig> skillConfigs = SkillConfigTable.GetSkillConfigsOfType(type);
        int random_index = Random.Range(0, skillConfigs.Count);
        SkillConfig skillConfig = skillConfigs[random_index];
        StoneOfPlayerInfoModel stoneInfo = new StoneOfPlayerInfoModel
        {
            skillStoneOfPlayerId = MySkillStones.GetNonRepeatID_LocalSave(),
            skillId = skillConfig.RECORD_ID,
            EXP = 0,
            BreakThrough = 0,
            Inherent = "false",
            inUsingMonsterOfPlayerId = "-1",
            inUsingSkillSlot = null
        };
        yield return MySkillStones.Add(stoneInfo);
        yield return stoneInfo;
    }
}