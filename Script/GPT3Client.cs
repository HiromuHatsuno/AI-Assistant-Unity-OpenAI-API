using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Scenes;

public class GPT3Client : ChatGPTBaseClient
{
    // 会話の履歴を保持するリストです。
    private readonly List<string> conversationHistory = new();

    // Startメソッドでは、APIエンドポイントの初期化とクライアントの初期化を行う
    private void Start()
    {
        InitializeEndPoint("https://api.openai.com/v1/completions");
        Initialize();
    }

    // Initializeメソッドでは、会話の履歴リストに背景情報を追加し、ボタンのリスナーにメソッドを追加
    private void Initialize()
    {
        // 初期化処理...
        conversationHistory.Add(backgroundInformation);
        conversationHistory.Add("The following is a conversation between you (ChatGPT) and the user. Please respond in Japanese to the part following GPT:.");
        conversationHistory.Add("日本語で返答して下さい");

        execButton.onClick.AddListener(OnSendButtonClick);
    }

    // OnSendButtonClickメソッドは、送信ボタンがクリックされたときに呼び出される
    // このメソッドでは、ユーザーの入力を取得し、APIに送信するためのメソッドを呼び出す
    private void OnSendButtonClick() => StartCoroutine(SendMessage(inputField.text));

    // HandleAPIResponseメソッドは、APIからの応答を処理するためのメソッド
    protected override IEnumerator HandleAPIResponse(string responseText)
    {
        var responseData = JsonConvert.DeserializeObject<ChatGPTResponse>(responseText);
        var output = responseData.Choices.FirstOrDefault().Text;
        
        outputText.text = output;

        conversationHistory[^1] = "GPT:" + output;
        yield return null;
    }

    // SendMessageメソッドは、ユーザーのメッセージをAPIに送信するメソッド
    // 会話履歴を更新し、APIリクエストデータを作成し、APIに送信
    private IEnumerator SendMessage(string text)
    {
        conversationHistory.Add("ユーザー:" + text + "\nGPT:");
        var prompt = string.Join("\n", conversationHistory);

        var requestData = new APIRequestData()
        {
            Prompt = prompt,
            MaxTokens = 100,
            Stop = "\n"
        };

        var jsonData = JsonConvert.SerializeObject(requestData, Formatting.Indented);

        yield return StartCoroutine(SendAPIRequest(jsonData));
    }

    // APIRequestDataクラスは、APIリクエストデータを表現するためのクラス
    [JsonObject]
    public class APIRequestData
    {
        [JsonProperty("model")] public string Model { get; set; } = GPT3Client.Model;
        [JsonProperty("prompt")] public string Prompt { get; set; } = "";
        [JsonProperty("temperature")] public int Temperature { get; set; } = 0;
        [JsonProperty("max_tokens")] public int MaxTokens { get; set; }
        [JsonProperty("stop")] public string Stop { get; set; } = "\n";
    }

    // ChatGPTResponseクラスは、APIレスポンスデータを表現するためのクラス
    [JsonObject]
    private class ChatGPTResponse
    {
        [JsonProperty("choices")] public Choice[] Choices { get; set; }
    }

    // Choiceクラスは、APIレスポンス内の返答の選択肢データを表現するためのクラスです。
    [JsonObject]
    private class Choice
    {
        [JsonProperty("text")] public string Text { get; set; }
    }
}