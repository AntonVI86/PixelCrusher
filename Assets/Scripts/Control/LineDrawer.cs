using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Transform _container;

    public event UnityAction Pressed;

    private List<Vector2> _fingerPositions = new List<Vector2>();
    private GameObject _currentLine;
    private LineRenderer _line;
    private EdgeCollider2D _collider;
    [SerializeField]private Progress _progress;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (_progress.IsCanDraw && _container != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
                Pressed?.Invoke();
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 tempFingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if(Vector2.Distance(tempFingerPosition, _fingerPositions[_fingerPositions.Count -1]) > 0.1f)
                {
                    UpdateLine(tempFingerPosition);
                    _progress.SendMoney();
                }
            }
        }
    }

    private void CreateLine()
    {
        _currentLine = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity);

        _line = _currentLine.GetComponent<LineRenderer>();
        _collider = _currentLine.GetComponent<EdgeCollider2D>();
        _fingerPositions.Clear();
        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _line.SetPosition(0, _fingerPositions[0]);
        _line.SetPosition(1, _fingerPositions[1]);
        _collider.points = _fingerPositions.ToArray();

        _line.transform.SetParent(_container);
        _line.transform.rotation = Quaternion.identity;
    }

    private void UpdateLine(Vector2 newFingerPosition)
    {
        _fingerPositions.Add(newFingerPosition);
        _line.positionCount++;
        _line.SetPosition(_line.positionCount - 1, newFingerPosition);
        _collider.points = _fingerPositions.ToArray();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _progress.GetIsCanDraw();
        Time.timeScale = 1;
        _container = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _container = eventData.pointerCurrentRaycast.gameObject.transform;
    }
}
