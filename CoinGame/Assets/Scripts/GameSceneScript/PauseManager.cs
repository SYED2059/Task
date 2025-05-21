using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static  bool isPaused = false;

    [Header("GameObject")]
    [SerializeField]private GameObject pauseMenuUI;

    [SerializeField] private GameObject objectPoolObject;

    public void PauseGame()
    {
        isPaused = true;
        objectPoolObject.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        objectPoolObject.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

}
