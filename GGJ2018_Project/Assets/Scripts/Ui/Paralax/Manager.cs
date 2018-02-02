using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paralaxe
{
	public class Manager : MonoBehaviour
	{
		[SerializeField]
		private Trigger[] triggers;

		[SerializeField]
		private int currentParalaxe;

		private void Awake()
		{
			triggers = FindObjectsOfType<Trigger>();
		}

		private void Start()
		{
			foreach (Trigger trigger in triggers)
			{
				trigger.AddOnExitEvent(SetParalaxe);
			}
		}

		private void OnDestroy()
		{
			foreach (Trigger trigger in triggers)
			{
				trigger.RemoveOnExitEvent(SetParalaxe);
			}
		}

		private void SetParalaxe(int idParalaxe)
		{
			Debug.Log("New paralaxe : " + idParalaxe);
			currentParalaxe = idParalaxe;
		}
	}
}