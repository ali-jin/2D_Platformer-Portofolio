using UnityEngine;

public class HealthCollectible : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickHealth;
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
        data.healthCollected.TryGetValue(id, out collected);
        if (collected) 
        {
            visual.gameObject.SetActive(false);
        }
    }


    public void SaveData(GameData data) 
    {
        if (data.healthCollected.ContainsKey(id))
        {
            data.healthCollected.Remove(id);
        }
        data.healthCollected.Add(id, collected);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthPlayer>().AddHealth(healthValue);
            gameObject.SetActive(false);
            CollectHealth();
        }
    }


    private void CollectHealth()
    {
        SoundManager.instance.PlaySound(pickHealth);
        collected = true;
    }
}
