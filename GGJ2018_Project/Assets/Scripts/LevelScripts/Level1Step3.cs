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
		Image image = blackscreen.transform.Find("Image").GetComponent<Image>();
        Text text = blackscreen.GetComponentInChildren<Text>();

        yield return new WaitForSeconds(timeBeforeFadeFromBlack);

        for (float f = timeForBlackScreenFade; f >0 ; f -= Time.deltaTime)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, f);
            yield return new WaitForEndOfFrame();
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        GameManager.Instance.Player.EnableInput();
    }


}
