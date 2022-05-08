using mainMenu;
using System.Collections.Generic;
using UnityEngine;
using Skill;
using System.Linq;

// 配置文件属于资源信息，不是账户信息，应该分离开处理。
namespace dataAccess
{
    public static partial class Stones
    {
        #region 技能石模型相关
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
        #endregion

        #region 财产数据相关
        // 用于过滤显示在技能石盒内的技能石
        public static List<string> TargetStonesFromAccount(SkillStonesBox.StoneFilterForm filterForm)
        {
            List<string> SkillStonesOfTypeAndExType = new List<string>(); //技能石本地id
            foreach (KeyValuePair<string, StoneOfPlayerInfo> keyValuePair in Dic)
            {
                if (keyValuePair.Value.Inherent == "true")
                {
                    continue;//原生技能不显示在技能石盒子内
                }
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfig(keyValuePair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("????"+ keyValuePair.Value.skillId);
                    continue;
                }
                List<int> exs = filterForm.exType.ToList();
                List<int> rare = filterForm.rare.ToList();
                if (_SkillConfig.TYPE == filterForm.type 
                    && exs.Contains(_SkillConfig.SP_LEVEL) 
                    && SkillConfig.RangeLimit(_SkillConfig.AIAttrs.AI_MIN_DIS, _SkillConfig.AIAttrs.AI_MAX_DIS, filterForm.close, filterForm.near, filterForm.far)
                    && rare.Contains(_SkillConfig.RARITY_LEVEL))
                {
                    SkillStonesOfTypeAndExType.Add(keyValuePair.Value.InstanceId);
                }
            }
            return SkillStonesOfTypeAndExType;
        }
        
        // exceptList ： 除了这些 技能石账户ID
        // extraList ：额外添加这些 技能石账户ID
        public static List<string> TargetStonesFromAccount_except(SkillStonesBox.StoneFilterForm filterForm, List<string> exceptList, List<string> extraList, bool notUsing)
        {
            List<string> origin = TargetStonesFromAccount(filterForm);
            List<string> list = new List<string>();
            for (int i = 0; i < origin.Count; i++)
            {
                if (extraList != null && extraList.Contains(origin[i]))
                {
                    list.Add(origin[i]);
                    continue;
                }
                StoneOfPlayerInfo infoModel = Get(origin[i]);
                if (notUsing)
                {
                    if (MyMonsters.Get(infoModel.unitInstanceId) != null)
                    {
                        continue;
                    }
                }

                if ((exceptList == null || !exceptList.Contains(infoModel.InstanceId)))
                {
                    list.Add(origin[i]);
                }
            }
            return list;
        }

        // 从账户随机抽取符合要求的技能石
        // exceptSkIDs : 除了这些技能ID。切记是技能ID
        public static StoneOfPlayerInfo SearchStoneForRandomSet(SkillStonesBox.StoneFilterForm filterForm, List<string> exceptSkIDs)
        {
            StoneOfPlayerInfo infoModel;
            List<string> exceptStones = new List<string>();
            for (int i = 0; i < exceptSkIDs.Count; i++)
            {
                List<string> exceptAccIds = Stones.GetMyStonesBySkillID(exceptSkIDs[i]);
                exceptStones.AddRange(exceptAccIds);
            }
            List<string> StoneAccIDs = Stones.TargetStonesFromAccount_except(filterForm, exceptStones, null, true);
            if (StoneAccIDs.Count == 0)
                return null;
            int ranDom = Random.Range(0, StoneAccIDs.Count);
            string stoneAccID = StoneAccIDs[ranDom];
            infoModel = Stones.Get(stoneAccID);
            return infoModel;
        }

        // 获取某个角色装备中的技能石列表应该是在已经读取了玩家所有技能石之后，这个过程从本地内存读就可以。我们只需要确保读取技能石，和下面这个函数总实质是一前一后。
        public static List<StoneOfPlayerInfo> GetEquippingStones(string instanceId)
        {
            var targetStones = new List<StoneOfPlayerInfo>();
            foreach(var keyValuePair in Dic)
            {
                if (keyValuePair.Value.unitInstanceId == instanceId)
                {
                    targetStones.Add(keyValuePair.Value);
                }
            }
            return targetStones;
        }
        
        // 获取一个角色的原生技能的对应技能石信息
        public static StoneOfPlayerInfo GetOriginSkillOfMonster(string monsterOfPlayerId)
        {
            StoneOfPlayerInfo targetStone = null;
            foreach(KeyValuePair<string, StoneOfPlayerInfo> keyValuePair in Dic)
            {
                if (keyValuePair.Value.unitInstanceId == monsterOfPlayerId && keyValuePair.Value.Inherent == "true")
                {
                    targetStone = keyValuePair.Value;
                }
            }
            return targetStone;
        }
        #endregion
    }
}