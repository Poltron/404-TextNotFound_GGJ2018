using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Level : MonoBehaviour {

    [SerializeField]
    protected List<Transform> gameObjects;

    [SerializeField]
    protected List<LevelStep> steps;

    protected int actualStep;
    public int ActualStep { get { return actualStep; } }

    abstract public void BeginLevel();
    abstract public void EndLevel();

    public void NextStep()
    {
        if (actualStep + 1 == steps.Count)
        {
            Debug.LogError("NextStep when this is the final level step ?");
            return;
        }

        steps[actualStep].IsActiveStep = false;
        actualStep++;

        steps[actualStep].BeginLevelStep();
        steps[actualStep].IsActiveStep = true;
    }
}
