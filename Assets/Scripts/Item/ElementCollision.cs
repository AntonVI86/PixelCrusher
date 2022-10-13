using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private bool _isFree;
    private float _time = 3;

    private void OnEnable()
    {
        _isFree = false;
    }
    private void Update()
    {
        if(_time > 0)
        {
            _time -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Mover mover))
        {
            if (_isFree == false)
            {
                gameObject.AddComponent<Rigidbody2D>();
                _isFree = true;
                _particle.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Shredder shredder))
        {
            if (_isFree)
            {
                gameObject.SetActive(false);
            }

            if (_time <= 0)
            {
                _isFree = true;
            }
        }
    }
}
