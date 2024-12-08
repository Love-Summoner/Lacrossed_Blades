using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_mouse : MonoBehaviour
{
    private Vector2 distance;
    void Update()
    {
        distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.right = distance.normalized;
    }
}
