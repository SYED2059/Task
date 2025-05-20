using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]private AudioSource musicSource;

    [SerializeField] private  AudioSource sfxSource;


    [SerializeField] private AudioClip mainMenuMusic;

    [SerializeField] private AudioClip gameMusic;

    [SerializeField] private AudioClip buttonClickSound;

    public Action OnPlayButtonClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PlayMainMenuMusic();
        OnPlayButtonClick += PlayButtonClick;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
        {
            PlayMainMenuMusic();
        }
        else
        {
            PlayGameMusic();
        }
    }

    public void PlayMainMenuMusic()
    {
        if (musicSource.clip != mainMenuMusic)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }
}
