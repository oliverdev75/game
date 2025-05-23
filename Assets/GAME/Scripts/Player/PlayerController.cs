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
        Vector2 lastLookDir = Vector2.zero;

        public InputKeycodes_SO inputKeycodes;

        private void Awake()
        {
            characterMover = GetComponent<CharacterMover>();
            characterKick = GetComponent<CharacterKick>();
        }

        private void Update()
        {
            if (GameManager.instance.inputsAreEnabled == false)
                return;

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

            // Eje horizontal
            if (Input.GetKey(inputKeycodes.rightKeycode))
                inputAxis.x = 1f;
            else if (Input.GetKey(inputKeycodes.leftKeycode))
                inputAxis.x = -1f;
            else
            {
                if (!string.IsNullOrEmpty(inputKeycodes.alt_rightAxis) && Input.GetAxisRaw(inputKeycodes.alt_rightAxis) > 0.5f)
                    inputAxis.x = 1f;
                else if (!string.IsNullOrEmpty(inputKeycodes.alt_leftAxis) && Input.GetAxisRaw(inputKeycodes.alt_leftAxis) > 0.5f)
                    inputAxis.x = -1f;
            }

            // Eje vertical
            if (Input.GetKeyDown(inputKeycodes.upKeycode))
                inputAxis.y = 1f;
            else if (Input.GetKeyDown(inputKeycodes.downKeycode))
                inputAxis.y = -1f;
            else
            {
                if (!string.IsNullOrEmpty(inputKeycodes.alt_upAxis) && Input.GetAxisRaw(inputKeycodes.alt_upAxis) > 0.5f)
                    inputAxis.y = 1f;
                else if (!string.IsNullOrEmpty(inputKeycodes.alt_downAxis) && Input.GetAxisRaw(inputKeycodes.alt_downAxis) > 0.5f)
                    inputAxis.y = -1f;
            }

            return inputAxis;
        }


    }

}