using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject _item;

    [SerializeField] private Button _acceptButton;

    public event UnityAction<CardView> Selected;

    public GameObject Item => _item;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Selected?.Invoke(this);
        gameObject.SetActive(false);
    }
}
