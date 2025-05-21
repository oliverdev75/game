using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BASE
{
    [RequireComponent(typeof(CharacterMover), typeof(CharacterKick))]
    public class PlayerController : MonoBehaviour
    {
        CharacterMover characterMover;
        CharacterKick characterKick;
        public InputKeycodes_SO inputKeycodes;
        public Vector2 lastLookDir = Vector2.zero;

        private void Awake()
        {
            characterMover = GetComponent<CharacterMover>();
            characterKick = GetComponent<CharacterKick>();
        }

        private void Update()
        {
            Vector2 moveDir = ReadInputs();

            if(moveDir.x != 0)
                lastLookDir.x = moveDir.x;

            characterMover.Move(moveDir);

            if (moveDir.y > 0)
                characterMover.Jump();

            if (moveDir.y < 0)
                characterKick.Kick(transform.position, lastLookDir);
        }

        Vector2 ReadInputs()
        {
            Vector2 inputAxis = Vector2.zero;

            if (Input.GetKey(inputKeycodes.rightKeycode))
                inputAxis.x = 1f;
            if (Input.GetKey(inputKeycodes.leftKeycode))
                inputAxis.x = -1f;

            if (Input.GetKeyDown(inputKeycodes.upKeycode))
                inputAxis.y = 1f;
            if (Input.GetKeyDown(inputKeycodes.downKeycode))
                inputAxis.y = -1f;

            return inputAxis;
        }

    }

}