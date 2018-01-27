using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField]
	private int score = 0;

	[SerializeField]
	private float timer = 0.0f;
	[SerializeField]
	private int seconde = 0;

	public void ResetScore()
	{
		score = 0;
	}

	public void AddScore(int add, int multiplicateur = 1)
	{
		score += (int)(add * multiplicateur);
		OnScoreEvent(score);
	}

	public int GetScore()
	{
		return score;
	}

	private void Update()
	{
		++timer;
		if (timer == 60)
		{
			timer = 0;
			++seconde;
		}
	}

	#region Events
	#region OnScoreEvent
	public delegate void ScoreEvent(int score);
	private event ScoreEvent OnScoreEvent;

	public void AddOnScoreEvent(ScoreEvent func)
	{
		OnScoreEvent += func;
	}

	public void RemoveOnScoreEvent(ScoreEvent func)
	{
		OnScoreEvent -= func;
	}

	private void ResetOnScoreEvent()
	{
		OnScoreEvent = null;
	}

	private void InvokeOnScoreEvent(int score)
	{
		if (OnScoreEvent != null)
			OnScoreEvent(score);
	}
	#endregion
	#endregion

}
