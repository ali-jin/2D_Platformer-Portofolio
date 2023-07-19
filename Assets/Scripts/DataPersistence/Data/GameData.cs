using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int totalScore;
    public float currentHealth;
    public Vector3 playerPosition;
    public Vector3 checkpointPosition;
    public SerializableDictionary<string, bool> shardCollected;
    public SerializableDictionary<string, bool> healthCollected;


    // Store all the data of the game
    public GameData()
    {
        this.totalScore = 0;
        this.currentHealth = 10;
        playerPosition = new Vector3(-6.98f, -0.01f, 0);
        checkpointPosition = new Vector3(-6.98f, -0.01f, 0);
        shardCollected = new SerializableDictionary<string, bool>();
        healthCollected = new SerializableDictionary<string, bool>();
    }
}
