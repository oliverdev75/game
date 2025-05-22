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

            // Debug para KeyCodes horizontales
            Debug.Log($"RightKeycode ({inputKeycodes.rightKeycode}): {Input.GetKey(inputKeycodes.rightKeycode)}");
            Debug.Log($"LeftKeycode ({inputKeycodes.leftKeycode}): {Input.GetKey(inputKeycodes.leftKeycode)}");

            // Eje horizontal con KeyCode
            if (Input.GetKey(inputKeycodes.rightKeycode))
                inputAxis.x = 1f;
            else if (Input.GetKey(inputKeycodes.leftKeycode))
                inputAxis.x = -1f;
            else
            {
                // Debug ejes alternativos
                if (!string.IsNullOrEmpty(inputKeycodes.alt_rightAxis))
                    Debug.Log($"Alt Right Axis ({inputKeycodes.alt_rightAxis}): {Input.GetAxisRaw(inputKeycodes.alt_rightAxis)}");
                if (!string.IsNullOrEmpty(inputKeycodes.alt_leftAxis))
                    Debug.Log($"Alt Left Axis ({inputKeycodes.alt_leftAxis}): {Input.GetAxisRaw(inputKeycodes.alt_leftAxis)}");

                if (!string.IsNullOrEmpty(inputKeycodes.alt_rightAxis) && Input.GetAxisRaw(inputKeycodes.alt_rightAxis) > 0.5f)
                    inputAxis.x = 1f;
                else if (!string.IsNullOrEmpty(inputKeycodes.alt_leftAxis) && Input.GetAxisRaw(inputKeycodes.alt_leftAxis) > 0.5f)
                    inputAxis.x = -1f;
            }

            // Debug para KeyCodes verticales
            Debug.Log($"UpKeycode ({inputKeycodes.upKeycode}): {Input.GetKeyDown(inputKeycodes.upKeycode)}");
            Debug.Log($"DownKeycode ({inputKeycodes.downKeycode}): {Input.GetKeyDown(inputKeycodes.downKeycode)}");

            // Eje vertical con KeyCode
            if (Input.GetKeyDown(inputKeycodes.upKeycode))
                inputAxis.y = 1f;
            else if (Input.GetKeyDown(inputKeycodes.downKeycode))
                inputAxis.y = -1f;
            else
            {
                // Debug ejes alternativos verticales
                if (!string.IsNullOrEmpty(inputKeycodes.alt_upAxis))
                    Debug.Log($"Alt Up Axis ({inputKeycodes.alt_upAxis}): {Input.GetAxisRaw(inputKeycodes.alt_upAxis)}");
                if (!string.IsNullOrEmpty(inputKeycodes.alt_downAxis))
                    Debug.Log($"Alt Down Axis ({inputKeycodes.alt_downAxis}): {Input.GetAxisRaw(inputKeycodes.alt_downAxis)}");

                if (!string.IsNullOrEmpty(inputKeycodes.alt_upAxis) && Input.GetAxisRaw(inputKeycodes.alt_upAxis) > 0.5f)
                    inputAxis.y = 1f;
                else if (!string.IsNullOrEmpty(inputKeycodes.alt_downAxis) && Input.GetAxisRaw(inputKeycodes.alt_downAxis) > 0.5f)
                    inputAxis.y = -1f;
            }

            Debug.Log($"Final inputAxis: {inputAxis}");
            return inputAxis;
        }

    }

}