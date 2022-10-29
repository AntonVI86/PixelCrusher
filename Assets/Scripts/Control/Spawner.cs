using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Spawner : ObjectPool
{
    [SerializeField] private List<GameObject> _globalCollection;
    [SerializeField] private float _timeBetweenSpawn;

    private List<GameObject> _objects = new List<GameObject>();

    public event UnityAction Losted;
    public event UnityAction<int> CountChanged;

    private int _itemCount;

    private Coroutine _coroutine;

    private void Awake()
    {
        for (int i = 0; i < _globalCollection.Count; i++)
        {
            if (_globalCollection[i].GetComponent<Item>().IsUnblocked)
            {
                _objects.Add(_globalCollection[i]);
            }
        }

        _capacity = 15;

        if(_capacity < PlayerPrefs.GetInt("itemAmount"))
        {
            _capacity = PlayerPrefs.GetInt("itemAmount");
        }
    }

    private void OnDisable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Start()
    {
        CountChanged?.Invoke(_itemCount);
        Initialize(_objects);
        AddChilds();

        _itemCount = _capacity;

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
                _itemCount--;
                CountChanged?.Invoke(_itemCount);
            }

            yield return new WaitForSeconds(_timeBetweenSpawn);

            if(_itemCount <= 0)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    private IEnumerator EndGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);

            Losted?.Invoke();
        }
    }

    public void AddNewItem(GameObject item)
    {
        item.GetComponent<Item>().SetUnblock();
        PlayerPrefs.SetInt("itemAmount", _capacity + 1);
    }

    public void ExceptItems(List<CardView> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < _objects.Count; j++)
            {
                if(list[i].Item == _objects[j])
                {
                    list.Remove(list[i]);
                } 
            }
        }
    }
}
