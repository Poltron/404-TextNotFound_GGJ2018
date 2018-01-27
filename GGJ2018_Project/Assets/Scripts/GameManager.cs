using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private LevelManager levelManager;
    public LevelManager LevelManager { get { return levelManager; } }

    private PlayerController player;
    public PlayerController Player { get { return player; } }

    private AudioSource dialogAudioSource;
    public AudioSource DialogAudioSource { get { return dialogAudioSource; } }

    protected GameManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        GameObject playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<PlayerController>();

        dialogAudioSource = GameObject.Find("DialogAudioSource").GetComponent<AudioSource>();
            
        levelManager.StartGame();
    }
}