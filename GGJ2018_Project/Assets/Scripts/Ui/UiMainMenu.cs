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
	private Button btnWebsite;
	[SerializeField]
	private Button btnQuit;
	[SerializeField]
	private Button btnOpenCredit;
	[SerializeField]
	private Button btnCloseCredit;
	#endregion

	#region vars Panels
	[SerializeField]
	private GameObject panCredit;
	#endregion

	private void Start()
	{
		btnPlay.onClick.AddListener(ClickPlay);
		btnWebsite.onClick.AddListener(ClickWebsite);
		btnQuit.onClick.AddListener(ClickQuit);
		btnOpenCredit.onClick.AddListener(ClickOpenCredit);
		btnCloseCredit.onClick.AddListener(ClickCloseCredit);

		btnPlay.Select();
	}

	private void ClickPlay()
	{
        SceneManager.LoadScene("Scene_merge");
	}

	private void ClickWebsite()
	{
		Application.OpenURL("https://ggj2018.guillaume-paringaux.fr"); 
	}

	private void ClickOpenCredit()
	{
		panCredit.SetActive(true);
	}

	private void ClickCloseCredit()
	{
		panCredit.SetActive(false);
		btnPlay.Select();
	}

	private void ClickQuit()
	{
		Application.Quit();
	}
}
