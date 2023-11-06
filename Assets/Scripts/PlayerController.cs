using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float horizontalInput;
    public float jumpForce;
    public bool isOnGround = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right *  speed * horizontalInput);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) 
        {
            isOnGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            isOnGround = true;
        }
    }
}
