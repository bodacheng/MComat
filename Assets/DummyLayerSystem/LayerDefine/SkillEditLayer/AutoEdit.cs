using mainMenu;
using dataAccess;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Skill;

public partial class SkillEditLayer : UILayer
{
    void FinishRemains()
    {
        UnitInfo info = PreScene.target._focusing;
        UnitConfig unitConfig = Units.GetUnitConfig(info.r_id);
        SkillSet now = NineSlot.GetCurrentNineAndTwo();
        SkillSet targetSkillSet = SkillSet.FixSkillSet(unitConfig.TYPE, now, 1, true);

        if (targetSkillSet == null)
        {
            NineSlot.ValidationWarn(SkillSet.SkillEditError.UnableToFinish, PreScene.target._focusing.id);
        }
        else
        {
            Finish(info, targetSkillSet);
        }
    }
    
    void RandomAll()
    {
        UnitInfo info = PreScene.target._focusing;
        UnitConfig unitConfig = Units.GetUnitConfig(info.r_id);
        StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(info.id);
        // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
        SkillSet targetSkillSet = SkillSet.RandomSkillSet(unitConfig.TYPE, originSkillInfo?.skillId, 1, true);

        ForceClearAll();
        Finish(info, targetSkillSet);
    }

    void Finish(UnitInfo info, SkillSet targetSkillSet)
    {
        AddRandomStoneToSlot(info.id, 1, targetSkillSet.a1);
        AddRandomStoneToSlot(info.id, 2, targetSkillSet.a2);
        AddRandomStoneToSlot(info.id, 3, targetSkillSet.a3);
        AddRandomStoneToSlot(info.id, 4, targetSkillSet.b1);
        AddRandomStoneToSlot(info.id, 5, targetSkillSet.b2);
        AddRandomStoneToSlot(info.id, 6, targetSkillSet.b3);
        AddRandomStoneToSlot(info.id, 7, targetSkillSet.c1);
        AddRandomStoneToSlot(info.id, 8, targetSkillSet.c2);
        AddRandomStoneToSlot(info.id, 9, targetSkillSet.c3);
        NineSlot.NineSlotsStatusRefresh();
        StonesBox.RestFilter();
    }
    
    void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillID)
    {
        if (NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
        {
            return;
        }
        
        StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(monsterOfPlayerId);
        List<string> Options = Stones.GetMyStonesBySkillID(skillID);
        if (originSkillInfo != null && skillID == originSkillInfo.skillId)
        {
            NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(originSkillInfo.InstanceId));
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
            NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(targetStoneId));
        }

        SkillConfig skillConfig = SkillConfigTable.GetSkillConfig(skillID);
        StonesBox._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL,
            PosCal.GetWorldPos(PreScene.target.FxCamera, 
                NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 
                3),
            StonesBox._SkillStoneBoxTabEffectsManager.transform);
    }
}
