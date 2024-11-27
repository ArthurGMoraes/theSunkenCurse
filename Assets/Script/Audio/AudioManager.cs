using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------AudioSource--------------")]
    [SerializeField] AudioSource musicSRC;
    [SerializeField] AudioSource bgsoundSRC;
    [SerializeField] AudioSource SFXSRC;

    [Header("--------------AudioClip--------------")]
    public AudioClip music;
    public AudioClip SFX;
    public AudioClip bgsound;


    void Start()
    {
        musicSRC.clip = music;
        musicSRC.volume = 90;
        musicSRC.Play();
        bgsoundSRC.clip = bgsound;
        bgsoundSRC.volume = 100;
        bgsoundSRC.Play();

    }
}
