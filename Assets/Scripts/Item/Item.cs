using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isUnblocked;

    public event UnityAction<Item> Destroyed;

    private float _timeToDestroy;

    public bool IsUnblocked => _isUnblocked;

    private void Start()
    {
        StartCoroutine(DestroyItem());
    }

    public void SetUnblock()
    {
        _isUnblocked = true;
    }

    public void SetBlock()
    {
        _isUnblocked = false;
    }

    private IEnumerator DestroyItem()
    {
        var delay = new WaitForSeconds(_timeToDestroy);

        while(transform.childCount > 0)
        {
            yield return delay;
        }

        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
