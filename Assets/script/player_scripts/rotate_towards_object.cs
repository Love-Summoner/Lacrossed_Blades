using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_towards_object : MonoBehaviour
{
    public Transform target, hand_location, rotation_object;
    private Vector2 distance;

    void Update()
    {
        distance = target.position - rotation_object.transform.position;
        transform.right = distance.normalized;

        distance = target.transform.position - hand_location.position;
            transform.position = transform.position + new Vector3(distance.x, distance.y, 0);
    }
}
