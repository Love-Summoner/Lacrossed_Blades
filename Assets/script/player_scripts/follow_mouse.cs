using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class follow_mouse : MonoBehaviour
{
    private Vector2 distance;
    private Lacrossed_Blades_player inputActions;


    private void Awake()
    {
        inputActions = new Lacrossed_Blades_player();
        inputActions.Player.Enable();

    }
    void Update()
    {
        if(is_mouse)
        {
            is_mouse = true;
            distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        transform.right = distance.normalized;
    }
    private bool is_mouse = false;
    public void look_in_direction(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().magnitude > 2 || is_mouse)
        {
            is_mouse = true;
            distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            return;
        }
        if(context.ReadValue<Vector2>() != Vector2.zero)
            distance = context.ReadValue<Vector2>();
    }
}
