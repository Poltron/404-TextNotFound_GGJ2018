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
	private string key;
	[SerializeField]
	private List<Weapon> Weapons;
	private ObjectEntity entity;

	private Weapon currentWeapon;

	private Animator myAnimator;

	private string currentWeaponCode;

	private void Start()
	{
		myAnimator = GetComponent<Animator>();
		entity = gameObject.GetComponentInParent<ObjectEntity>();
		currentWeapon = Weapons[0];
	}

	private void Update()
	{
		string value = entity.GetValue(key);

		if (value == currentWeaponCode)
			return;

		if (value == "Fist")
			SetWeapon(0);
		else if (value == "Sword")
			SetWeapon(1);
		else if (value == "Mace")
			SetWeapon(2);
		else if (value == "Shotgun")
			SetWeapon(3);
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
