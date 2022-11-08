using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _playingButton;

    [SerializeField] private ReseterItems _reseter;

    public const string TotalScore = "TotalScore";
    public const string ItemAmount = "ItemAmount";
    public const string LevelName = "LevelName";
    public const string StartLevel = "Level1";

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _playingButton.onClick.AddListener(OnClickPlayingButton);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnClickRestartButton);
        _playingButton.onClick.RemoveListener(OnClickPlayingButton);
    }

    private void OnClickRestartButton()
    {
        ResetGame();
    }

    private void OnClickPlayingButton()
    {
        if(PlayerPrefs.HasKey(LevelName))
            SceneManager.LoadScene(PlayerPrefs.GetString(LevelName));
        else
            SceneManager.LoadScene(StartLevel);
    }

    private void ResetGame()
    {
        int capacity = 15;
        int score = 0;

        PlayerPrefs.SetInt(TotalScore, score);
        PlayerPrefs.SetInt(ItemAmount, capacity);
        PlayerPrefs.SetString(LevelName, StartLevel);

        _reseter.Reset();
    }
}
