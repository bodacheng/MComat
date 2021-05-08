using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class MyMonsters
    {
        public static void LoadLocal()
        {
            Dic.Clear();
            List<MonsterOfPlayerDetailModel> charList = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
            foreach (MonsterOfPlayerDetailModel one in charList)
            {
                if (!Dic.ContainsKey(one.monsterOfPlayerId))
                    Dic.Add(one.monsterOfPlayerId, one);
                else
                    Debug.Log("重复的角色存档id：" + one.monsterOfPlayerId);
            }
        }
    }
}