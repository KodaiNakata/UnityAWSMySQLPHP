using System;
using static System.Diagnostics.Debug;
//using System.Diagnostics;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityRanking
{
    /// <summary>
    /// ランキングの管理クラス
    /// </summary>
    public class RankingManager : MonoBehaviour
    {
        /// <summary>
        /// 閉じるボタン
        /// </summary>
        [SerializeField]
        private Button closeButton;

        /// <summary>
        /// 送信ボタン
        /// </summary>
        [SerializeField]
        private Button sendButton;

        /// <summary>
        /// スコアのラベル
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI scoreLabel;

        /// <summary>
        /// ランキングの状態のラベル
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI rankingStateLabel;

        /// <summary>
        /// 名前の入力フィールド
        /// </summary>
        [SerializeField]
        private TMP_InputField nameField;

        /// <summary>
        /// ランキングビューの内容
        /// </summary>
        [SerializeField]
        private RectTransform rankingViewContent;

        /// <summary>
        /// ランキングビューのノードのプレハブ
        /// </summary>
        [SerializeField]
        private GameObject rankingViewNodePrefab;

        /// <summary>
        /// 自プレイヤーのスコア
        /// </summary>
        private IScore myScore;

        /// <summary>
        /// 名前のフィールドのテキストを取得する
        /// </summary>
        /// <returns>名前のフィールドのテキスト</returns>
        private string GetNameFieldText()
        {
            // 名前のフィールドが空白の時
            if (string.IsNullOrEmpty(nameField.text))
            {
                return "ななし";
            }
            return nameField.text;
        }

        /// <summary>
        /// 開始処理
        /// </summary>
        void Start()
        {
            myScore = RankingLoader.Instance.MyScore;
            scoreLabel.text = myScore.TextForDisplay;

            // 現在のランキングの取得
            StartCoroutine(GetCurrentRanking());
        }

        /// <summary>
        /// 現在のランキングを取得する
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetCurrentRanking()
        {
            // 読み込み中の文字を表示
            rankingStateLabel.text = "よみこみちゅう...";

            // パラメータの設定
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("rankingNum", "10");
            dic.Add("orderBy", ScoreOrderType.OrderByAscending.ToString());

            // POST通信を実施
            IEnumerator scoreRanking = ServerConnecter.Instance.Post("SelectScore.php", dic);
            yield return StartCoroutine(scoreRanking);

            // サーバー側からエラーが返ってきたとき
            if(String.IsNullOrEmpty((string)scoreRanking.Current))
            {
                rankingStateLabel.text = "よみこみふか";
            }
            else
            {
                ScoreRecord[] responseScoreRecords = JsonUtilCustom.FromJson<ScoreRecord>((string)scoreRanking.Current);

                // レスポンスデータが空白のとき
                if (responseScoreRecords.Length == 0)
                {
                    // ランキングの状態ラベルにデータなしを表示
                    rankingStateLabel.text = "ランキングのデータなし";
                }
                else
                {
                    // ランキングビューにスコアのランキングを表示
                    for(int number = 0; number < responseScoreRecords.Length; number++){
                        var r = Instantiate(rankingViewNodePrefab, rankingViewContent);
                        var rankingViewNode = r.GetComponent<RankingViewNode>();
                        rankingViewNode.NoText.text = (number + 1).ToString();
                        rankingViewNode.NameText.text = responseScoreRecords[number].name;
                        rankingViewNode.ScoreText.text = responseScoreRecords[number].score.ToString();
                    }
            
                    // 昇順のランキングとき
                    if(myScore.OrderType == ScoreOrderType.OrderByAscending)
                    {
                        // 自分のスコアが最下位のスコアより小さいとき
                        if(myScore.Value < responseScoreRecords[responseScoreRecords.Length - 1].score)
                        {
                            // 送信ボタンを活性
                            sendButton.interactable = true;

                            // とうろくOKの文字を表示
                            rankingStateLabel.text = "スコアのとうろくOK";
                        }
                        else
                        {
                            // 送信ボタンを非活性
                            sendButton.interactable = false;

                            // とうろくNGの文字を表示
                            rankingStateLabel.text = "スコアのとうろくNG";
                        }
                    }

                    // 降順のランキングとき
                    else if(myScore.OrderType == ScoreOrderType.OrderByDescending)
                    {
                        // 自分のスコアが最下位のスコアより大きいとき
                        if(myScore.Value > responseScoreRecords[responseScoreRecords.Length - 1].score)
                        {
                            // 送信ボタンを活性
                            sendButton.interactable = true;

                            // とうろくOKの文字を表示
                            rankingStateLabel.text = "スコアのとうろくOK";
                        }
                        else
                        {
                            // 送信ボタンを非活性
                            sendButton.interactable = false;

                            // とうろくNGの文字を表示
                            rankingStateLabel.text = "スコアのとうろくNG";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 閉じるボタンクリック時の処理
        /// </summary>
        public void OnCloseButtonClick()
        {
            // ランキングフォームを閉じる
            closeButton.interactable = false;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ranking");
        }

        /// <summary>
        /// 送信ボタンクリック時の処理
        /// </summary>
        public void OnSendButtonClick()
        {
            // スコアを送信
            StartCoroutine(SendScore());
        }

        /// <summary>
        /// スコアの送信
        /// </summary>
        /// <returns></returns>
        private IEnumerator SendScore()
        {
            // 送信ボタンを非活性
            sendButton.interactable = false;

            // とうろくちゅうの文字を表示
            rankingStateLabel.text = "とうろくちゅう...";

            // パラメータの設定
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", GetNameFieldText());
            dic.Add("score", myScore.TextForDisplay);

            // POST通信を実施
            IEnumerator insertRanking = ServerConnecter.Instance.Post("InsertScore.php", dic);
            yield return StartCoroutine(insertRanking);

            // サーバー側からエラーが返ってきたとき
            if(String.IsNullOrEmpty((string)insertRanking.Current))
            {
                rankingStateLabel.text = "とうろくふか";
            }
            else
            {
                rankingStateLabel.text = "とうろくかんりょう";
                //TODO：登録を完了した後、ランキングビューに反映するのと画面を閉じるを行うかどうか未定
            }
        }
    }
}
