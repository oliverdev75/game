using UnityEngine;

public class CrazyBallManager : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private Vector2 moveDirection;

    public Transform ballSpriteTransform;
    
    public float ballSpeed = 1;
    public float maxBallSpeed = 20f;
    public float speedIncreaseRate = 0.5f; // unidades por segundo

    public Vector2 corner = new Vector2(12f, 6f);

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        float radius = transform.localScale.y * 0.5f;

        // Aumenta la velocidad hasta el máximo
        ballSpeed = Mathf.Min(ballSpeed + speedIncreaseRate * Time.deltaTime, maxBallSpeed);

        transform.Translate(ballSpeed * Time.deltaTime * moveDirection);

        Vector3 pos = transform.position;

        if ((pos.y + radius) > corner.y)
        {
            pos.y = corner.y - radius;
            moveDirection.y *= -1f;
            moveDirection.x = ChangeDirectionRandomBased(moveDirection.x);
        }

        if ((pos.y - radius) < -corner.y)
        {
            pos.y = -corner.y + radius;
            moveDirection.y *= -1f;
            moveDirection.x = ChangeDirectionRandomBased(moveDirection.x);
        }

        if ((pos.x + radius) > corner.x)
        {
            pos.x = corner.x - radius;
            moveDirection.x *= -1f;
            moveDirection.y = ChangeDirectionRandomBased(moveDirection.y);
        }

        if ((pos.x - radius) < -corner.x)
        {
            pos.x = -corner.x + radius;
            moveDirection.x *= -1f;
            moveDirection.y = ChangeDirectionRandomBased(moveDirection.y);
        }

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        ballSpriteTransform.rotation = Quaternion.Euler(0, 0, angle * ballSpeed * 2);
        
        moveDirection.Normalize();
        transform.position = pos;
    }

    float ChangeDirectionRandomBased(float direction)
    {
        return (Random.Range(0, 9) == 0) ? Random.Range(-1f, 1f) : direction;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(corner, 0.5f);
        Gizmos.DrawSphere(-corner, 0.5f);
    }
}
