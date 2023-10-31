using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float force;
    public float jumpForce;
    float horizontal;

    public FixedJoystick joystick;

    public Transform groundCheck;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = (new Vector2(rb.velocity.x, jumpForce));
        }
        if (Input.GetButton("Jump") && !isGrounded())
        {
            rb.AddForce(rb.velocity.normalized * force);
        }
    }
    private void FixedUpdate()
    {
        if (isGrounded())
        {
            //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            
            Vector3 direction = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
            rb.velocity = new Vector2(direction.x * speed + horizontal * speed, direction.y + rb.velocity.y);
        }
        
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Finish")
        {
            GameManager.instance.Death();
        }
    }
}
