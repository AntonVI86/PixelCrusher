using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] protected int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab) 
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected void Initialize(List<GameObject> prefabs) 
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Count);
            GameObject spawned = Instantiate(prefabs[randomIndex], _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected void AddChilds()
    {
        foreach (var item in _pool)
        {
            item.GetComponent<PicturesCreator>().GenerateGrid(transform.position.x, transform.position.y);
        }
    }

    protected bool TryGetObject(out GameObject result) 
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
