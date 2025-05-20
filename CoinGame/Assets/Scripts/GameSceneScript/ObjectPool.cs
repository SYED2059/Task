using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject coinPrefab;

    public int poolSize = 20;

    private List<GameObject> pool = new List<GameObject>();

    public UnityAction<GameObject> OnCoinSpawned;

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(coinPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetCoin()
    {
        foreach (var coin in pool)
        {
            if (!coin.activeInHierarchy)
                return coin;
        }
        GameObject obj = Instantiate(coinPrefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public void SpawnCoin(Vector2 position)
    {
        GameObject coin = GetCoin();
        coin.transform.position = position;
        coin.SetActive(true);
        OnCoinSpawned?.Invoke(coin);
    }
}
