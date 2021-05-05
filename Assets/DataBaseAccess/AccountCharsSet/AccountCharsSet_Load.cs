using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public static void Load_List(Action<int> finished)
        {
            AccountCharInfoDic.Clear();
            List<MonsterOfPlayerDetailModel> charList = default;
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    charList = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
                    foreach (MonsterOfPlayerDetailModel one in charList)
                    {
                        if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                            AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                        else
                            Debug.Log("重复的角色存档id：" + one.monsterOfPlayerId);
                    }
                    finished.Invoke(1);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabRead.LoadItems(finished);
                    break;
            }
        }
    }
}