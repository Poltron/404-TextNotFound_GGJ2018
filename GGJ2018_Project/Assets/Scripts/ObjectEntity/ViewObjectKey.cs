using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewObjectKey : MonoBehaviour
{
	[SerializeField]
	private string viewKey;
	[SerializeField]
	private Text textValue;
	private ObjectEntity obj;

	private bool isInit;

	private void Awake()
	{
		obj = gameObject.GetComponentInParent<ObjectEntity>();


		Canvas canvas = FindObjectOfType<Canvas>();
		textValue.transform.SetParent(canvas.transform);
		textValue.gameObject.name = "view key : " + viewKey;
	}

	private void Start()
	{
		viewKey = obj.GetTrueKey(viewKey);
		isInit = !string.IsNullOrEmpty(viewKey);
	}

	private void OnDestroy()
	{
		if (textValue)
			Destroy(textValue.gameObject);
	}

	private void Update()
	{
		textValue.text = isInit ? viewKey + " : " + obj.GetValue(viewKey) : "Error : viewKey not exist";
		textValue.transform.position = Camera.main.WorldToScreenPoint(transform.position);
	}
}
