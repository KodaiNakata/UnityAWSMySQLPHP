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
        /// 直前のスコア
        /// </summary>
        private IScore lastScore;

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
        /// 直前のスコアのプロパティ
        /// </summary>
        /// <value></value>
        public IScore LastScore
        {
            get
            {
                return lastScore;
            }
        }

        /// <summary>
        /// スコアの送信とランキングの表示（スコア:数値型）
        /// </summary>
        /// <param name="score">スコア</param>
        public void SendScoreAndShowRanking(double score)
        {
            var sc = new NumberScore(score);
            LoadRankingScene(sc);
        }

        /// <summary>
        /// 時間の送信とランキングの表示（スコア:時間型）
        /// </summary>
        /// <param name="time">時間</param>
        public void SendTimeAndShowRanking(TimeSpan time)
        {
            var sc = new TimeScore(time);
            LoadRankingScene(sc);
        }

        /// <summary>
        /// ランキング画面の読み込み
        /// </summary>
        /// <param name="score">スコア情報のインタフェース</param>
        private void LoadRankingScene(IScore score)
        {
            lastScore = score;
            SceneManager.LoadScene("Ranking", LoadSceneMode.Additive);
        }
    }
}