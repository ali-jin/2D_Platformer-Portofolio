using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound made when player pick the new checkpoint
    private Transform currentCheckpoint; // Store the last checkpoint here
    private Health playerHealth;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }


    public void Respawn()
    {
        transform.position = currentCheckpoint.position; // Move player to the checkpoint  position
        playerHealth.Respawn(); // Restore health and reset animation

        // Move camera to checkpoint position
        // Camera.main.GetComponent<CameraController>().
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
        }
        // collision.GetComponent<Collider>().enabled = false;
        // collision.GetComponent<Animator>().SetTrigger()
    }
}
