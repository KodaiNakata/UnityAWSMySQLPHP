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
            //サーバーとのやり取りを追加予定
            yield return null;
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
            //名前が空白の場合はどのような処理をするのかを決める
            sendButton.interactable = false;

            //パラメータはスコアの値と名前（サーバーとのやり取り）
            yield return null;
        }
    }
}
