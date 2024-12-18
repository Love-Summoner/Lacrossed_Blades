using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_manager : MonoBehaviour
{
    public GameObject ball_prefab;
    private float time_left;
    private int red_score, blue_score, round_count, round_limit;
    public TMP_Text red_score_board, blue_score_board, Time_board;
    public float time_per_round, respawn_time;

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
                Debug.Log("red wins");
            }
            else
            {
                Debug.Log("blue wins");
            }
            return;
        }
        Time.timeScale -=Time.deltaTime;
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
}
