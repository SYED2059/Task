using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonColorFlash : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Color")]
    private Color flashColor = Color.green;

    private Color originalColor;


    private float flashDuration = 0.2f;

    [Header("Image")]
    private Image buttonImage;

    private Coroutine flashCoroutine;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashColor());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        buttonImage.color = originalColor;
    }

    private IEnumerator FlashColor()
    {
        buttonImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        buttonImage.color = originalColor;
    }

}
