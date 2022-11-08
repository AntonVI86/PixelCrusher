using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] private Progress _progress;

    public const string TotalScore = "TotalScore";

    public int GetScore()
    {
        int temp = PlayerPrefs.GetInt(TotalScore);       

        temp += _progress.CoinsCount;
        PlayerPrefs.SetInt(TotalScore, temp);

        return temp;
    }
}
