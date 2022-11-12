using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalForce;
    public float horizontalSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint; // origion of circle
    public float groundRadius; // size of circle
    public LayerMask groundLayerMask; // the stuff we can collide with
    public bool isGrounded;


    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;

        Move();
        Jump();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");
        if (x != 0.0f)
        {
            Flip(x);
            x = (x > 0) ? 1.0f : -1.0f; // sanitizing x

            rb2D.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            //rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, horizontalSpeed);

            var clampedVelocity = Mathf.Clamp(rb2D.velocity.x, -horizontalSpeed, horizontalSpeed);

            rb2D.velocity = new Vector2(clampedVelocity, rb2D.velocity.y);
        }
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0))
        {
            rb2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    public void Flip(float x)
    {
        if(x != 0.0f)
        {
            transform.localScale = new Vector3((float)((x > 0.0f) ? 1.0f : -1.0), 1.0f, 1.0f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
