
namespace UnityRanking
{
    /// <summary>
    /// スコア情報のインターフェース
    /// </summary>
    public interface IScore
    {
        /// <summary>
        /// スコアタイプのプロパティ
        /// </summary>
        ScoreType Type { get; }

        /// <summary>
        /// スコア昇順降順のプロパティ
        /// </summary>
        ScoreOrderType OrderType { get; }

        /// <summary>
        /// 表示用文字列のプロパティ
        /// </summary>
        string TextForDisplay { get; }

        /// <summary>
        /// 保存用文字列のプロパティ
        /// </summary>
        string TextForSave { get; }

        /// <summary>
        /// 値のプロパティ
        /// </summary>
        double Value { get; }

        /// <summary>
        /// 最下位のプロパティ
        /// </summary>
        int BottomRank { get; }
    }
}
