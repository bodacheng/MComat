using Api.Dto.Model;
using Skill;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        List<SkillStoneOfPlayerInfoModel> GetMyStonesOnNineSlot()
        {
            List<SkillStoneOfPlayerInfoModel> returnValue = new List<SkillStoneOfPlayerInfoModel>();
            List<string> IDlist = GetUsingStonesId();
            for (int i = 0; i < IDlist.Count; i++)
            {
                returnValue.Add(MySkillStonesReader.Get(IDlist[i]));
            }
            return returnValue;
        }
        
        // 获取当前九宫格内技能石存档id, 长度为当前九宫格内技能石数量
        public List<string> GetUsingStonesId()
        {
            A1DragAndDropCell.UpdateMyItem();
            A2DragAndDropCell.UpdateMyItem();
            A3DragAndDropCell.UpdateMyItem();
            B1DragAndDropCell.UpdateMyItem();
            B2DragAndDropCell.UpdateMyItem();
            B3DragAndDropCell.UpdateMyItem();
            C1DragAndDropCell.UpdateMyItem();
            C2DragAndDropCell.UpdateMyItem();
            C3DragAndDropCell.UpdateMyItem();
            
            List<string> IDs = new List<string>();
            
            string A1 = A1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string A2 = A2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string A3 = A3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B1 = B1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B2 = B2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B3 = B3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C1 = C1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C2 = C2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C3 = C3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            
            if (A1 != null)
                IDs.Add(A1);
            if (A2 != null)
                IDs.Add(A2);
            if (A3 != null)
                IDs.Add(A3);
            if (B1 != null)
                IDs.Add(B1);
            if (B2 != null)
                IDs.Add(B2);
            if (B3 != null)
                IDs.Add(B3);
            if (C1 != null)
                IDs.Add(C1);
            if (C2 != null)
                IDs.Add(C2);
            if (C3 != null)
                IDs.Add(C3);
            return IDs;
        }
        
        // 返回的是技能定义ID，长度固定为9
        public List<string> GetCurrentNineSlotAllSkillIds()
        {
            List<string> NineSkillIDs = new List<string>();
            string A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            string C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem()._SkillConfig.RECORD_ID : "-1";
            NineSkillIDs.Add(A1);
            NineSkillIDs.Add(A2);
            NineSkillIDs.Add(A3);
            NineSkillIDs.Add(B1);
            NineSkillIDs.Add(B2);
            NineSkillIDs.Add(B3);
            NineSkillIDs.Add(C1);
            NineSkillIDs.Add(C2);
            NineSkillIDs.Add(C3);
            return NineSkillIDs;
        }
        
        void ShowNineSlotExSurplus(int wholePoint)
        {
            int pointremain = wholePoint / 10;
            for (int i = 0; i < remainCharges.Count; i++)
            {
                if (i + 1 <= pointremain)
                {
                    remainCharges[i].SetActive(true);
                } 
                else
                {
                    remainCharges[i].SetActive(false);
                }
            }
        }
        
        public bool RefreshWholePointBasedOnCurrentNineSlots(SKStoneItem item, StoneCell replacePosition)
        {
            List<string> nineskillids = Instance.GetCurrentNineSlotAllSkillIds();

            if (item == null)
            {
                item = new SKStoneItem
                {
                    _SkillConfig = new SkillConfig()
                };
            }

            if (replacePosition == Instance.A1DragAndDropCell)
            {
                nineskillids[0] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.A2DragAndDropCell)
            {
                nineskillids[1] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.A3DragAndDropCell)
            {
                nineskillids[2] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.B1DragAndDropCell)
            {
                nineskillids[3] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.B2DragAndDropCell)
            {
                nineskillids[4] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.B3DragAndDropCell)
            {
                nineskillids[5] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.C1DragAndDropCell)
            {
                nineskillids[6] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.C2DragAndDropCell)
            {
                nineskillids[7] = item._SkillConfig.RECORD_ID;
            }
            if (replacePosition == Instance.C3DragAndDropCell)
            {
                nineskillids[8] = item._SkillConfig.RECORD_ID;
            }
            int wholepint = MySkillStonesReader.SkillBalancePoint(nineskillids[0], nineskillids[1], nineskillids[2], nineskillids[3], nineskillids[4], nineskillids[5], nineskillids[6], nineskillids[7], nineskillids[8]);
            return wholepint >= 0;
        }
        
        public void RefreshCurrentHpBasedOnNineSlots()
        {
            List<SkillStoneOfPlayerInfoModel> stonelist = GetMyStonesOnNineSlot();
            List<int> level = new List<int>();
            List<string> skillIDs = new List<string>();
            
            foreach(SkillStoneOfPlayerInfoModel one in stonelist)
            {
                level.Add(int.Parse(one.level));
                skillIDs.Add(one.skillId);
            }
            
            _HP.text = "HP:" + INI_Hp(skillIDs, level).ToString();
        }
        
        public static float INI_Hp(List<string> skillIDs, List<int> level)
        {
            float WholeHP = 0;
            for (int index = 0; index < skillIDs.Count; index++)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(skillIDs[index]);
                WholeHP += SkillEntity.StoneHpCal(_SkillConfig.HP_WEIGHT, level[index]);
            }
            return WholeHP;
        }
        
        // 这个是从角色存档来读取
        public int GetNineSlotWholePointOfMonster(GetMonsterOfPlayerDetailModel _AccountCharInfo)
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetMonsterEquipingStones(_AccountCharInfo.monsterOfPlayerId);
            string A1=null, A2=null, A3=null, B1=null, B2=null, B3=null, C1=null, C2=null, C3=null;
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
    }
}