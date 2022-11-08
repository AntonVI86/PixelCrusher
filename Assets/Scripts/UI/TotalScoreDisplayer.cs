using TMPro;
using UnityEngine;
using Agava.YandexGames;

[RequireComponent(typeof(TMP_Text))]
public class TotalScoreDisplayer : MonoBehaviour
{
    [SerializeField] private ScoreSaver _scoreSaver;

    public const string TotalScore = "TotalScore";
    public const string LeaderBoard = "PixelCrushBoard";

    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }
    private void Start()
    {       
        _scoreText.text = _scoreSaver.GetScore().ToString();
        Leaderboard.GetPlayerEntry(LeaderBoard, null);
        Leaderboard.SetScore(LeaderBoard, PlayerPrefs.GetInt(TotalScore));
    }
}
