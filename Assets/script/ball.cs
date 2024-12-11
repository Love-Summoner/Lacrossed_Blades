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
    public void change_hold_location(Transform new_loc)
    {
        held_location = new_loc;
    }
}
