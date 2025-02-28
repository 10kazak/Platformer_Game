using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 3;
    [SerializeField] private float jumpForce = 10f;
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

    public void GetDamage()
    {
        lives--;
        Debug.Log(lives);
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
