using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UnityRanking
{
    /// <summary>
    /// ランキングビューのノードクラス
    /// </summary>
    public class RankingViewNode : MonoBehaviour
    {
        /// <summary>
        /// 順位のテキスト
        /// </summary>
        public TextMeshProUGUI NoText;

        /// <summary>
        /// 名前のテキスト
        /// </summary>
        public TextMeshProUGUI NameText;

        /// <summary>
        /// スコアのテキスト
        /// </summary>
        public TextMeshProUGUI ScoreText;
    }    
}