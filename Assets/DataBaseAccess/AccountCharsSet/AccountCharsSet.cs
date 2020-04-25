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
        public static IDictionary<string, MonsterOfPlayerListModel> AccountCharListObjectsDic = new Dictionary<string, MonsterOfPlayerListModel>();
        public static IDictionary<string, GetMonsterOfPlayerDetailModel> AccountCharInfoDic = new Dictionary<string, GetMonsterOfPlayerDetailModel>();

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

        public IEnumerator UpdateMyCharInfo(GetMonsterOfPlayerDetailModel target)
        {
            IEnumerator getchar = GetAccountCharInfo(target.monsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel targetAccountCharInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (targetAccountCharInfo == null)
                yield break;

            yield return ExecuteCharDataUpate(target);
            yield break;
        }
        
        public IEnumerator AddOneCharacterToAccount(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            IEnumerator temp_enumerator = null;
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
            GetMonsterOfPlayerDetailModel result = null;
            if (temp_enumerator.Current != null)
                result = (GetMonsterOfPlayerDetailModel)temp_enumerator.Current;
            if (result == null)
            {
                Debug.Log("角色添加失败");
            }
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
                yield break;
            }
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
            GetMonsterOfPlayerDetailModel accountCharInfo = null;
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    accountCharInfo = LoadAccountCharacterInfoViaJsonFile(monsterlocalid);
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    IEnumerator load = LoadAccountCharacterInfoRemote(monsterlocalid, ApiLanguage.JaJp);
                    yield return load;
                    accountCharInfo = (GetMonsterOfPlayerDetailModel)load.Current;
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
            DicAdd<string, GetMonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, monsterlocalid, accountCharInfo);
            yield return accountCharInfo;            
        }
    }
}