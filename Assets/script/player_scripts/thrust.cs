using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrust : MonoBehaviour
{
    public GameObject hold_spot;
    public Transform attack_trnsform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            attack_trnsform.GetComponent<Attack>().ball_object = collision.GetComponent<ball>();
            ball ball = collision.gameObject.GetComponent<ball>();
            ball.held_location = hold_spot.transform;
            ball.is_held = true;
        }
        if (collision.CompareTag("Player"))
        {
            character c = collision.GetComponent<character>();
            c.damage();
        }
    }
}
