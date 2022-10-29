using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseterItems : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

    private const string _pathName = "Items";

    private void Awake()
    {
        GetAllItems();
    }

    private void GetAllItems()
    {
        Object[] objects = Resources.LoadAll(_pathName, typeof(Item));

        foreach (var element in objects)
        {
            Item item = (Item)element;
            _items.Add(item);
        }
    }

    public void Reset()
    {
        foreach(var item in _items)
        {
            item.SetBlock();
        }
    }
}
