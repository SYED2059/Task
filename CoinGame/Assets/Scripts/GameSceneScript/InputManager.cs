using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DetectCoinTap(touch.position);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DetectCoinTap(Input.mousePosition);
        }
    }

    void DetectCoinTap(Vector2 screenPosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            Coin coin = hit.collider.GetComponent<Coin>();
            if (coin != null && coin.gameObject.activeInHierarchy)
            {
                Debug.Log("COIN");
                coin.Collect();
            }
        }
    }
}
