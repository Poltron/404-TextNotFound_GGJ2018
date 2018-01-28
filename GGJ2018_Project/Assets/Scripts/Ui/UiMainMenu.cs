using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
	#region vars Button
	[SerializeField]
	private Button btnPlay;
	[SerializeField]
	private Button btnHightscore;
	[SerializeField]
	private Button btnQuit;
	#endregion

	private void Start()
	{
		btnPlay.onClick.AddListener(ClickPlay);
		btnHightscore.onClick.AddListener(ClickHightscore);
		btnQuit.onClick.AddListener(ClickQuit);

		btnPlay.Select();
	}

	private void ClickPlay()
	{
        SceneManager.LoadScene("Scene_merge");
	}

	private void ClickHightscore()
	{

	}

	private void ClickQuit()
	{
		Application.Quit();
	}
}
