using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class ServerController
{
    public static IEnumerator Post(string url, string jsonData, System.Action<string> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            Debug.Log("sending data");
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);

            }

            else
            {
                if (www.isDone)
                {

                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    // Do something with the result
                    if (callback != null) { callback.Invoke(result); }

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                    if (callback != null) { callback.Invoke(null); }
                }
            }
        }
    }

    public static IEnumerator Get(string url, System.Action<string> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("content-type", "application/json");
            //www.uploadHandler.contentType = "application/json";

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);

            }

            else
            {
                if (www.isDone)
                {

                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    // Do something with the result
                    if (callback != null) { callback.Invoke(result); }

                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                    if (callback != null) { callback.Invoke(null); }
                }
            }

        }
    }
}
