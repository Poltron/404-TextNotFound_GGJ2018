using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMainMenu : Button
{
	[SerializeField]
	private Image background;
	[SerializeField]
	private Text label;

	[SerializeField]
	private Font bold;
	[SerializeField]
	private Font normal;

	protected override void Awake()
	{
		background = transform.Find("Image").GetComponent<Image>();
		background.transform.localScale = new Vector3(1,1,1);
		label = transform.Find("Text").GetComponent<Text>();
	}

	public override void OnSelect(BaseEventData eventData)
	{
		//background.color = colors.highlightedColor;
		label.color = colors.highlightedColor;
		background.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
		label.fontSize = 40;
		//label.font = bold;
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		//background.color = colors.normalColor;
		label.color = colors.normalColor;
		background.transform.localScale = new Vector3(1, 1, 1);
		label.fontSize = 30;
		//label.font = normal;
	}
}
