using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep4 : MonoBehaviour
{
    bool hasBeenUsed;

    [SerializeField]
    AudioClip fightBegin;

    [SerializeField]
    AudioClip fightEnd;

    [SerializeField]
    List<GameObject> goblins;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !hasBeenUsed)
        {
            hasBeenUsed = true;
            Begin();
        }
    }

    public void Begin()
    {
        GameManager.Instance.DialogAudioSource.clip = fightBegin;
        GameManager.Instance.DialogAudioSource.Play();
    }

    public void Update()
    {
        if (false) // all goblins are dead
        {
            // si tous les goblins sont tués
            SoundEndFight();
        }
    }

    public void SoundEndFight()
    {
        GameManager.Instance.DialogAudioSource.clip = fightEnd;
        GameManager.Instance.DialogAudioSource.Play();
    }
}
