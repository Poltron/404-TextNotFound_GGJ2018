using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep4 : MonoBehaviour
{
    bool hasBeenUsed;

    [SerializeField]
    AudioClip fightBegin;

    /*[SerializeField]
    AudioClip fightEnd;*/

    [SerializeField]
    List<GameObject> goblins;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !hasBeenUsed)
        {
            hasBeenUsed = true;
            Begin();
			/*foreach (GameObject goblin in goblins)
			{
				goblin.GetComponent<GoblinController>().AddOnGoblinDie(CheckAllDie);
			}*/
        }
    }

	public void CheckAllDie()
	{
		bool isEnd = true;
		foreach (GameObject goblin in goblins)
		{
			if (!goblin.GetComponent<GoblinController>().isDead)
				isEnd = false;
		}

		if (isEnd)
			SoundEndFight();
	}

    public void Begin()
    {
		Debug.Log("Begin");
        GameManager.Instance.DialogAudioSource.clip = fightBegin;
        GameManager.Instance.DialogAudioSource.Play();
    }

    public void SoundEndFight()
    {
		Debug.Log("SoundEndFight");
		//GameManager.Instance.DialogAudioSource.clip = fightEnd;
        //GameManager.Instance.DialogAudioSource.Play();
    }
}
