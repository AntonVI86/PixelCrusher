using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Rotator _container;
    [SerializeField] private Progress _progress;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _drawingSound;

    public event UnityAction Pressed;

    private List<Vector2> _fingerPositions = new List<Vector2>();

    private GameObject _currentLine;
    private LineRenderer _line;
    private EdgeCollider2D _collider;

    private float _edgeRadius = 0.06f;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        Draw();
    }

    private void CreateLine()
    {
        _currentLine = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity);

        _line = _currentLine.GetComponent<LineRenderer>();
        _collider = _currentLine.GetComponent<EdgeCollider2D>();

        _collider.edgeRadius = _edgeRadius;
        _fingerPositions.Clear();

        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        _line.SetPosition(0, _fingerPositions[0]);
        _line.SetPosition(1, _fingerPositions[1]);

        _collider.points = _fingerPositions.ToArray();

        _line.transform.SetParent(_container.transform);
        _line.transform.rotation = Quaternion.identity;
    }

    private void UpdateLine(Vector2 newFingerPosition)
    {
        _fingerPositions.Add(newFingerPosition);
        _line.positionCount++;
        _line.SetPosition(_line.positionCount - 1, newFingerPosition);
        
        _collider.points = _fingerPositions.ToArray();

        if(_audioSource.isPlaying == false)
            _audioSource.PlayOneShot(_drawingSound);
    }

    private void Draw()
    {
        float distance = 0.1f;

        if (_progress.IsCanDraw && _container != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
                _container.GetComponent<Rotator>().ChangeView();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 tempFingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 offset = tempFingerPosition - _container.transform.position;

                tempFingerPosition = _container.transform.position + Vector3.ClampMagnitude(offset, _container.Radius);

                if (Vector2.Distance(tempFingerPosition, _fingerPositions[_fingerPositions.Count - 1]) > distance)
                {
                    UpdateLine(tempFingerPosition);
                    _progress.SendMoney();
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {       
        _progress.GetIsCanDraw();
        Pressed?.Invoke();
        _container = null;
        Time.timeScale = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _container = eventData.pointerCurrentRaycast.gameObject.GetComponent<Rotator>();
    }
}
