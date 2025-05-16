using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10;
    public float jumpForce = 5;
    public LayerMask groundLayerMask;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = 0f;
        if (Input.GetKey(KeyCode.A)) {
            inputX = -1;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputX = 1;
        }

        if (Input.GetKeyDown(KeyCode.W) && CheckCollisionInDirection(Vector2.down)) {
            Vector2 jumpVelocity = Vector2.up * jumpForce;
            rb.linearVelocityY = jumpVelocity.y;

            Debug.Log(CheckCollisionInDirection(Vector2.down));
        }

        Vector2 moveDirection = new Vector2(inputX, 0).normalized;
        Vector2 moveVelocity = moveSpeed * moveDirection;
        rb.linearVelocity = moveVelocity + new Vector2(0, rb.linearVelocityY);
    }

/*     void OnDrawGizmos()
    {
        if (rb) {
            Gizmos.DrawLine(transform.position, (Vector2) transform.position + rb.linearVelocity.normalized);      
            Gizmos.DrawRay(transform.position, Vector2.down);
        }
    } */

    bool CheckCollisionInDirection(Vector2 direction)
    {
        const float radius = 0.5f;
        const float offset = 0.1f;

        Vector2 center = (Vector2) transform.position;
        Vector2 perpendicular = Vector2.Perpendicular(direction.normalized) * radius;

        Debug.DrawRay(center + perpendicular, direction, Color.red, 0.1f);
        Debug.DrawRay(center, direction, Color.red, 0.1f);
        Debug.DrawRay(center - perpendicular, direction, Color.red, 0.1f);

        if (Physics2D.Raycast(center + perpendicular, direction, radius + offset, groundLayerMask)) {
            return true;
        }

        if (Physics2D.Raycast(center, direction, radius + offset, groundLayerMask)) {
            return true;
        }

        if (Physics2D.Raycast(center - perpendicular, direction, radius + offset, groundLayerMask)) {
            return true;
        }

        return false;
    }
}
