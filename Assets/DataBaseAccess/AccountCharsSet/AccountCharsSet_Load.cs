using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        #region 获取所有角色
        public static IEnumerator Load_List()
        {
            GetMonsterOfPlayerListForm form = new GetMonsterOfPlayerListForm
            {
                playerId = AccountSet._AccInfo.PlayerName
            };
            
            yield return LoadList_execution(
                form,
                model => {
                    AccountCharInfoDic.Clear();
                    foreach (GetMonsterOfPlayerDetailModel one in model.monsterOfPlayerList)
                    {
                        if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                            AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                        else
                            Debug.Log("重复的角色存档id："+ one.monsterOfPlayerId);
                    }
                },
                model => {
                    AccountCharInfoDic.Clear();
                    Debug.Log(" 人员列表读取失败 ");
                },
                Setting.Language
            );
        }
        
        public static IEnumerator LoadList_execution(GetMonsterOfPlayerListForm form, SuccessDelegate<GetMonsterOfPlayerListModel> success, FailDelegate<GetMonsterOfPlayerListModel> fail, ApiLanguage apiLanguage)
        {
            GetMonsterOfPlayerListModel listModel = new GetMonsterOfPlayerListModel();
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        listModel.monsterOfPlayerList = (List<GetMonsterOfPlayerDetailModel>)enumerator.Current;
                        success(listModel);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<GetMonsterOfPlayerListModel, GetMonsterOfPlayerListForm> 
                        (
                            "http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", 
                            form, 
                            ApiCaller.Instance.getHeader(apiLanguage),
                            model => {
                                success(model.data);
                            },
                            model => {
                                fail(model.data);
                            }
                        );
                    break;
            }
        }
        #endregion
        
        #region 获取单个角色
        /// <summary>
        /// Load the specified monsterlocalid.
        /// </summary>
        /// <returns>The load.</returns>
        /// <param name="monsterlocalid">Monsterlocalid.</param>       
        public static IEnumerator Load(string monsterlocalid)
        {
            GetMonsterOfPlayerDetailForm form = new GetMonsterOfPlayerDetailForm
            {
                monsterOfPlayerId = monsterlocalid
            };
            GetMonsterOfPlayerDetailModel accountCharInfo = null;
            yield return Load_execution(
                form,
                model => {
                    accountCharInfo = model;
                    DicAdd<string, GetMonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, monsterlocalid, accountCharInfo);
                },
                model => {
                    Debug.Log("读取角色失败："+ monsterlocalid);
                },
                Setting.Language
            );
            yield return accountCharInfo;
        }
        
        public static IEnumerator Load_execution(GetMonsterOfPlayerDetailForm form, SuccessDelegate<GetMonsterOfPlayerDetailModel> success, FailDelegate<GetMonsterOfPlayerDetailModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = LoadAccCharInfoViaJsonFile(form.monsterOfPlayerId);
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        success((GetMonsterOfPlayerDetailModel)enumerator.Current);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<GetMonsterOfPlayerDetailModel, GetMonsterOfPlayerDetailForm> 
                    (
                        "http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", 
                        form, 
                        ApiCaller.Instance.getHeader(apiLanguage),
                        model => {
                            success(model.data);
                        },
                        model => {
                            fail(model.data);
                        }
                    );
                    break;
            }
        }
        #endregion
    }
}