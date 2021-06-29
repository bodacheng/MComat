using System.Collections.Generic;
using UnityEngine;
using System;
using dataAccess;

namespace Api.Dto.Model {

    /// <summary>
    /// プレーヤ所有モンスター情報詳細取得モデル
    /// 作成者：Auto Generated
    /// バージョン：1.00 2019/07/01
    /// </summary>
    [Serializable]
    public class MonsterOfPlayerInfo
    {
        /// <summary>
        /// プレーヤ所有モンスターID
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// モンスターID
        /// </summary>
        public string monsterId { get; set; }
        
        public static CharDataInfo GetCharDataInfo(MonsterOfPlayerInfo accountCharInfo)
        {
            try
            {
                CharDataInfo charDataInfo = new CharDataInfo
                {
                    r_id = accountCharInfo.monsterId,
                    id = accountCharInfo.InstanceId
                };
                
                List<StoneOfPlayerInfo> targets = Stones.GetEquipingStones(accountCharInfo.InstanceId);
                NineAndTwo set = new NineAndTwo();
                CharConfig _CharConfigInfo = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(accountCharInfo.monsterId));
                if (_CharConfigInfo == null)
                {
                    Debug.Log("角色定义信息错误。monsterId：" + accountCharInfo.monsterId);
                    return null;
                }
                for (int i = 0; i < targets.Count; i++)
                {
                    switch(targets[i].inUsingSkillSlot)
                    {
                        case "1":
                            set.a1 = targets[i].skillId;
                            set.A1lv = targets[i].GetLevel();
                        break;
                        case "2":
                            set.a2 = targets[i].skillId;
                            set.A2lv = targets[i].GetLevel();
                        break;
                        case "3":
                            set.a3 = targets[i].skillId;
                            set.A3lv = targets[i].GetLevel();
                        break;
                        case "4":
                            set.b1 = targets[i].skillId;
                            set.B1lv = targets[i].GetLevel();
                        break;
                        case "5":
                            set.b2 = targets[i].skillId;
                            set.B2lv = targets[i].GetLevel();
                        break;
                        case "6":
                            set.b3 = targets[i].skillId;
                            set.B3lv = targets[i].GetLevel();
                        break;
                        case "7":
                            set.c1 = targets[i].skillId;
                            set.C1lv = targets[i].GetLevel();
                        break;
                        case "8":
                            set.c2 = targets[i].skillId;
                            set.C2lv = targets[i].GetLevel();
                        break;
                        case "9":
                            set.c3 = targets[i].skillId;
                            set.C3lv = targets[i].GetLevel();
                        break;
                    }
                }
                set.SetPassive(_CharConfigInfo.DEFENDABLE_FLAG, _CharConfigInfo.MoveType, _CharConfigInfo.RushType);
                charDataInfo.set = set;
                charDataInfo.set.SortNineAndTwo();
                return charDataInfo;
            }
            catch (Exception e)
            {
                Debug.Log("数据库信息有错误:" + e);
                return null;
            }
        }
        
        // 这个是从角色存档来读取
        public int GetNineSlotWholePointOfMonster(string monsterOfPlayerId)
        {
            List<StoneOfPlayerInfo> equipingstones = Stones.GetEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
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
            int wholePoint = NineAndTwo.SkillBalancePoint(A1,A2,A3,B1,B2,B3,C1,C2,C3);
            return wholePoint;
        }
	}
}