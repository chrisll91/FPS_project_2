using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    
    [SerializeField] AudioSource backgroundMusic;

    [SerializeField] AudioClip musicClip;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (musicClip != null)
        {
            PlayMusic(musicClip);
        }
    }

    private void PlayMusic(AudioClip musicClip)
    {
        backgroundMusic.clip = musicClip;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    
}
