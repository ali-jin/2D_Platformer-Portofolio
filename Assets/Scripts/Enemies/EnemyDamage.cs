using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<HealthPlayer>().TakeDamage(damage);
    }
}