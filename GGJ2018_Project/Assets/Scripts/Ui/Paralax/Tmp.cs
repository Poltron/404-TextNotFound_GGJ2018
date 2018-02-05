using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paralaxe
{
	public class Tmp : MonoBehaviour
	{
		[SerializeField]
		public Paralaxe.Manager paralaxe;

		private void Start()
		{
			paralaxe = FindObjectOfType<Paralaxe.Manager>();
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.E))
			{
				transform.Translate(Vector3.right * Time.deltaTime);
				paralaxe.MoveParalaxe(1);
			}
			else if (Input.GetKey(KeyCode.Z))
			{
				transform.Translate(Vector3.left * Time.deltaTime);
				paralaxe.MoveParalaxe(-1);
			}

			if (Input.GetKey(KeyCode.R))
			{
				transform.Translate(Vector3.right * Time.deltaTime * 2);
				paralaxe.MoveParalaxe(2);
			}
			else if (Input.GetKey(KeyCode.A))
			{
				transform.Translate(Vector3.left * Time.deltaTime * 2);
				paralaxe.MoveParalaxe(-2);
			}
		}
	}
}