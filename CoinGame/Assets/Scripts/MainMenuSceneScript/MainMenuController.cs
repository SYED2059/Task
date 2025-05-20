using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button playGameButton;

    [SerializeField] private Button exitGameButton;

    public AudioManager AudioManager;

    void OnEnable()
    {
        playGameButton.onClick.AddListener(GameSceneLoadFN);
        exitGameButton.onClick.AddListener(ExitGameFN);
    }

    void GameSceneLoadFN()
    {
        AudioManager.PlayButtonClickSound();
        StartCoroutine(LoadSceneAfterDelay(0.2f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.Data.LoadSceneAsync("GameScene");
    }

    void ExitGameFN()
    {
        AudioManager.PlayButtonClickSound();
        Application.Quit();
    }

    void OnDisable()
    {
        playGameButton.onClick.RemoveListener(GameSceneLoadFN);
        exitGameButton.onClick.RemoveListener(ExitGameFN);
    }
}
