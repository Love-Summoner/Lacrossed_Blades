using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller_recorder_startup : MonoBehaviour
{
    public GameObject player_icon_prefab;
    public float x_pos, y_pos, offset;
    private Player_controller_manager player_manager;
    private PlayerInput playerinput;
    private RectTransform player_icon_rect;
    private Vector2 default_position;
    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        DontDestroyOnLoad(this);
        transform.parent = GameObject.Find("Player_manager").transform;
        player_manager = GameObject.Find("Player_manager").GetComponent<Player_controller_manager>();
        player_manager.add_player_input(playerinput);

        Transform new_parent = GameObject.Find("Players").transform;

        GameObject temp = Instantiate(player_icon_prefab, new_parent);
        player_icon_rect = temp.GetComponent<RectTransform>();
        player_icon_rect.anchoredPosition = new Vector2(x_pos + offset * (new_parent.childCount - 1), y_pos);
        default_position = player_icon_rect.anchoredPosition;
        Color icon_color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        temp.GetComponent<RawImage>().color = icon_color;
        temp.GetComponentInChildren<TMP_Text>().text = "P" + new_parent.childCount.ToString();
        temp.GetComponentInChildren<TMP_Text>().color = icon_color;
    }
    public void change_slot(InputAction.CallbackContext context)
    {
        if (GameObject.FindGameObjectsWithTag("player_slot").Length == 0)
        {
            return;
        }
        Vector2 temp = context.ReadValue<Vector2>();
        
        if(GameObject.FindGameObjectsWithTag("player_slot").Length > 2)
        {
            if (temp.x > 0)
            {
                if (temp.y > 0)
                {
                    player_icon_rect.anchoredPosition = GameObject.Find("slot3").GetComponent<RectTransform>().anchoredPosition;
                    player_manager.set_slot(playerinput, 3);
                }
                else if (temp.y < 0) {
                    player_icon_rect.anchoredPosition = GameObject.Find("slot4").GetComponent<RectTransform>().anchoredPosition;
                    player_manager.set_slot(playerinput, 4);
                }
            }
            else if(temp.x < 0) 
            {
                if (temp.y > 0)
                {
                    player_icon_rect.anchoredPosition = GameObject.Find("slot1").GetComponent<RectTransform>().anchoredPosition;
                    player_manager.set_slot(playerinput, 1);
                }
                else if (temp.y < 0)
                {
                    player_icon_rect.anchoredPosition = GameObject.Find("slot2").GetComponent<RectTransform>().anchoredPosition;
                    player_manager.set_slot(playerinput, 2);
                }
            }
            return;
        }

        if(temp.x > 0)
        {
            player_icon_rect.anchoredPosition = GameObject.Find("slot2").GetComponent<RectTransform>().anchoredPosition;
            player_manager.set_slot(playerinput, 2);
        }
        else if(temp.x < 0)
        {
            player_icon_rect.anchoredPosition = GameObject.Find("slot1").GetComponent<RectTransform>().anchoredPosition;
            player_manager.set_slot(playerinput, 1);
        }
    }

    public void reset_icon()
    {
        player_icon_rect.anchoredPosition = default_position;
    }
}
