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
    public float time_per_round;

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

    }
}
