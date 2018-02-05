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

				if (nbLayers >= data.LayerLenght)
				{
					layers[nbLayers].gameObject.SetActive(false);
				}
				else
				{
					layers[nbLayers].gameObject.SetActive(true);
					layers[nbLayers].sprite = data.GetSprite(nbLayers);
				}
			}
			for (; nbLayers < data.LayerLenght; nbLayers++)
			{
				Image tmp = CreatePoolObject(data.GetSprite(nbLayers));
			}
		}

		private Image CreatePoolObject(Sprite sprite)
		{
			Image tmp = Instantiate(prefab, transform, false) as Image;
			tmp.sprite = sprite;
			layers.Add(tmp);
			return tmp;
		}

		public void MoveEntity (float speed)
		{

		}
	}
}