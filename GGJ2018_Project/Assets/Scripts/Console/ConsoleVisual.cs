using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleVisual : MonoBehaviour
{
	[SerializeField]
	private Text consoleText;

	private void OnGUI()
	{
		Event e = Event.current;
		if (e == null)
			return;

		if (e.type == EventType.KeyDown && e.isKey)
		{
			string key = e.keyCode.ToString();
			if (e.keyCode == KeyCode.Escape)
			{
				if (consoleText.text.Length > 0)
					consoleText.text = consoleText.text.Remove(consoleText.text.Length - 1);
			}
			else if (e.keyCode == KeyCode.Return)
				gameObject.SetActive(false);
			else
				consoleText.text += key;
		}
	}

	private void OnEnable()
	{
		consoleText.text = "";
	}
}