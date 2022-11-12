using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _authorsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private ReseterItems _reseter;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
        _continueButton.onClick.AddListener(OnClickContinueButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnClickStartButton);
        _continueButton.onClick.RemoveListener(OnClickContinueButton);
        _exitButton.onClick.RemoveListener(OnClickExitButton);
    }

    private void OnClickStartButton()
    {
        //ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnClickContinueButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LevelName"));
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }


    private void ResetGame()
    {
        int capacity = 15;
        PlayerPrefs.SetInt("itemAmount", capacity);
        PlayerPrefs.SetString("LevelName", "Level1");
        _reseter.Reset();
    }
}
