using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Crusher : MonoBehaviour
{
    [SerializeField] private AudioClip _crush;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ElementCollision element))
        {
            if (_audioSource.isPlaying == false)
                _audioSource.PlayOneShot(_crush);
        }
    }
}
