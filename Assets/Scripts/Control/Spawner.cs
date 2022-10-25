using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private float _timeBetweenSpawn;

    private Coroutine _coroutine;

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private void Start()
    {
        Initialize(_objects);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while(true)
        {
            if(TryGetObject(out GameObject item))
            {
                item.SetActive(true);
                item.transform.position = transform.position;
            }

            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
}
