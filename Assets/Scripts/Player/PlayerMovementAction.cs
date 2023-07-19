using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovementAction : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D body;
    private Animator anim;
    private float inputX;
    [SerializeField] private bool grounded;
    public PlayerInputActions playerControls;
    [SerializeField] public TextMeshProUGUI scoreText;

    [SerializeField] private AudioClip jumpSound;
    // [SerializeField] private AudioClip pickShardSound;
    private int totalScore = 0;

    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerControls = new PlayerInputActions();
        scoreText.text = "x" + totalScore;
    }


    // load the data of the score and player position
    public void LoadData(GameData data)
    {
        this.totalScore = data.totalScore;
        this.transform.position = data.playerPosition;
    }


    // Save the data of the score and player position
    public void SaveData(GameData data)
    {
        data.totalScore = this.totalScore;
        data.playerPosition = this.transform.position;
    }


    private void Update()
    {
        body.velocity = new Vector2(inputX * speed, body.velocity.y);

        scoreText.text = "x" + totalScore;
 
        //sets animation parameters
        anim.SetFloat("run", Mathf.Abs(body.velocity.x));
        anim.SetBool("grounded", grounded);

        //Flip player when facing left/right.
        if (body.velocity.x > 0f)
            transform.localScale = Vector3.one;
        else if (body.velocity.x < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);


    }


    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce );
            anim.SetTrigger("jump");
            SoundManager.instance.PlaySound(jumpSound);
            grounded = false;
        }
    }


    // Detect if the player is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shard")
        {
            totalScore += 1;
            scoreText.text = "x" + totalScore;
        }

        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    // Verify if the player can attack
    public bool canAttack()
    {
        return body.velocity.x == 0 && grounded;
    }
}