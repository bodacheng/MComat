using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class AccountCharsSet
    {
        public static IDictionary<string, GetMonsterOfPlayerDetailModel> AccountCharInfoDic = new Dictionary<string, GetMonsterOfPlayerDetailModel>();
        
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
        
        public static IEnumerator Update(GetMonsterOfPlayerDetailModel target)
        {
            if (AccountSet._AccInfo.accountprogress != PlayerAccountProgressStep.Freedom)//教程 阶段不保存
            {
                IEnumerator getchar = Load(target.monsterOfPlayerId);
                yield return getchar;
                GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
                yield break;
            }
            switch (AccountSet._playerinfoReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    yield return UpdateCharJsonSaveData(target);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return UpdateCharRemote(target,ApiLanguage.EnUs);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
            yield break;
        }
        
        public static IEnumerator AddToAccount(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            IEnumerator temp_enumerator = null;
            switch (AccountSet._playerinfoReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    temp_enumerator = AddNewCharToJsonSaveData(_accountCharacterInfo);// 内部已经包整理角色列表的处理
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
            yield return temp_enumerator;
            GetMonsterOfPlayerDetailModel result = null;
            if (temp_enumerator.Current != null)
                result = (GetMonsterOfPlayerDetailModel)temp_enumerator.Current;
            if (result == null)
            {
                Debug.Log("角色添加失败");
            }
        }
        
        public static GetMonsterOfPlayerDetailModel Get(string monsterlocalid)
        {
            if (monsterlocalid == null)
            {
                Debug.Log("流程错误");
                return null;
            }
            if (AccountCharInfoDic.ContainsKey(monsterlocalid))
            {
                if (AccountCharInfoDic[monsterlocalid] != null)
                    return AccountCharInfoDic[monsterlocalid];
            }
            return null;
        }
        
        public static IEnumerator Load(string monsterlocalid)
        {
            GetMonsterOfPlayerDetailModel accountCharInfo = null;
            switch (AccountSet._playerinfoReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    accountCharInfo = LoadAccCharInfoViaJsonFile(monsterlocalid);
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    IEnumerator load = LoadAccountCharacterInfoRemote(monsterlocalid, ApiLanguage.JaJp);
                    yield return load;
                    accountCharInfo = (GetMonsterOfPlayerDetailModel)load.Current;
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
            DicAdd<string, GetMonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, monsterlocalid, accountCharInfo);
            yield return accountCharInfo;            
        }
        
        public static IEnumerator LoadAll()
        {
            List<GetMonsterOfPlayerDetailModel> charList = new List<GetMonsterOfPlayerDetailModel>();
            switch (AccountSet._playerinfoReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    charList = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
            }
            AccountCharInfoDic.Clear();
            foreach (GetMonsterOfPlayerDetailModel one in charList)
            {
                if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                    AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                else
                    Debug.Log("重复的角色存档id："+ one.monsterOfPlayerId);
            }
            yield break;
        }
        
        public static IEnumerator LoadTutorial()
        {
            List<GetMonsterOfPlayerDetailModel> charList = new List<GetMonsterOfPlayerDetailModel>();
            charList = LoadAll_Json(Application.persistentDataPath + "/TutorialCharacterInfos");
            AccountCharInfoDic.Clear();
            foreach (GetMonsterOfPlayerDetailModel one in charList)
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