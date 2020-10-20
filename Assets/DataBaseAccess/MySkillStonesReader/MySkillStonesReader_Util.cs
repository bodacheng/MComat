using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Skill;

// 配置文件属于资源信息，不是账户信息，应该分离开处理。
namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        // 把所有技能的等级显示出来
        public static void ShowAllMyStoneLevel()
        {
            foreach (KeyValuePair<string, SKStoneItem> keyValuePair in RenderModelDic)
            {
                keyValuePair.Value.ShowStoneLevel();
            }
        }
        
        // 关闭所有技能石文字类提示
        public static void CloseAllMyStoneFloatInfo()
        {
            foreach (KeyValuePair<string, SKStoneItem> keyValuePair in RenderModelDic)
            {
                keyValuePair.Value.CloseInfo();
            }
        }
        
        public static void PreventStonesFromDestroy()
        {
            foreach (KeyValuePair<string, SKStoneItem> keyValuePair in RenderModelDic)
            {
                keyValuePair.Value.transform.SetParent(ResourceKeeper.dontDestroyOnLoadParent);
            }
        }
        
        // 用于过滤显示在技能石盒内的技能石
        public static List<string> TargetStonesFromAccount(string type, int ExType, bool close, bool near, bool far)
        {
            List<string> SkillStonesOfTypeAndExType = new List<string>(); //技能石本地id
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in Dic)
            {
                if (keyValuePair.Value.Inherent == "true")
                {
                    continue;//原生技能不显示在技能石盒子内
                }
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(keyValuePair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("????"+ keyValuePair.Value.skillId);
                    continue;
                }
                if (_SkillConfig.TYPE == type && 
                    (_SkillConfig.SP_LEVEL == ExType || ExType == -1) &&
                    SkillConfig.RangeLimit(_SkillConfig.AI_MIN_DIS, _SkillConfig.AI_MAX_DIS, close, near, far))
                {
                    SkillStonesOfTypeAndExType.Add(keyValuePair.Value.skillStoneOfPlayerId);
                }
            }
            return SkillStonesOfTypeAndExType;
        }
        
        // 获取某个角色装备中的技能石列表应该是在已经读取了玩家所有技能石之后，这个过程从本地内存读就可以。我们只需要确保读取技能石，和下面这个函数总实质是一前一后。
        public static List<SkillStoneOfPlayerInfoModel> GetEquipingStones(string monsterOfPlayerId)
        {
            List<SkillStoneOfPlayerInfoModel> targetStones = new List<SkillStoneOfPlayerInfoModel>();
            foreach(KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in Dic)
            {
                if (keyValuePair.Value.inUsingMonsterOfPlayerId == monsterOfPlayerId)
                {
                    targetStones.Add(keyValuePair.Value);
                }
            }
            return targetStones;
        }
        
        public static IEnumerator StoneGotcha()
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return SkillStoneGotcha("POLI0000000000000002",ApiLanguage.JaJp);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
        }        
    }
}