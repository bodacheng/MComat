using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class AccountCharsSet
    {
        public static IDictionary<string, MonsterOfPlayerDetailModel> AccountCharInfoDic = new Dictionary<string, MonsterOfPlayerDetailModel>();
        
        public static bool CheckExist(string key)
        {
            if (key == null)
            {
                return false;
            }
            if (AccountCharInfoDic.ContainsKey(key))
            {
                if (AccountCharInfoDic[key] != null)
                    return true;
            }
            return false;
        }
        
        public static MonsterOfPlayerDetailModel Get(string monsterlocalid)
        {
            if (monsterlocalid == null)
            {
                return null;
            }
            if (AccountCharInfoDic.ContainsKey(monsterlocalid))
            {
                if (AccountCharInfoDic[monsterlocalid] != null)
                    return AccountCharInfoDic[monsterlocalid];
            }
            return null;
        }
        
        public static IEnumerator LoadTutorial()
        {
            List<MonsterOfPlayerDetailModel> charList = new List<MonsterOfPlayerDetailModel>();
            //charList = LoadAll_Json(Application.persistentDataPath + "/TutorialCharacterInfos");
            AccountCharInfoDic.Clear();
            foreach (MonsterOfPlayerDetailModel one in charList)
            {
                if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                    AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                else
                    Debug.Log("重复的角色存档id："+ one.monsterOfPlayerId);
            }
            yield break;
        }
    }
}