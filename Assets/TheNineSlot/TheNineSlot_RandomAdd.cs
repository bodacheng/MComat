using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void Random()
        {
            ForceClearAll();
            
            GetMonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            
            NineAndTwo targetSkillSet = NineAndTwo.RandomSkillSet_BasedOnMyStones(charConfig.TYPE, originSkillInfo?.skillId, 1);

            Temp(info.monsterOfPlayerId, 1, targetSkillSet.A1skillid);
            Temp(info.monsterOfPlayerId, 2, targetSkillSet.A2skillid);
            Temp(info.monsterOfPlayerId, 3, targetSkillSet.A3skillid);
            Temp(info.monsterOfPlayerId, 4, targetSkillSet.B1skillid);
            Temp(info.monsterOfPlayerId, 5, targetSkillSet.B2skillid);
            Temp(info.monsterOfPlayerId, 6, targetSkillSet.B3skillid);
            Temp(info.monsterOfPlayerId, 7, targetSkillSet.C1skillid);
            Temp(info.monsterOfPlayerId, 8, targetSkillSet.C2skillid);
            Temp(info.monsterOfPlayerId, 9, targetSkillSet.C3skillid);
            
            NineSlotsStatusRefresh();
            TheNineSlot.target.mainProcessRunner.Run(SkillStonesBox.target.ArrangeSkillStonesToBox());
        }
        
        void Temp(string monsterOfPlayerId, int targetSlot, string skillid)
        {
            GetMonsterOfPlayerDetailModel charInfo = AccountCharsSet.Get(monsterOfPlayerId);
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(monsterOfPlayerId);
            List<SkillStoneOfPlayerInfoModel> Options = MySkillStonesReader.GetMyStonesBySkillID(skillid);

            if (originSkillInfo != null && skillid == originSkillInfo.skillId)
            {
                for (int i = 0; i < Options.Count; i++)
                {
                    if (Options[i] == originSkillInfo)
                        allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(Options.Count > 0 ? originSkillInfo.skillStoneOfPlayerId : null));
                }
            }else{
                Options.OrderByDescending(x => x.EXP);
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(Options.Count > 0 ? Options[0].skillStoneOfPlayerId : null));
            }
        }
    }
}