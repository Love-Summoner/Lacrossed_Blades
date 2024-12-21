using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public enum movement_states
{
    DEFAULT,
    KNOCKBACK
}
[RequireComponent(typeof(PlayerInput))]
public class Player_Controller : MonoBehaviour
{
    public movement_states movement_State = movement_states.DEFAULT;
    public float speed, knock_back_resistance, knock_back_power, knock_back_time;
    public Attack attack;
    public Transform character_sprite;

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
        if(movement.x < 0 && character_sprite.localScale.x > 0)
        {
            character_sprite.localScale = new Vector3(character_sprite.localScale.x * -1, character_sprite.localScale.y, character_sprite.localScale.z);
        }
        else if (movement.x > 0 && character_sprite.localScale.x < 0)
        {
            character_sprite.localScale = new Vector3(character_sprite.localScale.x*-1, character_sprite.localScale.y, character_sprite.localScale.z);
        }
        switch (movement_State) 
        {
            case movement_states.DEFAULT:
                rb.velocity = movement.normalized * speed;
                break;
            case movement_states.KNOCKBACK:
                rb.AddForce((movement.normalized * knock_back_resistance), ForceMode2D.Impulse);
                break;
        }
    }
    public void set_movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void change_movement_from_other_script(Vector2 new_direction)
    {
        movement = new_direction;
    }
    public void knockback_player(Vector2 direction)
    {
        movement_State = movement_states.KNOCKBACK;
        rb.AddForce(knock_back_power*direction, ForceMode2D.Impulse);
        StartCoroutine(reset_movement_state());
    }
    IEnumerator reset_movement_state()
    {
        yield return new WaitForSeconds(knock_back_time);
        movement_State = movement_states.DEFAULT;
    }
}
