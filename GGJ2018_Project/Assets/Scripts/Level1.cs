using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    float t;
    bool isFinished;

    void Start ()
    {
		
	}

    public override void BeginLevel() 
    {
        Debug.Log("BEGINNING");
    }

    void Update()
    {
        if (t > 5.0f && !isFinished)
        {
            Debug.Log("UPDATE");
            EndLevel();
        }

        t += Time.deltaTime;
    }

    public override void EndLevel()
    {
        isFinished = true;
        Debug.Log("END");
    }
}
