using Api.Common;
using Api.Dto.Form;
using Api.Dto.Form.Common;
using Api.Dto.Model;
using Api.Dto.Model.Common;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// API言語
/// </summary>


/// <summary>
/// APIデモ
/// </summary>
public class ApiDemo2 : MonoBehaviour {

    /// <summary>
    /// セッションID
    /// </summary>
    private InputField _sessionIdInput;

    /// <summary>
    /// 日本語送信
    /// </summary>
    private Button _sendJpJaButton;

    /// <summary>
    /// 英語送信
    /// </summary>
    private Button _sendEnUsButton;

    /// <summary>
    /// 中国語(簡体字)送信
    /// </summary>
    private Button _sendZhCnButton;

    /// <summary>
    /// 中国語(繁体字)送信
    /// </summary>
    private Button _sendZhTwButton;

    /// <summary>
    /// HTTPステータステキスト
    /// </summary>
    private Text _httpStatusText;

    /// <summary>
    /// レスポンステキスト
    /// </summary>
    private Text _responseText;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start() {

        // ==============================
        // コンポネントの取得
        // ==============================
        // セッションID
        _sessionIdInput = transform.Find("SessionIdInput").GetComponent<InputField>();
        // 日本語送信
        _sendJpJaButton = transform.Find("SendJpJaButton").GetComponent<Button>();
        // 英語送信
        _sendEnUsButton = transform.Find("SendEnUsButton").GetComponent<Button>();
        // 中国語(簡体字)送信
        _sendZhCnButton = transform.Find("SendZhCnButton").GetComponent<Button>();
        // 中国語(繁体字)送信
        _sendZhTwButton = transform.Find("SendZhTwButton").GetComponent<Button>();
        // HTTPステータステキスト
        _httpStatusText = transform.Find("HttpStatusText").GetComponent<Text>();
        // レスポンステキスト
        _responseText = transform.Find("ResponseText").GetComponent<Text>();


        // ==============================
        // イベントの登録
        // ==============================
        // 日本語送信
        //_sendJpJaButton.onClick.AddListener(() => {
        //    // 送信
        //    Debug.Log("日本語送信2");
        //    SendApi(ApiLanguage.JaJp);
        //});
        //// 英語送信
        //_sendEnUsButton.onClick.AddListener(() => {
        //    // 送信
        //    Debug.Log("英語送信2");
        //    SendApi(ApiLanguage.EnUs);
        //});
        //// 中国語(簡体字)送信
        //_sendZhCnButton.onClick.AddListener(() => {
        //    // 送信
        //    Debug.Log("中国語(簡体字)送信2");
        //    SendApi(ApiLanguage.ZhCn);
        //});
        //// 中国語(繁体字)送信
        //_sendZhTwButton.onClick.AddListener(() => {
        //    // 送信
        //    Debug.Log("中国語(繁体字)送信2");
        //    SendApi(ApiLanguage.ZhTw);
        //});
    }

    //private IEnumerator SendApi(ApiLanguage apiLanguage) {

    //    // ==============================
    //    // フォームの生成
    //    // ==============================
    //    // フォーム
    //    CertificationForm form = new CertificationForm();
    //    form.sessionId = _sessionIdInput.text;


    //    // ==============================
    //    // ヘッダーの生成
    //    // ==============================
    //    // ヘッダー
    //    Dictionary<string, string> headers = new Dictionary<string, string>();
    //    // ヘッダーの設定
    //    switch (apiLanguage) {
    //        case ApiLanguage.JaJp:
    //            headers.Add("Accept-Language", "ja-JP");
    //            break;
    //        case ApiLanguage.EnUs:
    //            headers.Add("Accept-Language", "en-US");
    //            break;
    //        case ApiLanguage.ZhCn:
    //            headers.Add("Accept-Language", "zh-CN");
    //            break;
    //        case ApiLanguage.ZhTw:
    //            headers.Add("Accept-Language", "zh-TW");
    //            break;
    //    }


    //    // ==============================
    //    // API送信
    //    // ==============================
    //    // 送信
    //    yield return ApiCaller.Instance.Post<BaseModel<GetPlayerInfoModel>, CertificationForm>("http://160.16.187.230/AssetStoreFight/player/getPlayerInfo", form, headers,
    //        model => {
    //            // 成功
    //            StringBuilder sb = new StringBuilder();
    //            sb.AppendLine($"HTTPステータス：{model.httpStatus}");
    //            sb.AppendLine($"ステータス：{model.status}");
    //            sb.AppendLine($"メッセージ：{model.message}");
    //            sb.AppendLine($"詳細メッセージ：{model.detailMessage}");
    //            sb.AppendLine($"データ．プレーヤID：{model.data.playerId}");
    //            sb.AppendLine($"データ．ニックネーム：{model.data.nickname}");
    //            sb.AppendLine($"データ．レベル：{model.data.level}");
    //            sb.AppendLine($"データ．経験値：{model.data.experience}");
    //            sb.AppendLine($"データ．ダイヤモンド数：{model.data.diamondCount}");
    //            sb.AppendLine($"データ．コイン数：{model.data.coinCount}");
    //            _responseText.text = sb.ToString();
    //        },
    //        model => {
    //            // 失敗
    //            StringBuilder sb = new StringBuilder();
    //            sb.AppendLine($"HTTPステータス：{model.httpStatus}");
    //            sb.AppendLine($"ステータス：{model.status}");
    //            sb.AppendLine($"メッセージ：{model.message}");
    //            sb.AppendLine($"詳細メッセージ：{model.detailMessage}");
    //            sb.AppendLine($"データ：{model.data}");
    //            _responseText.text = sb.ToString();
    //        }
    //    );

    //    yield break;
    //}
}