using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRanking
{
    /// <summary>
    /// 時間のスコア
    /// </summary>
    public class TimeScore : IScore
    {
        /// <summary>
        /// 時間
        /// </summary>
        private TimeSpan time;

        /// <summary>
        /// フォーマット
        /// </summary>
        private string format;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="format">フォーマット</param>
        public TimeScore(TimeSpan time, string format = "")
        {
            this.time = time;
            this.format = format;
        }

        /// <summary>
        /// スコアの型のプロパティ
        /// </summary>
        public ScoreType Type
        {
            get { return ScoreType.Time; }
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
                    return new DateTime(0).Add(time).ToString(format);
                }
                else
                {
                    return new DateTime(0).Add(time).ToString();
                }
            }
        }

        /// <summary>
        /// 保存用文字列のプロパティ
        /// </summary>
        public string TextForSave
        {
            get { return time.Ticks.ToString(); }
        }

        /// <summary>
        /// 値のプロパティ
        /// </summary>
        public double Value
        {
            get { return time.Ticks; }
        }
    }
}
