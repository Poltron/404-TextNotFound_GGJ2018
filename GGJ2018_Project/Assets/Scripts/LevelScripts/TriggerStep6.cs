using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep6 : MonoBehaviour {

    bool hasBeenUsed;
    bool kingSpeechTriggered;

    [SerializeField]
    CameraFocusZone camFocusZone;

    [SerializeField]
    List<AudioClip> clips;
    
    [SerializeField]
    AudioClip kingspeech;

    [SerializeField]
    float timeBetweenAudioClips;

    float timer;

    int actualIndex;

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
        GameManager.Instance.DialogAudioSource.clip = clips[0];
        GameManager.Instance.DialogAudioSource.Play();
    }

    public void Update()
    {
        if (!kingSpeechTriggered)
        {
            if (hasBeenUsed)
            {
                timer += Time.deltaTime;

                if (timer > timeBetweenAudioClips)
                {
                    timer = 0.0f;

                    actualIndex++;
                    if (actualIndex == clips.Count)
                    {
                        actualIndex = 0;
                    }

                    GameManager.Instance.DialogAudioSource.clip = clips[actualIndex];
                    GameManager.Instance.DialogAudioSource.Play();
                }
            }
            foreach (ObjectEntity entity in GameManager.Instance.GetComponent<ObjectCommand>().visibleObjects)
            {
                if (entity.GetName() == "KING")
                {
                    GameManager.Instance.DialogAudioSource.clip = kingspeech;
                    GameManager.Instance.DialogAudioSource.Play();

                    StartCoroutine(DepopCamzone(camFocusZone));

                    kingSpeechTriggered = true;
                    break;
                }
            }
        }
    }

    IEnumerator DepopCamzone(CameraFocusZone zone)
    {
        yield return new WaitForSeconds(2.5f);

        zone.gameObject.SetActive(false);
    }
}
