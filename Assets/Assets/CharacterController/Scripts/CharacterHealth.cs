using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public bool isAlive = true;

    public GameObject deathSplashEffect;

    [ContextMenu("Death")]
    public void Death()
    {
        Instantiate(deathSplashEffect,transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
