using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _objects;

    private int _objectCount = 0;

    private void Start()
    {
        Initialize(_objects);

        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while(true)
        {
            if(TryGetObject(out GameObject item))
            {
                item.SetActive(true);
                item.transform.position = transform.position;
                //item.GetComponent<PicturesCreator>().GenerateGrid(transform.position.x, transform.position.y);
                _objectCount++;
            }

            yield return new WaitForSeconds(5);
        }
    }
}
