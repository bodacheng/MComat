using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class AccountCharsSet
    {
        public static AccountCharsSet instance;
        public static IDictionary<string, MonsterOfPlayerListModel> AccountCharacterInfoListObjectsDictionary = new Dictionary<string, MonsterOfPlayerListModel>();
        public static IDictionary<string, GetMonsterOfPlayerDetailModel> AccountCharacterInfoDictionary = new Dictionary<string, GetMonsterOfPlayerDetailModel>();

        public static AccountCharsSet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountCharsSet();
                }
                return instance;
            }
        }

        public static bool CheckIfContainsAccountCharsSetKey(string key)
        {
            return key != null && AccountCharacterInfoListObjectsDictionary.Keys.Contains(key);
        }

        public IEnumerator UpdateMyCharInfo(GetMonsterOfPlayerDetailModel characterDataInfo)
        {
            IEnumerator getchar = GetAccountCharInfo(characterDataInfo.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (targetAccountCharacterInfo == null)
                yield break;

            yield return ExecuteCharDataUpate(characterDataInfo);
            yield break;
        }
        
        IEnumerator temp_enumerator;
        GetMonsterOfPlayerDetailModel result;
        public IEnumerator AddOneCharacterToAccount(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            temp_enumerator = null;
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    temp_enumerator = AddNewCharToJsonSaveData(_accountCharacterInfo);// 内部已经包整理角色列表的处理
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            yield return temp_enumerator;
            result = null;
            if (temp_enumerator.Current != null)
                result = (GetMonsterOfPlayerDetailModel)temp_enumerator.Current;
            if (result != null)
            {
                //yield return MonsterBox.AddOneNewIcon(result.monsterOfPlayerId);
            }else{
                Debug.Log("角色添加失败");
            }
        }

        public IEnumerator LoadMyOwnedAccountCharacterInfoList()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    LoadAccountCharacterInfoListObjectsViaJsonFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return LoadAccountCharacterInfoListObjectsRemote(ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            foreach (KeyValuePair<string, MonsterOfPlayerListModel> keyValuePair in AccountCharacterInfoListObjectsDictionary)
            {
                IEnumerator getchar = Instance.GetAccountCharInfo(keyValuePair.Value.monsterOfPlayerId);
                yield return getchar;
            }
            yield break;
        }

        // 这一步的执行应该是毫不犹豫的因为上一步已经确定了数据无误可以更新
        // 所以在这里应该也是对应三个版本。
        public IEnumerator ExecuteCharDataUpate(GetMonsterOfPlayerDetailModel after)//再执行
        {
            if (AccountSet.instance._PlayerAccountInfo.accountprogress != PlayerAccountProgressStep.Freedom)//教程 阶段不保存
            {
                IEnumerator getchar = GetAccountCharInfo(after.monsterOfPlayerId);
                yield return getchar;
                GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;

                if (targetAccountCharacterInfo == null)
                {
                    Debug.Log("欲更新角色不存在。");
                    yield break;
                }
                yield break;
            }
            Debug.Log("开始尝试更新角色信息。monsterOfPlayerId:"+after.monsterOfPlayerId);
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    yield return UpdateCharJsonSaveData(after);
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return UpdateCharRemote(after,ApiLanguage.EnUs);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            //yield return MySkillStonesReader.Instance.VerifyAllMyStonesUsingMonsterInfo();
            yield break;
        }

        public IEnumerator GetAccountCharInfo(string monsterlocalid)
        {
            if (monsterlocalid == null)
            {
                yield return null;
            }
            else
            {
                if (AccountCharacterInfoDictionary.ContainsKey(monsterlocalid))
                {
                    if (AccountCharacterInfoDictionary[monsterlocalid] != null)
                    {
                        yield return AccountCharacterInfoDictionary[monsterlocalid];
                    }
                    else
                    {
                        Debug.Log("角色字典内存丢失？？localid:" + monsterlocalid);
                        yield return null;
                    }
                }
                else
                {
                    Debug.Log("开始新注册角色信息：" + monsterlocalid);
                    GetMonsterOfPlayerDetailModel accountCharacterInfo = null;
                    switch (AccountSet.Instance._playerinfoReferenceMode)
                    {
                        case playerinfoReferenceMode.localTestSaveData:
                            accountCharacterInfo = LoadAccountCharacterInfoViaJsonFile(monsterlocalid);
                            break;
                        case playerinfoReferenceMode.remoteTestPlayer:
                            IEnumerator load = LoadAccountCharacterInfoRemote(monsterlocalid, ApiLanguage.JaJp);
                            yield return load;
                            accountCharacterInfo = (GetMonsterOfPlayerDetailModel)load.Current;
                            break;
                        case playerinfoReferenceMode.formalVersion:
                            break;
                    }

                    if (accountCharacterInfo != null)
                    {
                        if (!AccountCharacterInfoDictionary.ContainsKey(monsterlocalid))
                        {
                            AccountCharacterInfoDictionary.Add(monsterlocalid, accountCharacterInfo);
                        }
                        else
                        {
                            Debug.Log("错误?localid重复:" + monsterlocalid);
                            AccountCharacterInfoDictionary[monsterlocalid] = accountCharacterInfo;
                        }
                        yield return accountCharacterInfo;
                    }
                }
            }
        }

        int IntCompare(int i1, int i2)
        {
            return i1 > i2 ? 1 : i1 < i2 ? -1 : 0;
        }
    }
}