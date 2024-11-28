using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public static AudioManager instance;

    [Header("------------- Audio Source -------------")]
    [SerializeField] AudioSource AmbienceSource;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------- Audio Clip -------------")]

    public AudioClip ambienceClip;
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip healthClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        ChangeVolume();
    }

    void Start()
    {
        ChangeVolume();

        AmbienceSource.clip = ambienceClip;
        AmbienceSource.Play();

        MusicSource.clip = musicClip;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

}
