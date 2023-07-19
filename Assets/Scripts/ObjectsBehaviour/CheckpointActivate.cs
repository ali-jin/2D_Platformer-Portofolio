using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// visual effect when checkpoint is trigger
public class CheckpointActivate : MonoBehaviour
{
    private ParticleSystem activateCheckpoint;

    private void Awake() 
    {
        activateCheckpoint = this.GetComponentInChildren<ParticleSystem>();
        activateCheckpoint.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player") 
        {
            activateCheckpoint.Play();
        }
    }
}
