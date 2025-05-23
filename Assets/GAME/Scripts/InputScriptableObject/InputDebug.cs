using UnityEngine;

public class InputDebug : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i <= 19; i++) // Puedes probar hasta 19 o más si tu mando tiene botones extra
        {
            KeyCode key = KeyCode.Joystick1Button0 + i;
            if (Input.GetKeyDown(key))
            {
                Debug.Log("Joystick1 → Botón presionado: " + key);
            }
        }
    }
}
