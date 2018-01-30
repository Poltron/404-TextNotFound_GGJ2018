using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem particules;
	private ObjectEntity entity;

	void Start()
	{
		entity = GetComponent<ObjectEntity>();
	}

	void Update()
	{
		if (entity.GetValue("FIRE") == "ON" && particules.isPlaying == false)
		{
			particules.Play();
		}
		else if (entity.GetValue("FIRE") == "OFF" && particules.isPlaying)
		{
			particules.Stop();
		}
	}
}
