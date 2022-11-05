using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRanking;

public class SampleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var score = 200;
        UnityRanking.RankingLoader.Instance.SendScoreAndShowRanking(score, ScoreOrderType.OrderByAscending, 10);

        //TODO：時間型のスコアの動作を試す。
        // var time = 123456;
        // var timeScore = new System.TimeSpan(0,0,0,0,time);
        // UnityRanking.RankingLoader.Instance.SendTimeAndShowRanking(timeScore, ScoreOrderType.OrderByAscending, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
