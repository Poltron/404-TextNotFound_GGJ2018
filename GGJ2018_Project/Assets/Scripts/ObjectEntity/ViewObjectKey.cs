using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewObjectKey : MonoBehaviour
{
	private class Viewer
	{
		public string key;
		public Text textContent;
	}

	[SerializeField]
	private string[] viewKey;
	[SerializeField]
	private Text textValue;
	private List<Viewer> allViewer;
	private Viewer nameEntity;

	private ObjectEntity parentEntity;
	private RectTransform trans;

	private void Awake()
	{
		parentEntity = gameObject.GetComponentInParent<ObjectEntity>();
		allViewer = new List<Viewer>();
	}

	private void Start()
	{
		GameObject contentViewer = GameObject.FindGameObjectWithTag("VariableViewer");
		transform.SetParent(contentViewer.transform);
		gameObject.name = "view object : " + parentEntity.name;
		trans = gameObject.GetComponent<RectTransform>();
		trans.anchorMin = new Vector2(0.5f, 0.0f);
		trans.anchorMax = new Vector2(0.5f, 1.0f);
		trans.localScale = Vector3.one;
		trans.pivot = new Vector2(0.5f, 1.0f);
		trans.rotation = Quaternion.identity;

		Viewer newViewer = new Viewer();
		nameEntity = newViewer;
		nameEntity.textContent = Instantiate(textValue);
		nameEntity.textContent.transform.SetParent(transform);
		nameEntity.textContent.transform.localScale = Vector3.one;
		nameEntity.textContent.text = parentEntity.GetName();
		nameEntity.textContent.transform.rotation = Quaternion.identity;

		foreach (string key in viewKey)
		{
			newViewer = new Viewer();
			newViewer.key = parentEntity.GetTrueKey(key);
			if (string.IsNullOrEmpty(newViewer.key))
				continue;
			newViewer.textContent = Instantiate(textValue);
			newViewer.textContent.transform.SetParent(transform);
			newViewer.textContent.transform.localScale = Vector3.one;

			allViewer.Add(newViewer);
		}
	}

	private void Update()
	{
		GetComponent<Image>().enabled = parentEntity.gameObject.activeSelf;
		foreach (Viewer v in allViewer)
		{
			v.textContent.gameObject.SetActive(parentEntity.gameObject.activeSelf);
		}
		nameEntity.textContent.gameObject.SetActive(parentEntity.gameObject.activeSelf);

		nameEntity.textContent.text = parentEntity.GetName();

		Vector3 newPos;
        if (parentEntity.GetComponent<KingGoblin>())
            newPos = Camera.main.WorldToScreenPoint(parentEntity.transform.position - Vector3.up * 2.0f);
        else if (parentEntity.GetName() == "BRIDGE")
            newPos = Camera.main.WorldToScreenPoint(parentEntity.transform.position);
        else
            newPos = Camera.main.WorldToScreenPoint(parentEntity.transform.position - Vector3.up);
		transform.position = newPos;
		transform.rotation = Quaternion.identity;

		//trans.offsetMin = new Vector2(trans.offsetMin.x, 0.0f);
		//trans.offsetMax = new Vector2(trans.offsetMax.x, 0.0f);

		foreach (Viewer v in allViewer)
		{
			v.textContent.text = v.key + " : " + parentEntity.GetValue(v.key);
			v.textContent.transform.rotation = Quaternion.identity;
		}
	}
}
