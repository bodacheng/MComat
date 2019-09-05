using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class AccountCharsSet
    {
        public static AccountCharsSet instance;
        public static IDictionary<string, MonsterOfPlayerListModel> accountCharacterInfoListObjectsDictionary = new Dictionary<string, MonsterOfPlayerListModel>();
        public static IDictionary<string, GetMonsterOfPlayerDetailModel> AccountCharacterInfoDictionary = new Dictionary<string, GetMonsterOfPlayerDetailModel>();
        //public static AccountCharacterInfo[] ownedChars;//本单例模式的处理对象

        public static bool checkifContainsAccountCharsSetKey(string key)
        {
            if (key == null)
                return false;
            if (accountCharacterInfoListObjectsDictionary.Keys.Contains(key))
                return true;
            else
                return false;        
        }

        private AccountCharsSet()
        {
        }
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

        public IEnumerator updateMyCharInfo(GetMonsterOfPlayerDetailModel characterDataInfo)
        {
            IEnumerator getchar = getAccountCharacterInfo(characterDataInfo.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (targetAccountCharacterInfo == null)
                yield break;

            if (checkCharDataUpateInfo(targetAccountCharacterInfo, characterDataInfo))
            {
                yield return executeCharDataUpate(characterDataInfo);
            }
            yield break;
        }

        public IEnumerator loadMyOwnedAccountCharacterInfoList()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    loadAccountCharacterInfoListObjectsViaJsonFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return loadAccountCharacterInfoListObjectsRemote(ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            yield break;
        }

        // 这个地方就应该产生一个本地版本的更新核实。即所谓本地和远程双把关。
        // 每次更新一个角色，包括的核实信息有以下：
        // 1. 技能编辑格式自身没有问题(必杀与普工平衡)
        // 2. 有对应的石头 
        // 3. 如果更新需要消耗，有足够的钱
        public bool checkCharDataUpateInfo(GetMonsterOfPlayerDetailModel before, GetMonsterOfPlayerDetailModel after)//先检查
        {
            bool ok = false;

            if (before.a1_skill_stone_record_id != after.a1_skill_stone_record_id)
            {
            }
            if (before.a2_skill_stone_record_id != after.a2_skill_stone_record_id)
            {
            }
            if (before.a3_skill_stone_record_id != after.a3_skill_stone_record_id)
            {
            }
            if (before.b1_skill_stone_record_id != after.b1_skill_stone_record_id)
            {
            }
            if (before.b2_skill_stone_record_id != after.b2_skill_stone_record_id)
            {
            }
            if (before.b3_skill_stone_record_id != after.b3_skill_stone_record_id)
            {
            }
            if (before.c1_skill_stone_record_id != after.c1_skill_stone_record_id)
            {
            }
            if (before.c2_skill_stone_record_id != after.c2_skill_stone_record_id)
            {
            }
            if (before.c3_skill_stone_record_id != after.c3_skill_stone_record_id)
            {
            }
            return true;
        }

        // 这一步的执行应该是毫不犹豫的因为上一步已经确定了数据无误可以更新
        // 所以在这里应该也是对应三个版本。
        public IEnumerator executeCharDataUpate(GetMonsterOfPlayerDetailModel after)//再执行
        {
            if (AccountSet.instance._PlayerAccountInfo.accountprogress != playerAccountProgressStep.Freedom)//教程 阶段不保存
            {
                IEnumerator getchar = getAccountCharacterInfo(after.monsterOfPlayerId);
                yield return getchar;
                GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;

                if (targetAccountCharacterInfo == null)
                {
                    Debug.Log("欲更新角色不存在。");
                    yield break;
                }
                yield break;
            }
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    yield return updateCharJsonSaveData(after);
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return updateCharRemote(after,ApiLanguage.EnUs);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            yield return MySkillStonesReader.Instance.refreshAllMyStonesUsingMonsterInfo();
            yield break;
        }

        public IEnumerator plusExpForAccountChar(string charlocalID, int plusExp)
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    yield return plusExpForAccountCharLocalSaveData(charlocalID, plusExp);
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:

                    break;
                case playerinfoReferenceMode.formalVersion:
                    yield return plusExpForAccountCharRemote(charlocalID, plusExp);
                    break;
            }
        }

        public IEnumerator getAccountCharacterInfo(string monsterlocalid)
        {
            if (monsterlocalid == null)
            {
                yield return null;
                yield break;
            }
            if (AccountCharacterInfoDictionary.ContainsKey(monsterlocalid))
            {
                if (AccountCharacterInfoDictionary[monsterlocalid] != null)
                {
                    yield return AccountCharacterInfoDictionary[monsterlocalid];
                    yield break;
                }
                else
                {
                    Debug.Log("角色字典内存丢失？？localid:" + monsterlocalid);
                }
            }

            GetMonsterOfPlayerDetailModel accountCharacterInfo = null;
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    accountCharacterInfo = loadAccountCharacterInfoViaJsonFile(monsterlocalid);
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    IEnumerator load = loadAccountCharacterInfoRemote(monsterlocalid,ApiLanguage.JaJp);
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
                yield break;
            }
            yield return null;
            yield break;
        }

        private int intCompare(int i1, int i2)
        {
            if (i1 > i2)
            {
                return 1;
            }
            if (i1 < i2)
            {
                return -1;
            }
            return 0;
        }

        public void sellOneChar(string localID)
        {
        }
    }
}