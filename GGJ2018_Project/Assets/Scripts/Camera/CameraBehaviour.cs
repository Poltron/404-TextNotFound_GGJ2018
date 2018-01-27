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

    void Awake()
    {
        state = CamState.FollowingPlayer;
    }
	
	void FixedUpdate ()
    {
	    if (state == CamState.FollowingPlayer)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, GameManager.Instance.Player.transform.position, Time.deltaTime);
            newPos.z = transform.position.z;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }	
        else if (state == CamState.Focused)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, focusedObject.position, Time.deltaTime);
            newPos.z = transform.position.z;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
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
