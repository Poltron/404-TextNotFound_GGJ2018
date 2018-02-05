using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Paralaxe
{
	public class Entity : MonoBehaviour
	{
		[SerializeField]
		private List<Image> layers;

		[SerializeField]
		private Image prefab;

		public void SetLayers(Data data)
		{
			int nbLayers = 0;
			for (nbLayers = 0; nbLayers < layers.Count; ++nbLayers)
			{
				Debug.Log(layers[nbLayers].name);
			}
			for (; nbLayers < data.LayerLenght; nbLayers++)
			{
				Image tmp = CreatePoolObject();
				Debug.Log(layers[nbLayers].name);
			}
		}

		private Image CreatePoolObject()
		{
			Image tmp = Instantiate(prefab, transform, true) as Image;
			layers.Add(tmp);
			return tmp;
		}
	}
}