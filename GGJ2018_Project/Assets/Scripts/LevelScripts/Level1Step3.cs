using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level1Step3 : LevelStep
{
    [SerializeField]
    private AudioClip alive;

    [SerializeField]
    private float timeBeforeFadeFromBlack;

    [SerializeField]
    private float timeForBlackScreenFade;


    public override void BeginLevelStep()
    {
        Debug.Log("Begin Level 1 Step 3");
        GameManager.Instance.DialogAudioSource.clip = alive;
        GameManager.Instance.DialogAudioSource.Play();

        GameManager.Instance.Player.GetComponent<Animator>().SetTrigger("Idle");

        StartCoroutine(FadeBlackScreen());
    }

    public override void UpdateStep()
    {

    }

    public override void EndLevelStep()
    {
        Debug.Log("End Level 1 Step 3");
    }

    private IEnumerator FadeBlackScreen()
    {
        GameObject blackscreen = GameObject.Find("InitialGameOverScreen");
		CanvasGroup grp = blackscreen.GetComponentInChildren<CanvasGroup>();

        yield return new WaitForSeconds(timeBeforeFadeFromBlack);

		for (float t = 0.0f ; t < 1.0f ; t += Time.deltaTime / timeForBlackScreenFade)
        {
			grp.alpha = Mathf.Lerp(1.0f, 0.0f, t);
			yield return new WaitForEndOfFrame();
        }
		grp.alpha = 0.0f;
        GameManager.Instance.Player.EnableInput();
    }


}
