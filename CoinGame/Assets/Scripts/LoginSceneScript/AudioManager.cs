using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource sfxSource;

    [Header("AudioClip")]
    [SerializeField] private AudioClip coinCollectClip;

    [SerializeField] private AudioClip ButtonClickClip;


    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    public List<SceneMusic> sceneMusicList;

    private Dictionary<string, AudioClip> musicMap;

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

        musicMap = new Dictionary<string, AudioClip>();

        foreach (var item in sceneMusicList)
        {
            musicMap[item.sceneName] = item.musicClip;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (musicMap.TryGetValue(scene.name, out AudioClip clip))
        {
            PlayMusic(clip);
        }
        else
        {
            Debug.LogWarning("No music assigned for scene: " + scene.name);
            musicSource.Stop();
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayCoinCollectSound()
    {
        if (sfxSource != null && coinCollectClip != null)
        {
            sfxSource.PlayOneShot(coinCollectClip);
        }
    }

    public void PlayButtonClickSound()
    {
        if (sfxSource != null && ButtonClickClip != null)
        {
            sfxSource.PlayOneShot(ButtonClickClip);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}