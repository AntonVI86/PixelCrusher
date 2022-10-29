using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaver : MonoBehaviour
{
    public const string LevelName = "LevelName";
    private void Awake()
    {
        PlayerPrefs.SetString(LevelName, SceneManager.GetActiveScene().name);
    }
}
