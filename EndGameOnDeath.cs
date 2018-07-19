using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameOnDeath : MonoBehaviour {

    GameManager gamemanager;

    void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void WinGame()
    {
        gamemanager.GameOver();
    }

}
