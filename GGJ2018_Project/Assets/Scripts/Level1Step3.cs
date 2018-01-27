using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject blackscreen = GameObject.Find("BlackScreen");
        SpriteRenderer spriteRenderer = blackscreen.GetComponent<SpriteRenderer>();

        yield return new WaitForSeconds(timeBeforeFadeFromBlack);

        for (float f = timeForBlackScreenFade; f >0 ; f -= Time.deltaTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, f);
            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

        GameManager.Instance.Player.EnableInput();
    }
}
