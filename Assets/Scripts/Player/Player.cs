using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int score;

    public Player(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }

    public string GetName()
    {
        return this.name;
    }
    public int GetScore()
    {
        return this.score;
    }
}
