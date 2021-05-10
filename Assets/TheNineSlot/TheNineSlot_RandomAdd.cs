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
            MonsterOfPlayerInfo info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            NineAndTwo now = TheNineSlot.target.GetCurrentNineAndTwo();
            NineAndTwo targetSkillSet = NineAndTwo.FixSkillSet(charConfig.TYPE, now, 1, true);

            if (targetSkillSet == null)
            {
                target.ValiationWarn(NineAndTwo.SkillEditError.UnableToFinish, MemberDetail.target._focusing.InstanceId);
            }
            else
            {
                IEnumerator temp()
                {
                    // 如果角色有原生技能，则已经存在于targetSkillSet当中
                    AddRandomStoneToSlot(info.InstanceId, 1, targetSkillSet.A1skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 2, targetSkillSet.A2skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 3, targetSkillSet.A3skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 4, targetSkillSet.B1skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 5, targetSkillSet.B2skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 6, targetSkillSet.B3skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 7, targetSkillSet.C1skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 8, targetSkillSet.C2skillid);
                    yield return null;
                    AddRandomStoneToSlot(info.InstanceId, 9, targetSkillSet.C3skillid);
                    yield return null;
                    NineSlotsStatusRefresh();
                    SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
                }

                PreScene.target.mainProcessRunner.RunAsQueued(temp());
            }
        }

        void RandomAll()
        {
            MonsterOfPlayerInfo info = MemberDetail.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.monsterId);
            StoneOfPlayerInfo originSkillInfo = MySkillStones.GetOriginSkillOfMonster(info.InstanceId);
            // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
            NineAndTwo targetSkillSet = NineAndTwo.RandomSkillSet(charConfig.TYPE, originSkillInfo?.skillId, 1, true);

            IEnumerator temp()
            {
                ForceClearAll();
                // 如果角色有原生技能，则已经存在于targetSkillSet当中
                AddRandomStoneToSlot(info.InstanceId, 1, targetSkillSet.A1skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 2, targetSkillSet.A2skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 3, targetSkillSet.A3skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 4, targetSkillSet.B1skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 5, targetSkillSet.B2skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 6, targetSkillSet.B3skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 7, targetSkillSet.C1skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 8, targetSkillSet.C2skillid);
                yield return null;
                AddRandomStoneToSlot(info.InstanceId, 9, targetSkillSet.C3skillid);
                yield return null;
                NineSlotsStatusRefresh();
                SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
            }

            PreScene.target.mainProcessRunner.RunAsQueued(temp());
        }

        
        void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillid)
        {
            if (allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
            {
                return;
            }

            StoneOfPlayerInfo originSkillInfo = MySkillStones.GetOriginSkillOfMonster(monsterOfPlayerId);
            List<string> Options = MySkillStones.GetMyStonesBySkillID(skillid);
            if (originSkillInfo != null && skillid == originSkillInfo.skillId)
            {
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(MySkillStones.GetRenderModel(originSkillInfo.InstanceId));
            }else{
                Options.OrderByDescending(x => MySkillStones.Get(x).EXP);
                string targetStoneId = null;
                for (int i = 0; i < Options.Count; i++)
                {
                    StoneOfPlayerInfo stoneInfo = MySkillStones.Get(Options[i]);
                    if (MyMonsters.Get(stoneInfo.inUsingMonsterOfPlayerId) == null)
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