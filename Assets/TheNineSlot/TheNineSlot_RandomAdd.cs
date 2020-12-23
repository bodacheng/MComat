using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Skill;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void Random()
        {
            MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            NineAndTwo targetSkillSet = NineAndTwo.RandomSkillSet(charConfig.TYPE, originSkillInfo?.skillId, 1, true);
            
            IEnumerator temp()
            {
                ForceClearAll();
                // 如果角色有原生技能，则已经存在于targetSkillSet当中
                AddRandomStoneToSlot(info.monsterOfPlayerId, 1, targetSkillSet.A1skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 2, targetSkillSet.A2skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 3, targetSkillSet.A3skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 4, targetSkillSet.B1skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 5, targetSkillSet.B2skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 6, targetSkillSet.B3skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 7, targetSkillSet.C1skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 8, targetSkillSet.C2skillid);
                yield return null;
                AddRandomStoneToSlot(info.monsterOfPlayerId, 9, targetSkillSet.C3skillid);
                yield return null;
                NineSlotsStatusRefresh();
                yield return SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
            }
            
            PreScene.target.mainProcessRunner.Run(temp());
        }
        
        void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillid)
        {
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(monsterOfPlayerId);
            List<string> Options = MySkillStonesReader.GetMyStonesBySkillID(skillid);
            if (originSkillInfo != null && skillid == originSkillInfo.skillId)
            {
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(originSkillInfo.skillStoneOfPlayerId));
            }else{
                Options.OrderByDescending(x => MySkillStonesReader.Get(x).EXP);
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStonesReader.GetRenderModel(Options.Count > 0 ? Options[0] : null));
            }

            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillid);
            //SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL, 
            //ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 3), 
            //SkillStonesBox.target._SkillStoneBoxTabEffectsManager.transform);
        }
    }
}