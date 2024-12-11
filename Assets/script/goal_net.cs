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
            manager.score_goal(is_red);
            Destroy(collision.gameObject);
        }
    }
}
