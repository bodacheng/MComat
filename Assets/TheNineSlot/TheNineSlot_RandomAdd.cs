using UnityEngine;
using dataAccess;
using System.Collections.Generic;
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
            UnitInfo info = PreScene.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.r_id);
            SkillSet now = GetCurrentNineAndTwo();
            SkillSet targetSkillSet = SkillSet.FixSkillSet(charConfig.TYPE, now, 1, true);

            if (targetSkillSet == null)
            {
                ValiationWarn(SkillSet.SkillEditError.UnableToFinish, PreScene.target._focusing.id);
            }
            else
            {
                // 如果角色有原生技能，则已经存在于targetSkillSet当中
                AddRandomStoneToSlot(info.id, 1, targetSkillSet.a1);
                AddRandomStoneToSlot(info.id, 2, targetSkillSet.a2);
                AddRandomStoneToSlot(info.id, 3, targetSkillSet.a3);
                AddRandomStoneToSlot(info.id, 4, targetSkillSet.b1);
                AddRandomStoneToSlot(info.id, 5, targetSkillSet.b2);
                AddRandomStoneToSlot(info.id, 6, targetSkillSet.b3);
                AddRandomStoneToSlot(info.id, 7, targetSkillSet.c1);
                AddRandomStoneToSlot(info.id, 8, targetSkillSet.c2);
                AddRandomStoneToSlot(info.id, 9, targetSkillSet.c3);
                NineSlotsStatusRefresh();
                SkillStonesBox.target.RestFilter();
            }
        }

        void RandomAll()
        {
            UnitInfo info = PreScene.target._focusing;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.r_id);
            StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(info.id);
            // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
            SkillSet targetSkillSet = SkillSet.RandomSkillSet(charConfig.TYPE, originSkillInfo?.skillId, 1, true);

            ForceClearAll();
            // 如果角色有原生技能，则已经存在于targetSkillSet当中
            AddRandomStoneToSlot(info.id, 1, targetSkillSet.a1);
            AddRandomStoneToSlot(info.id, 2, targetSkillSet.a2);
            AddRandomStoneToSlot(info.id, 3, targetSkillSet.a3);
            AddRandomStoneToSlot(info.id, 4, targetSkillSet.b1);
            AddRandomStoneToSlot(info.id, 5, targetSkillSet.b2);
            AddRandomStoneToSlot(info.id, 6, targetSkillSet.b3);
            AddRandomStoneToSlot(info.id, 7, targetSkillSet.c1);
            AddRandomStoneToSlot(info.id, 8, targetSkillSet.c2);
            AddRandomStoneToSlot(info.id, 9, targetSkillSet.c3);
            NineSlotsStatusRefresh();
            SkillStonesBox.target.RestFilter();
        }

        void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillid)
        {
            if (allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
            {
                return;
            }

            StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(monsterOfPlayerId);
            List<string> Options = Stones.GetMyStonesBySkillID(skillid);
            if (originSkillInfo != null && skillid == originSkillInfo.skillId)
            {
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(originSkillInfo.InstanceId));
            }else{
                Options.OrderByDescending(x => Stones.Get(x).EXP);
                string targetStoneId = null;
                for (int i = 0; i < Options.Count; i++)
                {
                    StoneOfPlayerInfo stoneInfo = Stones.Get(Options[i]);
                    if (MyMonsters.Get(stoneInfo.inUsingMonsterOfPlayerId) == null)
                    {
                        targetStoneId = Options[i];
                        break;
                    }
                }
                allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(targetStoneId));
            }

            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillid);
            SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL,
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 3),
            SkillStonesBox.target._SkillStoneBoxTabEffectsManager.transform);
        }
    }
}