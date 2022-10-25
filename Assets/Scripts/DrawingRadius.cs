using UnityEngine;
using DG.Tweening;

public class DrawingRadius : MonoBehaviour
{
    [SerializeField] private Rotator _rotator;
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private Vector2 _maxRadius;

    private float _duration = 0.5f;

    private void Start()
    {
        transform.DOScale(_maxRadius, _duration).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetEase(Ease.Linear);
    }
}
