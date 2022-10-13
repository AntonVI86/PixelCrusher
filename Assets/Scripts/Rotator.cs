using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Start()
    {
        StartCoroutine(Rotate());    
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0,0,_speed * Time.deltaTime);

            yield return null;
        }
    }
}
