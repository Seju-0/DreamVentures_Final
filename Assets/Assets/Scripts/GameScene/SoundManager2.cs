using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager2 : MonoBehaviour
{
    public static SoundManager2 Instance;

    [Header("Sound Settings")]
    public AudioClip clickClip;
    public AudioClip clickClip2;
    private AudioSource audioSource;

    private void Awake()
    {
        // Make singleton instance
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickSound()
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
    public void PlayClickSound2()
    {
        if (audioSource != null && clickClip2 != null)
        {
            audioSource.PlayOneShot(clickClip2);
        }
    }
}
