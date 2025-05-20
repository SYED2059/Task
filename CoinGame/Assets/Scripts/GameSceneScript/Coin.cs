using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour
{
    public System.Action OnCollected;

    public void Collect()
    {
        Debug.Log("CheckThisPlace");
        gameObject.SetActive(false);
        OnCollected?.Invoke();
        AudioManager.Instance.PlayCoinCollectSound();
    }

}
