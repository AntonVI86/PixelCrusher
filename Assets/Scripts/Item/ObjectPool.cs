using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;

    [SerializeField] protected int Capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(List<GameObject> prefabs) 
    {
        for (int i = 0; i < Capacity; i++)
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

    protected bool TryGetItemObject(out GameObject result)
    {
        if(_pool.Count > 0)
        {
            int index = Random.Range(0, _pool.Count);
           
            result = _pool[index];
            _pool.Remove(_pool[index]);

            return result != null;
        }

        return result = null;
    }
}
