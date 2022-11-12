using UnityEngine;
using DG.Tweening;

public class MovingRotator : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Start()
    {
        transform.DOMove(_target.position, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
