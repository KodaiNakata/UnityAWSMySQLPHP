using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRanking
{
    /// <summary>
    /// 数値のスコア
    /// </summary>
    public class NumberScore : IScore
    {
        /// <summary>
        /// スコア
        /// </summary>
        private double score;

        /// <summary>
        /// フォーマット
        /// </summary>
        private string format;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="score">スコア</param>
        /// <param name="format">フォーマット</param>
        public NumberScore(double score, string format = "")
        {
            this.score = score;
            this.format = format;
        }

        /// <summary>
        /// スコアの型のプロパティ
        /// </summary>
        public ScoreType Type
        {
            get { return ScoreType.Number; }
        }

        /// <summary>
        /// 表示用文字列のプロパティ
        /// </summary>
        public string TextForDisplay
        {
            get
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return score.ToString(format);
                }
                else
                {
                    return score.ToString();
                }
            }
        }

        /// <summary>
        /// 保存用文字列のプロパティ
        /// </summary>
        public string TextForSave
        {
            get { return score.ToString(); }
        }

        /// <summary>
        /// 値のプロパティ
        /// </summary>
        public double Value
        {
            get { return score; }
        }
    }
}

