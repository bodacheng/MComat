using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;
using Gs2.Weave.Unit;
using Gs2.Weave.Login;

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
                    foreach (MonsterOfPlayerDetailModel one in model.monsterOfPlayerList)
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
                        listModel.monsterOfPlayerList = (List<MonsterOfPlayerDetailModel>)enumerator.Current;
                        success(listModel);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    // GS2 version

                    //yield return me_LoginDirector._myclient.Client.Inventory.ListInventoryModels(
                    //r => {
                    //    if (r.Error != null)
                    //    {
                    //        // エラーが発生した場合に到達
                    //        // r.Error は発生した例外オブジェクトが格納されている
                    //    }
                    //    else
                    //    {
                    //        for (int i = 0; i < r.Result.Items.Count; i++)
                    //        {
                    //            Debug.Log(r.Result.Items[i].Name); // list[InventoryModel] インベントリモデルのリスト
                    //        }
                    //    }
                    //},
                    //"unit"
                    //);

                    //yield return me_LoginDirector._myclient.Client.Inventory.ListItemModels(
                    //    r => {
                    //        if (r.Error != null)
                    //        {
                    //            // エラーが発生した場合に到達
                    //            // r.Error は発生した例外オブジェクトが格納されている
                    //        }
                    //        else
                    //        {
                    //            for (int i = 0; i < r.Result.Items.Count; i++)
                    //            {
                    //                Debug.Log(r.Result.Items[i].Name); // list[ItemModel] アイテムモデルのリスト
                    //            }
                    //        }
                    //    },
                    //    //me_LoginDirector._mysession.Session,    // GameSession ログイン状態を表すセッションオブジェクト
                    //    "unit",   //  ネームスペース名
                    //    "UnitInventory"   //  インベントリの種類名
                    //);

                    //yield return me_LoginDirector._myclient.Client.Inventory.ListInventories(
                    //    r => {
                    //        if (r.Error != null)
                    //        {
                    //            // エラーが発生した場合に到達
                    //            // r.Error は発生した例外オブジェクトが格納されている
                    //        }
                    //        else
                    //        {
                    //            for (int i = 0; i < r.Result.Items.Count; i++)
                    //            {
                    //                Debug.Log(r.Result.Items[i].CurrentInventoryCapacityUsage); // ist[Inventory] インベントリのリスト
                    //            }
                    //            Debug.Log(r.Result.NextPageToken); // string リストの続きを取得するためのページトークン
                    //        }
                    //    },
                    //    me_LoginDirector._mysession.Session,    // GameSession ログイン状態を表すセッションオブジェクト
                    //    "unit",   //  ネームスペース名
                    //    null,   //  データの取得を開始する位置を指定するトークン(オプション値)
                    //    null   //  データの取得件数(オプション値)
                    //);

                    //yield return me_LoginDirector._myclient.Client.Inventory.GetInventory(
                    //    r => {
                    //        if (r.Error != null)
                    //        {
                    //            // エラーが発生した場合に到達
                    //            // r.Error は発生した例外オブジェクトが格納されている
                    //        }
                    //        else
                    //        {
                    //            Debug.Log(r.Result.Item.InventoryId); // string インベントリ
                    //            Debug.Log(r.Result.Item.InventoryName); // string インベントリモデル名
                    //            Debug.Log(r.Result.Item.CurrentInventoryCapacityUsage); // integer 現在のインベントリのキャパシティ使用量
                    //            Debug.Log(r.Result.Item.CurrentInventoryMaxCapacity); // integer 現在のインベントリの最大キャパシティ
                    //        }
                    //    },
                    //    me_LoginDirector._mysession.Session,    // GameSession ログイン状態を表すセッションオブジェクト
                    //    "unit",   //  ネームスペース名
                    //    "UnitInventory"   //  インベントリの種類名
                    //);

    yield return Auth._myclient.Client.Inventory.GetItem(
    r => {
        if (r.Error != null)
        {
            // エラーが発生した場合に到達
            // r.Error は発生した例外オブジェクトが格納されている
        }
        else
        {
            for (int i = 0; i < r.Result.Items.Count; i++)
            {
                Debug.Log(r.Result.Items[i].ItemName); // list[ItemSet] 有効期限毎の{model_name}
                Debug.Log(r.Result.Items[i].Count);
                Debug.Log(r.Result.Items[i].ItemSetId);
            }
            Debug.Log(r.Result.ItemModel.Name); // string アイテムモデルの種類名
            Debug.Log(r.Result.ItemModel.Metadata); // string アイテムモデルの種類のメタデータ
            Debug.Log(r.Result.ItemModel.StackingLimit); // long スタック可能な最大数量
            Debug.Log(r.Result.ItemModel.AllowMultipleStacks); // boolean スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか
            Debug.Log(r.Result.ItemModel.SortValue); // integer 表示順番
            Debug.Log(r.Result.Inventory.InventoryId); // string インベントリ
            Debug.Log(r.Result.Inventory.InventoryName); // string インベントリモデル名
            Debug.Log(r.Result.Inventory.CurrentInventoryCapacityUsage); // integer 現在のインベントリのキャパシティ使用量
            Debug.Log(r.Result.Inventory.CurrentInventoryMaxCapacity); // integer 現在のインベントリの最大キャパシティ
        }
    },
    Auth._mysession.Session,    // GameSession ログイン状態を表すセッションオブジェクト
    "unit",   //  ネームスペース名
    "UnitInventory",   //  インベントリの種類名
    "boxer"   //  アイテムモデルの種類名
);

                    //yield return ApiCaller.Instance.Post<GetMonsterOfPlayerListModel, GetMonsterOfPlayerListForm> 
                    //    (
                    //        "http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", 
                    //        form, 
                    //        ApiCaller.Instance.getHeader(apiLanguage),
                    //        model => {
                    //            success(model.data);
                    //        },
                    //        model => {
                    //            fail(model.data);
                    //        }
                    //    );
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
            MonsterOfPlayerDetailModel accountCharInfo = null;
            yield return Load_execution(
                form,
                model => {
                    accountCharInfo = model;
                    DicAdd<string, MonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, monsterlocalid, accountCharInfo);
                },
                model => {
                    Debug.Log("读取角色失败："+ monsterlocalid);
                },
                Setting.Language
            );
            yield return accountCharInfo;
        }
        
        public static IEnumerator Load_execution(GetMonsterOfPlayerDetailForm form, SuccessDelegate<MonsterOfPlayerDetailModel> success, FailDelegate<MonsterOfPlayerDetailModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = LoadAccCharInfoViaJsonFile(form.monsterOfPlayerId);
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        success((MonsterOfPlayerDetailModel)enumerator.Current);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<MonsterOfPlayerDetailModel, GetMonsterOfPlayerDetailForm> 
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