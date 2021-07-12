using Api.Dto.Model;
using UnityEngine;
using dataAccess;
using System.Collections;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void ValiationWarn(SkillSet.SkillEditError skillEditError, string monsterOfPlayerID)
        {
            UnitInfo charInfo = MyMonsters.Get(monsterOfPlayerID);
            switch(skillEditError)
            {
                case SkillSet.SkillEditError.RepeatedSkill:
                    IEnumerator temp()
                    {
                        _ValiWarn.text = "不可装备相同技能";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.RunAsQueued(temp());
                break;
                case SkillSet.SkillEditError.UnBalanced:
                    IEnumerator temp2()
                    {
                        _ValiWarn.text = "角色："+ monsterOfPlayerID + "技能点数失衡";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.RunAsQueued(temp2());
                break;
                case SkillSet.SkillEditError.NoNormalStart:
                    IEnumerator temp3()
                    {
                        _ValiWarn.text = "第一竖列必须有一个非必杀技";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.RunAsQueued(temp3());
                break;
                case SkillSet.SkillEditError.UnableToFinish:
                    IEnumerator temp4()
                    {
                        _ValiWarn.text = "没法补全当前九宫格";
                        yield return new WaitForSecondsRealtime(2f);
                        _ValiWarn.text = "";
                    }
                    PreScene.target.mainProcessRunner.RunAsQueued(temp4());
                    break;
            }
        }
        
    }
}