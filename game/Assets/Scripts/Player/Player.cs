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

    [SerializeField]
    private float attentionLevel;

    public Player(string name, int score, float attentionLevel)
    {
        this.name = name;
        this.score = score;
        this.attentionLevel = attentionLevel;
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

    public float GetAttentionLevel()
    {
        return this.attentionLevel;
    }
}
