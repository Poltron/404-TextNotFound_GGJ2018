using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleVisual : MonoBehaviour
{
	public delegate void EndEdit(string text);
	private event EndEdit OnEndEdit;

	[SerializeField]
	private InputField consoleField;
	[SerializeField]
	private Text historyText;
	[SerializeField]
	private ConsoleWriter console;
	private string[] codes;
	private string[] colors;

	private void Awake()
	{
		AddOnEndEdit(SaveHistory);
		codes = new string[] { "", "", "" };
		colors = new string[] { "1111111", "1111111", "1111111" };
		SaveHistory("");
	}

	private void Start()
	{
		console.AddOnErrorCommand(SetErrorColor);
		consoleField.onEndEdit.AddListener(delegate { InvokeOnEndEdit(consoleField.text); });
	}

	private void Update()
	{
		if (!gameObject.activeSelf)
			return;
		if (!consoleField.isFocused)
			consoleField.ActivateInputField();
		consoleField.caretPosition = consoleField.text.Length;
		consoleField.text = consoleField.text.ToUpper();
	}

	private void OnEnable()
	{
		consoleField.text = "";
		consoleField.ActivateInputField();
	}

	private void SetErrorColor(string cmd)
	{
		colors[2] = "ff0000";

		historyText.text = ">\t<color=#" + colors[0] + ">" + codes[0] + "</color>\n";
		historyText.text += ">\t<color=#" + colors[1] + ">" + codes[1] + "</color>\n";
		historyText.text += ">\t<color=#" + colors[2] + ">" + codes[2] + "</color>";
	}

	private void SaveHistory(string text)
	{
		if (text == "")
			return;
		codes[0] = codes[1];
		codes[1] = codes[2];
		codes[2] = text;

		colors[0] = colors[1];
		colors[1] = colors[2];
		colors[2] = "1111111";

		historyText.text = ">\t<color=#" + colors[0] + ">" + codes[0] + "</color>\n";
		historyText.text += ">\t<color=#" + colors[1] + ">" + codes[1] + "</color>\n";
		historyText.text += ">\t<color=#" + colors[2] + ">" + codes[2] + "</color>";
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