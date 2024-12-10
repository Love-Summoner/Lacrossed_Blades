using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    public Attack attack;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float horizontal, vertical;
    private Vector2 movement;
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2 (horizontal, vertical);

        rb.velocity = movement.normalized * speed;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack.attack();
        }
    }
}
