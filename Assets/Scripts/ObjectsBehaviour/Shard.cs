using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AudioClip pickShardSound;
    [SerializeField] private string id;
    private SpriteRenderer visual;
    private bool collected = false;


    [ContextMenu ("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }


    public void LoadData(GameData data) 
    {
        data.shardCollected.TryGetValue(id, out collected);
        if (collected) 
        {
            visual.gameObject.SetActive(false);
        }
    }


    public void SaveData(GameData data) 
    {
        if (data.shardCollected.ContainsKey(id))
        {
            data.shardCollected.Remove(id);
        }
        data.shardCollected.Add(id, collected);
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            visual.gameObject.SetActive(false);
            CollectShard();
        }
    }


    private void CollectShard() 
    {
        SoundManager.instance.PlaySound(pickShardSound);
        collected = true;
    }
}
