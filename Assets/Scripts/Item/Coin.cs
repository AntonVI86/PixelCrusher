using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _price;

    public event UnityAction<Coin> Destroyed;

    private float _lifeTime = 3;
    private Coroutine _coroutine;

    public int Price => _price;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(DestroyCoin(gameObject));
    }

    private void OnDisable()
    {
        Destroyed?.Invoke(this);
        StopCoroutine(_coroutine);
    }

    private IEnumerator DestroyCoin(GameObject coin)
    {
        float time = 0;

        while (time < _lifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
