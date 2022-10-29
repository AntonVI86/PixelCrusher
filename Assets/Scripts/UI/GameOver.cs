using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _lostPanel;
    [SerializeField] private Progress _progress;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private CardDisplayer _cardDisplayer;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _progress.Reached += OnWin;
        _spawner.Losted += OnLost;
        _nextButton.onClick.AddListener(OnNextLevel);
        _restartButton.onClick.AddListener(OnRestartLevel);
    }

    private void OnDisable()
    {
        _progress.Reached -= OnWin;
        _spawner.Losted -= OnLost;
        _nextButton.onClick.RemoveListener(OnNextLevel);
        _restartButton.onClick.RemoveListener(OnRestartLevel);
    }

    private void OnWin()
    {
        _winPanel.SetActive(true);
        _progress.GetIsCanDraw();
        Time.timeScale = 0;
        _cardDisplayer.gameObject.SetActive(true);
    }

    private void OnLost()
    {
        _lostPanel.SetActive(true);
        _progress.GetIsCanDraw();
        Time.timeScale = 0;
    }

    private void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
