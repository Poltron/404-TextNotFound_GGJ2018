using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCommand : MonoBehaviour
{
	[SerializeField]
	private ConsoleWriter console;
	[SerializeField]
	private MapColumn mapColumn;
	[SerializeField]
	private string[] cmdPrefix;
	[SerializeField]
	private ObjectEntity[] allObject;
	[SerializeField]
	public List<ObjectEntity> visibleObjects;

	[SerializeField]
	private AudioSource objAdded;
	[SerializeField]
	private AudioSource objRemoved;
	[SerializeField]
	private AudioSource valueChanged;

	[SerializeField]
	private GameObject fxDisparition;
	[SerializeField]
	private GameObject fxApparition;

	private void Awake()
	{
		console.AddOnSendCommand(Add);
		console.AddOnSendCommand(Remove);
		console.AddOnSendCommand(Set);
		console.AddOnSendCommand(SendCorrectCommand);
		console.AddOnSendCommand(Jeremie);
		console.AddOnSendCommand(Kiss);
		console.AddOnSendCommand(MoveTo);
		console.AddOnSendCommand(Quit);
		console.AddOnErrorCommand(ErrorCommand);
	}

	public void Add(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "ADD", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 2)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		Instantiate(objAdded, Camera.main.transform.position, Quaternion.identity);

		foreach (ObjectEntity obj in allObject)
		{
			if (!string.Equals(obj.GetName(), args[0], System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			int column = 0;
			if (!int.TryParse(args[1], out column))
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			Vector3 position = mapColumn.PositionColumn(column - 1);
			if (position.z != 0.0f)
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			Instantiate(obj.gameObject, position, Quaternion.identity);
			Instantiate(fxApparition, obj.transform.position, Quaternion.identity);
			return;
		}
		console.InvokeOnErrorCommand(cmd);
		return;
	}

	public void Remove(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "REMOVE", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 1)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}
		if (args[0] == "HERO")
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		bool destroyed = false;
		for (int i = visibleObjects.Count - 1 ; i >= 0 ; --i)
		{
			ObjectEntity obj = visibleObjects[i];
			if (!string.Equals(obj.GetName(), args[0], System.StringComparison.InvariantCultureIgnoreCase))
				continue;

			obj.gameObject.SetActive(false);
			Instantiate(fxDisparition, obj.transform.position, Quaternion.identity);
			destroyed = true;
		}
		Instantiate(objRemoved, Camera.main.transform.position, Quaternion.identity);
		if (!destroyed)
			console.InvokeOnErrorCommand(cmd);
		return;
	}

	public void Set(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "SET", System.StringComparison.InvariantCultureIgnoreCase))
			return;
		if (args.Length != 2)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		if (args[0] == "WEAPON")
		{
			Instantiate(valueChanged, Camera.main.transform.position, Quaternion.identity);
			return;
		}

		bool isSet = false;
		foreach (ObjectEntity obj in visibleObjects)
		{
			if (!obj.HasKey(args[0]))
				continue;

			isSet = obj.SetValue(args[0], args[1]);
			if (!isSet)
				break;
		}

		Instantiate(valueChanged, Camera.main.transform.position, Quaternion.identity);

		if (!isSet)
			console.InvokeOnErrorCommand(cmd);
	}

	public void Kiss(string cmd, string[] args)
	{
		if (cmd != "KISS")
			return;
		if (args.Length != 1)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		if (args[0] == "PRINCESS")
		{
			GameObject princess = GameObject.FindGameObjectWithTag("Princess");
			if (princess == null)
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			ObjectEntity entity = princess.GetComponent<ObjectEntity>();
			if (!visibleObjects.Contains(entity))
			{
				console.InvokeOnErrorCommand(cmd);
				return;
			}
			EndGame end = princess.GetComponent<EndGame>();
			if (end.isVisible)
				end.Finish();
		}
		else
		{
			ErrorCommand("");
			ErrorCommand("");
			ErrorCommand("");
			ErrorCommand("");
			console.InvokeOnErrorCommand(cmd);
		}
	}

	public void MoveTo(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "MOVETO", System.StringComparison.InvariantCultureIgnoreCase))
			return;

		foreach (Transform t in FindObjectsOfType<Transform>())
		{
			if (!string.Equals(t.name, args[0], System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			Vector3 pos = t.position;
			GameManager.Instance.Player.transform.position = t.position;
			pos.z = -10.0f;
			Camera.main.transform.position = pos;
			return;
		}

	}

	public void Quit(string cmd, string[] args)
	{
		if (!string.Equals(cmd, "QUIT", System.StringComparison.InvariantCultureIgnoreCase))
			return;

		if (args.Length != 1)
		{
			console.InvokeOnErrorCommand(cmd);
			return;
		}

		if (args[0] == "MENU")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Jeanweb");
		}
		else if (args[0] == "GAME")
			Application.Quit();
	}


	private void SendCorrectCommand(string cmd, string[] args)
	{
		foreach (string s in cmdPrefix)
		{
			if (s == cmd)
				return;
		}
		console.InvokeOnErrorCommand(cmd);
	}

	private void ErrorCommand(string cmd)
	{
		//if (!string.Equals(cmd, "ADD", System.StringComparison.InvariantCultureIgnoreCase))
		//	return;
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectEntity>().GetValue("ALIVE") == "FALSE")
			return;

		foreach (ObjectEntity obj in allObject)
		{
			if (obj == null || !string.Equals(obj.GetName(), "GOBLIN", System.StringComparison.InvariantCultureIgnoreCase))
				continue;
			int[] pos = new int[] { 0, 1, 2, 7, 8, 9 };
			Vector3 position = mapColumn.PositionColumn(pos[Random.Range(0, pos.Length)]);
			Instantiate(obj.gameObject, position, Quaternion.identity);
			return;
		}
	}

	private void Jeremie(string cmd, string[] args)
	{
		if (cmd != "JEREMIE")
			return;

		ObjectEntity[] allEntity = FindObjectsOfType<ObjectEntity>();

		foreach (ObjectEntity obj in allEntity)
		{
			obj.SetAll("ISSOU");
		}
	}

	#region ListObject
	public void AddObject(ObjectEntity obj)
	{
		if (!visibleObjects.Contains(obj))
			visibleObjects.Add(obj);
	}

	public void RemoveObject(ObjectEntity obj)
	{
		if (visibleObjects.Contains(obj))
			visibleObjects.Remove(obj);
	}
	#endregion
}
