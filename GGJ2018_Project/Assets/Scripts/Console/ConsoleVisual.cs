using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleVisual : MonoBehaviour
{
	public delegate void EndEdit(string text);
	private event EndEdit OnEndEdit;

	[SerializeField]
	private Text consoleText;

	private void OnGUI()
	{
		if (!Input.anyKeyDown)
			return;

		Event e = Event.current;
		if (e == null)
			return;

		if (e.isKey)
		{
			switch (e.keyCode)
			{
				case KeyCode.Backspace:
					if (consoleText.text.Length > 0)
						consoleText.text = consoleText.text.Remove(consoleText.text.Length - 1);
					break;
				case KeyCode.Return:
					InvokeOnEndEdit(consoleText.text);
					break;
				case KeyCode.Space:
					consoleText.text += " ";
					break;
				case KeyCode.Keypad0:
				case KeyCode.Keypad1:
				case KeyCode.Keypad2:
				case KeyCode.Keypad3:
				case KeyCode.Keypad4:
				case KeyCode.Keypad5:
				case KeyCode.Keypad6:
				case KeyCode.Keypad7:
				case KeyCode.Keypad8:
				case KeyCode.Keypad9:
				case KeyCode.Alpha0:
				case KeyCode.Alpha1:
				case KeyCode.Alpha2:
				case KeyCode.Alpha3:
				case KeyCode.Alpha4:
				case KeyCode.Alpha5:
				case KeyCode.Alpha6:
				case KeyCode.Alpha7:
				case KeyCode.Alpha8:
				case KeyCode.Alpha9:
					string key = e.keyCode.ToString();
					consoleText.text += key[key.Length - 1];
					break;
				default:
					key = e.keyCode.ToString();
					if (key.Length > 1)
						return;
					if (char.IsLetterOrDigit(key[0]))
						consoleText.text += key;
					break;
			}
			e.Use();
		}
	}

	private void OnEnable()
	{
		consoleText.text = "";
	}

	#region Events
	#region OnEndEdit
	public void AddOnEndEdit(EndEdit func)
	{
		OnEndEdit += func;
	}

	public void RemoveOnEndEdit(EndEdit func)
	{
		OnEndEdit -= func;
	}

	private void ResetOnEndEdit(EndEdit func)
	{
		OnEndEdit = null;
	}

	private void InvokeOnEndEdit(string text)
	{
		if (OnEndEdit != null)
			OnEndEdit(text);
	}
	#endregion
	#endregion
}