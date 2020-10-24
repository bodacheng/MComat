using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using Skill;
using Api.Dto.Model;

public partial class NineAndTwo : MonoBehaviour
{
    // 根据账户内拥有的技能石来安排九宫格内技能石排布。
    public static NineAndTwo RandomSkillSet_BasedOnMyStones(string type, string originSkill, int skilllevel)
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
                    SkillRandomAdd_BasedOnMyStones(type, nineAndTwo, i);
                }
            }
            else if (i == 2) // A2
            {
                if (originSkillConfig != null && originSkillConfig.SP_LEVEL != 0)
                {
                    nineAndTwo.A2skillid = originSkillConfig.RECORD_ID; 
                }else{
                    SkillRandomAdd_BasedOnMyStones(type, nineAndTwo, i);
                }
            }
            else
            {
                SkillRandomAdd_BasedOnMyStones(type, nineAndTwo, i);
            }
        }
        
        nineAndTwo.A1level = skilllevel;
        nineAndTwo.A2level = skilllevel;
        nineAndTwo.A3level = skilllevel;
        nineAndTwo.B1level = skilllevel;
        nineAndTwo.B2level = skilllevel;
        nineAndTwo.B3level = skilllevel;
        nineAndTwo.C1level = skilllevel;
        nineAndTwo.C2level = skilllevel;
        nineAndTwo.C3level = skilllevel;
        
        return nineAndTwo;
    }
    
    static SkillStoneOfPlayerInfoModel SearchStoneForRandomSet(string type, int[] ExType, bool close, bool near, bool far, List<string> exceptSkIDs)
    {
        SkillStoneOfPlayerInfoModel infoModel;
        List<string> StoneAccIDs = MySkillStonesReader.TargetStonesFromAccount_unusing(type, ExType, close, near, far, exceptSkIDs);
        if (StoneAccIDs.Count == 0)
            return null;
        int ranDom = Random.Range(0, StoneAccIDs.Count);
        string stoneAccID = StoneAccIDs[ranDom];
        infoModel = MySkillStonesReader.Get(stoneAccID);
        return infoModel;
    }
    
    static void SkillRandomAdd_BasedOnMyStones(string focusingtype, NineAndTwo nineAndTwo, int targetSlot) 
    {
        List<string> exceptSKIds = new List<string>();
        if (nineAndTwo.A1skillid != null)
            exceptSKIds.Add(nineAndTwo.A1skillid);
        if (nineAndTwo.A2skillid != null)
            exceptSKIds.Add(nineAndTwo.A2skillid);
        if (nineAndTwo.A3skillid != null)
            exceptSKIds.Add(nineAndTwo.A3skillid);
        if (nineAndTwo.B1skillid != null)
            exceptSKIds.Add(nineAndTwo.B1skillid);
        if (nineAndTwo.B2skillid != null)
            exceptSKIds.Add(nineAndTwo.B2skillid);
        if (nineAndTwo.B3skillid != null)
            exceptSKIds.Add(nineAndTwo.B3skillid);
        if (nineAndTwo.C1skillid != null)
            exceptSKIds.Add(nineAndTwo.C1skillid);
        if (nineAndTwo.C2skillid != null)
            exceptSKIds.Add(nineAndTwo.C2skillid);
        if (nineAndTwo.C3skillid != null)
            exceptSKIds.Add(nineAndTwo.C3skillid);
        
        if (targetSlot == 1)
        {
            SkillStoneOfPlayerInfoModel infoModel = SearchStoneForRandomSet(focusingtype, new int[1] {0}, false, false, false, exceptSKIds);
            if (infoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("A1的普攻都找不到");
                return;
            }
            string skillid = infoModel.skillId;
            nineAndTwo.A1skillid = skillid;
        }else{
            int[] CanEx = RemainSlotSPLevelCal(nineAndTwo).ToArray();            
            SkillStoneOfPlayerInfoModel stoneInfoModel = SearchStoneForRandomSet(focusingtype, CanEx, false, false, false, exceptSKIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot +"找到合适技能石");
                return;
            }
            
            string skillid = stoneInfoModel.skillId;
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