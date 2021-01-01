using System.Collections;
using UnityEngine;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        static IEnumerator LoadOneUnit(string monsterID)
        {
            MonstersConfigTable.Row row = MonstersConfigTable.Instance.Find_RECORD_ID(monsterID);
            MonsterOfPlayerDetailModel returnValue = null;
            // GS2 version
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
                            MonsterOfPlayerDetailModel monsterOfPlayerDetailModel = new MonsterOfPlayerDetailModel
                            {
                                monsterId = monsterID,
                                monsterOfPlayerId = r.Result.Items[i].ItemSetId
                            };
                            returnValue = monsterOfPlayerDetailModel;
                        }
                    }
                },
                Auth._mysession.Session,    // GameSession ログイン状態を表すセッションオブジェクト
                "unit", //  ネームスペース名
                "UnitInventory",    //  インベントリの種類名
                row.REAL_NAME //  アイテムモデルの種類名
            );
            yield return returnValue;
        }
    }
}
