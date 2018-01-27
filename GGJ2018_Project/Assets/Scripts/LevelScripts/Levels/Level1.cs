using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    float actualTimer;

    bool isFinished;
    
    public override void BeginLevel() 
    {
        Debug.Log("BEGINNING LEVEL 1");
        actualTimer = 0.0f;
        steps[0].IsActiveStep = true;
        steps[0].BeginLevelStep();  
    }

    void Update()
    {
    }

    public override void EndLevel()
    {
        isFinished = true;
        Debug.Log("END LEVEL 1");
        GameManager.Instance.LevelManager.NextLevel();
    }
}
