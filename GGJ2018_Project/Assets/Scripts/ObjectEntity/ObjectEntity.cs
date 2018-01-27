using System.Collections;
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

	private ObjectCommand objCommand;

	private void Awake()
	{
		objCommand = FindObjectOfType<ObjectCommand>();
		if (objCommand == null)
			Destroy(gameObject);
		values = new Dictionary<string, string>();
	}

	private void Start()
	{
		foreach (SValues v in inspectorValues)
		{
			values.Add(v.key, v.value);
		}
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
		return values.ContainsKey(key);
	}

	public string GetValue(string key)
	{
		if (!values.ContainsKey(key))
			return null;
		return values[key];
	}

	public bool SetValue(string key, string value)
	{
		if (!values.ContainsKey(key))
			return false;
		values[key] = value;
		return true;
	}
}
