using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRanking
{
    /// <summary>
    /// スコアの昇順降順の型
    /// </summary>
    public enum ScoreOrderType
    {
        /// <summary>
        /// 昇順(数値が小さい方がハイスコア)
        /// </summary>
        OrderByAscending,
        /// <summary>
        /// 降順(数値が大きい方がハイスコア)
        /// </summary>
        OrderByDescending
    }
}