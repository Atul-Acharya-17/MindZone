using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class CortexFacade:MonoBehaviour
{

    public List<GameObject> buttons = new List<GameObject>();

    public static string appUrl = "wss://localhost:6868";
    public static string appName = "MindZone";

    private static string clientID = "LJGRcgyRuuMF6SddP3jxuoMdtxctj1zjkcGhNXQi";
    private static string clientSecret = "hlkwqCw9Fe0Li4peGQHb5jgvOObJwJnL9W8IfkSjrvJ1ndh3UucSS3lTGCRGmEvDFEBZ1b6ZB92bFE6dY9q0j77Tg7CwYIMIAMnE26CGwwqJvFoBSfA8NiAp1TZOi3fi";
    private static string dataDir = "UnityApp";

    //private static string cortexToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBJZCI6ImNvbS5hdHVsYWNoYXJ5YTE3Lm1pbmR6b25lIiwiYXBwVmVyc2lvbiI6IjEuMCIsImV4cCI6MTYyMTg0Mjk1NSwibmJmIjoxNjIxNTgzNzU1LCJ1c2VySWQiOiI5MjBhZTA2ZS1lNDJhLTQ0ZWEtYjVhMS1kMGI1YThkNmIwYjYiLCJ1c2VybmFtZSI6ImF0dWxhY2hhcnlhMTciLCJ2ZXJzaW9uIjoiMi4wIn0=.Yg9Z6kuVLa7PPhU5sTwPjinWzNVjZE0iWnIsTCHlU8g=";

    private static string device = "EPOCPLUS-3B9ACA8A";
    private static string appVersion = "2.7.0 Dev";

    private static List<string> streamNameList = new List<string>();

    private static DataStreamManager dataStream = DataStreamManager.Instance;
    private static RecordManager recordManager = RecordManager.Instance;
    private static BCITraining trainer = new BCITraining();

    public static void SetUpConfiguration()
    {
        dataStream.SetAppConfig(clientID, clientSecret,
                                 appVersion, appName,
                                 dataDir
                                 );
    }

    public static void Authorize()
    {
        Debug.Log("Authorizing");

        dataStream.StartAuthorize();
    }

    public static void Stop()
    {
        Debug.Log("Application ending");
        dataStream.Stop();
    }

    public static void StartDataStream()
    {
        Debug.Log("Subscribing to sys and com for mental commands");

        streamNameList.Add(DataStreamName.MentalCommands);
        streamNameList.Add(DataStreamName.SysEvents);

        dataStream.StartDataStream(streamNameList, device);
    }

    public static void Train()
    {
        trainer.StartTraining("concentrate", "mentalCommand");
    }

    public static void InitializeTrainer()
    {
        trainer.Init();
    }
    public static void QueryProfile()
    {
        trainer.QueryProfile();
    }
    public static void CreateProfile(string profileName)
    {
        trainer.CreateProfile(profileName);
    }

    public static void LoadProfile(string profileName)
    {
        trainer.LoadProfile(profileName);
    }
}
