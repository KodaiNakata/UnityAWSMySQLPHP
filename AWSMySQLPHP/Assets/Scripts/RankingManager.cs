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
        /// データが見つからない場合のノードのプレハブ
        /// </summary>
        [SerializeField]
        private GameObject notFoundNodePrefab;

        /// <summary>
        /// サーバー接続失敗した場合のノードのプレハブ
        /// </summary>
        [SerializeField]
        private GameObject failedConnectionNodePrefab;

        /// <summary>
        /// 読み込み中のノードのプレハブ
        /// </summary>
        [SerializeField]
        private GameObject readingNodePrefab;

        /// <summary>
        /// 直前のスコア
        /// </summary>
        private IScore lastScore;

        /// <summary>
        /// 開始処理
        /// </summary>
        void Start()
        {
            lastScore = RankingLoader.Instance.LastScore;
            scoreLabel.text = lastScore.TextForDisplay;

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
            Instantiate(readingNodePrefab, rankingViewContent);

            // パラメータの設定
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("rankingNum", "10");
            dic.Add("orderBy", ScoreOrderType.OrderByAscending.ToString());

            // POST通信を実施
            IEnumerator scoreRanking = ServerConnecter.Instance.Post("SelectScore.php", dic);
            yield return StartCoroutine(scoreRanking);

            // Debug.Log("ランキング取得後のレスポンスデータ:" + JsonUtilCustom.FromJson<ScoreRecord>((string)scoreRanking.Current)[0].score_id.ToString());
            ScoreRecord[] responseScoreRecords = JsonUtilCustom.FromJson<ScoreRecord>((string)scoreRanking.Current);

            // レスポンスデータが空白のとき
            if (responseScoreRecords.Length == 0)
            {
                // ランキングビューにデータなしを表示
                Instantiate(notFoundNodePrefab, rankingViewContent);
            }
            else
            {
                // ランキングビューにスコアのランキングを表示
                for(int number = 0;number < responseScoreRecords.Length; number++){
                    var r = Instantiate(rankingViewNodePrefab, rankingViewContent);
                    var rankingViewNode = r.GetComponent<RankingViewNode>();
                    rankingViewNode.NoText.text = (number + 1).ToString();
                    rankingViewNode.NameText.text = responseScoreRecords[number].name;
                    rankingViewNode.ScoreText.text = responseScoreRecords[number].score.ToString();
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
            // パラメータの設定
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", nameField.text);
            dic.Add("score", lastScore.TextForDisplay);

            //TODO：名前が空白の場合はどのような処理をするのかを決める
            sendButton.interactable = false;

            // POST通信を実施
            yield return ServerConnecter.Instance.Post("InsertScore.php", dic);

            // Debug.Log("スコア送信後のレスポンスデータ:" + responseData);
        }
    }
}
