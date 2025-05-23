using UnityEditor.U2D.Aseprite;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Scriptable Objects/AudioClipData")]
public class AudioClipData : ScriptableObject
{

    public string clipName;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = false;

    

}
