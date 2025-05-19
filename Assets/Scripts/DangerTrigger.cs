using UnityEngine;

public class DangerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterHealth>() != null)
        {
            collision.GetComponent<CharacterHealth>().Death();
        }
    }
}
