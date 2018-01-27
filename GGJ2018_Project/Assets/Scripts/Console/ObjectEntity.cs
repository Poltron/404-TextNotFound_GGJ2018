using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEntity : MonoBehaviour
{
	[SerializeField]
	private string objName;

	private ObjectCommand objCommand;

	private void Awake()
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
}
