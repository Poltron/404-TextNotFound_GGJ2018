using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1 : Level
{
    float actualTimer;

    bool isFinished;
    
    public override void BeginLevel() 
    {
        Debug.Log("BEGINNING LEVEL 1");
        actualTimer = 0.0f;
        steps[0].IsActiveStep = true;
        steps[0].BeginLevelStep();
		StartCoroutine(GenerateUniqueId());
	}

    void Update()
    {

    }

    public override void EndLevel()
    {
        isFinished = true;
        Debug.Log("END LEVEL 1");
        GameManager.Instance.LevelManager.NextLevel();
	}

	IEnumerator GenerateUniqueId()
	{
		using (var w = UnityWebRequest.Get("https://ggj2018.guillaume-paringaux.fr/unity/getUniqueId"))
		{
			yield return w.SendWebRequest();
			if (w.isNetworkError || w.isHttpError)
			{
				print(w.error);
			}
			else
			{
				Debug.Log("ServeurId : " + w.downloadHandler.text);
				PlayerPrefs.SetString("ServerId", w.downloadHandler.text);
			}
		}
	}
}
