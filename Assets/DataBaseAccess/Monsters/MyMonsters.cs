using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class MyMonsters
    {
        public static IDictionary<string, MonsterOfPlayerInfo> Dic = new Dictionary<string, MonsterOfPlayerInfo>();
        
        public static bool CheckExist(string key)
        {
            if (key == null)
            {
                return false;
            }
            if (Dic.ContainsKey(key))
            {
                if (Dic[key] != null)
                    return true;
            }
            return false;
        }
        
        public static MonsterOfPlayerInfo Get(string monsterlocalid)
        {
            if (monsterlocalid == null)
            {
                return null;
            }
            if (Dic.ContainsKey(monsterlocalid))
            {
                if (Dic[monsterlocalid] != null)
                    return Dic[monsterlocalid];
            }
            return null;
        }
        
        public static void LoadTutorial()
        {
            List<MonsterOfPlayerInfo> charList = new List<MonsterOfPlayerInfo>();
            //charList = LoadAll_Json(Application.persistentDataPath + "/TutorialCharacterInfos");
            Dic.Clear();
            foreach (MonsterOfPlayerInfo one in charList)
            {
                if (!Dic.ContainsKey(one.InstanceId))
                    Dic.Add(one.InstanceId, one);
                else
                    Debug.Log("重复的角色存档id："+ one.InstanceId);
            }
        }
    }
}