using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
        if (instance != null)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        AmbienceSource.clip = ambienceClip;
        AmbienceSource.Play();

        MusicSource.clip = musicClip;
        MusicSource.Play();


        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
