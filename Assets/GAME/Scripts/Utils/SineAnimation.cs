using UnityEngine;

public class SineAnimation : MonoBehaviour
{
    public float frequency;
    public float amplitde;
    public Vector3 direction = Vector3.up;

    private Vector3 Ipos = Vector2.zero;

    private void Awake()
    {
        Ipos = transform.position;
    }
    void Update()
    {
        transform.position = Ipos + direction * Mathf.Sin(Time.time * frequency) * amplitde;
    }
}
