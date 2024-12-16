using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(PlayerInput))]
public class Player_Controller : MonoBehaviour
{
    public float speed;
    public Attack attack;
    private Lacrossed_Blades_player inputActions;
    private PlayerInput PlayerInput;

    private Rigidbody2D rb;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        inputActions = new Lacrossed_Blades_player();
        inputActions.Player.Enable();

        //putActions.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        //putActions.Player.Move.canceled += ctx => movement = Vector2.zero;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float horizontal, vertical;
    private Vector2 movement;
    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }
    public void set_movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
