using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> levels;
    
    void Start()
    {

    }

	void Update()
    {

	}

    public void StartGame()
    {
        if (levels.Count < 1)
            return;

        levels[0].BeginLevel();
    }
}
