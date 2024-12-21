using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_net : MonoBehaviour
{
    public bool is_red = true;
    public Game_manager manager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity *= .9f;
            collision.gameObject.GetComponent<ball>().decelerating = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            manager.score_goal(is_red);
            StartCoroutine(delayed_destruct(collision.gameObject));
        }
    }
    IEnumerator delayed_destruct(GameObject destrucitible)
    {
        yield return new WaitForSeconds(.1f);
        Destroy(destrucitible);
    }
}
