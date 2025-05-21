using UnityEngine;

namespace BASE
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class CharacterMover : MonoBehaviour
    {
        public float playerSpeed = 10;
        public float timeToReachMaxSpeed = 0.25f;
        float currentPlayerSpeed = 0f;

        public float jumpHeight = 1.5f;
        [SerializeField] LayerMask GroundlayerMask;

        [Header("Move   Player Rotation")]
        public float rotationAmount = 5f;
        public float rotationSpeed = 20f;

        private Rigidbody2D rb;
        private CircleCollider2D circleCollider;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            if(Mathf.Abs(rb.linearVelocityX) > 0.1f)
                spriteRenderer.flipX = rb.linearVelocity.x > 0;

            float tiltAmount = Mathf.Clamp(rb.linearVelocity.x, -1f, 1f) * rotationAmount;

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, -tiltAmount),
                Time.deltaTime * rotationSpeed
            );
        }


        public void Move(Vector2 moveDirection)
        {
            if (moveDirection.x != 0)
            {

                // Aceleracion del personaje
                if (timeToReachMaxSpeed > 0)
                {
                    if (currentPlayerSpeed < playerSpeed)
                    {
                        currentPlayerSpeed += (playerSpeed / timeToReachMaxSpeed) * Time.deltaTime;
                        if (currentPlayerSpeed > playerSpeed)
                        {
                            currentPlayerSpeed = playerSpeed;
                        }
                    }
                }
                else
                    currentPlayerSpeed = playerSpeed;

                // Movimiento
                Vector2 moveDir = Vector2.right * moveDirection.x;
                moveDir.Normalize();
                if (CheckCollissionsInDirection(Vector2.down) || CheckCollissionsInDirection(moveDir) == false)
                {
                    Vector2 playerForce = moveDir * currentPlayerSpeed;
                    rb.linearVelocity = new Vector2(playerForce.x, rb.linearVelocity.y);
                }
            }
            else
            {
                currentPlayerSpeed = 0;
            }

        }

        public void Jump()
        {
            if (!CheckCollissionsInDirection(Vector2.down))
                return;

            float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * gravity);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }

        bool CheckCollissionsInDirection(Vector2 dir)
        {
            float rayLength = 0.1f + (circleCollider.radius);
            Vector2 center = (Vector2)transform.position + (Vector2)(dir.normalized * 0.1f);
            Vector2 perpendicular = Vector2.Perpendicular(dir.normalized) * (circleCollider.radius);

            Debug.DrawRay(center, dir, Color.green);
            Debug.DrawRay(center + perpendicular, dir, Color.green);
            Debug.DrawRay(center + -perpendicular, dir, Color.green);

            if (Physics2D.Raycast(center, dir.normalized, rayLength, GroundlayerMask))
                return true;

            if (Physics2D.Raycast(center + perpendicular, dir.normalized, rayLength, GroundlayerMask))
                return true;

            if (Physics2D.Raycast(center - perpendicular, dir.normalized, rayLength, GroundlayerMask))
                return true;

            return false;
        }
        void OnDrawGizmos()
        {
            if (rb == null)
                return;

            Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position, (Vector2)transform.position + (rb.linearVelocity.normalized * rb.linearVelocity.magnitude) / playerSpeed);

            Gizmos.DrawRay(transform.position, Vector2.down);
        }
    }
}