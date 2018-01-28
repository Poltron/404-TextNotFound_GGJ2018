using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
	[SerializeField]
	private float duration;
	[SerializeField]
	private Sprite sp;
	public bool isVisible;

	[SerializeField]
	private GameObject fx_death;

	IEnumerator UploadTimer(int minute, int second, bool kiss)
	{
		WWWForm form = new WWWForm();
		form.AddField("timerMinute", minute);
		form.AddField("timerSecond", second);
		form.AddField("kiss", kiss.ToString());

		using (var w = UnityWebRequest.Post("https://ggj2018.guillaume-paringaux.fr/index.php?/unity/addScore", form))
		{
			yield return w.SendWebRequest();
			if (w.isNetworkError || w.isHttpError)
			{
				print(w.error);
			}
			else
			{
				print("Finished Uploading Score");
				Debug.Log("WTEXT = " + w.downloadHandler.text);
			}
		}
	}

	public void Finish()
	{
		ScoreManager score = FindObjectOfType<ScoreManager>();
		StartCoroutine(UploadTimer(score.minute, score.seconde, true));
		StartCoroutine(Appear("Scene_Jeanweb"));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag != "Attack")
			return;
		ScoreManager score = FindObjectOfType<ScoreManager>();
		StartCoroutine(UploadTimer(score.minute, score.seconde, false));
		GetComponent<SpriteRenderer>().sprite = sp;
		StartCoroutine(Appear(SceneManager.GetActiveScene().name));
		Instantiate(fx_death, transform.position, Quaternion.identity);
	}

	private void OnBecameVisible()
	{
		isVisible = true;
	}

	private void OnBecameInvisible()
	{
		isVisible = false;
	}

	private IEnumerator Appear(string sceneToLoad)
	{
		GameObject blackscreen = GameObject.Find("InitialGameOverScreen");
		Vector3 pos = Camera.main.transform.position;
		pos.z = 0.0f;
		blackscreen.transform.position = pos;
		CanvasGroup grp = blackscreen.GetComponentInChildren<CanvasGroup>();
		GameManager.Instance.Player.GetComponent<Animator>().SetTrigger("Dead");

		for (float t = 0.0f ; t < 1.0f ; t += Time.deltaTime / duration)
		{
			grp.alpha = Mathf.Lerp(0.0f, 1.0f, t);
			yield return new WaitForEndOfFrame();
		}
		grp.alpha = 1.0f;
		GameManager.Instance.Player.DisableInput();

		yield return new WaitForSeconds(5.0f);

		SceneManager.LoadScene(sceneToLoad);
	}
}
