using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : MonoBehaviour {

	private static HeartContainer instance;

	public static HeartContainer GetHeartContainer() { return instance; }

	public List<GameObject> hearts;

	private void Awake()
	{
		instance = this;
	}

	public void SetHeart(int life)
	{
		int i = 0;
		foreach(GameObject heart in hearts)
		{
			if(i < life)
			{
				heart.SetActive(true);
			}
			else
			{
				heart.SetActive(false);
			}
			i++;
		}
	}
}
