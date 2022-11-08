using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shredder : ObjectPool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private List<GameObject> _coinPrefab;

    [SerializeField] private Progress _progress;

    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _crushSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Initialize(_coinPrefab);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetObject(out GameObject coin))
        {
            if (collision.gameObject.TryGetComponent(out ElementCollision element))
            {
                int index = Random.Range(0, _spawnPoints.Length);

                _audioSource.PlayOneShot(_coinSound);
                coin.GetComponent<Coin>().Destroyed += OnCoinDestroyed;
                SetCoin(coin, new Vector2(Random.Range(_spawnPoints[0].position.x, _spawnPoints[_spawnPoints.Length - 1].position.x), _spawnPoints[0].position.y));
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
        coin.Destroyed -= OnCoinDestroyed;

        _progress.AddCoins(coin);
    }
}
