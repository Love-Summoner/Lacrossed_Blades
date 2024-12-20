using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] bounce_clips;
    public bool is_held = false;
    public Transform held_location;
    public GameObject current_holder;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("attack") || collision.CompareTag("thrust"))
        {
            play_bounce_sound();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        play_bounce_sound();
    }

    private void play_bounce_sound()
    {
        source.clip = bounce_clips[Random.Range(0, bounce_clips.Length)];
        source.pitch = Random.Range(.95f, 1.05f);
        source.Play();
    }
}
