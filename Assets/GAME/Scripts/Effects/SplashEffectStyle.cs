using UnityEngine;

public class SplashEffectStyle : MonoBehaviour
{
   public ParticleSystem[] splashEffect;
   public void SetSplashColor(Color color)
   {
      foreach (ParticleSystem splashEffect in splashEffect)
      {
         var splashEffectMain = splashEffect.main;
         splashEffectMain.startColor = color;
      }
   }
}
