using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 3;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
    }
}
