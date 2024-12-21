using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AudioSource source, clash_source;
    public AudioClip[] slash_sounds;
    public float attack_time, attack_force, throw_force;
    private BoxCollider2D attack_area;
    public GameObject thrust_area;
    public ball ball_object;
    private Lacrossed_Blades_player inputActions;
    public Animator animator;

    private void Awake()
    {
        ball_object = GameObject.Find("ball").GetComponent<ball>();
        attack_area = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
        
    }

    private bool is_attacking = false;
    public void attack()
    {
        if(GameObject.Find("ball") != null)
            ball_object = GameObject.Find("ball").GetComponent<ball>();
        else
        {
            ball_object = null;
        }
        if (ball_object != null && ball_object.current_holder == gameObject) 
        {
            throw_ball();
        }
        if (!is_attacking)
        {
            StartCoroutine(attack_event());
        }
    }
    public void thrust()
    {
        if (!is_attacking)
        {
            StartCoroutine(thrust_event());
        }
    }
    IEnumerator attack_event()
    {
        play_slash_sound();
        is_attacking = true;
        attack_area.enabled = true;
        animator.Play("Slash");
        yield return new WaitForSeconds(attack_time);
        animator.Play("Idle");
        attack_area.enabled = false;
        is_attacking = false;
    }
    IEnumerator thrust_event()
    {
        play_slash_sound();
        is_attacking = true;
        thrust_area.SetActive(true);
        animator.Play("Thrust_anim");
        yield return new WaitForSeconds(.4f);
        animator.Play("Idle");
        yield return new WaitForSeconds(attack_time- .4f);
        thrust_area.SetActive(false);
        is_attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.Find("ball") != null)
            ball_object = GameObject.Find("ball").GetComponent<ball>();
        else
        {
            ball_object = null;
        }
        if (collision.CompareTag("ball")) 
        {
            ball_object.current_holder = null;
            ball_object.is_held = false;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.right * attack_force, ForceMode2D.Impulse);
        }
        if (collision.CompareTag("Player"))
        {
            character c = collision.GetComponent<character>();
            c.damage(transform.parent.right);
        }
        if (collision.CompareTag("attack") || collision.CompareTag("thrust"))
        {
            if (collision.CompareTag("attack"))
            {
                character c = collision.transform.parent.parent.GetComponent<character>();
                c.knockback_player_from_character(transform.parent.right);
            }
            else
            {
                character c = collision.transform.parent.parent.parent.GetComponent<character>();
                c.knockback_player_from_character(transform.parent.right);
            }
            play_clash_sound();
        }
    }
    private void play_clash_sound()
    {
        clash_source.pitch = Random.Range(.9f, 1.1f);
        clash_source.Play();
    }
    private void throw_ball()
    { 
        ball_object.current_holder = null;
        ball_object.is_held = false;
        Rigidbody2D rb = ball_object.GetComponent<Rigidbody2D>();
         rb.velocity = Vector2.zero;
         rb.AddForce(transform.right * attack_force, ForceMode2D.Impulse);
       
    }
    private void OnDisable()
    {
        animator.Play("Idle");
        thrust_area.SetActive(false);
        is_attacking = false;
        attack_area.enabled = false;
    }
    private void play_slash_sound()
    {
        source.clip = slash_sounds[Random.Range(0, slash_sounds.Length)];
        source.pitch = Random.Range(.95f, 1.05f);
        source.Play();
    }
}
