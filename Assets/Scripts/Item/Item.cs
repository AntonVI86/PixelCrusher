using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Debug.Log("sadf");
            gameObject.SetActive(false);
        }
    }
}
