using System.Collections;
using dataAccess;
using mainMenu;
using UnityEngine;
using Api.Dto.Form;
using Api.Common;
using Api.Dto.Model;
using System.Collections.Generic;

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
        
        skillStoneLevelUpForm.UseGold = CurrentGoldExaust.ToString();
        
        yield return LevelUpStoneTest(
            skillStoneLevelUpForm,
            model => {
                MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M1Stone);
                MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M2Stone);
                MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M3Stone);
                MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M4Stone);
                MySkillStonesReader.RemoveStone(skillStoneLevelUpForm.M5Stone);
            },
            model => {
                // 各种方面的合法分析
                // 1. 技能石拥有数量不可以低于30
                // 2. 3阶以上技能石不能牺牲。这样看，1到2阶，或者就是1阶技能石，他们不是没有实战价值而是入手容易，从这个角度讲玩家也不至于因为误操作而破坏账号。
                // 3. 满级石不再接受牺牲材料？
                // 照这么看的话可能用ArrangeWarnWindow处理的报错就只有第一条，其他几条。。。不显示那些不能用来做材料的技能石不就行了吗
                string warn = "";
                for (int i = 0; i < model.warnMessage.Count; i++)
                {
                    warn += model.warnMessage[i] +"/n";
                }
                LoadingCanvas.target.ArrangeWarnWindow(warn);
            }
            , Setting.Language
        );
    }
    
    // 技能石升级
    public IEnumerator LevelUpStoneTest(SkillStoneLevelUpForm form, SuccessDelegate<SkillStoneLevelUpModel> success, FailDelegate<SkillStoneLevelUpModel> fail, ApiLanguage apiLanguage)
    {
        switch (AccountSet.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                SkillStoneLevelUpModel SkillStoneLevelUpModel = new SkillStoneLevelUpModel();
                List<string> wrongs = form.LocalCheck();
                if (wrongs.Count == 0)
                {
                    SkillStoneLevelUpModel.LocalAnalysis(form);
                    success(SkillStoneLevelUpModel);
                }else{
                    SkillStoneLevelUpModel.warnMessage = wrongs;
                    fail(SkillStoneLevelUpModel);
                }
            break;
            case PlayerInfoRefMode.remoteTestPlayer:
                yield return ApiCaller.Instance.Post<SkillStoneLevelUpModel, SkillStoneLevelUpForm>("目前地址未定", form, ApiCaller.Instance.getHeader(apiLanguage), 
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
    }

    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        SkillStoneOfPlayerInfoModel StoneInfoModel = MySkillStonesReader.Get(stoneOfPlayerId);
        if (StoneInfoModel == null)
            return;
        PreScene.target.mainProcessRunner.Run(LevelUpStone(StoneInfoModel.skillStoneOfPlayerId));
    }
}