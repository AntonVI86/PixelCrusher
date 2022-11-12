using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isUnblocked = false;

    public bool IsUnblocked => _isUnblocked;

    public void SetUnblock()
    {
        _isUnblocked = true;
    }

    public void SetBlock()
    {
        _isUnblocked = false;
    }
}
