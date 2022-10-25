using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    private const string MainMenu = "MainMenu";

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnClickExitButton);
        _restartButton.onClick.AddListener(OnClickRestartButton);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnClickExitButton);
        _restartButton.onClick.RemoveListener(OnClickRestartButton);
    }

    private void OnClickExitButton()
    {
        SceneManager.LoadScene(MainMenu);
    }

    private void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
