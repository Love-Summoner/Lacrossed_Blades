using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_functions : MonoBehaviour
{
    
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Initialization")
        {
            load_main_menu();
        }
    }

    public void load_2_player_scene()
    {
        SceneManager.LoadScene("2_player_game");
    }
    public void load_4_player_scene()
    {
        SceneManager.LoadScene("4_player_game");
    }
    public void Quit_game()
    {
        Application.Quit();
    }
    public void load_main_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_menu");
    }
    public void reset_icons_from_menu()
    {
        GameObject.Find("Player_manager").GetComponent<Player_controller_manager>().reset_icons();
    }
}
