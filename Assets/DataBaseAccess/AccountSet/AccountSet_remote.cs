using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using System.Text.RegularExpressions;

using System.Text;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Form.Common;
using Api.Dto.Model;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class AccountSet
    {
        public IEnumerator login()
        {
            WWWForm form = new WWWForm();
            form.AddField("userId","abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcd");
            
            IEnumerator ask = RemoteAccess.generalRemoteAccess(form,"http://160.16.187.230/AssetStoreFight/player/login");
            yield return ask;
            DownloadHandler downloadHandler = (DownloadHandler)ask.Current;
            
            if (downloadHandler != null)
            {
                string response = System.Text.Encoding.UTF8.GetString(downloadHandler.data);
                Debug.Log("login返回:" + response);
                JsonData jsonvale = JsonMapper.ToObject(downloadHandler.text);
                this.sessionId = jsonvale["data"]["sessionId"].ToJson();
                this.sessionId = Regex.Replace(this.sessionId, @"[^a-zA-Z0-9\u4e00-\u9fa5\s]", "");
            }else{
                Debug.Log("login失败.按理说应该返回大厅终止程序进行");
                string response = System.Text.Encoding.UTF8.GetString(downloadHandler.data);
                Debug.Log("以下是login报错response" + response);
            }
            yield break;
        }
        
        private IEnumerator loadCustomerInfoFromRemoteServer(ApiLanguage apiLanguage) {

            // ==============================
            // フォームの生成
            // ==============================
            // フォーム
            CertificationForm form = new CertificationForm();
            form.sessionId = this.sessionId;
       
            // ==============================
            // API送信
            // ==============================
            // 送信
            yield return ApiCaller.Instance.Post<BaseModel<GetPlayerInfoModel>, CertificationForm>("http://160.16.187.230/AssetStoreFight/player/getPlayerInfo", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     _PlayerAccountInfo.Coin = model.data.coinCount;
                     _PlayerAccountInfo.Diamond = model.data.diamondCount;
                 }
                ,
                 model => {
                     _PlayerAccountInfo.Coin = model.data.coinCount;
                     _PlayerAccountInfo.Diamond = model.data.diamondCount;
                 }
            );
            yield break;
        }
    }
}