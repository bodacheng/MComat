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

                List<SkillStoneOfPlayerInfoModel> targets = MySkillStonesReader.GetMonsterEquipingStones(accountCharacterInfo.monsterOfPlayerId);
                NineAndTwo nineAndTwo = new NineAndTwo();
                CharConfig _TempCharacterResourceInfo = MonstersConfigTable.Instance.RowToCharacterResourceInfo(MonstersConfigTable.Instance.Find_RECORD_ID(accountCharacterInfo.monsterId));
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
	}
}
