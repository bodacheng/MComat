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
            //RandomAll();
            FinishRemains();
        }

        void FinishRemains()
        {
            MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            NineAndTwo now = TheNineSlot.target.GetCurrentNineAndTwo();
            NineAndTwo targetSkillSet = NineAndTwo.FixSkillSet(charConfig.TYPE, now, 1, true);

            if (targetSkillSet == null)
            {
                target.ValiationWarn(NineAndTwo.SkillEditError.UnableToFinish, MemberDetail.target._focusing.monsterOfPlayerId);
            }
            else
            {
                IEnumerator temp()
                {
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
        }

        void RandomAll()
        {
            MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStones.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
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
            if (allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
            {
                return;
            }

            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStones.GetOriginSkillOfMonster(monsterOfPlayerId);
            List<string> Options = MySkillStones.GetMyStonesBySkillID(skillid);
            if (originSkillInfo != null && skillid == originSkillInfo.skillId)
            {
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStones.GetRenderModel(originSkillInfo.skillStoneOfPlayerId));
            }else{
                Options.OrderByDescending(x => MySkillStones.Get(x).EXP);
                string targetStoneId = null;
                for (int i = 0; i < Options.Count; i++)
                {
                    SkillStoneOfPlayerInfoModel stoneInfo = MySkillStones.Get(Options[i]);
                    if (AccountCharsSet.Get(stoneInfo.inUsingMonsterOfPlayerId) == null)
                    {
                        targetStoneId = Options[i];
                        break;
                    }
                }
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStones.GetRenderModel(targetStoneId));
            }

            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillid);
            SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL,
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 3),
            SkillStonesBox.target._SkillStoneBoxTabEffectsManager.transform);
        }
    }
}