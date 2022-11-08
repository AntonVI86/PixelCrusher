using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class CardView : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private ParticleSystem _lineRotator;

    [SerializeField] private Button _acceptButton;
    [SerializeField] private TMP_Text _name;

    public const string IncreaseSize = "Increase";

    public event UnityAction<CardView> Selected;

    private Animator _animator;
    private AudioSource _audioSource;

    public Item Item => _item;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _lineRotator.Stop();
    }

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(OnButtonClick);
    }

    public void AnimateCard()
    {
        _animator.SetTrigger(IncreaseSize);
        _audioSource.Play();
        _lineRotator.Play();
    }

    private void OnButtonClick()
    {
        Selected?.Invoke(this);
    }
}
