using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleWriter : MonoBehaviour
{
	public delegate void SendCommand(string cmd, string[] args);
	private event SendCommand OnSendCommand;

	public delegate void ErrorCommand(string cmd);
	private event ErrorCommand OnErrorCommand;

    [SerializeField]
	private GameObject console;
	[SerializeField]
	private ConsoleVisual writer;
	[SerializeField]
	private bool debugCmd;

	private bool justeChange;

	void Start()
	{
		writer.AddOnEndEdit(SendConsole);
		if (debugCmd)
			AddOnSendCommand(DebugCommand);
		ShowConsole(false);
	}

	private void Update()
	{
		if (!justeChange && Input.GetKeyDown(KeyCode.Return))
		{
			ShowConsole(true);
		}
		if (justeChange)
			justeChange = false;
	}

	public void SendConsole(string cmdLine)
	{
		string[] parseCommand = cmdLine.Split(' ');

		string cmd = parseCommand.Length > 0 ? parseCommand[0] : "";

		string[] args = new string[] { };
		if (parseCommand.Length > 1)
		{
			args = new string[parseCommand.Length - 1];

			for (int i = 1 ; i < parseCommand.Length ; ++i)
			{
				args[i - 1] = parseCommand[i];
			}
		}

		if (!string.IsNullOrEmpty(cmd))
			InvokeOnSendCommand(cmd, args);

		ShowConsole(false);
	}

	private void ShowConsole(bool b)
	{
		console.SetActive(b);
		enabled = !console.activeSelf;
		justeChange = true;
	}

	private void DebugCommand(string cmd, string[] args)
	{
		Debug.Log("cmd : " + cmd);
		foreach (string arg in args)
		{
			Debug.Log("\t arg : " + arg);
		}
	}

	#region Events
	#region OnSendCommand
	public void AddOnSendCommand(SendCommand func)
	{
		OnSendCommand += func;
	}

	public void RemoveOnSendCommand(SendCommand func)
	{
		OnSendCommand -= func;
	}

	private void ResetOnSendCommand()
	{
		OnSendCommand = null;
	}

	private void InvokeOnSendCommand(string cmd, string[] args)
	{
		if (OnSendCommand != null)
			OnSendCommand(cmd, args);
	}
	#endregion

	#region OnSendCommand
	public void AddOnErrorCommand(ErrorCommand func)
	{
		OnErrorCommand += func;
	}

	public void RemoveOnErrorCommand(ErrorCommand func)
	{
		OnErrorCommand -= func;
	}

	private void ResetOnErrorCommand()
	{
		OnErrorCommand = null;
	}

	public void InvokeOnErrorCommand(string cmd)
	{
		if (OnErrorCommand != null)
			OnErrorCommand(cmd);
	}
	#endregion
	#endregion

}
