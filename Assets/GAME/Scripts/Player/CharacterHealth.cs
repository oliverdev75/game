using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public bool isAlive = true;

    public GameObject deathSplashEffect;
    Color deathSplashColor;

    [ContextMenu("Death")]
    public void Death()
    {
        GameObject particle = Instantiate(deathSplashEffect,transform.position, Quaternion.identity);
        particle.GetComponent<SplashEffectStyle>().SetSplashColor(deathSplashColor);

        gameObject.SetActive(false);

        GameLevelManager.instance.CheckGameLevelFinished();
    }

    public void SetDeathSplashColor(Color color)
    {
        deathSplashColor = color;
    }
}
