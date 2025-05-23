using System.Threading.Tasks;
using BASE;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterKick : MonoBehaviour
{
    public ParticleSystem kickParticleSystem;

    public float kickForce;
    [FormerlySerializedAs("kickStunSec")] [Range(100,1000)]
    public int kickStunMiliMillisecondsDelay = 250;
    public LayerMask playerMask;

    private Vector2 lastOrigin;
    private Vector2 lastDirection;
    private float rayLength = 2f;

    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Kick(Vector2 origin, Vector2 direction)
    {
        if (direction.x > 0)
        {
            kickParticleSystem.transform.localScale = Vector3.one;
            kickParticleSystem.transform.rotation = Quaternion.Euler(0, -270, 90);
        }
        else
        {
            kickParticleSystem.transform.localScale = new Vector3(-1,1,1);
            kickParticleSystem.transform.rotation = Quaternion.Euler(0, -90, 90);
        }

        kickParticleSystem.Play();
        AudioManager.Instance.PlayOneShot("Punch");

        lastOrigin = origin + direction.normalized * 0.1f;
        lastDirection = direction.normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(lastOrigin, lastDirection, rayLength, playerMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject != gameObject)
            {
                Rigidbody2D rb = hit.transform.GetComponent<Rigidbody2D>();
                rb.AddForce(lastDirection * kickForce, ForceMode2D.Impulse);
                hit.transform.GetComponent<CharacterKick>().KickStun();
                
                AudioManager.Instance.PlayOneShot("AirWhoosh");
            }
        }
    }

    public async void KickStun()
    {
        playerController.enabled = false;

        await Task.Delay(kickStunMiliMillisecondsDelay);

        playerController.enabled = true;    
    }


    private void OnDrawGizmos()
    {
        if (lastDirection == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(lastOrigin, lastOrigin + lastDirection * rayLength);
        Gizmos.DrawSphere(lastOrigin, 0.05f);
    }
}

