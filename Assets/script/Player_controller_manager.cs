using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_controller_manager : MonoBehaviour
{
    private List<PlayerInput> input_list = new List<PlayerInput>();
    private PlayerInput[] selected_players = new PlayerInput[4];
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void add_player_input(PlayerInput new_input)
    {
        input_list.Add(new_input);
    }
    public void allocate_player_inputs()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);

        for (int i = 0; i < players.Length; i++) 
        {
            GameObject p = GameObject.Find("Player" + (i+1).ToString());
            if (selected_players[i] != null)
            {
                p.GetComponent<PlayerInput>().SwitchCurrentControlScheme(selected_players[i].currentControlScheme, selected_players[i].GetDevice<InputDevice>());
                if(selected_players[i].GetDevice<InputDevice>() == Keyboard.current || selected_players[i].GetDevice<InputDevice>() == Mouse.current)
                {
                    p.GetComponent<PlayerInput>().SwitchCurrentControlScheme(selected_players[i].currentControlScheme, Keyboard.current, Mouse.current);
                }
            }
            else
            {
                p.GetComponent<PlayerInput>().enabled = false;
            }
        }
        if(SceneManager.GetActiveScene().name != "Main_menu")
            Destroy(gameObject);
    }
    public void set_slot(PlayerInput new_input, int slot_number)
    {
        for (int i = 0; i < 4; i++) 
        {
            if (selected_players[i] == new_input) 
            {

                selected_players[i] = null;
            }
        }
        if (selected_players[slot_number - 1] != null)
        {
            selected_players[slot_number - 1].GetComponent<Controller_recorder_startup>().reset_icon();
        }
        selected_players[slot_number-1] = new_input;
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        allocate_player_inputs();
    }
}
