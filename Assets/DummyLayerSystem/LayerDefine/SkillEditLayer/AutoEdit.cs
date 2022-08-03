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
        var info = PreScene.target._focusing;
        var unitConfig = Units.GetUnitConfig(info.r_id);
        var now = NineSlot.GetCurrentNineAndTwo();
        var targetSkillSet = SkillSet.FixSkillSet(unitConfig.TYPE, now, 1, true);
        
        if (targetSkillSet == null)
        {
            // 这里必须有某些其他处理（比如不让按钮显示？）
        }
        else
        {
            Finish(info, targetSkillSet);
        }
    }
    
    void RandomAll()
    {
        var info = PreScene.target._focusing;
        var unitConfig = Units.GetUnitConfig(info.r_id);
        var originSkillInfo = Stones.GetOriginSkillOfMonster(info.id);
        // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
        var targetSkillSet = SkillSet.RandomSkillSet(unitConfig.TYPE, originSkillInfo?.skillId, 1, true);
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
        if (NineSlot.allSlot[targetSlot - 1]._cell.GetItem() != null)
        {
            return;
        }
        
        var originSkillInfo = Stones.GetOriginSkillOfMonster(monsterOfPlayerId);
        var Options = Stones.GetMyStonesBySkillID(skillID);
        if (originSkillInfo != null && skillID == originSkillInfo.skillId)
        {
            NineSlot.allSlot[targetSlot - 1]._cell.AddItem(Stones.GetRenderModel(originSkillInfo.InstanceId));
        }else{
            Options.OrderByDescending(x => Stones.Get(x).level);
            string targetStoneId = null;
            for (int i = 0; i < Options.Count; i++)
            {
                StoneOfPlayerInfo stoneInfo = Stones.Get(Options[i]);
                if (dataAccess.Units.Get(stoneInfo.unitInstanceId) == null)
                {
                    targetStoneId = Options[i];
                    break;
                }
            }
            NineSlot.allSlot[targetSlot - 1]._cell.AddItem(Stones.GetRenderModel(targetStoneId));
        }

        var skillConfig = SkillConfigTable.GetSkillConfig(skillID);
        StonesBox._tabEffects.SkillButtonExplosion(skillConfig.SP_LEVEL,
            PosCal.GetWorldPos(PreScene.target.FxCamera, 
                NineSlot.allSlot[targetSlot - 1]._cell.GetComponent<RectTransform>(), 
                3),
            StonesBox._tabEffects.transform);
    }
}
