using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text; 
    [SerializeField]
    private GameFlow game; 
    [SerializeField]
    private float timeLeft = 30.0f;

    private int minutes;
    private int seconds;
    private bool finishGame = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        minutes = (int)timeLeft / 60;
        seconds = (int)timeLeft % 60;

        if(finishGame)
        {
            return;
        }
        
        if(timeLeft < 0)
        {
            finishGame = true;
            game.EndTimer();
            game.Status();
            return;
        }

        setTimer(minutes, seconds);
    }

    private void setTimer(int minutes, int seconds)
    {
        text.text = formatTime(minutes) + ":" + formatTime(seconds);
    }

    private string formatTime(int time)
    {
        if(time < 10) 
        {
            return "0" + time;
        }
        return time.ToString();
    }
}
