using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// APIデモ
/// </summary>
public class ApiDemo : MonoBehaviour {

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
        _sendJpJaButton.onClick.AddListener(() => {
            // 送信
            Debug.Log("日本語送信");
            StartCoroutine(SendApi(ApiLanguage.JaJp));
        });
        // 英語送信
        _sendEnUsButton.onClick.AddListener(() => {
            // 送信
            Debug.Log("英語送信");
            StartCoroutine(SendApi(ApiLanguage.EnUs));
        });
        // 中国語(簡体字)送信
        _sendZhCnButton.onClick.AddListener(() => {
            // 送信
            Debug.Log("中国語(簡体字)送信");
            StartCoroutine(SendApi(ApiLanguage.ZhCn));
        });
        // 中国語(繁体字)送信
        _sendZhTwButton.onClick.AddListener(() => {
            // 送信
            Debug.Log("中国語(繁体字)送信");
            StartCoroutine(SendApi(ApiLanguage.ZhTw));
        });
    }

    private IEnumerator SendApi(ApiLanguage apiLanguage) {

        // ==============================
        // フォームの準備
        // ==============================
        // フォーム
        WWWForm form = new WWWForm();
        form.AddField("sessionId", _sessionIdInput.text);


        // ==============================
        // API送信
        // ==============================
        // リクエスト
        UnityWebRequest req = UnityWebRequest.Post("http://160.16.187.230/AssetStoreFight/player/getPlayerInfo", form);
        // ヘッダーの設定
        switch (apiLanguage) {
            case ApiLanguage.JaJp:
                req.SetRequestHeader("Accept-Language", "ja-JP");
                break;
            case ApiLanguage.EnUs:
                req.SetRequestHeader("Accept-Language", "en-US");
                break;
            case ApiLanguage.ZhCn:
                req.SetRequestHeader("Accept-Language", "zh-CN");
                break;
            case ApiLanguage.ZhTw:
                req.SetRequestHeader("Accept-Language", "zh-TW");
                break;
        }

        // 送信
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError) {
            // 出力
            Debug.Log("失敗");
            _httpStatusText.text = req.responseCode.ToString();
            _responseText.text = req.downloadHandler.text;
        }
        else {
            // 出力
            Debug.Log("成功");
            _httpStatusText.text = req.responseCode.ToString();
            _responseText.text = req.downloadHandler.text;
        }
    }
}