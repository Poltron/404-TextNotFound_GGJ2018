using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
	[SerializeField]
	private float duration;

	public void Finish()
	{
		StartCoroutine(Appear("Scene_Jeanweb"));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag != "Attack")
			return;
		StartCoroutine(Appear(SceneManager.GetActiveScene().name));
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
		GameManager.Instance.Player.EnableInput();

		yield return new WaitForSeconds(5.0f);
		
		SceneManager.LoadScene(sceneToLoad);
	}
}
