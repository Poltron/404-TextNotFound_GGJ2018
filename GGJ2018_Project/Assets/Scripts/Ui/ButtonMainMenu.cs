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

	protected override void Start()
	{
		background = transform.Find("Image").GetComponent<Image>();
		background.enabled = false;
		label = transform.Find("Text").GetComponent<Text>();
	}

	public override void OnSelect(BaseEventData eventData)
	{
		background.color = colors.highlightedColor;
		label.color = colors.highlightedColor;
		background.enabled = true;
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		background.color = colors.normalColor;
		label.color = colors.normalColor;
		background.enabled = false;
	}
}
