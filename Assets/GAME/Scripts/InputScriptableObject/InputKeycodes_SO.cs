using UnityEngine;

namespace BASE
{

    [CreateAssetMenu(fileName = "InputKeycodes_SO", menuName = "Scriptable Objects/InputKeycodes_SO")]
    public class InputKeycodes_SO : ScriptableObject
    {
        public KeyCode upKeycode;
        public KeyCode downKeycode;
        public KeyCode rightKeycode;
        public KeyCode leftKeycode;

        public string alt_upAxis;
        public string alt_downAxis;
        public string alt_rightAxis;
        public string alt_leftAxis;
    }
}