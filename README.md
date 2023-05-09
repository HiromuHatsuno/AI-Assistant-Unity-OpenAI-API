# AI-Assistant-Unity-OpenAI-API
UnityでOpen AIのAPIを用いて、AIアシスタントを実装するためのクラス

## 各種クラスの概要説明
* **GPTTurboClient.cs<br>**
gpt-3.5-turboをUnity上で実行するためのクラス<br>
gpt-3.5-turboがfine-tuningに対応していない為、プロンプトを用いたAIアシスタントの作成に利用可能

* **GPT3Client.cs<br>**
davinciをUnity上で実行するためのクラス<br>
davinciをベースにfine-tuningしたモデルを利用可能

* **GPTBaseClient.cs<br>**
GPTTurboClient.csとGPT3Client.csのベースクラス<br>
それぞれのクラスで共通化できる箇所を共通化した
