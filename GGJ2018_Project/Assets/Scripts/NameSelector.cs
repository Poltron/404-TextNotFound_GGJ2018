using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSelector : MonoBehaviour
{
	[System.Serializable]
	private struct SName
	{
		public string categorie;
		public string[] allNames;
	}

	[SerializeField]
	private SName[] allNames;

	public string GetRandom(string cat)
	{
		foreach (SName c in allNames)
		{
			if (c.categorie != cat)
				continue;
			if (c.allNames.Length < 1)
				return "404#PASDENOM";
			return c.allNames[Random.Range(0, c.allNames.Length)];
		}
		return "404#PASDENOM";
	}
}
