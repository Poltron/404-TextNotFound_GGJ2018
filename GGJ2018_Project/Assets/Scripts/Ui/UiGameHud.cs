using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGameHud : MonoBehaviour
{
	[SerializeField]
	private ScoreManager scoreManager;

	[SerializeField]
	private Text txtScore;
	[SerializeField]
	private Text txtTimer;

	private void OnEnable()
	{
		//scoreManager.AddOnScoreEvent(UpdateScore);
		scoreManager.AddOnTimerEvent(UpdateTimer);
	}

	private void OnDestroy()
	{
		//scoreManager.RemoveOnScoreEvent(UpdateScore);
		scoreManager.RemoveOnTimerEvent(UpdateTimer);
	}

	public void UpdateScore(int score)
	{
		txtScore.text = "SCORE : " + score.ToString();
	}

	public void UpdateTimer(int seconde, int minute)
	{
		txtTimer.text = "TIMER : " + minute.ToString("D2") + ":" + seconde.ToString("D2");
	}
}
