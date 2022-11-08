using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button _resetGameButton;

    private void OnEnable()
    {
        _resetGameButton.onClick.AddListener(OnClickStartButton);
    }

    private void OnDisable()
    {
        _resetGameButton.onClick.RemoveListener(OnClickStartButton);
    }

    private void OnClickStartButton()
    {
        SceneManager.LoadScene(0);
    }
}
