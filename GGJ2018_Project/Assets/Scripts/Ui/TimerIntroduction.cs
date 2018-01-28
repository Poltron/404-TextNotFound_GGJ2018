using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIntroduction : MonoBehaviour
{
	[SerializeField]
	private float duration;

	[SerializeField]
	private Text timer;
	
	private void Update ()
	{
		duration -= Time.deltaTime;

		timer.text = "Start in " + ((int)duration).ToString() + " seconds...";

		if (duration <= 1)
			gameObject.SetActive(false);
	}

	private void OnGUI()
	{
		if (Input.anyKey)
		{
			gameObject.SetActive(false);
		}
	}
}
