using Api.Dto.Form.Common;
using Api.Dto.Model.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public enum ApiLanguage {
    JaJp,
    EnUs,
    ZhCn,
    ZhTw
}

namespace Api.Common {

    /// <summary>
    /// Api処理に成功する場合のコールバック関数
    /// </summary>
    /// <typeparam name="M">モデル型</typeparam>
    /// <param name="model">モデル</param>
    public delegate void SuccessDelegate<M>(M model);

    /// <summary>
    /// Api処理に失敗する場合のコールバック関数
    /// </summary>
    /// <typeparam name="M">モデル型</typeparam>
    /// <param name="model">モデル</param>
    public delegate void FailDelegate<M>(M model);

    /// <summary>
    /// APIコーラー
    /// </summary>
    public class ApiCaller {

        public static ApiCaller instance;
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private ApiCaller()
        {
        }

        public static ApiCaller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiCaller();
                }
                return instance;
            }
        }
        
        public Dictionary<string, string> getHeader(ApiLanguage apiLanguage)
        {
            // ==============================
            // ヘッダーの生成
            // ==============================
            // ヘッダー
            Dictionary<string, string> headers = new Dictionary<string, string>();
            // ヘッダーの設定
            switch (apiLanguage) {
                case ApiLanguage.JaJp:
                    headers.Add("Accept-Language", "ja-JP");
                    break;
                case ApiLanguage.EnUs:
                    headers.Add("Accept-Language", "en-US");
                    break;
                case ApiLanguage.ZhCn:
                    headers.Add("Accept-Language", "zh-CN");
                    break;
                case ApiLanguage.ZhTw:
                    headers.Add("Accept-Language", "zh-TW");
                    break;
            }
            return headers;
        }

        /// <summary>
        /// POSTで送信します。
        /// </summary>
        /// <typeparam name="M">モデル型</typeparam>
        /// <typeparam name="F">フォーム型</typeparam>
        /// <param name="url">URL</param>
        /// <param name="form">フォーム</param>
        /// <param name="headers">ヘッダー</param>
        /// <param name="success">API処理に成功時のコールバック関数</param>
        /// <param name="fail">API処理に失敗時のコールバック関数</param>
        public IEnumerator Post<M, F>(string url, F form, Dictionary<string, string> headers, SuccessDelegate<AbstractModel<M>> success, FailDelegate<AbstractModel<M>> fail){

            // ==============================
            // フォームの生成
            // ==============================
            // WWWフォーム
            WWWForm wwwForm = new WWWForm();
            foreach (var prop in form.GetType().GetProperties()) {
                if (prop.GetValue(form) != null) {
                    wwwForm.AddField(prop.Name, prop.GetValue(form) as string);
                }
            }

            // ==============================
            // リクエスト送信
            // ==============================
            // リクエスト
            UnityWebRequest req = UnityWebRequest.Post(url, wwwForm);
            // ヘッダー
            if (headers != null) {
                foreach (var h in headers) {
                    req.SetRequestHeader(h.Key, h.Value);
                }
            }

            // 送信
            yield return req.SendWebRequest();

            // ==============================
            // レスポンス解析
            // ==============================
            // モデル
            try {
                Debug.Log(url + "  上述API尝试解析以下字符串" + req.downloadHandler.text);
                var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                AbstractModel<M> model = new AbstractModel<M>
                {
                    data = JsonConvert.DeserializeObject<M>(req.downloadHandler.text, settings),
                    httpStatus = (int)req.responseCode // httpStatus? Status? 貌似取值用model要有一个统一定义
                };

                if (!(req.isHttpError || req.isNetworkError)) {
                    Debug.Log(url + "以下为成功信息："+ req.downloadHandler.text);
                    success(model);
                }
                else {
                    Debug.Log(url + "以下是错误内容：" + req.downloadHandler.text);
                    foreach (var prop in form.GetType().GetProperties())
                    {
                        if (prop.GetValue(form) != null) {
                            Debug.Log("form信息为："+  prop.Name + " : " + prop.GetValue(form) as string);
                        }
                    }
                    fail(model);
                }
            }
            catch (Exception e) {
                Debug.LogError("レスポンスの解析に失敗しました。\n" + req.downloadHandler.text);
                Debug.Log("这个是catch到的错误" + e);
                foreach (var prop in form.GetType().GetProperties())
                {
                    if (prop.GetValue(form) != null) {
                        Debug.Log("form信息为："+  prop.Name + " : " + prop.GetValue(form) as string);
                    }
                }
            }
        }
    }
}
