using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class character : MonoBehaviour
{
    private Game_manager game_manager;
    private Transform respawn_point;
    public int max_health = 2;
    public bool is_red = true;
    private int current_health = 0;
    public float respawn_time = 3;
    void Start()
    {
        game_manager = GameObject.Find("Game_manager").GetComponent<Game_manager>();
        current_health = max_health;
        if(is_red )
            respawn_point = GameObject.Find("Red_respawn_point").transform;
        else
            respawn_point = GameObject.Find("Blue_respawn_point").transform;
    }

    public void damage()
    {
        current_health--;
        if(current_health <= 0)  
            death_event();
    }

    private void death_event()
    {
        transform.position = respawn_point.position;
        current_health = max_health;
        game_manager.respawn_character(gameObject);
    }
}
