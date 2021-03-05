using System.Collections;
using Api.Dto.Model;
using dataAccess;
using Api.Dto.Form;
using Api.Common;
using mainMenu;

public static class RewardManager
{
    public static IEnumerator RequestRewardsExaution(RequestRewardForm form, SuccessDelegate<GetRewardModel> success, FailDelegate<GetRewardModel> fail, ApiLanguage apiLanguage)
    {
        switch (AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                switch(form.fightEventType)
                {
                    case FightEventType.Quest:
                        for (int i = 0; i < form.StoneOfPlayerIDs.Count; i++)
                        {
                            yield return ExpUpForStones_Local(form.StoneOfPlayerIDs[i], 20);
                        }
                        if (AccountSet._AccInfo.ArcadeProcess == form.eventNum)
                        {
                            AccountSet._AccInfo.ArcadeProcess = form.eventNum + 1;
                        }
                        SingleThreadProcesser.backup.RunAsQueued(AccountSet.SaveCustomerInfo());
                        break;
                    case FightEventType.Arena:
                        break;
                }
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                yield return ApiCaller.Instance.Post<GetRewardModel, RequestRewardForm> 
                (
                    "http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", 
                    form, 
                    ApiCaller.Instance.getHeader(apiLanguage),
                    model => {
                        success(model.data);
                    },
                    model => {
                        fail(model.data);
                    }
                );
                break;
        }
    }
        
    public static IEnumerator ExpUpForStones_Local(string StoneOfPlayerID, int addExp)
    {
        SkillStoneOfPlayerInfoModel stoneOfPlayer =  MySkillStones.Get(StoneOfPlayerID);
        int formerExp = stoneOfPlayer.EXP;
        stoneOfPlayer.EXP = formerExp + addExp;
        yield return MySkillStones.Update(StoneOfPlayerID);
    }
}