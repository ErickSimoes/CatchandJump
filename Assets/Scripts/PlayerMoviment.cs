using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private float horizontal;
    private bool facingRight = true;
    private Vector3 myLocalScale;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallForce;
    private Vector2 gravity;

    void Start()
    {
        
    }

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //rb.AddForce(Vector2.up * jump);
            rb.velocity = new Vector2(rb.velocity.x, jump);
            
            gravity = new Vector2(0, -Physics2D.gravity.y);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        //gameObject.transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!IsGrounded() && rb.velocity.y < 0f)
        {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1.1f);
            
            rb.velocity -= gravity * fallForce * Time.fixedDeltaTime;

            //rb.velocity -= new Vector2(rb.velocity.x, -Physics2D.gravity.y) * fallForce * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void Flip() {
        if ((facingRight && horizontal < 0f) || (!facingRight && horizontal > 0f))
        {
            facingRight = !facingRight;
            myLocalScale = transform.localScale;
            myLocalScale.x *= -1f;
            transform.localScale = myLocalScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
        }
    }
}
