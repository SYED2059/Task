using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("ObjectPool")]
    public ObjectPool ObjectPool;

    [Header("GameObject")]
    [SerializeField] private GameObject endGamePanel;

    [Header("Button")]
    [SerializeField] private Button restartButton;

    [SerializeField] private Button mainMenuButton;

    [Header("int")]
    [SerializeField] private int score = 0;

    [Header("float")]
    [SerializeField] private float gameDuration = 30f;
    [SerializeField] private float timer;

    private Coroutine spawnRoutine;

    public static GameManager Data;

    [Header("AudioManager")]
    public AudioManager AudioManager;

    private void Awake()
    {
        Data = this;
    }

    void OnEnable()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(BackToMenu);
    }

    void Start()
    {
        timer = gameDuration;
        score = 0;
        UpdateScoreText();
        UpdateTimerText();

        endGamePanel.SetActive(false);
        ObjectPool.OnCoinSpawned += SetupCoin;

        spawnRoutine = StartCoroutine(SpawnCoins());

        StartCoroutine(GameTimer());
    }

    void SetupCoin(GameObject coinObj)
    {
        Coin coin = coinObj.GetComponent<Coin>();
        if (coin != null)
        {
            coin.OnCollected = () =>
            {
                score++;
                UpdateScoreText();
            };
        }
    }

    public IEnumerator SpawnCoins()
    {
        while (true)
        {
            if (!PauseManager.isPaused)
            {
                float waitTime = Random.Range(0.5f, 1f);
                yield return new WaitForSeconds(waitTime);

                Vector2 spawnPos = GetRandomPosition();
                ObjectPool.SpawnCoin(spawnPos);
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float minX = bottomLeft.x + 0.5f;
        float maxX = topRight.x - 0.5f;
        float minY = bottomLeft.y + 0.5f;
        float maxY = topRight.y - 0.5f;

        Vector2 spawnPos;
        int maxAttempts = 20;

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            spawnPos = new Vector2(x, y);

            if (IsPositionFree(spawnPos, 1))
                return spawnPos;
        }

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    bool IsPositionFree(Vector2 position, float minDistance)
    {
        foreach (Transform coin in ObjectPool.transform)
        {
            if (coin.gameObject.activeInHierarchy)
            {
                float dist = Vector2.Distance(position, coin.position);
                if (dist < minDistance)
                    return false;
            }
        }
        return true;
    }

    IEnumerator GameTimer()
    {
        while (timer > 0)
        {
            if (!PauseManager.isPaused)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();
            }
            yield return null;
        }

        EndGame();
    }

    void UpdateScoreText()
    {
        UIManager.Data.UpdateScoreFN(score);
    }

    void UpdateTimerText()
    {
        UIManager.Data.UpdateTimerFN(timer);
    }

    void EndGame()
    {
        StopCoroutine(spawnRoutine);
        foreach (Transform coin in ObjectPool.transform)
        {
            coin.gameObject.SetActive(false);
        }

        endGamePanel.SetActive(true);
        UIManager.Data.finalScoreText.text = $"Final Score: {score}";
    }

    public void RestartGame()
    {
        AudioManager.PlayButtonClickSound();
        StartCoroutine(ReloadSceneAsyncAfterDelay(0.2f));
    }

    IEnumerator ReloadSceneAsyncAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadingScreen.Instance.Show(() =>
        {
            SceneLoader.Data.ReloadSceneAsync();
        });
    }

    public void BackToMenu()
    {
        AudioManager.PlayButtonClickSound();
        StartCoroutine(LoadSceneAsyncAfterDelay(0.2f));
    }

    IEnumerator LoadSceneAsyncAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.Data.LoadSceneAsync("MainMenuScene");
    }

    void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartGame);
        mainMenuButton.onClick.RemoveListener(BackToMenu);
    }
}
