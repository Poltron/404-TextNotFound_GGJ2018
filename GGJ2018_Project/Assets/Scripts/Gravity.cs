using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

	void Start()
    {
		
	}
	
	void FixedUpdate()
    {
        if (_rigidbody && !_rigidbody.isKinematic)
        {
            _rigidbody.velocity -= new Vector2(0, 9.81f * Time.fixedDeltaTime);
        }
	}
}
