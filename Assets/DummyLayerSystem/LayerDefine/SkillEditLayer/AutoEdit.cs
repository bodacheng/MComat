using mainMenu;
using dataAccess;
using UnityEngine;
using System.Linq;

public partial class SkillEditLayer : UILayer
{
    void FinishRemains()
    {
        var info = PreScene.target.Focusing;
        var unitConfig = Units.GetUnitConfig(info.r_id);
        var now = nineSlot.GetCurrentNineAndTwo();
        var targetSkillSet = SkillSet.FixSkillSet(unitConfig.TYPE, now, true, info.id);
        
        if (targetSkillSet == null)
        {
            // 这里必须有某些其他处理（比如不让按钮显示？）
            PopupLayer.ArrangeWarnWindow(Translate.Get("NoEnoughStoneToFill"));
        }
        else
        {
            Finish(info, targetSkillSet);
        }
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
        nineSlot.NineSlotsStatusRefresh();
        stonesBox.RestFilter();
    }
    
    void AddRandomStoneToSlot(string unitInstanceId, int targetSlot, string skillID)
    {
        if (nineSlot.AllSlot[targetSlot - 1]._cell.GetItem() != null)
        {
            return;
        }
        
        var originSkillInfo = Stones.GetOriginSkillOfUnit(unitInstanceId);
        var options = Stones.GetMyStonesBySkillID(skillID);
        if (originSkillInfo != null && skillID == originSkillInfo.SkillId)
        {
            Stones.SetTempUnitUsage(originSkillInfo.InstanceId, unitInstanceId);
            nineSlot.AllSlot[targetSlot - 1]._cell.AddItem(Stones.GetRenderModel(originSkillInfo.InstanceId));
        }else{
            options = options.OrderByDescending(x => Stones.Get(x).Level).ToList();
            string targetStoneId = null;
            for (int i = 0 ; i < options.Count; i++)
            {
                var stoneInfo = Stones.Get(options[i]);
                if ((unitInstanceId != stoneInfo.unitInstanceId && dataAccess.Units.Get(stoneInfo.unitInstanceId) == null)
                    ||
                    unitInstanceId == stoneInfo.unitInstanceId)
                {
                    targetStoneId = options[i];
                    break;
                }
            }
            Stones.SetTempUnitUsage(targetStoneId, unitInstanceId);
            nineSlot.AllSlot[targetSlot - 1]._cell.AddItem(Stones.GetRenderModel(targetStoneId));
        }
        
        var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(skillID);
        stonesBox._tabEffects.SkillButtonExplosion(skillConfig.SP_LEVEL,
            PosCal.GetWorldPos(PreScene.target.noPostProcessCamera, 
                nineSlot.AllSlot[targetSlot - 1]._cell.GetComponent<RectTransform>(), 
                3),
            stonesBox._tabEffects.transform);
    }
}
