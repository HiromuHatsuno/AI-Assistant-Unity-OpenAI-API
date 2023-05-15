# AI-Assistant-Unity-Powered-by-GPT-3.5-GPT-3
UnityでGPT APIを用いて、AIアシスタントを実装するためのクラス

## 各種クラスの概要説明
* **GPTTurboClient.cs<br>**
Unity上でGPT-3.5のAPIを用いるためのクラス<br>
GPT-3.5のAPIがfine-tuningに対応していない為、プロンプトを用いたAIアシスタントの作成に利用可能

* **GPT3Client.cs<br>**
GPT-3のAPIをUnity上で用いるためのクラス<br>
GPT-3のモデルクラスにfine-tuningしたモデルを利用可能

* **GPTBaseClient.cs<br>**
GPTTurboClient.csとGPT3Client.csのベースクラス<br>
それぞれのクラスで共通化できる箇所を共通化した
