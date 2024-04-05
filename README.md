# RO価格確認機 1.2.1

このツールは、チャットログまたは受信パケットに含まれるアイテム獲得メッセージを抽出して これを素にRO公式ツールが使用しているAPIから価格情報するものです。

![image](https://github.com/majo-sama/RoItemKakakuChecker/assets/157029319/1b9122c6-482d-43fd-8e51-d7e4ea664b9d)

## ダウンロード

- [RoItemKakakuChecker_1.2.0.zip](https://github.com/majo-sama/RoItemKakakuChecker/releases/download/1.2.0/RoItemKakakuChecker_1.2.0.zip)

## 機能・使い方

### チャットログからアイテムを確認モード

`/savechat` コマンドにより出力されたチャットログファイルを解析してアイテムの一覧を作成し、これをもとにアイテムの価格を検索するモードです。

1. ゲーム内でアイテムを獲得します（ドロップアイテムを拾う・倉庫から取り出すなど）。
2. ゲーム内で `/savechat` コマンドを実行しチャットログを保存します。
3. RO価格確認機を起動し、チャットログの保存フォルダを指定します。
4. `ログ読込` ボタンを押します。
5. `価格取得` ボタンを押します。

### 倉庫監視モード

ゲーム内で倉庫を開いた際の受信した通信パケットを解析してアイテムの一覧を作成し、これをもとにアイテムの価格を検索するモードです。

1. `倉庫監視 開始` ボタンを押します。
2. ゲーム内で倉庫を開きます。
3. `倉庫監視 停止` ボタンを押します。
4. `価格取得` ボタンを押します。

### 会話メッセージ取得モード

会話メッセージの受信パケットを解析して一覧を作成し、同時にファイルに出力するモードです。
おまけ機能です。ちゃんとした機能が欲しければ他のツールを使った方が良いです。
通常・PT・ギルド・Wisのみに対応しています。（Wisは受信メッセージのみ対応）

1. `チャット監視 開始` ボタンを押します。
2. ゲーム内で会話をします。
3. `チャット監視 停止` ボタンを押します。

## 留意すべき仕様

- チャットログからアイテム名を判別できないもの（名称が長すぎて改行される場合など）やアイテム名が公式ツールに登録されていないカード挿し・エンチャ・強化済み装備など）は正確なデータを取得することができません。
  - **基本的に装備品は対象外だと思ってください。**
- アイテム情報の取得に失敗した場合、情報が表示されない または近い名称の別のアイテムの情報が表示されることがあります。
  - 倉庫に溜まったカードのおおよその値段を調べる程度の使い方を推奨します。
- 会話メッセージ取得機能では、Wisの送信メッセージ（自身の発言）やチャットルーム、その他のメッセージは記録されません。

## 注意事項

- このツールは価格取得の際にRO公式ツールが使用しているAPIを利用するため、 **大量のデータを頻繁に再取得することは避けてください**。
  - `「X」日以内にサーバーから取得したデータは再取得しない` の設定で大きめの数字を指定することを推奨します。
    - 無理な使い方をしてガンホーに怒られても知りませんよ！
- 使用に際して何らかの不利益が発生したとしても作者は責任を負いません。

## このツールのガンホーゲームズサービス利用約款に対する解釈

本ツールは、 `/savechat` により出力されたチャットログをもとにRO公式ツールのAPIを呼び出す、またはROのサーバーからクライアントに向けた通信パケットからアイテム情報などを取得し
それをもとにRO公式ツールのAPIを呼び出し アイテムの価格情報を取得するなどしています。

これらの行為に近いものが [ガンホーゲームズサービス利用約款](https://www.gungho.jp/rules/) 第10条の1 (8) (9) (19) (20) (22) (23)に記載されていますが、
本ツールはこれらのいずれにも該当しないと考えています。

詳しい説明は省きますが、通信の傍受を行わず（傍受の定義については辞書を参照のこと）、またAPI実行の際にも過度に負担をかけないための仕組みが存在しています。

リバースエンジニアリング等も行っていませんし、ゲーム内における送信パケットには一切の手も加えてはいません。

と作者は言い訳をしますが屁理屈かもしれません。目をつけられたら普通にアウト判定されると思います。ガンホーに怒られたら消します。

## 連絡先

- 不具合・意見等があれば 丼@majo-sama まで連絡または Issue を立てるなり PR 作るなりしてください。

## 変更履歴

- v1.2.1
  - 倉庫監視モードでAPI制限が正しく設定されない問題を修正
  - 会話メッセージ取得時、文字色が正しくない問題および稀に文字化けする問題を修正
  - 会話メッセージ取得時、一覧が自動でスクロールするよう変更
- v1.2.0
  - 倉庫監視モード・会話メッセージ取得モードを追加
- v1.1.3
  - チャットログ読込時、同一のアイテムを10個以上所持している場合にこれが一覧に登録されない問題を修正
- v1.1.2
  - エンチャント・カード付きの装備も価格の検索対象とするよう修正
    - ただしエンチャント・カードを考慮しない価格を表示します
    - 実際の価格は必ず公式ツールを確認するようにしてください
  - 2,147,483,647zを超える金額のアイテムを検索できるよう修正
