using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackAction : MonoBehaviour
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
    private Rigidbody2D body;
    private Health enemyHealth;
    // [SerializeField] private bool grounded;
    private PlayerMovementAction playerMovement;
    public PlayerInputActions playerControls;
    private float cooldownTimer = Mathf.Infinity;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovementAction>();
        playerControls = new PlayerInputActions();
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && playerMovement.canAttack())
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

    }


    // See the range attack of the player
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }

}