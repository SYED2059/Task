using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ObjectPool ObjectPool;

    //[SerializeField] private TextMeshProUGUI scoreText;

    //[SerializeField] public TextMeshProUGUI timerText;

    [SerializeField] private GameObject endGamePanel;

    //[SerializeField] private TextMeshProUGUI finalScoreText;


    [SerializeField] private Button restartButton;

    [SerializeField] private Button mainMenuButton;

    [SerializeField] private int score = 0;


    [SerializeField] private float gameDuration = 30f;
    [SerializeField] private float timer;

    private Coroutine spawnRoutine;

    public static GameManager Data;

    private void Awake()
    {
        Data = this;
    }

    void Start()
    {
        timer = gameDuration;
        score = 0;
        UpdateScoreText();
        UpdateTimerText();

        endGamePanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(BackToMenu);

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

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            float waitTime = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(waitTime);

            Vector2 spawnPos = GetRandomPosition();
            ObjectPool.SpawnCoin(spawnPos);
        }
    }

    Vector2 GetRandomPosition()
    {
        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float minX = bottomLeft.x + 0.5f; // padding
        float maxX = topRight.x - 0.5f;
        float minY = bottomLeft.y + 0.5f;
        float maxY = topRight.y - 0.5f;

        Vector2 spawnPos;
        int maxAttempts = 20; // prevent infinite loop

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            spawnPos = new Vector2(x, y);

            if (IsPositionFree(spawnPos, 1)) // 1.0f is minimum distance between coins
                return spawnPos;
        }

        // If no free spot found, just return a random pos anyway
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
            timer -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        EndGame();
    }

    void UpdateScoreText()
    {
        //scoreText.text = $"Score: {score}";
        UIManager.Data.UpdateScoreFN(score);
    }

    void UpdateTimerText()
    {
        //timerText.text = $"Time: {Mathf.Ceil(timer)}";
        UIManager.Data.UpdateTimerFN(timer);
    }

    void EndGame()
    {
        StopCoroutine(spawnRoutine);

        // Disable all coins
        foreach (Transform coin in ObjectPool.transform)
        {
            coin.gameObject.SetActive(false);
        }

        endGamePanel.SetActive(true);
        UIManager.Data.finalScoreText.text = $"Final Score: {score}";
    }

    public void RestartGame()
    {
        SceneLoader.Data.ReloadSceneAsync();
    }

    public void BackToMenu()
    {
        SceneLoader.Data.LoadSceneAsync("MainMenuScene");
    }
}
