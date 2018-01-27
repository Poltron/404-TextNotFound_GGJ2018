using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CamState
{
	FollowingPlayer,
	Focused
}

public class CameraBehaviour : MonoBehaviour
{
	CamState state;
	private Transform focusedObject;

	[SerializeField]
	private float speedLerpToLeft;
	[SerializeField]
	private float speedLerpToRight;
	[SerializeField]
	private float speedLerpToCenter;
	[SerializeField]
	private float speedLerpToFocus;

	void Awake()
	{
		state = CamState.FollowingPlayer;
	}

	void FixedUpdate()
	{
		Vector3 newPos = new Vector3();
		if (state == CamState.FollowingPlayer)
		{
			if (GameManager.Instance.Player.cameraPointRight.activeInHierarchy)
			{
				newPos = Vector3.Lerp(transform.position, GameManager.Instance.Player.cameraPointRight.transform.position, speedLerpToRight * Time.deltaTime);
			}
			else if (GameManager.Instance.Player.cameraPointLeft.activeInHierarchy)
			{
				newPos = Vector3.Lerp(transform.position, GameManager.Instance.Player.cameraPointLeft.transform.position, speedLerpToLeft * Time.deltaTime);
			}
			else
			{
				newPos = Vector3.Lerp(transform.position, GameManager.Instance.Player.transform.position, speedLerpToCenter * Time.deltaTime);
			}
		}
		else if (state == CamState.Focused)
		{
			newPos = Vector3.Lerp(transform.position, focusedObject.position, speedLerpToFocus * Time.deltaTime);
		}

		//if(!GameManager.Instance.Player.IsOnGround())
		//{
		//	newPos.y = transform.position.y;
		//}

		newPos.y = transform.position.y;
		newPos.z = transform.position.z;
		transform.position = newPos;
	}

	public void EnableFocus(Transform focus)
	{
		focusedObject = focus;
		state = CamState.Focused;
	}

	public void DisableFocus()
	{
		state = CamState.FollowingPlayer;
	}
}
