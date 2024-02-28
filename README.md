# RO価格確認機

このツールは、チャットログに含まれるアイテム獲得メッセージを抽出して これを素にRO公式ツールが使用しているAPIから価格情報するものです。

## 使い方

1. ゲーム内でアイテムを獲得します（倉庫から取り出すなど）。
2. ゲーム内で /savechat を実行しチャットログを保存します。
3. RO価格確認機を起動し、チャットログの保存フォルダを指定します。
4. ログ読込ボタンを押します。
5. 価格取得ボタンを押します。

## 留意すべき仕様

- チャットログからアイテム名を判別できないもの（名称が長すぎて改行される場合など）やアイテム名が公式ツールに登録されていないカード挿し・エンチャ・強化済み装備など）はデータを取得することができません。
  - **基本的に装備品は対象外だと思ってください。**
- アイテム情報の取得に失敗した場合、情報が表示されない または近い名称の別のアイテムの情報が表示されることがあります。
  - 倉庫に溜まったカードのおおよその値段を調べる程度の使い方を推奨します。

## 注意事項

- このツールはRO公式ツールが使用しているAPIを利用するため、 **大量のデータを頻繁に再取得することは避けてください**。
  - `「X」日以内にサーバーから取得したデータは再取得しない` の設定で大きめの数字を指定することを推奨します。
    - 無理な使い方をしてガンホーに怒られても知りませんよ！
- 使用に際して何らかの不利益が発生したとしても作者は責任を負いません。

## 連絡先

- 不具合・意見等があれば 丼@majo-sama まで連絡または Issue を立てるなりしてください。
