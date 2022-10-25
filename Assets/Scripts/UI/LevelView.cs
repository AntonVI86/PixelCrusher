using TMPro;
using UnityEngine;
using DG.Tweening;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;

    private float _duration = 2f;

    private void Start()
    {
        _level.DOFade(0, _duration).OnComplete(() => gameObject.SetActive(false));
    }
}
