using UnityEngine;

public class CrazyBallManager : MonoBehaviour
{

    private CircleCollider2D circleCollider2D;
    private Vector2 moveDirection;
    public float ballSpeed = 1;
    public Vector2 corner = new Vector2(12f, 6f);

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        float radius = transform.localScale.y * 0.5f;
        transform.Translate(ballSpeed * Time.deltaTime * moveDirection);
        ballSpeed = Mathf.Clamp(ballSpeed, 1, 30);

        if ((transform.position.y + radius) > corner.y)
        {
            moveDirection.y *= -1f;
            moveDirection.x = ChangeDirectionRandomBased(moveDirection.x);
            Debug.Log(transform.position.y + " > " + corner.y);
        }

        if ((transform.position.y - radius) < -corner.y)
        {
            moveDirection.y *= -1f;
            moveDirection.x = ChangeDirectionRandomBased(moveDirection.x);
            Debug.Log(transform.position.y + " > " + corner.y);
        }

        if ((transform.position.x + radius) > corner.x)
        {
            moveDirection.x *= -1f;
            moveDirection.y = ChangeDirectionRandomBased(moveDirection.y);
            Debug.Log(transform.position.x + " > " + corner.x);
        }
        
        if ((transform.position.x - radius) < -corner.x)
        {
            moveDirection.x *= -1f;
            moveDirection.y = ChangeDirectionRandomBased(moveDirection.y);
            Debug.Log(transform.position.x + " > " + corner.x);
        }
    }

    float ChangeDirectionRandomBased(float direction)
    {
        if ((int) Random.Range(0f, 9f) == 0f)
        {
            return Random.Range(-1f, 1f);
        }
        else
        {
            return direction;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(corner, 0.5f);
        Gizmos.DrawSphere(-corner, 0.5f);
    }

}
