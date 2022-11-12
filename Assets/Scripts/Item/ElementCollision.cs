using System.Collections;
using UnityEngine;

public class ElementCollision : MonoBehaviour
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private LayerMask _layer;

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
        if (_isFree == false)
        {
            if (collision.gameObject.TryGetComponent(out DrawingLine line))
            {
                _rb = gameObject.AddComponent<Rigidbody2D>();
                _rb.mass = 0.1f;
                Vector3 direction = Vector3.Cross(transform.position - line.transform.position,new Vector3(0,0,-1f));
                
                _rb.AddForce(direction * 0.3f, ForceMode2D.Impulse);
                line.PlayCrushSound();
                gameObject.layer = _freeElementLayerNumber;
                _isFree = true;
                _particle.SetActive(true);
            }
        }      

        if (_isFree == false)
        {
            if (collision.gameObject.TryGetComponent(out Shredder shredder))
            {
                _particle.SetActive(true);
                StartCoroutine(DestroyElement());
            }
        }
        if (_isFree)
        {
            if (collision.gameObject.TryGetComponent(out Shredder shredder))
            {
                _particle.gameObject.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }

    private void CheckNeightboars()
    {
        Collider2D[] result = Physics2D.OverlapBoxAll(transform.position, transform.localScale, Vector2.Angle(Vector2.zero, transform.position), _layer);

        if(result.Length <= 4)
        {
            Transform newParent = Instantiate(gameObject.transform, result[0].transform);
           
            foreach (var item in result)
            {
                item.transform.SetParent(newParent);
            }
        }
    }

    private IEnumerator DestroyElement()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        gameObject.SetActive(false);
    }
}
