using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    private Button _exit;

    private void Awake()
    {
        _exit = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exit.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _exit.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
