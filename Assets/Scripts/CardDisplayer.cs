using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private List<CardView> _collection = new List<CardView>();
    [SerializeField] private Spawner _spawner;

    private List<CardView> _tempList = new List<CardView>();

    private void Start()
    {
        _spawner.ExceptItems(_collection);
        SelectItems();
    }

    public void SelectItems()
    {
        for (int i = 0; i < _collection.Count; i++)
        {
            _tempList.Add(_collection[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, _tempList.Count);

            CardView newCard = Instantiate(_tempList[index], transform);
            _tempList.Remove(_tempList[index]);
            
            newCard.Selected += OnSelected;
        }
    }

    private void OnSelected(CardView card)
    {
        _spawner.AddNewItem(card.Item);

        for (int i = 0; i < _collection.Count; i++)
        {
            if(_collection[i].Item == card.Item)
            {
                _collection.Remove(_collection[i]);
            }
        }

        card.Selected -= OnSelected;
        gameObject.SetActive(false);
    }
}
