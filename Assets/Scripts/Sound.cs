using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Sound : MonoBehaviour
{
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Sprite _soundOn;

    public const string SoundOn = "SoundOn";

    private Image _soundImage;
    private Button _soundButton;
    private bool _isMute;

    private void Awake()
    {
        _soundImage = GetComponent<Image>();
        _soundButton = GetComponent<Button>();       
    }

    private void OnEnable()
    {
        _isMute = PlayerPrefs.GetInt(SoundOn) == 1 ? true : false;

        MuteSound();
        _soundButton.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _isMute = !_isMute;
        PlayerPrefs.SetInt(SoundOn, _isMute ? 1 : 0);

        MuteSound();
    }

    private void MuteSound()
    {
        if (_isMute)
        {
            _soundImage.sprite = _soundOn;
            AudioListener.pause = false;
        }
        else
        {
            _soundImage.sprite = _soundOff;
            AudioListener.pause = true;
        }
    }
}
