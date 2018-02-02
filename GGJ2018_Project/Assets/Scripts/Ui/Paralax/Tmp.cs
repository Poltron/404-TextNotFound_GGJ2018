using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paralaxe
{
	public class Tmp : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKey(KeyCode.D))
			{
				transform.Translate(Vector3.right * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.Q))
			{
				transform.Translate(Vector3.left * Time.deltaTime);
			}
		}
	}
}