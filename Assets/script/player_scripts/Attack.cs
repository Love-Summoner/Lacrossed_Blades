using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attack_time, attack_force, throw_force;
    private BoxCollider2D attack_area;
    public GameObject thrust_area;
    public ball ball_object;
    private Lacrossed_Blades_player inputActions;

    private void Awake()
    {
        ball_object = GameObject.Find("ball").GetComponent<ball>();
        attack_area = GetComponent<BoxCollider2D>();
        thrust_area = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        
        
    }

    private bool is_attacking = false;
    public void attack()
    {
        if (ball_object.is_held) 
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
        is_attacking = true;
        attack_area.enabled = true;
        yield return new WaitForSeconds(attack_time);
        attack_area.enabled = false;
        is_attacking = false;
    }
    IEnumerator thrust_event()
    {
        is_attacking = true;
        thrust_area.SetActive(true);
        yield return new WaitForSeconds(attack_time);
        thrust_area.SetActive(false);
        is_attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ball_object = GameObject.Find("ball").GetComponent<ball>();
        if (collision.CompareTag("ball")) 
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.right * attack_force, ForceMode2D.Impulse);
        }
        if (collision.CompareTag("Player"))
        {
            character c = collision.GetComponent<character>();
            c.damage();
        }
    }
    private void throw_ball()
    {
        ball_object.is_held = false;
        Rigidbody2D rb = ball_object.GetComponent<Rigidbody2D>();
         rb.velocity = Vector2.zero;
         rb.AddForce(transform.right * attack_force, ForceMode2D.Impulse);
    }
}
