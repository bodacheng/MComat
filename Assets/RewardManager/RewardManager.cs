using System.Collections;
using Api.Dto.Model;
using dataAccess;
using Api.Dto.Form;
using Api.Common;
using mainMenu;

public static class RewardManager
{
    public static void RequestRewardsExaution(SuccessDelegate<GetRewardModel> success, FailDelegate<GetRewardModel> fail, ApiLanguage apiLanguage)
    {
        switch (Account.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                break;
        }
    }
        
    public static void ExpUpForStones_Local(string StoneOfPlayerID, int addExp)
    {
        StoneOfPlayerInfo stoneOfPlayer =  Stones.Get(StoneOfPlayerID);
        int formerExp = stoneOfPlayer.EXP;
        stoneOfPlayer.EXP = formerExp + addExp;
        //yield return MySkillStones.Update(StoneOfPlayerID);
    }
}