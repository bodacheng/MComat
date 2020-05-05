using Api.Dto.Model;
using Skill;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using System.Collections;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void ValiationWarn(SkillEditError skillEditError, string monsterOfPlayerID)
        {
            GetMonsterOfPlayerDetailModel charInfo = AccountCharsSet.Get(monsterOfPlayerID);

            switch(skillEditError)
            {
                case SkillEditError.RepeatedSkill:
                    IEnumerator temp()
                    {
                        _ValiWarn.text = "不可装备相同技能";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.Instance.mainProcessRunner.Run(temp());
                break;
                case SkillEditError.UnBalanced:
                    IEnumerator temp2()
                    {
                        _ValiWarn.text = "角色："+ monsterOfPlayerID + "技能点数失衡";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.Instance.mainProcessRunner.Run(temp2());
                break;
            }
        }
        
        public SkillEditError CheckEditBasedOnCurrent(SKStoneItem item, StoneCell replacePosition)
        {
            List<string> nineskillids = target.GetCurrentNineSlotAllSkillIds();// 基于九宫格
            
            if (item == null)
            {
                item = new SKStoneItem
                {
                    _SkillConfig = new SkillConfig()
                };
            }
            
            if (replacePosition == target.A1DragAndDropCell)
            {
                nineskillids[0] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.A2DragAndDropCell)
            {
                nineskillids[1] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.A3DragAndDropCell)
            {
                nineskillids[2] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.B1DragAndDropCell)
            {
                nineskillids[3] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.B2DragAndDropCell)
            {
                nineskillids[4] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.B3DragAndDropCell)
            {
                nineskillids[5] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.C1DragAndDropCell)
            {
                nineskillids[6] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.C2DragAndDropCell)
            {
                nineskillids[7] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == target.C3DragAndDropCell)
            {
                nineskillids[8] = item._SkillConfig.RECORD_ID;
            }

            List<string> checkSame = new List<string>();
            for (int i = 0; i < nineskillids.Count; i++)
            {
                if (!checkSame.Contains(nineskillids[i]))
                {
                    if (SkillConfigTable.GetSkillConfigByID(nineskillids[i]) != null)
                        checkSame.Add(nineskillids[i]);
                }else{
                    return SkillEditError.RepeatedSkill;
                }
            }
            
            int wholepint = MySkillStonesReader.SkillBalancePoint(nineskillids[0], nineskillids[1], nineskillids[2], nineskillids[3], nineskillids[4], nineskillids[5], nineskillids[6], nineskillids[7], nineskillids[8]);
            if (wholepint < 0)
                return SkillEditError.UnBalanced;
            return SkillEditError.Perfect;
        }
        
        public SkillEditError CheckEditBasedOnSaveDataAfterOneStoneRemoved(string monsterOfPlayerId, string SkillID) // 基于存档
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetMonsterEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "2":
                        A2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "3":
                        A3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "4":
                        B1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "5":
                        B2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "6":
                        B3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "7":
                        C1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "8":
                        C2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "9":
                        C3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                }
            }
            
            List<string> checkSame = new List<string>();
            bool CheckRepeat(string skillID)
            {
                if (checkSame.Contains(skillID))
                {
                    return true;
                }
                if (SkillConfigTable.GetSkillConfigByID(skillID) != null)
                {
                    checkSame.Add(skillID);
                }
                return false;
            }
            
            if (CheckRepeat(A1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(A2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(A3))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B3))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C3))
            {
                return SkillEditError.RepeatedSkill;
            }
                        
            int wholePoint = MySkillStonesReader.SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
            if (wholePoint < 0)
                return SkillEditError.UnBalanced;
            return SkillEditError.Perfect;
        }
        
        // 这个是从角色存档来读取
        public int GetNineSlotWholePointOfMonster(GetMonsterOfPlayerDetailModel _AccountCharInfo)
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetMonsterEquipingStones(_AccountCharInfo.monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = equipingstones[i].skillId;
                        break;
                    case "2":
                        A2 = equipingstones[i].skillId;
                        break;
                    case "3":
                        A3 = equipingstones[i].skillId;
                        break;
                    case "4":
                        B1 = equipingstones[i].skillId;
                        break;
                    case "5":
                        B2 = equipingstones[i].skillId;
                        break;
                    case "6":
                        B3 = equipingstones[i].skillId;
                        break;
                    case "7":
                        C1 = equipingstones[i].skillId;
                        break;
                    case "8":
                        C2 = equipingstones[i].skillId;
                        break;
                    case "9":
                        C3 = equipingstones[i].skillId;
                        break;
                }
            }
            int wholePoint = MySkillStonesReader.SkillBalancePoint(A1,A2,A3,B1,B2,B3,C1,C2,C3);
            return wholePoint;
        }
        
        public enum SkillEditError
        {
            UnBalanced,
            RepeatedSkill,
            Perfect
        }
    }
}