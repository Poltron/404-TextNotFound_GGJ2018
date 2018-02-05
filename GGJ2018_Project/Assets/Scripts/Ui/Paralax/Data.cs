using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Paralaxe
{
	[CreateAssetMenu(fileName = "Paralaxe", menuName = "Paralaxe/Data", order = 1)]
	public class Data : ScriptableObject
	{
		[System.Serializable]
		public struct Layer
		{
			[SerializeField]
			public string name;
			[SerializeField]
			public Sprite sprite;
			[SerializeField]
			public float speed;
		}

		[SerializeField]
		private Layer[] layers;
		public int LayerLenght { get { return layers.Length; } }

		[SerializeField]
		private int _id;
		public int Id
		{
			get
			{
				return _id;
			}
		}

		public Sprite GetSprite (int index)
		{
			return layers[index].sprite;
		}
	}
}