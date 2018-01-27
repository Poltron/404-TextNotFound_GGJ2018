using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LevelStep : MonoBehaviour {

    [HideInInspector]
    public bool IsActiveStep;

    abstract public void BeginLevelStep();
    abstract public void EndLevelStep();

    private void Update()
    {
        if (IsActiveStep)
        {
            UpdateStep();
        }
    }

    abstract public void UpdateStep();

}
