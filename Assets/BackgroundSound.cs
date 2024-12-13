using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}
