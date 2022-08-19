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
            string responseData = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("rankingNum", "10");
            dic.Add("orderBy", ScoreOrderType.OrderByAscending.ToString());

            // POST通信を実施
            yield return ServerConnecter.Instance.Post("SelectScore.php", dic, responseData);

            Debug.Log("ランキング取得後のレスポンスデータ:" + responseData);

            // レスポンスデータが空白のとき
            if (responseData == string.Empty)
            {
                Instantiate(notFoundNodePrefab, rankingViewContent);
            }
            else
            {

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
            string responseData = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", nameField.text);
            dic.Add("score", lastScore.TextForDisplay);

            //名前が空白の場合はどのような処理をするのかを決める
            sendButton.interactable = false;

            // POST通信を実施
            yield return ServerConnecter.Instance.Post("InsertScore.php", dic, responseData);

            Debug.Log("スコア送信後のレスポンスデータ:" + responseData);
        }
    }
}
