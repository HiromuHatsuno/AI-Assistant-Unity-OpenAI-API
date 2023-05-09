using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ChatGPTと通信を行うための基底クラス
public abstract class ChatGPTBaseClient : MonoBehaviour
{
    // APIエンドポイントURL
    private string apiEndPoint;

    [Header("APIキー")]
    [FormerlySerializedAs("APIKey")] public string apiKey = "APIキー";

    [Header("使用するモデル名")]
    [SerializeField] protected string model;

    protected static string Model;

    // 背景情報
    [SerializeField][Multiline(10)] protected string backgroundInformation;

    // 入力フィールド
    [SerializeField] protected InputField inputField;

    // 出力テキスト
    [SerializeField] protected Text outputText;

    // 実行ボタン
    [SerializeField] protected Button execButton;

    private void Start() => SetModel();

    // APIエンドポイントの初期化
    protected void InitializeEndPoint(string apiEndPoint) => this.apiEndPoint = apiEndPoint;

    // Modelの設定
    private void SetModel() => Model = model;

    // APIからのレスポンスを処理する抽象メソッド
    protected abstract IEnumerator HandleAPIResponse(string jsonData);

    // APIリクエストを送信するメソッド
    protected IEnumerator SendAPIRequest(string jsonData)
    {
        // UnityWebRequestを使ってリクエストを作成
        using var request = new UnityWebRequest(apiEndPoint, "POST")
        {
            uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        // リクエストヘッダーを設定
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // リクエストを送信し、レスポンスを待つ
        yield return request.SendWebRequest();

        // リクエストが失敗した場合のエラーハンドリング
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            // レスポンスを処理する
            yield return HandleAPIResponse(request.downloadHandler.text);
        }
    }
}
