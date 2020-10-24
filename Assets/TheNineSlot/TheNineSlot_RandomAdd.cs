using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void Random()
        {
            Clear();
            
            GetMonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            
            NineAndTwo targetSkillSet = NineAndTwo.RandomSkillSet_BasedOnMyStones(charConfig.TYPE, originSkillInfo?.skillId, 1);
            
            List<SkillStoneOfPlayerInfoModel> A1Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.A1skillid);
            List<SkillStoneOfPlayerInfoModel> A2Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.A2skillid);
            List<SkillStoneOfPlayerInfoModel> A3Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.A3skillid);
            
            List<SkillStoneOfPlayerInfoModel> B1Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.B1skillid);
            List<SkillStoneOfPlayerInfoModel> B2Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.B2skillid);
            List<SkillStoneOfPlayerInfoModel> B3Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.B3skillid);
            
            List<SkillStoneOfPlayerInfoModel> C1Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.C1skillid);
            List<SkillStoneOfPlayerInfoModel> C2Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.C2skillid);
            List<SkillStoneOfPlayerInfoModel> C3Options = MySkillStonesReader.GetMyStonesBySkillID(targetSkillSet.C3skillid);
            
            A1Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(A1Options.Count > 0 ? A1Options[0].skillStoneOfPlayerId : null));
            A2Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(A2Options.Count > 0 ? A2Options[0].skillStoneOfPlayerId : null));
            A3Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(A3Options.Count > 0 ? A3Options[0].skillStoneOfPlayerId : null));
            
            B1Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(B1Options.Count > 0 ? B1Options[0].skillStoneOfPlayerId : null));
            B2Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(B2Options.Count > 0 ? B2Options[0].skillStoneOfPlayerId : null));
            B3Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(B3Options.Count > 0 ? B3Options[0].skillStoneOfPlayerId : null));
            
            C1Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(C1Options.Count > 0 ? C1Options[0].skillStoneOfPlayerId : null));
            C2Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(C2Options.Count > 0 ? C2Options[0].skillStoneOfPlayerId : null));
            C3Slot._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(C3Options.Count > 0 ? C3Options[0].skillStoneOfPlayerId : null));

            NineSlotsStatusRefresh();
            TheNineSlot.target.mainProcessRunner.Run(SkillStonesBox.target.ArrangeSkillStonesToBox());
        }
    }
}