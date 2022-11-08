using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DrawingLine : MonoBehaviour
{
    [SerializeField] private AudioClip _crush;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrushSound()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.PlayOneShot(_crush);
    }
}
