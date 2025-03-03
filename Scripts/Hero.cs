using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private int key;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private float _sensetive;

    [SerializeField] private float boostSpeed = 2f;
    [SerializeField] private float boostDuration = 3f;

    public Joystick joystick;

    private float boostTimer = 3f;
    private bool isBoosting = false;
    private float originalSpeed;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    public static Hero Instance { get; set; }

    public static int control = 1; 

    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        key = 4;
        keys = 0;
        lives = 3;
        health = lives;
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Instance = this;
        originalSpeed = speed;
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    private void Move()
    {
        if (control == 2) 
        {
            if (SystemInfo.supportsGyroscope)
            {
                if (isGrounded) State = States.Run;
                Vector3 dir = transform.right * Input.acceleration.x; ;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
                sprite.flipX = dir.x < 0.0f;
            }
        }
    }

    private void Run()
    {
        if (control == 1) 
        {
            if (isGrounded) State = States.Run;
            Vector3 dir = transform.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
            sprite.flipX = dir.x < 0.0f;
        }
    }

    private void RunJoystick()
    {
        if (control == 3) 
        {
            if (isGrounded) State = States.Run;
            Vector3 dir = transform.right * joystick.Horizontal;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
            sprite.flipX = dir.x < 0.0f;
        }
    }

    public override void GetDamage()
    {
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

    public override void GetKey()
    {
        keys += 1;
        Debug.Log(keys);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Start()
    {
    }

    public void OnJumpButtonDown()
    {
        if (isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        if (isGrounded) State = States.Idle;

        if (control == 1) 
        {
            if (Input.GetButton("Horizontal"))
            {
                Run();
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        if (control == 2)
        {
            Move();           
        }

        if (control == 3) 
        { 
            if (joystick.Horizontal != 0)
            {
                RunJoystick();
            }
        }

        CheckGround();

        if (health > lives)
        {
            health = lives;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = aliveHeart;
            }
            else
            {
                hearts[i].sprite = deadHeart;
            }

            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (keys > key)
        {
            SceneManager.LoadScene(1);
        }

        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                EndBoost();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("boot"))
        {
            StartBoost();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("hp"))
        {
            health += 1;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("End"))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void StartBoost()
    {
        isBoosting = true;
        boostTimer = boostDuration;
        speed *= boostSpeed;
    }

    private void EndBoost()
    {
        isBoosting = false;
        speed = originalSpeed;
        Debug.Log("Boost ended!");
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
