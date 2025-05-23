using UnityEngine;

public class SplashEffectStyle : MonoBehaviour
{
   public ParticleSystem[] splashEffect;
   public float alpha = 0.75f;
   public void SetSplashColor(Color color)
   {
      foreach (ParticleSystem splashEffect in splashEffect)
      {
         var splashEffectMain = splashEffect.main;
         splashEffectMain.startColor = new Color(color.r, color.g, color.b, alpha);
      }
   }
}
