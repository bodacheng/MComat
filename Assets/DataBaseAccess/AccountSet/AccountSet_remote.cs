using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using Api.Common;
using Api.Dto.Form.Common;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class AccountSet
    {
        static string deviceId;
        public static IEnumerator login()
        {
            deviceId = SystemInfo.deviceUniqueIdentifier;
            deviceId = "asdadasdafwerfefe";
            WWWForm form = new WWWForm();
            form.AddField("deviceId", deviceId);
            IEnumerator ask = RemoteAccess.generalRemoteAccess(form, "http://160.16.187.230:8089/player/login");
            yield return ask;
            DownloadHandler downloadHandler = (DownloadHandler)ask.Current;
            if (downloadHandler != null)
            {
                string response = System.Text.Encoding.UTF8.GetString(downloadHandler.data);
                Debug.Log("login返回:" + response);
                JsonData jsonvale = JsonMapper.ToObject(downloadHandler.text);
                if (jsonvale["deviceId"] != null)
                {
                
                }else{
                    IEnumerator _registered = registered();
                    yield return _registered;
                    if (_registered.Current == null)
                    {
                        Debug.Log("注册失败，退出");
                        yield return null;
                        yield break;
                    }
                    DownloadHandler _registereddownloadHandler = (DownloadHandler)_registered.Current;
                    if (_registereddownloadHandler != null)
                    {
                        yield return login();
                    }else{
                        Debug.Log("注册失败，退出");
                        yield return null;
                        yield break;
                    }
                }
            }else{
                Debug.Log("网络错误等等");
            }
        }
        
        public static IEnumerator registered()
        {
            WWWForm form = new WWWForm();
            form.AddField("deviceId", deviceId);
            IEnumerator ask = RemoteAccess.generalRemoteAccess(form, "http://160.16.187.230:8089/player/addPlayer");
            yield return ask;
            DownloadHandler downloadHandler = (DownloadHandler)ask.Current;
            if (downloadHandler != null)
            {
                string response = System.Text.Encoding.UTF8.GetString(downloadHandler.data);
                Debug.Log("注册返回:" + response);
                JsonData jsonvale = JsonMapper.ToObject(downloadHandler.text);
            }else{
                Debug.Log("注册失败.按理说应该返回大厅终止程序进行");
            }
        }

        static IEnumerator loadCustomerInfoFromRemoteServer(ApiLanguage apiLanguage)
        {

            // ==============================
            // フォームの生成
            // ==============================
            // フォーム
            CertificationForm form = new CertificationForm
            {
            };
            
            // ==============================
            // API送信
            // ==============================
            // 送信
            yield return ApiCaller.Instance.Post<GetPlayerInfoModel, CertificationForm>("http://160.16.187.230:8089/player/getPlayer", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model =>
                 {
                     Debug.Log(model);
                     _AccInfo.PlayerName = model.data.playerName;
                     _AccInfo.coinCount = model.data.coinCount;
                     _AccInfo.diamondCount = model.data.diamondCount;
                 }
                ,
                 model =>
                 {
                    _AccInfo.PlayerName = model.data.playerName;
                     _AccInfo.coinCount = model.data.coinCount;
                     _AccInfo.diamondCount = model.data.diamondCount;
                 }
            );
            yield break;
        }
    }
}