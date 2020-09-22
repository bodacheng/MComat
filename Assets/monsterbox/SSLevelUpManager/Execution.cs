using System.Collections;
using dataAccess;
using mainMenu;
using UnityEngine;
using Api.Dto.Form;
using Api.Common;
using Api.Dto.Model;

// 执行
public partial class SSLevelUpManager : MonoBehaviour
{
    public IEnumerator LevelUpStone(string PlayerSkillStoneID)
    {
        SkillStoneLevelUpForm skillStoneLevelUpForm = new SkillStoneLevelUpForm();
        
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        skillStoneLevelUpForm.targetStoneID = PlayerSkillStoneID;
        
        skillStoneLevelUpForm.M1Stone = item1 != null ? item1.SkillStoneOfPlayerId : null;
        skillStoneLevelUpForm.M2Stone = item2 != null ? item2.SkillStoneOfPlayerId : null;
        skillStoneLevelUpForm.M3Stone = item3 != null ? item3.SkillStoneOfPlayerId : null;
        skillStoneLevelUpForm.M4Stone = item4 != null ? item4.SkillStoneOfPlayerId : null;
        skillStoneLevelUpForm.M5Stone = item5 != null ? item5.SkillStoneOfPlayerId : null;
        
        skillStoneLevelUpForm.UseGold = CurrentAddExp().ToString();
        
        yield return LevelUpStoneTest(skillStoneLevelUpForm,
             model => {
                 MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M1Stone);
                 MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M2Stone);
                 MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M3Stone);
                 MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M4Stone);
                 MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M5Stone);
             },
             model => {
             
             }
             , ApiLanguage.EnUs
        );
    }
    
    // 技能石升级
    public IEnumerator LevelUpStoneTest(SkillStoneLevelUpForm form, SuccessDelegate<SkillStoneOfPlayerInfoModel> success, FailDelegate<SkillStoneOfPlayerInfoModel> fail, ApiLanguage apiLanguage)
    {
        switch (AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                bool succeed = true;
                if (succeed)
                {
                    success(null);
                }else{
                    fail(null);
                }
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                yield return ApiCaller.Instance.Post<SkillStoneOfPlayerInfoModel, SkillStoneLevelUpForm>("目前地址未定", form, ApiCaller.Instance.getHeader(apiLanguage), 
                    model => {
                        success(model.data);
                    },
                    model => {
                        fail(model.data);
                    }
                );
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
        }
        yield break;
    }

    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        PreScene.target.mainProcessRunner.Run(LevelUpStone(focusingSSD.GetSTTarget().skillStoneOfPlayerId));
    }
}