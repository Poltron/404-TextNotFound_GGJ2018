﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEntity : MonoBehaviour
{
	[System.Serializable]
	private struct SValues
	{
		public string key;
		public string value;
	}

	[SerializeField]
	private string objName;
	[SerializeField]
	private List<SValues> inspectorValues;
	private Dictionary<string, string> values;

	private static int keyId;
	private ObjectCommand objCommand;

	private void Awake()
	{
		values = new Dictionary<string, string>();

		foreach (SValues v in inspectorValues)
		{
			values.Add(v.key + (keyId++), v.value);
		}
	}

	private void Start()
	{
		objCommand = FindObjectOfType<ObjectCommand>();
		if (objCommand == null)
			Destroy(gameObject);
	}

	private void OnBecameVisible()
	{
		objCommand.AddObject(this);
	}

	private void OnBecameInvisible()
	{
		objCommand.RemoveObject(this);
	}

	public string GetName()
	{
		return objName;
	}

	public bool HasKey(string key)
	{
		foreach (KeyValuePair<string, string> pair in values)
		{
			if (string.Equals(pair.Key, key, System.StringComparison.InvariantCultureIgnoreCase))
				return true;
		}
		return false;
	}

	public string GetTrueKey(string key)
	{
		foreach (KeyValuePair<string, string> pair in values)
		{
			string keyName = pair.Key.Substring(0, key.Length);
			if (string.Equals(keyName, key, System.StringComparison.InvariantCultureIgnoreCase))
				return pair.Key;
		}
		return null;
	}

	public string GetValue(string key)
	{
		foreach (KeyValuePair<string, string> pair in values)
		{
			string keyName = pair.Key.Substring(0, key.Length);
			if (string.Equals(keyName, key, System.StringComparison.InvariantCultureIgnoreCase))
				return pair.Value;
		}
		return null;
	}

	public bool SetValue(string key, string value)
	{
		foreach (KeyValuePair<string, string> pair in values)
		{
			if (!string.Equals(pair.Key, key, System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			values[pair.Key] = value;
			return true;
		}
		return false;
	}
}