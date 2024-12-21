using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class character : MonoBehaviour
{
    public AudioSource source;
    private PlayerInput input;
    private Game_manager game_manager;
    private Transform respawn_point;
    private Player_Controller player_controller;
    public int max_health = 2;
    public bool is_red = true;
    private int current_health = 0;
    public float respawn_time = 3;
    public SpriteRenderer sprite;
    void Start()
    {
        player_controller = GetComponent<Player_Controller>();
        input = GetComponent<PlayerInput>();
        game_manager = GameObject.Find("Game_manager").GetComponent<Game_manager>();
        current_health = max_health;
        if(is_red )
            respawn_point = GameObject.Find("Red_respawn_point").transform;
        else
            respawn_point = GameObject.Find("Blue_respawn_point").transform;
    }

    public void damage(Vector2 direction)
    {
        if(invincible)
            return;
        current_health--;
        if(current_health <= 0)  
            death_event();
        play_injury_sound();
        player_controller.knockback_player(direction);
    }

    private void death_event()
    {
        GameObject.Find("ball").GetComponent<ball>().is_held = false;
        transform.position = new Vector2(100000, 100000);
        current_health = max_health;
        StartCoroutine(start_respawn());
    }
    IEnumerator start_respawn()
    {
        yield return new WaitForSeconds(respawn_time);
        transform.position = respawn_point.position;
        StartCoroutine(invincibility_event());
    }
    private InputDevice device;
    private string control_scheme;
    private void OnDisable()
    {
        device = input.GetDevice<InputDevice>();
        control_scheme = input.currentControlScheme;
    }
    private bool just_started = true;
    private void OnEnable()
    {
        if (just_started)
        {
            just_started = false;
            return;
        }
        input.SwitchCurrentControlScheme(control_scheme, device);
        if (device == Keyboard.current || device == Mouse.current)
        {
            input.SwitchCurrentControlScheme(control_scheme, Keyboard.current, Mouse.current);
        }
    }
    private void play_injury_sound()
    {
        source.pitch = Random.Range(.9f, 1.1f);
        source.Play();
    }
    public void knockback_player_from_character(Vector2 direction)
    {
        player_controller.knockback_player(direction);
    }
    IEnumerator invincibility_event()
    {
        invincible = true;
        yield return new WaitForSeconds(2);
        invincible = false;
    }
    private bool is_faded = false;
    private bool invincible = false;
    IEnumerator fade_in_out()
    {
        sprite.color = new Color(1, 1, 1, .4f);
        is_faded = true;
        yield return new WaitForSeconds(.2f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.2f);
        is_faded = false;

    }
    private void Update()
    {
        if (invincible && !is_faded)
        {
            StartCoroutine(fade_in_out());
        }
    }
}
