using Unity.VisualScripting;
using UnityEngine;
using System;

public class CharacterHealth : MonoBehaviour
{
    public bool isAlive = true;

    public GameObject deathSplashEffect;
    Color deathSplashColor;
    
    public Action onDeath;

    [ContextMenu("Death")]
    public void Death()
    {
        GameObject particle = Instantiate(deathSplashEffect,transform.position, Quaternion.identity);
        particle.GetComponent<SplashEffectStyle>().SetSplashColor(deathSplashColor);
        
        AudioManager.Instance.PlayOneShot("Splat");

        gameObject.SetActive(false);
        onDeath?.Invoke();

        GameLevelManager.instance.CheckGameLevelFinished();
    }

    public void SetDeathSplashColor(Color color)
    {
        deathSplashColor = color;
    }
}
