using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGoblin : MonoBehaviour
{
	[SerializeField]
	private ObjectCommand command;
	[SerializeField]
	private ObjectEntity entity;
	[SerializeField]
	private float intervalSpawn;
	private Rigidbody2D myRigidBody;
	private float gravity;
	private MapColumn map;
	private int column;

	private float currentTime;

	private void Start()
	{
		map = FindObjectOfType<MapColumn>();
		command = FindObjectOfType<ObjectCommand>();
		myRigidBody = GetComponent<Rigidbody2D>();
		NameSelector nameSelector = FindObjectOfType<NameSelector>();
		entity.SetName(nameSelector.GetRandom("BOSS"));

		gravity = 9.81f;

		Vector3 pos = transform.position;
		pos.x = map.transform.position.x;
		transform.position = pos;
		JumpTo(map.GetNbrColumn() - 1);
	}

	private void Update()
	{
		currentTime -= Time.deltaTime;
		if (currentTime < 0.0f)
		{
			currentTime = intervalSpawn;
			SpawnGoblin();
		}
		Gravity();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag != "Attack")
			return;
		string life = entity.GetValue("LIFE");
		int l = 0;
		if (int.TryParse(life, out l))
		{
			--l;
			entity.SetValue("LIFE", l.ToString());
		}
		JumpTo(column == 0 ? map.GetNbrColumn() - 1 : 0);
	}

	public void Gravity()
	{
		Vector3 moveDirection = myRigidBody.velocity;

		moveDirection.y -= gravity * Time.deltaTime;
		myRigidBody.velocity = moveDirection;
	}

	private void JumpTo(int column)
	{
		Vector3 pos = map.PositionColumn(column);
		transform.position = pos;
		this.column = column;
	}

	public void SpawnGoblin()
	{
		int columnSpawn = column == 0 ? 4 : 7;
		command.Add("ADD", new string[] { "GOBLIN", columnSpawn.ToString() });
	}
}
