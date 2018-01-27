using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Weapon
{
	public string name;
	public string trigger;
	public int damage;
	public int code;
}

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField]
	private List<Weapon> Weapons;

	private Weapon currentWeapon;

	private Animator myAnimator;

	private void Start()
	{
		myAnimator = GetComponent<Animator>();
		currentWeapon = Weapons[0];
	}

	public void SetWeapon(int code)
	{
		foreach(Weapon w in Weapons)
		{
			if(code == w.code)
			{
				myAnimator.SetTrigger(w.trigger);
				currentWeapon = w;
			}
		}
	}
}
