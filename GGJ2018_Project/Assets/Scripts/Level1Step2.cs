using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Step2 : LevelStep
{
    [SerializeField]
    private AudioClip ifonlyhewas;

    public override void BeginLevelStep()
    {
        Debug.Log("Begin Level 1 Step 2");
        GameManager.Instance.DialogAudioSource.clip = ifonlyhewas;
        GameManager.Instance.DialogAudioSource.Play();
    }

    public override void UpdateStep()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndLevelStep();
        }
    }

    public override void EndLevelStep()
    {
        Debug.Log("End Level 1 Step 2");
        GameManager.Instance.LevelManager.NextStep();
    }
}
