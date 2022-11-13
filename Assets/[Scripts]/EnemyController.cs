using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalSpeed;
    public Transform inFrontPoint; // origion of circle
    public Transform aheadPoint; // origion of circle
    public Transform groundPoint; // origion of circle
    public float groundRadius; // size of circle
    public LayerMask groundLayerMask; // the stuff we can collide with
    public bool isObstacalAhead;
    public bool isGroundAhead;
    public bool isGrounded;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        isObstacalAhead = Physics2D.Linecast(groundPoint.position, inFrontPoint.position, groundLayerMask);
        isGroundAhead = Physics2D.Linecast(groundPoint.position, aheadPoint.position,groundLayerMask);
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);

        if (isGrounded && isGroundAhead)
        {
            Move();
        }
        if (!isGroundAhead || isObstacalAhead)
        {
            Flip();
        }
    }

    public void Move()
    {
        transform.position += new Vector3(direction.x * horizontalSpeed * Time.deltaTime, 0.0f);
    }

    public void Flip()
    {
        var x = transform.localScale.x * -1.0f;
        direction *= -1.0f;
        transform.localScale = new Vector3(x, 1.0f, 1.0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
        Gizmos.DrawLine(groundPoint.position, inFrontPoint.position);
        Gizmos.DrawLine(groundPoint.position, aheadPoint.position);
    }
}
