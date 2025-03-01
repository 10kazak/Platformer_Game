using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.VersionControl.Asset;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;





    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        lives = 3;
        health = lives;
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Instance = this;
    }

    private void Run()
    {
        if (isGrounded) State = States.Run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
        health -= 1;
        if (health == 0)
        {
            foreach (var h in hearts)
            {
                h.sprite = deadHeart;
            }
            Die();
        }
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isGrounded) State = States.Idle;

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        CheckGround();

        if (health > lives)
        {
            health = lives;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) { hearts[i].sprite = aliveHeart; }
            else { hearts[i].sprite = deadHeart; }

            if (i < lives) { hearts[i].enabled = true; }
            else { hearts[i].enabled = false; }
        }
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded) State = States.Jump;
    }
}

public enum States
{
    Idle,
    Run,
    Jump
}
