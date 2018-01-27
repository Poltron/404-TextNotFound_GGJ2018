using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [SerializeField]
    private Transform toFollow;
	
	void Update () {
        transform.position = new Vector3(toFollow.position.x, transform.position.y, transform.position.z);
    }
}
