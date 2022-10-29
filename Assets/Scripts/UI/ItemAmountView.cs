using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ItemAmountView : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private TMP_Text _amountText;

    private void Awake()
    {
        _amountText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        _spawner.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _spawner.CountChanged -= OnCountChanged;
    }

    private void OnCountChanged(int value)
    {
        _amountText.text = value.ToString();
    }
}
