using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rotate_towards_object : MonoBehaviour
{
    public Transform target, hand_location;
    private Vector2 distance;

    void Update()
    {
        distance = target.position - transform.position;
        transform.right = distance.normalized;

        distance = target.transform.position - hand_location.position;
        if (distance.magnitude > .1f) 
        {
            transform.Translate(distance);
        }
    }
}
