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
        
        SkillEditError CheckEditBasedOnCurrent(List<string> nineskillids)
        {
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
            return wholepint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
        }
        
        // 检查起始技能有没有普通技能
        SkillEditError CheckStartSKills(string a1skill, string a2skill, string a3skill)
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