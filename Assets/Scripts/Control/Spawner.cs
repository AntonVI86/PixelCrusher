using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private List<GameObject> _globalCollection;
    [SerializeField] private float _timeBetweenSpawn;

    private List<GameObject> _objects = new List<GameObject>();

    public const string ItemAmount = "ItemAmount";

    public event UnityAction Losted;
    public event UnityAction<int> CountChanged;

    private int _itemCount;
    private int _itemInWave = 3;
    private float _timeToLoose = 15f;

    private Coroutine _coroutine;

    private void Awake()
    {
        AddObjectToSpawner();
        GetCapacity();
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

        _itemCount = Capacity;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        var delay = new WaitForSeconds(_timeBetweenSpawn);

        while(true)
        {
            if (_itemInWave > 0)
            {
                if (TryGetItemObject(out GameObject item))
                {
                    item.GetComponent<Item>().Destroyed += OnItemDestroyed;

                    item.SetActive(true);
                    item.transform.position = transform.position;

                    _itemCount--;
                    _itemInWave--;

                    CountChanged?.Invoke(_itemCount);
                }
            }

            yield return delay;

            if(_itemCount <= 0)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    private IEnumerator EndGame()
    {
        var delay = new WaitForSeconds(_timeToLoose);

        while (true)
        {
            yield return delay;

            Losted?.Invoke();
        }
    }

    public void AddNewItemToNextLevel(GameObject item)
    {
        item.GetComponent<Item>().SetUnblock();
        PlayerPrefs.SetInt(ItemAmount, Capacity + 1);
    }

    public void OnItemDestroyed(Item item)
    {
        _itemInWave++;

        item.GetComponent<Item>().Destroyed -= OnItemDestroyed;
    }

    private void AddObjectToSpawner()
    {
        for (int i = 0; i < _globalCollection.Count; i++)
        {
            if (_globalCollection[i].GetComponent<Item>().IsUnblocked)
            {
                _objects.Add(_globalCollection[i]);
            }
        }
    }

    private int GetCapacity()
    {
        Capacity = 15;

        if (Capacity < PlayerPrefs.GetInt(ItemAmount))
        {
            Capacity = PlayerPrefs.GetInt(ItemAmount);
        }

        return Capacity;
    }
}
