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
    /// <summary>
    /// サーバー側のIPアドレス
    /// </summary>
    private const string SERVER_ADDRESS = "localhost";

    /// <summary>
    /// 自クラスのインスタンス
    /// </summary>
    private static ServerConnecter instance;

    /// <summary>
    /// 自クラスのインスタンスのプロパティ
    /// </summary>
    /// <value></value>
    public static ServerConnecter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerConnecter();
            }
            return instance;
        }
    }

    /// <summary>
    /// Post通信処理
    /// </summary>
    /// <param name="serverFileName">サーバー側のファイル名</param>
    /// <param name="requestParams">リクエストパラメータ</param>
    /// <returns></returns>
    public IEnumerator Post(string serverFileName, Dictionary<string, string> requestParams, string responseData)
    {
        // リクエストパラメータの設定
        WWWForm wWWForm = new WWWForm();
        foreach (KeyValuePair<string, string> requestParam in requestParams)
        {
            wWWForm.AddField(requestParam.Key, requestParam.Value);
        }

        // POST通信
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(SERVER_ADDRESS + "/" + serverFileName, wWWForm))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isDone)
            {
                Debug.Log("HttpPost OK:" + unityWebRequest.downloadHandler.text);
                //TODO:レスポンスデータを格納
                responseData = unityWebRequest.downloadHandler.text;
            }
            else
            {
                Debug.Log("HttpPost NG:" + unityWebRequest.error);
            }
        }
    }
}
