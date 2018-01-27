using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCommand : MonoBehaviour
{
	[SerializeField]
	private ConsoleWriter console;
	[SerializeField]
	private MapColumn mapColumn;
	[SerializeField]
	private ObjectEntity[] allObject;
	[SerializeField]
	private List<ObjectEntity> visibleObjects;

	private void Awake()
	{
		console.AddOnSendCommand(Add);
		console.AddOnSendCommand(Remove);
		console.AddOnSendCommand(Set);
		console.AddOnErrorCommand(ErrorCommand);
	}

	private void Add(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "ADD", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 2)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		foreach (ObjectEntity obj in allObject)
		{
			if (!string.Equals(obj.GetName(), args[0], System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			int column = 0;
			if (!int.TryParse(args[1], out column))
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			Vector3 position = mapColumn.PositionColumn(column - 1);
			if (position.z != 0.0f)
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			Instantiate(obj.gameObject, position, Quaternion.identity);
			return;
		}
		console.InvokeOnErrorCommand(cmd);
		return;
	}

	private void Remove(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "REMOVE", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 1)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		bool destroyed = false;
		for (int i = visibleObjects.Count - 1 ; i >= 0 ; --i)
		{
			ObjectEntity obj = visibleObjects[i];
			if (!string.Equals(obj.GetName(), args[0], System.StringComparison.InvariantCultureIgnoreCase))
				continue;

			Destroy(obj.gameObject);
			destroyed = true;
		}
		if (!destroyed)
			console.InvokeOnErrorCommand(cmd);
		return;
	}

	private void Set(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "SET", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 2)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		bool isSet = false;
		foreach (ObjectEntity obj in visibleObjects)
		{
			if (!obj.HasKey(args[0]))
				continue;
			int newValue = 0;
			if (!int.TryParse(args[1], out newValue))
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			isSet = obj.SetValue(args[0], args[1]);
			if (!isSet)
				break;
		}
		if (!isSet)
			console.InvokeOnErrorCommand(cmd);
	}

	private void ErrorCommand(string cmd)
	{
		//if (!string.Equals(cmd, "ADD", System.StringComparison.InvariantCultureIgnoreCase))
		//	return;

		foreach (ObjectEntity obj in allObject)
		{
			if (!string.Equals(obj.GetName(), "Cube", System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			int column = 0;

			Vector3 position = mapColumn.PositionColumn(column);
			Instantiate(obj.gameObject, position, Quaternion.identity);
			return;
		}
	}

	#region ListObject
	public void AddObject(ObjectEntity obj)
	{
		if (!visibleObjects.Contains(obj))
			visibleObjects.Add(obj);
	}

	public void RemoveObject(ObjectEntity obj)
	{
		if (visibleObjects.Contains(obj))
			visibleObjects.Remove(obj);
	}
	#endregion
}
