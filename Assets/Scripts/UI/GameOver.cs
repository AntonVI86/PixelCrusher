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

    private void OnEnable()
    {
        _progress.Reached += OnWin;
        _nextButton.onClick.AddListener(OnNextLevel);
    }

    private void OnDisable()
    {
        _progress.Reached -= OnWin;
        _nextButton.onClick.RemoveListener(OnNextLevel);
    }

    private void OnWin()
    {
        _winPanel.SetActive(true);
        _progress.GetIsCanDraw();
        Time.timeScale = 0;
    }

    private void OnLost()
    {
        _lostPanel.SetActive(true);
        _progress.GetIsCanDraw();
    }

    private void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
