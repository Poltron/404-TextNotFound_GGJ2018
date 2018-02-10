using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtEnd : MonoBehaviour
{
	private AudioSource audioListener;

	private void Start()
	{
		audioListener = gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!audioListener.isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
