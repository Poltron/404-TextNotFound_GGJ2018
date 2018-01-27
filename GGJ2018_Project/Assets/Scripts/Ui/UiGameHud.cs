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

	private void OnEnable()
	{
		scoreManager.AddOnScoreEvent(UpdateScore);
	}

	private void OnDestroy()
	{
		scoreManager.RemoveOnScoreEvent(UpdateScore);
	}

	public void UpdateScore(int score)
	{
		txtScore.text = "Score : " + score.ToString();
	}
}
