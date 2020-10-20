using System.Collections.Generic;
using UnityEngine;
using System;
using dataAccess;
using Api.Dto.Model.Common;

namespace Api.Dto.Model {

	/// <summary>
	/// プレーヤ所有モンスター情報詳細取得モデル
	/// 作成者：Auto Generated
	/// バージョン：1.00 2019/07/01
	/// </summary>
	[Serializable]
	public class GetMonsterOfPlayerDetailModel {

		/// <summary>
		/// プレーヤ所有モンスターID
		/// </summary>
		public string monsterOfPlayerId { get; set; }

		/// <summary>
		/// プレーヤレコードID
		/// </summary>
		public string playerId { get; set; }

		/// <summary>
		/// モンスターID
		/// </summary>
		public string monsterId { get; set; }
        
        public static CharDataInfo GetCharDataInfo(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            try
            {
                CharDataInfo characterDataInfo = new CharDataInfo
                {
                    ResourceID = accountCharacterInfo.monsterId,
                    monsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId
                };

                List<SkillStoneOfPlayerInfoModel> targets = MySkillStonesReader.GetEquipingStones(accountCharacterInfo.monsterOfPlayerId);
                NineAndTwo nineAndTwo = new NineAndTwo();
                CharConfig _TempCharacterResourceInfo = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(accountCharacterInfo.monsterId));
                if (_TempCharacterResourceInfo == null)
                {
                    Debug.Log("角色定义信息错误。monsterId：" + accountCharacterInfo.monsterId);
                    return null;
                }
                for (int i = 0; i < targets.Count; i++)
                {
                    switch(targets[i].inUsingSkillSlot)
                    {
                        case "1":
                            nineAndTwo.A1skillid = targets[i].skillId;
                            nineAndTwo.A1level = targets[i].GetLevel();
                        break;
                        case "2":
                            nineAndTwo.A2skillid = targets[i].skillId;
                            nineAndTwo.A2level = targets[i].GetLevel();
                        break;
                        case "3":
                            nineAndTwo.A3skillid = targets[i].skillId;
                            nineAndTwo.A3level = targets[i].GetLevel();
                        break;
                        case "4":
                            nineAndTwo.B1skillid = targets[i].skillId;
                            nineAndTwo.B1level = targets[i].GetLevel();
                        break;
                        case "5":
                            nineAndTwo.B2skillid = targets[i].skillId;
                            nineAndTwo.B2level = targets[i].GetLevel();
                        break;
                        case "6":
                            nineAndTwo.B3skillid = targets[i].skillId;
                            nineAndTwo.B3level = targets[i].GetLevel();
                        break;
                        case "7":
                            nineAndTwo.C1skillid = targets[i].skillId;
                            nineAndTwo.C1level = targets[i].GetLevel();
                        break;
                        case "8":
                            nineAndTwo.C2skillid = targets[i].skillId;
                            nineAndTwo.C2level = targets[i].GetLevel();
                        break;
                        case "9":
                            nineAndTwo.C3skillid = targets[i].skillId;
                            nineAndTwo.C3level = targets[i].GetLevel();
                        break;
                    }
                }
                nineAndTwo.moveType = _TempCharacterResourceInfo.MoveType;
                nineAndTwo.rushType = _TempCharacterResourceInfo.RushType;
                nineAndTwo.canDefend = _TempCharacterResourceInfo.DEFENDABLE_FLAG;
                characterDataInfo._NineAndTwo = nineAndTwo;
                characterDataInfo._NineAndTwo.SortNineAndTwo();
                return characterDataInfo;
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
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetEquipingStones(monsterOfPlayerId);
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
