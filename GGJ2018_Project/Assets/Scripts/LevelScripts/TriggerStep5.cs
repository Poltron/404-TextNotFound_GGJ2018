using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep5 : MonoBehaviour {

    [SerializeField]
    AudioClip bridgemisplaced;

    bool hasBeenUsed;

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
        GameManager.Instance.DialogAudioSource.clip = bridgemisplaced;
        GameManager.Instance.DialogAudioSource.Play();
    }
}
