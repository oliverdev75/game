using System;
using UnityEngine;

public class CharacterLegsAnimation : MonoBehaviour
{
    [SerializeField] Transform rLeg;
    [SerializeField] Transform lLeg;
    [SerializeField]
    [Range(0f, 45f)]
    float legMoveAmount = 25f;
    [Range(1f, 10f)]
    [SerializeField] float cycleSpeed = 0.5f;

    Rigidbody2D rb;
    float animTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float speedMag = Mathf.Clamp(rb.linearVelocity.x,-1,1);
        if (Mathf.Abs(speedMag) > 0)
            animTime += Time.deltaTime * speedMag * (legMoveAmount * cycleSpeed);
        else
            animTime = 0f;      // Reset leg animation

        float sinAngle = Mathf.Sin(animTime) * legMoveAmount;
        float cosAngle = Mathf.Cos(animTime) * legMoveAmount;

        rLeg.localRotation = Quaternion.Euler(0f, 0f, sinAngle);
        lLeg.localRotation = Quaternion.Euler(0f, 0f, cosAngle);
    }
}
