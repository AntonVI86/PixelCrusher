using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollision : MonoBehaviour
{
    [SerializeField] private GameObject _particle;

    private Rigidbody2D _rb;

    private bool _isFree;

    private int _freeElementLayerNumber = 11;
    private float _timeToDestroy = 1;

    private void Start()
    {
        _particle.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Crusher mover))
        {
            if (_isFree == false)
            {
                _rb = gameObject.AddComponent<Rigidbody2D>();
                _rb.mass = 0.1f;
                Vector3 direction = Vector3.Cross(transform.position - mover.transform.position,new Vector3(0,0,-1f));
                Application.targetFrameRate = 60;
                _rb.AddForce(direction * 0.6f, ForceMode2D.Impulse);
                    //new Vector2(Random.Range(-4,4), Random.Range(5,6));

                gameObject.layer = _freeElementLayerNumber;
                _isFree = true;
                _particle.SetActive(true);
            }
        }      

        if (collision.gameObject.TryGetComponent(out Shredder shredder))
        {
            if (_isFree == false)
            {
                _particle.SetActive(true);
                StartCoroutine(DestroyElement());
            }

            if (_isFree)
            {
                _particle.gameObject.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator DestroyElement()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        gameObject.SetActive(false);
    }
}
