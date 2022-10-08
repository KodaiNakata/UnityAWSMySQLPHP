using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityRanking
{
    /// <summary>
    /// ランキングの読み込みクラス
    /// </summary>
    public class RankingLoader
    {
        /// <summary>
        /// 自クラスのインスタンス 
        /// </summary>
        private static RankingLoader instance;

        /// <summary>
        /// 自プレイヤーのスコア
        /// </summary>
        private IScore myScore;

        /// <summary>
        /// 自クラスのインスタンスのプロパティ
        /// </summary>
        /// <value></value>
        public static RankingLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RankingLoader();
                }
                return instance;
            }
        }

        /// <summary>
        /// 自プレイヤーのスコアのプロパティ
        /// </summary>
        /// <value></value>
        public IScore MyScore
        {
            get
            {
                return myScore;
            }
        }

        /// <summary>
        /// スコアの送信とランキングの表示（スコア:数値型）
        /// </summary>
        /// <param name="score">スコア</param>
        /// <param name="orderType">スコアの昇順降順</param>
        public void SendScoreAndShowRanking(double score, ScoreOrderType orderType)
        {
            var sc = new NumberScore(score, orderType);
            LoadRankingScene(sc);
        }

        /// <summary>
        /// 時間の送信とランキングの表示（スコア:時間型）
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="orderType">スコアの昇順降順</param>
        public void SendTimeAndShowRanking(TimeSpan time, ScoreOrderType orderType)
        {
            var sc = new TimeScore(time, orderType);
            LoadRankingScene(sc);
        }

        /// <summary>
        /// ランキング画面の読み込み
        /// </summary>
        /// <param name="score">スコア情報のインタフェース</param>
        private void LoadRankingScene(IScore score)
        {
            myScore = score;
            SceneManager.LoadScene("Ranking", LoadSceneMode.Additive);
        }
    }
}