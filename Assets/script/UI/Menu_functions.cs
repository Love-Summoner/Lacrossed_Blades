using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_functions : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void load_2_player_scene()
    {
        SceneManager.LoadScene("2_player_game");
    }
    public void load_4_player_scene()
    {
        SceneManager.LoadScene("4_player_game");
    }
}
