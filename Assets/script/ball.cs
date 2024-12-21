using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] bounce_clips;
    public bool is_held = false, invincible = false, decelerating = false;
    public Transform held_location;
    public GameObject current_holder;
    public float respawn_time, fade_interval;

    private SpriteRenderer sprite;
    private CircleCollider2D box;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(invincibility_event());
    }
    private void Update()
    {
        if (invincible && !is_faded)
        {
            StartCoroutine (fade_in_out());
        }
        if (is_held)
        {
            transform.position = held_location.position;
        }
        if (decelerating) 
        { 
            if(rb.velocity.magnitude > 7.9f)
            {
                decelerating = false;
                return;
            }
            rb.velocity-=(rb.velocity.normalized * Time.deltaTime*6);
            if(rb.velocity.magnitude < .2f)
            {
                rb.velocity = Vector2.zero;
            }
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
    IEnumerator invincibility_event()
    {
        box.enabled = false;
        invincible = true;
        yield return new WaitForSeconds(respawn_time);
        invincible = false;
        box.enabled = true;
    }
    private bool is_faded = false;
    IEnumerator fade_in_out()
    {
        sprite.color = new Color(1, 1, 1, .4f);
        is_faded = true;
        yield return new WaitForSeconds(fade_interval/2);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(fade_interval / 2);
        is_faded =false;
         
    }
    
}
