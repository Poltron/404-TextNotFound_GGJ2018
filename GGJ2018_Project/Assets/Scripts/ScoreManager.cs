using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField]
	private int score = 0;

	[SerializeField]
	public float timer = 0.0f;
	[SerializeField]
	public int seconde = 0;
	[SerializeField]
	public int minute = 0;

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
		timer += Time.deltaTime;
		if (timer >= 1)
		{
			timer = 0;
			++seconde;
			if (seconde != 60)
				OnTimerEvent(seconde, minute);
		}

		if (seconde > 59) 
		{
			seconde = 0;
			++minute;
			OnTimerEvent(seconde, minute);
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
	#region OnTimerEvent
	public delegate void TimerEvent(int seconde, int minute);
	private event TimerEvent OnTimerEvent;

	public void AddOnTimerEvent(TimerEvent func)
	{
		OnTimerEvent += func;
	}

	public void RemoveOnTimerEvent(TimerEvent func)
	{
		OnTimerEvent -= func;
	}

	private void ResetOnTimerEvent()
	{
		OnTimerEvent = null;
	}

	private void InvokeOnTimerEvent(int seconde, int minute)
	{
		if (OnTimerEvent != null)
			OnTimerEvent(seconde, minute);
	}
	#endregion
	#endregion

}
