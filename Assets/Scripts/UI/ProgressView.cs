using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Slider _progressBar;

    [SerializeField] private Progress _progress;

    private void Start()
    {
        _progressBar.maxValue = _progress.MaxCoins;
        _progressBar.value = _progress.CoinsCount;
    }

    private void OnEnable()
    {
        _progress.MoneyChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _progress.MoneyChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(Progress progress)
    {
        StartCoroutine(ChangeBarValue());
        _scoreText.text = $"{progress.CoinsCount} / {progress.MaxCoins}";
    }

    private IEnumerator ChangeBarValue()
    {
        float speed = 50f;

        while(_progressBar.value != _progress.CoinsCount)
        {
            _progressBar.value = Mathf.MoveTowards(_progressBar.value, _progress.CoinsCount, speed * Time.deltaTime);
            yield return null;
        }
    }
}
