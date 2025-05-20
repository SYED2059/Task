using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button playGameButton;

    [SerializeField] private Button exitGameButton;

    void OnEnable()
    {
        playGameButton.onClick.AddListener(GameSceneLoadFN);
        exitGameButton.onClick.AddListener(ExitGameFN);
    }

    void OnDisable()
    {
        playGameButton.onClick.RemoveListener(GameSceneLoadFN);
        exitGameButton.onClick.RemoveListener(ExitGameFN);
    }

    void GameSceneLoadFN()
    {
        SceneLoader.Data.LoadSceneAsync("GameScene");
    }

    void ExitGameFN()
    {
        Application.Quit();
    }
}
