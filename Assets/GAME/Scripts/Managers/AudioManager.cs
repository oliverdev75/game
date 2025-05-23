using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioClipData> audioClips;
    private Dictionary<string, AudioClipData> clipDict;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        clipDict = new Dictionary<string, AudioClipData>();

        foreach (var clipData in audioClips)
        {
            if (!clipDict.ContainsKey(clipData.clipName))
                clipDict.Add(clipData.clipName, clipData);
        }
    }

    public void PlaySound(string clipName)
    {
        if (clipDict.TryGetValue(clipName, out var data))
        {
            audioSource.clip = data.clip;
            audioSource.volume = data.volume;
            audioSource.loop = data.loop;
            audioSource.pitch = 1f + Random.Range(-data.pitchVariation, data.pitchVariation);
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioManager: The clip with name '{clipName}' was not found.");
        }
    }

    public void PlaySound(AudioClipData data)
    {
        audioSource.clip = data.clip;
        audioSource.volume = data.volume;
        audioSource.loop = data.loop;
        audioSource.pitch = 1f + Random.Range(-data.pitchVariation, data.pitchVariation);
        audioSource.Play();
    }

    public void PlayOneShot(string clipName)
    {
        if (clipDict.TryGetValue(clipName, out var data))
        {
            audioSource.PlayOneShot(data.clip, data.volume);
        }
        else
        {
            Debug.LogWarning($"AudioManager: The clip with name '{clipName}' was not found.");
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
