using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMgr : MonoBehaviour
{

    public List<GameObject> playerRanks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        string url = "http://localhost:8000/players";
        StartCoroutine(ServerController.Get(url, 
            result =>
            {
                Debug.Log("{ \"players\": " + result + "}");
                PlayerRecords playerRecords = new PlayerRecords();
                playerRecords = JsonUtility.FromJson<PlayerRecords>("{ \"players\": " + result + "}");
                Debug.Log(playerRecords.GetPlayers().Count);

                for (int i = 0; i < playerRecords.GetPlayers().Count; ++i)
                {
                    playerRanks[i].transform.Find("Rank").GetComponent<Text>().text = (i+1).ToString();
                    playerRanks[i].transform.Find("Name").GetComponent<Text>().text = playerRecords.GetPlayers()[i].GetName();
                    playerRanks[i].transform.Find("Score").GetComponent<Text>().text = playerRecords.GetPlayers()[i].GetScore().ToString();
                }

                if (playerRecords.GetPlayers().Count < playerRanks.Count)
                {
                    for (int i = playerRecords.GetPlayers().Count; i < playerRanks.Count; ++i)
                    {
                        playerRanks[i].transform.Find("Rank").GetComponent<Text>().text = "---";
                        playerRanks[i].transform.Find("Name").GetComponent<Text>().text = "---";
                        playerRanks[i].transform.Find("Score").GetComponent<Text>().text = "---";
                    }
                }
            }
            ));
    }
}
