using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("------------- Audio Source -------------")]
    [SerializeField] AudioSource AmbienceSource;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------- Audio Clip -------------")]

    public AudioClip ambienceClip;
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip healthClip;
    void Start()
    {
        AmbienceSource.clip = ambienceClip;
        AmbienceSource.Play();

        MusicSource.clip = musicClip;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
