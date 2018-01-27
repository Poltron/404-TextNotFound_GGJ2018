using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> levels;

    [SerializeField]
    private int activeLevel;

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

        GameManager.Instance.Player.DisableInput();

        levels[0].BeginLevel();
        activeLevel = 0;
    }

    public void NextLevel()
    {
        if (levels.Count + 1 == activeLevel)
        {
            Debug.LogError("NextLevel on last level in the list ?");
            return;
        }

        activeLevel++;
        
        levels[activeLevel].BeginLevel();
    }

    public void NextStep()
    {
        levels[activeLevel].NextStep();
    }
}
