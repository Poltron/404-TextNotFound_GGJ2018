using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapColumn : MonoBehaviour
{
	[SerializeField]
	private Camera worldCamera;
	[SerializeField]
	private Transform contentText;
	[SerializeField]
	private Text textColumn;
	[SerializeField]
	private int nbrColumn;
	[SerializeField]
	private List<Transform> columns;
	[SerializeField]
	private Transform player;

	private void Awake()
	{
		DivideScreen();
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void DivideScreen()
	{
		Vector3 topLeft = worldCamera.ViewportToWorldPoint(Vector3.up);
		Vector3 topRight = worldCamera.ViewportToWorldPoint(Vector3.one);
		topLeft.z = 0.0f;
		topRight.z = 0.0f;

		float distance = Vector3.Distance(topLeft, topRight);
		float stepSize = distance / nbrColumn;

		Vector3 pos = topLeft;
		pos.x += stepSize * 0.5f;

		for (int i = 0 ; i < nbrColumn ; ++i)
		{
			GameObject newColumn = new GameObject("Column " + i);
			newColumn.transform.SetParent(worldCamera.transform);
			newColumn.transform.position = pos;

			Text textObj = Instantiate(textColumn, contentText);
			textObj.transform.position = worldCamera.WorldToScreenPoint(pos);
			textObj.text = (i + 1).ToString();

			if (i == nbrColumn - 1)
			{
				int childs = textObj.transform.childCount;
				for (int j = childs - 1 ; j >= 0 ; --j)
				{
					Destroy(textObj.transform.GetChild(j).gameObject);
				}
			}
			pos.x += stepSize;
			columns.Add(newColumn.transform);
		}
	}

	public Vector3 PositionColumn(int col)
	{
		if (col < 0 || col >= nbrColumn)
			return Vector3.forward;
		return columns[col].position;
	}

	public Vector3 GetColumnPlayer()
	{
		float distance = float.MaxValue;
		Vector3 pos = PositionColumn(0);

		foreach (Transform trans in columns)
		{
			float dist = Vector3.Distance(trans.position, player.position);
			if (dist > distance)
				continue;
			distance = dist;
			pos = trans.position;
		}
		return pos;
	}
}
