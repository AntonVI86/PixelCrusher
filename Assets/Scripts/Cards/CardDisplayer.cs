using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private List<CardView> _collection = new List<CardView>();

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _pointOfIncreasePosition;
    [SerializeField] private GameObject _nextButton;

    private List<CardView> _tempList = new List<CardView>();

    private void OnEnable()
    {
        //_spawner.ExceptItems(_collection);
    }

    private void Start()
    {
        _nextButton.SetActive(false);

        ReceiveRandomItemsToChoice();
    }

    public void ReceiveRandomItemsToChoice()
    {
        int cardsOnScreen = 3;

        AddItemsToTempList();

        if (_tempList.Count < cardsOnScreen)
        {
            cardsOnScreen = _tempList.Count;
        }

        for (int i = 0; i < cardsOnScreen; i++)
        {
            int index = Random.Range(0, _tempList.Count);

            CardView newCard = Instantiate(_tempList[index], transform);
            _tempList.Remove(_tempList[index]);
            
            newCard.Selected += OnSelected;
        }
    }

    private void AddItemsToTempList()
    {
        for (int i = 0; i < _collection.Count; i++)
        {
            _tempList.Add(_collection[i]);
        }
    }

    private void OnSelected(CardView card)
    {
        _spawner.AddNewItemToNextLevel(card.Item.gameObject);

        RemoveItemFromCollection(card);

        card.Selected -= OnSelected;

        AnimateCard(card);

        transform.parent.gameObject.SetActive(false);
        _nextButton.SetActive(true);
    }

    private void RemoveItemFromCollection(CardView card)
    {
        for (int i = 0; i < _collection.Count; i++)
        {
            if (_collection[i].Item == card.Item)
            {
                _collection.Remove(_collection[i]);
            }
        }
    }

    private void AnimateCard(CardView card)
    {
        card.gameObject.transform.SetParent(_pointOfIncreasePosition);
        card.GetComponent<RectTransform>().DOMove(_pointOfIncreasePosition.position, 1f).SetUpdate(true);
        card.AnimateCard();
    }
}
