using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRecords 
{
    [SerializeField]
    private List<Player> players;

    public List<Player> GetPlayers()
    {
        return this.players;
    }
}
