using Api.Dto.Model;
using Skill;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using UnityEngine.UI;

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

        public void RefreshCurrentHpBasedOnNineSlots()
        {
            List<SkillStoneOfPlayerInfoModel> stonelist = GetMyStonesOnNineSlot();
            List<int> level = new List<int>();
            List<string> skillIDs = new List<string>();
            
            foreach(SkillStoneOfPlayerInfoModel one in stonelist)
            {
                level.Add(one.GetLevel());
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
    }
}