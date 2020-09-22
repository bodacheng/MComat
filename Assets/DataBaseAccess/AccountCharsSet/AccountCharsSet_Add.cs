using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class AccountCharsSet
    {                
        public static IEnumerator AddToAccount(GetMonsterOfPlayerDetailModel _accountCharacterInfo)
        {
            IEnumerator temp_enumerator = null;
            switch (AccountSet.ReferenceMode)
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
    }
}