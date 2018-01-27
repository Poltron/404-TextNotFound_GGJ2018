using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusZone : MonoBehaviour {

    [SerializeField]
    private Transform positionToHold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            CameraBehaviour camBehaviour = Camera.main.GetComponent<CameraBehaviour>();
            camBehaviour.EnableFocus(positionToHold);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            CameraBehaviour camBehaviour = Camera.main.GetComponent<CameraBehaviour>();
            camBehaviour.DisableFocus();
        }
    }
}
