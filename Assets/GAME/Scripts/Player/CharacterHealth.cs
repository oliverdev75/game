using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public bool isAlive = true;

    public GameObject deathSplashEffect;

    [ContextMenu("Death")]
    public void Death()
    {
        Instantiate(deathSplashEffect,transform.position, Quaternion.identity);
        GameLevelManager.instance.CheckGameLevelFinished();
        gameObject.SetActive(false);
    }
}
