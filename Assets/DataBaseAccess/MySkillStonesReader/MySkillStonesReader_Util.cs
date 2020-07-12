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
        public static List<string> TargetStonesFromAccount(string type, int ExType, bool close, bool near, bool far, bool outrange)
        {
            List<string> SkillStonesOfTypeAndExType = new List<string>(); //技能石本地id
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in Dic)
            {
                if (keyValuePair.Value.Inherent == "true")
                {
                    continue;//原生技能不显示在技能石盒子内
                }
                SkillConfig _SkillConfigOfSkillStone = SkillConfigTable.GetSkillConfigByID(keyValuePair.Value.skillId);
                if (_SkillConfigOfSkillStone == null)
                {
                    Debug.Log("????"+ keyValuePair.Value.skillId);
                    continue;
                }
                if (_SkillConfigOfSkillStone.TYPE == type && (_SkillConfigOfSkillStone.SP_LEVEL == ExType || ExType == -1) &&
                    SkillConfig.RangeLimit(_SkillConfigOfSkillStone.AI_MIN_DIS,_SkillConfigOfSkillStone.AI_MAX_DIS,close, near, far, outrange))
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
        
        public static int SkillBalancePoint(string A1skillid, string A2skillid, string A3skillid, string B1skillid, string B2skillid, string B3skillid, string C1skillid, string C2skillid, string C3skillid)
        {
            SkillConfig _SkillConfigA1 = SkillConfigTable.GetSkillConfigByID(A1skillid);
            SkillConfig _SkillConfigA2 = SkillConfigTable.GetSkillConfigByID(A2skillid);
            SkillConfig _SkillConfigA3 = SkillConfigTable.GetSkillConfigByID(A3skillid);
            SkillConfig _SkillConfigB1 = SkillConfigTable.GetSkillConfigByID(B1skillid);
            SkillConfig _SkillConfigB2 = SkillConfigTable.GetSkillConfigByID(B2skillid);
            SkillConfig _SkillConfigB3 = SkillConfigTable.GetSkillConfigByID(B3skillid);
            SkillConfig _SkillConfigC1 = SkillConfigTable.GetSkillConfigByID(C1skillid);
            SkillConfig _SkillConfigC2 = SkillConfigTable.GetSkillConfigByID(C2skillid);
            SkillConfig _SkillConfigC3 = SkillConfigTable.GetSkillConfigByID(C3skillid);
            List<SkillConfig> allnineskill = new List<SkillConfig>();
            
            if (_SkillConfigA1 != null)
                allnineskill.Add(_SkillConfigA1);
            if (_SkillConfigA2 != null)
                allnineskill.Add(_SkillConfigA2);
            if (_SkillConfigA3 != null)
                allnineskill.Add(_SkillConfigA3);
            if (_SkillConfigB1 != null)
                allnineskill.Add(_SkillConfigB1);
            if (_SkillConfigB2 != null)
                allnineskill.Add(_SkillConfigB2);
            if (_SkillConfigB3 != null)
                allnineskill.Add(_SkillConfigB3);
            if (_SkillConfigC1 != null)
                allnineskill.Add(_SkillConfigC1);
            if (_SkillConfigC2 != null)
                allnineskill.Add(_SkillConfigC2);
            if (_SkillConfigC3 != null)
                allnineskill.Add(_SkillConfigC3);
                
            int wholeskillpoint = 0;
            for (int i = 0; i < allnineskill.Count; i++)
            {
                switch (allnineskill[i].SP_LEVEL)
                {
                    case 0:
                        wholeskillpoint += 10;
                        break;
                    case 1:
                        wholeskillpoint -= 10;
                        break;
                    case 2:
                        wholeskillpoint -= 20;
                        break;
                    case 3:
                        wholeskillpoint -= 30;
                        break;
                    case -1:
                        break;
                }
            }
            return wholeskillpoint;
        }
        
        public static IEnumerator StoneGotcha()
        {
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.localTestSaveData:
                    break;
                case playerInfoRefMode.remoteTestPlayer:
                    yield return SkillStoneGotcha("POLI0000000000000002",ApiLanguage.JaJp);
                    yield return LoadMySkillstonesRemote(ApiLanguage.JaJp);
                    break;
                case playerInfoRefMode.formalVersion:
                    break;
            }
        }        
    }
}