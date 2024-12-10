using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public bool is_held = false;
    public Transform held_location;

    private void Update()
    {
        if (is_held)
        {
            transform.position = held_location.position;
        }
    }
}
