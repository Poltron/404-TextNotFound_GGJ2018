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
	public AudioClip source;
	public AudioClip touch;
}

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField]
	private string key;
	[SerializeField]
	private List<Weapon> Weapons;
	public List<Weapon> GetListWeapon()
	{
		return Weapons;
	}
	private ObjectEntity entity;

	[SerializeField]
	private Weapon currentWeapon;
	public Weapon GetCurrentWeapon()
	{
		return currentWeapon;
	}

	private Animator myAnimator;
	private AudioSource sourceAudio;

	private void Start()
	{
		myAnimator = GetComponent<Animator>();
		entity = gameObject.GetComponentInParent<ObjectEntity>();
		currentWeapon = Weapons[0];
		sourceAudio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		string value = entity.GetValue(key);

		if (value == currentWeapon.name)
			return;

		if (value == "FIST")
			SetWeapon(0);
		else if (value == "SWORD")
			SetWeapon(1);
		else if (value == "MACE")
			SetWeapon(2);
		else if (value == "SHOTGUN")
		{
			SetWeapon(3);
		}
	}

	public void Attack()
	{
		sourceAudio.clip = currentWeapon.source;
		sourceAudio.loop = false;
		sourceAudio.Play();
	}

	public void SetWeapon(int code)
	{
		foreach (Weapon w in Weapons)
		{
			if (code == w.code)
			{
				myAnimator.SetTrigger(w.trigger);
				currentWeapon = w;
			}
		}
	}
}
