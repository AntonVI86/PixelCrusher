using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    [SerializeField] private GameObject _round;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _drawingHelp;

    private Rigidbody2D _rigidbody;

    public float Radius => _radius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(Rotate());    
    }

    public void ChangeView()
    {
        _round.SetActive(true);
        _arrow.SetActive(false);       
    }

    public void ActivateHelp()
    {
        if(_round.activeSelf == false)
        {
            _arrow.SetActive(true);
        }

        _drawingHelp.SetActive(true);
    }

    public void DisableHelp()
    {
        _arrow.SetActive(false);
        _drawingHelp.SetActive(false);
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            _rigidbody.MoveRotation(_rigidbody.rotation + _speed * Time.fixedDeltaTime);

            yield return null;
        }
    }
}
