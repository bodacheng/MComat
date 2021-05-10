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
            List<MonsterOfPlayerInfo> charList = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
            foreach (MonsterOfPlayerInfo one in charList)
            {
                if (!Dic.ContainsKey(one.InstanceId))
                    Dic.Add(one.InstanceId, one);
                else
                    Debug.Log("重复的角色存档id：" + one.InstanceId);
            }
        }
    }
}