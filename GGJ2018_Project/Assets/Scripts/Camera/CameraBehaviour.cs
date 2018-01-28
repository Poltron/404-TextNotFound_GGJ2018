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
	private float offsetY;
	[SerializeField]
	private float DistMaxY;

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

	private void Start()
	{
		
	}

	void FixedUpdate()
	{
		Vector3 newPos = new Vector3();
		if (state == CamState.FollowingPlayer)
		{
			if (GameManager.Instance.Player.cameraPointRight.activeInHierarchy)
			{
				newPos.x = Vector3.Lerp(transform.position, GameManager.Instance.Player.cameraPointRight.transform.position, speedLerpToRight * Time.deltaTime).x;
			}
			else if (GameManager.Instance.Player.cameraPointLeft.activeInHierarchy)
			{
				newPos.x = Vector3.Lerp(transform.position, GameManager.Instance.Player.cameraPointLeft.transform.position, speedLerpToLeft * Time.deltaTime).x;
			}
			else
			{
				newPos.x = Vector3.Lerp(transform.position, GameManager.Instance.Player.transform.position, speedLerpToCenter * Time.deltaTime).x;
			}

			if(GameManager.Instance.Player.IsOnGround())
			{
				newPos.y = Vector3.Lerp(transform.position, GameManager.Instance.Player.transform.position + new Vector3(0, offsetY, 0), speedLerpToCenter * Time.deltaTime).y;
			}
			else
			{
				Debug.Log("POOOOOOOOOOOOOOO = " + Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position));
				if(GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity.y > 0f)
				{
					newPos.y = transform.position.y;
				}
				else if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) >= DistMaxY)
				{
					newPos.y = GameManager.Instance.Player.transform.position.y;
				}
				else
				{
					newPos.y = transform.position.y;
				}
			}
		}
		else if (state == CamState.Focused)
		{
			newPos = Vector3.Lerp(transform.position, focusedObject.position, speedLerpToFocus * Time.deltaTime);
		}

		//newPos.y = transform.position.y;
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
