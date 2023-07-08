GPT-Client-Sample-For-Unity
UnityでGPT APIを用いたサンプルクラスです。

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

## 注意点
Newtonsoft Json Packageを使用しているので、事前に導入を行なってください。
「com.unity.nuget.newtonsoft-json」をPackageManagerから追加することで、「Newtonsoft.Json」を使用することができます。
