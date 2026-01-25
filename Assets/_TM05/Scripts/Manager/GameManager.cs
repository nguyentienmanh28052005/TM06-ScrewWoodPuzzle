using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    public SaveDataPlayer data;

    public ScrewNut previosScrewNut;
    public ScrewNut currentScrewNut;
    public Screw currentScrew;
    public int level;

    public bool busy;

    public GameState gameState = GameState.Main;
    
    public GameObject unScrewToolPanel;
    
    public enum GameState
    {
        Main,
        UnScrew,
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Save Test");
            data.Save(1, 5);
            data.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Load Test");
            data.Value(1);
        }
    }
}
