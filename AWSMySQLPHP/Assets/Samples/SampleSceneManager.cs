using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRanking;

public class SampleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityRanking.RankingLoader.Instance.SendScoreAndShowRanking(200, ScoreOrderType.OrderByAscending, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
