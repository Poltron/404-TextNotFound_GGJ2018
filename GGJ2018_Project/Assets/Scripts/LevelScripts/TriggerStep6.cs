using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep6 : MonoBehaviour {

    bool hasBeenUsed;

    [SerializeField]
    AudioClip kingshowedup;

    [SerializeField]
    AudioClip isaidtheking;

    [SerializeField]
    AudioClip kingneeded;

    [SerializeField]
    float timeBetweenAudioClips;

    float timer;

    bool Active;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !hasBeenUsed)
        {
            hasBeenUsed = true;
            Do();
        }
    }

    public void Do()
    {
        GameManager.Instance.DialogAudioSource.clip = kingshowedup;
        GameManager.Instance.DialogAudioSource.Play();
    }

    public void Update()
    {

    }
}
