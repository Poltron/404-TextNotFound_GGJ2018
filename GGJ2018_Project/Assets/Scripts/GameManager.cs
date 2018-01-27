using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private LevelManager levelManager;

    protected GameManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        levelManager.StartGame();
    }
}