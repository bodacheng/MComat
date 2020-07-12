using Api.Dto.Model;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using System.Collections;
using Skill;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public enum SkillEditError
        {
            UnBalanced,
            RepeatedSkill,
            NoNormalStart,
            Perfect
        }
        
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
                    PreScene.target.mainProcessRunner.Run(temp());
                break;
                case SkillEditError.UnBalanced:
                    IEnumerator temp2()
                    {
                        _ValiWarn.text = "角色："+ monsterOfPlayerID + "技能点数失衡";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.Run(temp2());
                break;
                case SkillEditError.NoNormalStart:
                    IEnumerator temp3()
                    {
                        _ValiWarn.text = "第一竖列必须有一个非必杀技";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.Run(temp3());
                break;
            }
        }
        
        // 靠9个技能ID判断技能组是否合法，技能编辑原始函数
        public static SkillEditError CheckEdit(string A1, string A2, string A3, string B1, string B2, string B3, string C1, string C2, string C3)
        {
            // 第一列技能必须有普通技能
            if (CheckStartSKills(A1, B1, C1) == SkillEditError.NoNormalStart)
            {
                return SkillEditError.NoNormalStart;
            }
            
            // 检查技能重复
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
            return wholePoint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
        }
        
        SkillEditError CheckEditBasedOnCurrent(List<string> nineskillids)
        {
            List<string> checkSame = new List<string>();
            for (int i = 0; i < nineskillids.Count; i++)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(nineskillids[i]);
                if (_SkillConfig == null)
                    continue;
                if (!checkSame.Contains(nineskillids[i]))
                {
                    checkSame.Add(nineskillids[i]);
                }else{
                    return SkillEditError.RepeatedSkill;
                }
            }
            int wholepint = MySkillStonesReader.SkillBalancePoint(nineskillids[0], nineskillids[1], nineskillids[2], nineskillids[3], nineskillids[4], nineskillids[5], nineskillids[6], nineskillids[7], nineskillids[8]);
            return wholepint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
        }
        
        // 检查起始技能有没有普通技能
        static SkillEditError CheckStartSKills(string a1skill, string a2skill, string a3skill)
        {
            // 第一列技能必须有普通技能
            List<string> NormalSkillsOfAList = new List<string>();            
            SkillConfig _SkillConfigA1 = SkillConfigTable.GetSkillConfigByID(a1skill);
            SkillConfig _SkillConfigB1 = SkillConfigTable.GetSkillConfigByID(a2skill);
            SkillConfig _SkillConfigC1 = SkillConfigTable.GetSkillConfigByID(a3skill);
            
            if (_SkillConfigA1 != null && _SkillConfigA1.SP_LEVEL == 0)
                NormalSkillsOfAList.Add(_SkillConfigA1.REAL_NAME);
            if (_SkillConfigB1 != null && _SkillConfigB1.SP_LEVEL == 0)
                NormalSkillsOfAList.Add(_SkillConfigB1.REAL_NAME);
            if (_SkillConfigC1 != null && _SkillConfigC1.SP_LEVEL == 0)
                NormalSkillsOfAList.Add(_SkillConfigC1.REAL_NAME);
                
            return NormalSkillsOfAList.Count == 0 ? SkillEditError.NoNormalStart : SkillEditError.Perfect;
        }
    }
}