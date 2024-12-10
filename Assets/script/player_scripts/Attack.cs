using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attack_time, attack_force, throw_force;
    private BoxCollider2D attack_area;
    void Start()
    {
        attack_area = GetComponent<BoxCollider2D>();
    }

    
    public void attack()
    {
        if (attack_area.enabled == false)
        {
            StartCoroutine(attack_event());
        }
    }

    IEnumerator attack_event()
    {
        attack_area.enabled = true;
        yield return new WaitForSeconds(attack_time);
        attack_area.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball")) 
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.right * attack_force, ForceMode2D.Impulse);
        }
    }
}
