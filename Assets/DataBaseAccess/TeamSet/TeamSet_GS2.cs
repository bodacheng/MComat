using System.Collections;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Model;
using UnityEngine;

// 站位信息应该有多个版本，其中包括剧情模式版本，不同的竞技场对应版本等等。
namespace dataAccess
{
    public partial class TeamSet
    {
        public static IEnumerator TestGS2(GetMonsterTeamOfPlayerForm form, SuccessDelegate<MonsterTeamOfPlayerModel> success, FailDelegate<MonsterTeamOfPlayerModel> fail, ApiLanguage apiLanguage)
        {
            yield return Auth._myclient.Client.Formation.GetForm(
                r => {
                    if (r.Error != null)
                    {
                        // エラーが発生した場合に到達
                        // r.Error は発生した例外オブジェクトが格納されている
                    }
                    else
                    {
                        Debug.Log(r.Result.Item.Name); // string フォームの保存領域の名前
                        Debug.Log(r.Result.Item.Index); // integer 保存領域のインデックス
                        Debug.Log(r.Result.Item.Slots); // list[Slot] スロットリスト
                        Debug.Log(r.Result.Mold.Name); // string フォームの保存領域の名前
                        Debug.Log(r.Result.Mold.UserId); // string ユーザーID
                        Debug.Log(r.Result.Mold.Capacity); // integer 現在のキャパシティ
                        Debug.Log(r.Result.MoldModel.Name); // string フォームの保存領域名
                        Debug.Log(r.Result.MoldModel.Metadata); // string メタデータ
                        Debug.Log(r.Result.MoldModel.FormModel.Name); // string フォームの種類名
                        Debug.Log(r.Result.MoldModel.FormModel.Metadata); // string フォームの種類のメタデータ
                        Debug.Log(r.Result.MoldModel.FormModel.Slots); // list[SlotModel] スリットリスト
                        Debug.Log(r.Result.MoldModel.InitialMaxCapacity); // integer フォームを保存できる初期キャパシティ
                        Debug.Log(r.Result.MoldModel.MaxCapacity); // integer フォームを保存できるキャパシティ
                        Debug.Log(r.Result.FormModel.Name); // string フォームの種類名
                        Debug.Log(r.Result.FormModel.Metadata); // string フォームの種類のメタデータ
                        Debug.Log(r.Result.FormModel.Slots); // list[SlotModel] スリットリスト
                    }
                },
                Auth._mysession.Session,
                "party",   //  ネームスペース名
                "party",   //  フォームの保存領域の名前
                1   //  保存領域のインデックス
            );
        }
    }
}