using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player variables
    private Rigidbody2D rb;
    public float speed;
    private float horizontalInput;
    public float jumpForce;
    public bool isOnGround = true;
    //powerup variables
    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    public float powerupStrength;
    //Jump pad variables
    public float jumpPadStrength;


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
        //powerupIndicator follows player as well as has the same rotation
        powerupIndicator.transform.position = transform.position;
        powerupIndicator.transform.rotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector2.up * jumpPadStrength, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

}
