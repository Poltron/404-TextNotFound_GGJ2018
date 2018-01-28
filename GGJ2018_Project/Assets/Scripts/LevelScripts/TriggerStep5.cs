using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep5 : MonoBehaviour
{

	[SerializeField]
	AudioClip kinggoblinspeech;

    [SerializeField]
    CameraFocusZone focusZone;

	[SerializeField]
	private GameObject[] activeObject;

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
        if (kinggoblinspeech)
        {
            GameManager.Instance.DialogAudioSource.clip = kinggoblinspeech;
            GameManager.Instance.DialogAudioSource.Play();
        }

		foreach (GameObject obj in activeObject)
			obj.SetActive(true);
	}

    void Update()
    {
        if (hasBeenUsed)
        {
            if (int.Parse(activeObject[0].GetComponent<ObjectEntity>().GetValue("LIFE")) <= 0 || !activeObject[0].gameObject.activeInHierarchy)
            {
                StartCoroutine(DisableZone(focusZone));
            }
        }
    }

    IEnumerator DisableZone(CameraFocusZone zone)
    {
        yield return new WaitForSeconds(1.5f);
        zone.gameObject.SetActive(false);
    }
}
