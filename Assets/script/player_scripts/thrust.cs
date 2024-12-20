using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrust : MonoBehaviour
{
    public AudioSource source;
    public GameObject hold_spot;
    public Transform attack_trnsform;
    public Attack attack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            attack_trnsform.GetComponent<Attack>().ball_object = collision.GetComponent<ball>();
            ball ball = collision.gameObject.GetComponent<ball>();
            ball.held_location = hold_spot.transform;
            ball.is_held = true;
            ball.current_holder = attack.gameObject;
        }
        if (collision.CompareTag("Player"))
        {
            character c = collision.GetComponent<character>();
            c.damage(transform.parent.parent.right);
        }
        if (collision.CompareTag("attack") || collision.CompareTag("thrust"))
        {
            if (collision.CompareTag("attack"))
            {
                character c = collision.transform.parent.parent.GetComponent<character>();
                c.knockback_player_from_character(transform.parent.parent.right);
            }
            else
            {
                character c = collision.transform.parent.parent.parent.GetComponent<character>();
                c.knockback_player_from_character(transform.parent.parent.right);
            }
            source.Play();
        }
    }
    private void play_clash_sound()
    {
        source.pitch = Random.Range(.9f, 1.1f);
        source.Play();
    }
}
