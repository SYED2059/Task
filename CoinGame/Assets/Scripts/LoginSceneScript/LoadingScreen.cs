using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    [Header("UI Elements")]
    public GameObject loadingScreenRoot;
    [SerializeField] private Image fadePanel;
    [SerializeField] private Slider loadingBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            loadingScreenRoot.SetActive(false);
            SetFadeAlpha(0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(System.Action onComplete = null)
    {
        StartCoroutine(FadeLoading(onComplete));
    }

    private IEnumerator FadeLoading(System.Action onComplete)
    {
        loadingScreenRoot.SetActive(true);
        loadingBar.value = 0f;

        // Fade In
        yield return StartCoroutine(Fade(0f, 1f, 0.3f));

        // Fake Loading Bar
        float duration = Random.Range(1f, 2f);
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            loadingBar.value = progress;
            yield return null;
        }

        loadingBar.value = 1f;

        // Fade Out
        yield return StartCoroutine(Fade(1f, 0f, 0.3f));

        loadingScreenRoot.SetActive(false);
        onComplete?.Invoke();
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / duration);
            SetFadeAlpha(alpha);
            yield return null;
        }
        SetFadeAlpha(to);
    }

    private void SetFadeAlpha(float alpha)
    {
        var color = fadePanel.color;
        color.a = alpha;
        fadePanel.color = color;
    }
}
