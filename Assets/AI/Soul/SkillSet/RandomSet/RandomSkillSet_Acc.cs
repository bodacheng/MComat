using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Skill;
using Api.Dto.Model;
using mainMenu;

public partial class NineAndTwo
{
    // 根据账户内拥有的技能石来安排九宫格内技能石排布。
    public static NineAndTwo RandomSkillSet(string type, string originSkill, int skilllevel, bool baseOnAcc)
    {
        NineAndTwo nineAndTwo = new NineAndTwo();
        SkillConfig originSkillConfig = SkillConfigTable.GetSkillConfigByID(originSkill);
        
        for (int i = 1; i <= 9; i++)
        {
            if (i == 1)
            {
                if (originSkillConfig != null && originSkillConfig.SP_LEVEL == 0)
                {
                    nineAndTwo.A1skillid = originSkillConfig.RECORD_ID;
                }else{
                    SkillRandomAdd(type, nineAndTwo, i, baseOnAcc);
                }
            }
            else if (i == 2) // A2
            {
                if (originSkillConfig != null && originSkillConfig.SP_LEVEL != 0)
                {
                    nineAndTwo.A2skillid = originSkillConfig.RECORD_ID; 
                }else{
                    SkillRandomAdd(type, nineAndTwo, i, baseOnAcc);
                }
            }
            else
            {
                SkillRandomAdd(type, nineAndTwo, i, baseOnAcc);
            }
        }
        nineAndTwo.SetSkillLevel(skilllevel);
        nineAndTwo.SortNineAndTwo();
        return nineAndTwo;
    }
    
    // exceptSkIDs : 除了这些技能ID。切记是技能ID
    static SkillStoneOfPlayerInfoModel SearchStoneForRandomSet(SkillStonesBox.StoneFilterForm filterForm, List<string> exceptSkIDs)
    {
        SkillStoneOfPlayerInfoModel infoModel;

        List<string> exceptStones = new List<string>();
        for (int i = 0; i < exceptSkIDs.Count; i++)
        {
            List<string> exceptAccIds = MySkillStonesReader.GetMyStonesBySkillID(exceptSkIDs[i]);
            exceptStones.AddRange(exceptAccIds);
        }
        List<string> StoneAccIDs = MySkillStonesReader.TargetStonesFromAccount_except(filterForm, exceptStones, null);
        if (StoneAccIDs.Count == 0)
            return null;
        int ranDom = Random.Range(0, StoneAccIDs.Count);
        string stoneAccID = StoneAccIDs[ranDom];
        infoModel = MySkillStonesReader.Get(stoneAccID);
        return infoModel;
    }
    
    static void SkillRandomAdd(string focusingtype, NineAndTwo nineAndTwo, int targetSlot, bool baseOnAcc)
    {
        List<string> exceptSKIds = nineAndTwo.SkillIDList();
        
        if (targetSlot == 1)
        {
            SkillStonesBox.StoneFilterForm filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = new int[1] { 0 },
                close = false,
                near = false,
                far = false
            };
            if (baseOnAcc)
            {
                SkillStoneOfPlayerInfoModel infoModel = SearchStoneForRandomSet(filterForm, exceptSKIds);
                if (infoModel == null) // 如果账户已经没有符合要求的石头
                {
                    Debug.Log("A1的普攻都找不到");
                    return;
                }
                nineAndTwo.A1skillid = infoModel.skillId;
            }else{
                string skid = SearchStoneForRandomSet2(filterForm, exceptSKIds);
                if (skid == null) // 如果账户已经没有符合要求的石头
                {
                    Debug.Log("A1的普攻都找不到");
                    return;
                }
                nineAndTwo.A1skillid = skid;
            }
        }else{
            SkillStonesBox.StoneFilterForm filterForm = new SkillStonesBox.StoneFilterForm
            {
                type = focusingtype,
                exType = RemainSlotSPLevelCal(nineAndTwo).ToArray(),
                close = false,
                near = false,
                far = false
            };
            string skillid = null;
            if (baseOnAcc)
            {
                SkillStoneOfPlayerInfoModel stoneInfoModel = SearchStoneForRandomSet(filterForm, exceptSKIds);
                if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
                {
                    Debug.Log("无法为" + targetSlot +"找到合适技能石");
                    return;
                }
                skillid = stoneInfoModel.skillId;
            }else{
                string skid = SearchStoneForRandomSet2(filterForm, exceptSKIds);
                if (skid == null) // 如果账户已经没有符合要求的石头
                {
                    Debug.Log("无法为" + targetSlot +"找到合适技能石");
                    return;
                }
                skillid = skid;
            }
            
            switch (targetSlot)
            {
                case 2:
                    nineAndTwo.A2skillid = skillid;
                    break;
                case 3:
                    nineAndTwo.A3skillid = skillid;
                    break;
                case 4:
                    nineAndTwo.B1skillid = skillid;
                    break;
                case 5:
                    nineAndTwo.B2skillid = skillid;
                    break;
                case 6:
                    nineAndTwo.B3skillid = skillid;
                    break;
                case 7:
                    nineAndTwo.C1skillid = skillid;
                    break;
                case 8:
                    nineAndTwo.C2skillid = skillid;
                    break;
                case 9:
                    nineAndTwo.C3skillid = skillid;
                    break;
            }
        }
    }
}