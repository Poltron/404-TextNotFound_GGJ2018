using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Level : MonoBehaviour {

    [SerializeField]
    private List<Transform> gameObjects;

    [SerializeField]
    private List<LevelStep> steps;

    abstract public void BeginLevel();
    abstract public void EndLevel();
}
