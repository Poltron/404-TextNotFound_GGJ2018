using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paralaxe
{
	public class Trigger : MonoBehaviour
	{
		[SerializeField]
		private int idParalaxe;

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.layer != Data.Layer.Player)
				return;

			InvokeOnExitEvent(idParalaxe);
		}

		#region Events
		#region OnExit
		public delegate void OnExit(int idParalaxe);
		private event OnExit OnExitEvent;

		public void AddOnExitEvent(OnExit func)
		{
			OnExitEvent += func;
		}

		public void RemoveOnExitEvent(OnExit func)
		{
			OnExitEvent -= func;
		}

		private void ResetOnExitEvent()
		{
			OnExitEvent = null;
		}

		private void InvokeOnExitEvent(int idParalaxe)
		{
			if (OnExitEvent != null)
				OnExitEvent(idParalaxe);
		}
		#endregion
		#endregion
	}
}