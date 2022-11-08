using System.Collections;
using UnityEngine;

public class ElementCollision : MonoBehaviour
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private SpriteRenderer _renderer;

    private Rigidbody2D _rigidbody;

    private bool _hasRigidbody;

    private int _freeElementLayerNumber = 11;
    private float _timeToDestroy = 1;

    private void Start()
    {
        _particle.SetActive(false);
    }

    public void GetColor(Texture2D picture, int xPosition, int yPosition)
    {
        _renderer.color = picture.GetPixel(xPosition, yPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasRigidbody == false)
        {
            if (collision.gameObject.TryGetComponent(out DrawingLine line))
            {
                AddRigidbody(line);
            }
        }      

        if (_hasRigidbody == false)
        {
            if (collision.gameObject.TryGetComponent(out Shredder shredder))
            {
                _particle.SetActive(true);
                StartCoroutine(DestroyElement());
            }
        }
        if (_hasRigidbody)
        {
            if (collision.gameObject.TryGetComponent(out Shredder shredder))
            {
                _particle.gameObject.SetActive(true);

                Destroy(gameObject);
            }
        }
    }
    private IEnumerator DestroyElement()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(gameObject);
    }

    private void AddRigidbody(DrawingLine line)
    {
        _rigidbody = gameObject.AddComponent<Rigidbody2D>();

        _rigidbody.mass = 0.1f;
        Vector3 direction = Vector3.Cross(transform.position - line.transform.position, new Vector3(0, 0, -1f));

        _rigidbody.AddForce(direction * 0.3f, ForceMode2D.Impulse);

        gameObject.layer = _freeElementLayerNumber;

        _hasRigidbody = true;
        _particle.SetActive(true);
        line.PlayCrushSound();
    }
}
