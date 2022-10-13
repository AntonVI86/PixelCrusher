using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : ObjectPool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _coinPrefab;

    [SerializeField] private Progress _progress;

    private void Start()
    {
        Initialize(_coinPrefab);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ElementCollision element))
        {
            if (TryGetObject(out GameObject coin))
            {
                coin.GetComponent<Coin>().Destroyed += OnCoinDestroyed;
                int index = Random.Range(0, _spawnPoints.Length);
                SetCoin(coin, _spawnPoints[index].position);
            }
        }
    }

    private void SetCoin(GameObject coin, Vector3 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
    }

    private void OnCoinDestroyed(Coin coin)
    {
        coin.GetComponent<Coin>().Destroyed -= OnCoinDestroyed;

        _progress.AddCoins(coin);
    }
}
