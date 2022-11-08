using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class NextLevel : MonoBehaviour
{
    private Button _nextLevel;

    private void Awake()
    {
        _nextLevel = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _nextLevel.onClick.AddListener(OnClickNextLevelButton);
    }

    private void OnDisable()
    {
        _nextLevel.onClick.RemoveListener(OnClickNextLevelButton);
    }

    private void OnClickNextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
