using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour

{
     int score = 0;



    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions >1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore (int points)
    {
        score += points;
    }

    public int GetScore ()
    {
        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
