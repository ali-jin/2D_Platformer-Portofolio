using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound made when player pick the new checkpoint
    private Transform currentCheckpoint; // Store the last checkpoint here
    private HealthPlayer playerHealth;


    private void Awake()
    {
        playerHealth = GetComponent<HealthPlayer>();
    }


    // Load the last checkpoint position
    public void LoadData(GameData data)
    {
        this.currentCheckpoint.position = data.checkpointPosition;
    }


    // Save the last checkpoint position
    public void SaveData(GameData data)
    {
        data.checkpointPosition = this.currentCheckpoint.position;
    }


    public void Respawn()
    {
        transform.position = new Vector3(currentCheckpoint.position.x, currentCheckpoint.position.y + 1f, 
            currentCheckpoint.position.z); // Move player to the checkpoint  position
        playerHealth.Respawn(); // Restore health and reset animation
    }


    // trigger visual effect if player touch checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
}
