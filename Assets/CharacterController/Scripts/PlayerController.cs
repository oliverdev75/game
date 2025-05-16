using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BASE
{
    [RequireComponent(typeof(CharacterMover))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] CharacterMover characterMover;
        public InputKeycodes_SO inputKeycodes;


        private void Update()
        {
            Vector2 moveDir = ReadInputs();

            characterMover.Move(moveDir);

            if (moveDir.y > 0)
                characterMover.Jump();
        }

        Vector2 ReadInputs()
        {
            Vector2 inputAxis = Vector2.zero;

            if (Input.GetKey(inputKeycodes.rightKeycode))
                inputAxis.x = 1f;
            if (Input.GetKey(inputKeycodes.leftKeycode))
                inputAxis.x = -1f;

            if (Input.GetKey(inputKeycodes.upKeycode))
                inputAxis.y = 1f;
            if (Input.GetKey(inputKeycodes.downKeycode))
                inputAxis.y = -1f;

            return inputAxis;
        }

    }

}