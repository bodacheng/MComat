using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.Text.RegularExpressions;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Form.Common;
using Api.Dto.Model;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static IEnumerator login()
        {
            WWWForm form = new WWWForm();
            form.AddField("userId", "abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd");
            IEnumerator ask = RemoteAccess.generalRemoteAccess(form,"http://160.16.187.230/AssetStoreFight/player/login");
            yield return ask;
            DownloadHandler downloadHandler = (DownloadHandler)ask.Current;
            if (downloadHandler != null)
            {
                string response = System.Text.Encoding.UTF8.GetString(downloadHandler.data);
                Debug.Log("login返回:" + response);
                JsonData jsonvale = JsonMapper.ToObject(downloadHandler.text);
                sessionId = jsonvale["data"]["sessionId"].ToJson();
                sessionId = Regex.Replace(sessionId, @"[^a-zA-Z0-9\u4e00-\u9fa5\s]", "");
            }else{
                Debug.Log("login失败.按理说应该返回大厅终止程序进行");
            }
            yield break;
        }

        static IEnumerator loadCustomerInfoFromRemoteServer(ApiLanguage apiLanguage)
        {

            // ==============================
            // フォームの生成
            // ==============================
            // フォーム
            CertificationForm form = new CertificationForm
            {
                sessionId = sessionId
            };

            // ==============================
            // API送信
            // ==============================
            // 送信
            yield return ApiCaller.Instance.Post<BaseModel<GetPlayerInfoModel>, CertificationForm>("http://160.16.187.230/AssetStoreFight/player/getPlayerInfo", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model =>
                 {
                     _AccInfo.Coin = model.data.coinCount;
                     _AccInfo.Diamond = model.data.diamondCount;
                 }
                ,
                 model =>
                 {
                     _AccInfo.Coin = model.data.coinCount;
                     _AccInfo.Diamond = model.data.diamondCount;
                 }
            );
            yield break;
        }
    }
}