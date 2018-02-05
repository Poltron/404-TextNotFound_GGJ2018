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
		private Data[] datas;

		[SerializeField]
		private Data currentData;

		#region Paralaxe gameobjects
		[SerializeField]
		private Entity left;
		[SerializeField]
		private Entity right;
		[SerializeField]
		private Entity center;
		#endregion

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
			if (currentData != null)
			{
				SetParalaxe(currentData.Id);
			}
			else if (datas[0] != null)
			{
				SetParalaxe(datas[0].Id);
			}
			else
			{
				Debug.LogError("CROTTE il y a pas de paralaxe...prendre RDV avec le level designer");
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
			if (currentData != null && idParalaxe == currentData.Id)
				return;

			foreach (Data data in datas)
			{
				if (data.Id == idParalaxe)
				{
					left.SetLayers(data);
					right.SetLayers(data);
					center.SetLayers(data);
					currentData = data; 
					break;
				}
			}
		}
	}
}