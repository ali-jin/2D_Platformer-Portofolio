using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow Player
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }


    public void MoveToNeWRoom(Transform _newRomm)
    {
        currentPosX = _newRomm.position.x;
    }
}
