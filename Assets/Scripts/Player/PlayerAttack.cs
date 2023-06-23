using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;

    [Header ("Attack Parameters")]
    [SerializeField] private float range;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;


    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    //References
    [SerializeField] private AudioClip attackSound;
    private Animator anim;
    private Health enemyHealth;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }


    private void Attack()
    {
        SoundManager.instance.PlaySound(attackSound);

        anim.SetTrigger("attack");
        cooldownTimer = 0;

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<Health>().TakeDamage(damage);
        }

    }


    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}