using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorChangeState : MonoBehaviour
{
    [SerializeField] private List<Rotator> _rotators = new List<Rotator>();

    [SerializeField] private Progress _progress;
    [SerializeField] private LineDrawer _line;

    private void OnEnable()
    {
        _progress.Paused += OnPaused;
        _line.Pressed += OnPressed;
    }

    private void OnDisable()
    {
        _progress.Paused -= OnPaused;
        _line.Pressed -= OnPressed;
    }

    private void OnPaused()
    {
        Time.timeScale = 0;

        foreach (var rotate in _rotators)
        {
            rotate.ActivateHelp();
        }
    }

    private void OnPressed()
    {
        foreach (var rotate in _rotators)
        {
            rotate.DisableHelp();
        }
    }
}
