using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorChangeState : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rotators = new List<GameObject>();

    private LineDrawer _line;

    private void Awake()
    {
        _line = GetComponent<LineDrawer>();
    }
    private void OnEnable()
    {
        _line.Pressed += OnPressed;
    }

    private void OnDisable()
    {
        _line.Pressed -= OnPressed;
    }

    private void OnPressed()
    {
        foreach (var rotator in _rotators)
        {
            if (Time.timeScale == 0)
            {
                if(rotator.transform.childCount <= 1)
                    rotator.transform.GetChild(0).gameObject.SetActive(true);
                else
                    rotator.transform.GetChild(0).gameObject.SetActive(false);
            }

            rotator.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
