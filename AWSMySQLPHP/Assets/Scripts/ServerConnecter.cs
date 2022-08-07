using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// サーバーとの接続クラス
/// </summary>
public class ServerConnecter
{
    private const string SERVER_ADDRESS = "";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ServerConnecter()
    {

    }

    /// <summary>
    /// Post通信処理
    /// </summary>
    /// <param name="requestParams">リクエストパラメータ</param>
    /// <returns></returns>
    private IEnumerator Post(Dictionary<string, string> requestParams)
    {
        // リクエストパラメータの設定
        WWWForm wWWForm = new WWWForm();
        foreach (KeyValuePair<string, string> requestParam in requestParams)
        {
            wWWForm.AddField(requestParam.Key, requestParam.Value);
        }

        // POST通信
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(SERVER_ADDRESS, wWWForm))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isDone)
            {
                Debug.Log("HttpPost OK:" + unityWebRequest.downloadHandler.text);
                //TODO:レスポンスデータを格納
            }
            else
            {
                Debug.Log("HttpPost NG:" + unityWebRequest.error);
            }
        }
    }
}
