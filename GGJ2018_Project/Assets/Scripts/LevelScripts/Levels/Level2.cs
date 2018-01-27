using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level
{
    float t;
    bool isFinished;

    void Start()
    {

    }

    public override void BeginLevel()
    {
        Debug.Log("BEGINNING LEVEL 2");
    }

    void Update()
    {
        if (t > 5.0f && !isFinished)
        {
            Debug.Log("UPDATE LEVEL 2");
            EndLevel();
        }

        t += Time.deltaTime;
    }

    public override void EndLevel()
    {
        isFinished = true;
        Debug.Log("END LEVEL 2");
    }
}
