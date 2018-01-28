using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour/*Singleton<GameManager>*/
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

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
		_instance = this;
	}

	private void Start()
	{
		GameObject playerGO = GameObject.Find("Player");
		Debug.Log(playerGO);
		player = playerGO.GetComponent<PlayerController>();

		dialogAudioSource = GameObject.Find("DialogAudioSource").GetComponent<AudioSource>();

		SceneManager.LoadSceneAsync("Scene_HUD", LoadSceneMode.Additive);

		levelManager.StartGame();
	}
}