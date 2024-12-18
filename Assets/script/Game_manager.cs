using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_manager : MonoBehaviour
{
    public GameObject ball_prefab, red_win_screen, blue_win_screen, pause_menu;
    private float time_left;
    private int red_score, blue_score, round_count, round_limit;
    public TMP_Text red_score_board, blue_score_board, Time_board;
    public float time_per_round, respawn_time;
    private Lacrossed_Blades_player controls;
    private void Awake()
    {
        controls = new Lacrossed_Blades_player();

    }
    private void Start()
    {
        time_left = time_per_round;
        Time_board.text = Mathf.FloorToInt(time_left / 60).ToString() + ":00";
    }
    public void score_goal(bool is_red)
    {
        if (is_red) { red_score++; }
        else { blue_score++; }

        GameObject temp = Instantiate(ball_prefab, transform);
        temp.name = "ball";

        red_score_board.text = red_score.ToString();
        blue_score_board.text = blue_score.ToString();
    }

    void Update()
    {
        time_left -= Time.deltaTime;

        if(time_left < 0)
        {
            end_game();
            return;
        }
        string seconds_lecf_in_minute = Mathf.FloorToInt(time_left % 60).ToString("00");

        Time_board.text = Mathf.FloorToInt(time_left/60).ToString() + ":" + seconds_lecf_in_minute;
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            pause_game();
        }
    }
    private void round_end()
    {

    }
    private void end_game()
    {
        if(Time.timeScale <= .1f)
        {
            Time.timeScale = 0;
            if(red_score > blue_score)
            {
                red_win_screen.SetActive(true);
            }
            else
            {
                blue_win_screen.SetActive(true);
            }
            return;
        }
        if (red_score != blue_score)
        {
            Time.timeScale -= Time.deltaTime;
        }
        else { Time.timeScale = 1; }
    }
    public void respawn_character(GameObject characer_object)
    {
        StartCoroutine(start_respawn(characer_object));
    }
    IEnumerator start_respawn(GameObject characer_object)
    {
        characer_object.SetActive(false);
        yield return new WaitForSeconds(respawn_time);
        characer_object.SetActive(true);
    }
    public void pause_game()
    {
        pause_menu.SetActive(!pause_menu.activeSelf);
        Time.timeScale = pause_menu.activeSelf ? 0 : 1;
    }
}
