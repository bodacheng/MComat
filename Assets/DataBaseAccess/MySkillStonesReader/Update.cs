using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;
using System;

namespace dataAccess
{
    public partial class Stones
    {
        // 更新存档数据
        public static void Update(IDictionary<string, Tuple<string, string>> ToEditStones, Action success, Action fail)
        {
            switch (Account.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    foreach (KeyValuePair<string, Tuple<string, string>> kv in ToEditStones)
                    {
                        if (!Dic.ContainsKey(kv.Key) || Dic[kv.Key] == null)
                        {
                            Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + kv.Key);
                            fail.Invoke();
                            return;
                        }
                        StoneOfPlayerInfo ofPlayerInfo = Dic[kv.Key].Clone();
                        ofPlayerInfo.inUsingMonsterOfPlayerId = kv.Value.Item1;
                        ofPlayerInfo.inUsingSkillSlot = kv.Value.Item2;
                        Update_Json(ofPlayerInfo);
                    }
                    success.Invoke();
                break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    CloudScript.UpdateSkillEdit(ToEditStones, success, fail);
                break;
                case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
    }
}