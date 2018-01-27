using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Step1 : LevelStep
{
    [SerializeField]
    float timeBeforeBeginningLevel;

    float actualTimer;

    public override void BeginLevelStep()
    {
        Debug.Log("Begin Level 1 Step 1");
        actualTimer = timeBeforeBeginningLevel;
    }

    public override void UpdateStep()
    {
        if (actualTimer < 0)
        {
            EndLevelStep();
        }

        actualTimer -= Time.deltaTime;
    }

    public override void EndLevelStep()
    {
        Debug.Log("End Level 1 Step 1");
        GameManager.Instance.LevelManager.NextStep();
    }
}
