using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour
{
    public System.Action OnCollected;

    [Header("SpriteRenderer")]
    private SpriteRenderer ThisSpriteRenderer;

    void Awake()
    {
        ThisSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Collect()
    {
        Debug.Log("CheckCoinCollect");
        StartCoroutine(PlayCollectEffect());
    }

    //private IEnumerator PlayCollectEffect()
    //{
    //    ThisSpriteRenderer.color = Color.red;
    //    AudioManager.Instance.PlayCoinCollectSound();
    //    yield return new WaitForSeconds(0.1f);
    //    OnCollected?.Invoke();
    //    gameObject.SetActive(false);
    //    ThisSpriteRenderer.color = Color.white;
    //}

    private IEnumerator PlayCollectEffect()
    {
        ThisSpriteRenderer.color = Color.gray;

        AudioManager.Instance.PlayCoinCollectSound();

        float duration = 0.010f;
        float elapsed = 0f;
        Color startColor = ThisSpriteRenderer.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Color fadedColor = startColor;
            fadedColor.a = Mathf.Lerp(1f, 0f, t);
            ThisSpriteRenderer.color = fadedColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Color finalColor = startColor;
        finalColor.a = 0f;
        ThisSpriteRenderer.color = finalColor;
        OnCollected?.Invoke();
        gameObject.SetActive(false);
        ThisSpriteRenderer.color = Color.white;
    }

}
