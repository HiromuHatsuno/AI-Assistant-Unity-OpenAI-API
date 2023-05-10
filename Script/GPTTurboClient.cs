using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

// GPTのTurboクライアントを表現するクラス
public class GPTTurboClient : GPTBaseClient
{
    // 会話の履歴を保持するリストです。
    private readonly List<ChatGPTMessage> _messageList = new();

    // Startメソッドでは、APIエンドポイントの初期化とクライアントの初期化を行う
    private void Start()
    {
        InitializeEndPoint("https://api.openai.com/v1/chat/completions");

        Initialize();
    }

    // Initializeメソッドでは、会話の履歴リストに背景情報を追加し、ボタンのリスナーにメソッドを追加
    private void Initialize()
    {
        // ボタンのクリックリスナーにメソッドを追加
        execButton.onClick.AddListener(OnSendButtonClick);

        // システムメッセージ（背景情報）をメッセージリストに追加
        _messageList.Add(new ChatGPTMessage { role = "system", content = backgroundInformation });
    }

    // ボタンクリック時の処理
    private void OnSendButtonClick() => StartCoroutine(SendMessageToRequest(inputField.text));

    // APIレスポンスを処理するメソッド
    protected override IEnumerator HandleAPIResponse(string responseText)
    {
        // レスポンスをデシリアライズ
        var responseObject = JsonConvert.DeserializeObject<ChatGPTResponse>(responseText);

        // レスポンスをメッセージリストに追加
        _messageList.Add(responseObject.choices[0].message);

        // レスポンスをテキストフィールドに出力
        outputText.text = responseObject.choices[0].message.content;

        yield return null;
    }

    // メッセージを送信するメソッド
    private IEnumerator SendMessageToRequest(string userMessage)
    {
        // ユーザーメッセージをリストに追加
        _messageList.Add(new ChatGPTMessage { role = "user", content = userMessage });

        // リクエストデータを作成
        var requestData = new
        {
            model = Model,
            messages = _messageList
        };

        // リクエストデータをシリアライズ
        var jsonData = JsonConvert.SerializeObject(requestData);

        // リクエストを送信
        yield return StartCoroutine(SendAPIRequest(jsonData));
    }

    // メッセージを表現するクラス
    [JsonObject]
    public class ChatGPTMessage
    {
        [JsonProperty("role")] public string role;
        [JsonProperty("content")] public string content;
    }

    // レスポンスを表現するクラス
    [JsonObject]
    public class ChatGPTResponse
    {
        [JsonProperty("choices")] public Choice[] choices;

        [JsonObject]
        public class Choice
        {
            public ChatGPTMessage message;
        }
    }
}
