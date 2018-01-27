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

	private void Awake()
	{
		obj = gameObject.GetComponentInParent<ObjectEntity>();
		Canvas canvas = FindObjectOfType<Canvas>();
		textValue.transform.SetParent(canvas.transform);
		textValue.gameObject.name = "view key : " + viewKey;
	}

	private void Update()
	{
		textValue.text = viewKey + " : " + obj.GetValue(viewKey).ToString();
		textValue.transform.position = Camera.main.WorldToScreenPoint(transform.position);
	}
}
