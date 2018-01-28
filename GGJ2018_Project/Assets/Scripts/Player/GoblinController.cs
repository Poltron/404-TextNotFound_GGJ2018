using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
	#region isMovingRight
	[SerializeField]
	private bool isMovingRight = false;

	public bool GetIsMovingRight()
	{
		return isMovingRight;
	}
	#endregion
	#region isMovingLeft
	[SerializeField]
	private bool isMovingLeft = false;

	public bool GetIsMovingLeft()
	{
		return isMovingLeft;
	}
	#endregion
	#region isJumping
	private bool isKeyJump = false;

	public bool GetIsJumping()
	{
		return isKeyJump;
	}
	#endregion
	#region isAttacking
	private bool isKeyAttack = false;

	public bool GetIsAttacking()
	{
		return isKeyAttack;
	}
	#endregion
	#region Jump
	[Header("Jump")]
	[SerializeField]
	private float jumpHeight;
	[SerializeField]
	private float jumpTime;
	#endregion

	[Header("Variables")]
	[SerializeField]
	private List<Transform> feets;
	[SerializeField]
	private float speed;
	[SerializeField]
	private GameObject attackCollider;
	[SerializeField]
	private GameObject weapon;

	private float defaulGravity = 9.81f;
	private Transform myTransform;
	private Rigidbody2D myRigidBody;
	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private float gravity;
	private bool justGrounded = false;
	private bool isOnGround;

	[SerializeField]
	private float distanceAttack;
	public int distanceFollow;
	private bool isFollowing;
	private bool isAttack;
	[SerializeField]
	private float cooldownAttack;
	private float timerCooldown;

	[SerializeField]
	private float gapAtkCol;

	[SerializeField]
	private int life;

	public bool isDead;

	private bool isInputEnabled = true;

	private ConsoleWriter console;

	private PlayerController player;

	private void Start()
	{
		myTransform = transform;
		myRigidBody = gameObject.GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		gravity = defaulGravity;
		myAnimator.SetTrigger("Idle");

		console = FindObjectOfType<ConsoleWriter>();
		console.AddOnSendCommand(SetAlive);

		player = FindObjectOfType<PlayerController>();

		attackCollider.tag = "Attack";

	}

	private void OnDestroy()
	{
		console.RemoveOnSendCommand(SetAlive);
		ResetOnGoblinDie();
	}

	private void Update()
	{
		if (player.isDead)
			return;

		CheckFollowing();

        if (isDead)
        {
            if (myRigidBody)
                myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
            return;
        }

		if (!isFollowing)
			return;
		else
			myAnimator.SetTrigger("Run");

		if (timerCooldown > 0)
			timerCooldown -= Time.deltaTime;

		Gravity();
		Jump();
		Move();
		CheckAttack();
		Attack();
	}

	public void Attack()
	{
		if (isAttack && timerCooldown <= 0)
		{
			attackCollider.SetActive(true);
			StartCoroutine(WaitForFrame());
			weapon.GetComponent<Animator>().SetTrigger("Attack");
			isAttack = false;
			timerCooldown = cooldownAttack;
		}
	}

	IEnumerator WaitForFrame()
	{
		yield return null;
		attackCollider.SetActive(false);
	}

	public void Move()
	{
		if (GameManager.Instance.Player.transform.position.x > transform.position.x)
		{
			isMovingLeft = false;
			isMovingRight = true;
			mySpriteRenderer.flipX = false;
			weapon.GetComponent<SpriteRenderer>().flipX = false;
			attackCollider.transform.localPosition = new Vector3(gapAtkCol, 0, 0);

		}
		else
		{
			isMovingLeft = true;
			isMovingRight = false;
			mySpriteRenderer.flipX = true;
			weapon.GetComponent<SpriteRenderer>().flipX = true;
			attackCollider.transform.localPosition = new Vector3(-gapAtkCol, 0, 0);
		}

		if (isAttack)
		{
			isMovingRight = false;
			isMovingLeft = false;
		}

		if (isMovingRight)
		{
			Vector3 moveDirection = myRigidBody.velocity;

			moveDirection.x = 1.0f * speed * 100f * Time.deltaTime;

			myRigidBody.velocity = moveDirection;
			return;
		}
		else if (isMovingLeft)
		{
			Vector3 moveDirection = myRigidBody.velocity;

			moveDirection.x = -1.0f * speed * 100f * Time.deltaTime;

			myRigidBody.velocity = moveDirection;
			return;
		}
		else
		{
			myRigidBody.velocity = new Vector2(0.0f, myRigidBody.velocity.y);
		}
	}

	public void CheckFollowing()
	{
		if (GameManager.Instance.Player == null)
			return;

		isFollowing = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= distanceFollow;
	}

	public void CheckAttack()
	{
		isAttack = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= distanceAttack;
	}

	public void Gravity()
	{
		Vector3 moveDirection = myRigidBody.velocity;

		moveDirection.y -= gravity * Time.deltaTime;
		myRigidBody.velocity = moveDirection;
	}

	public void Jump()
	{
		if (IsOnGround())
		{
			if (isKeyJump)
			{
				Vector3 moveDirection = myRigidBody.velocity;

				moveDirection.y = JumpForce(jumpHeight, jumpTime);
				myRigidBody.velocity = moveDirection;
			}
		}
	}

	public float JumpForce(float height, float time)
	{
		gravity = CalculeGravity(height, time);
		return (4.0f * height) / time;
	}

	private float CalculeGravity(float height, float time)
	{
		return (8.0f * height) / (time * time);
	}

	public bool IsOnGround()
	{
		foreach (var trans in feets)
		{
			RaycastHit2D[] ray = Physics2D.RaycastAll(trans.position, -Vector2.up, 0.1f);

			foreach (RaycastHit2D r in ray)
			{
				if (r.transform.tag == "Platform")
				{
					if (justGrounded == false && isOnGround == false)
						justGrounded = true;
					else
						justGrounded = false;
					isOnGround = true;
					return true;
				}
			}
		}
		isOnGround = false;
		return false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Attack")
			--life;

		if (life <= 0)
		{
			isDead = true;
			myAnimator.SetTrigger("Dead");
			GetComponent<ObjectEntity>().SetValue("ISALIVE", "FALSE");
			GetComponent<Collider2D>().enabled = false;
			InvokeOnGoblinDie();
		}
	}

	public void SetAlive(string cmd, string[] args)
	{
		if (cmd != "SET")
			return;
		if (args[0] == "ISALIVE" && args[1] == "TRUE")
		{
			if (isFollowing)
			{
				Debug.Log(gameObject.name + " revive");
				isDead = false;
				life = 3;
				myAnimator.SetTrigger("Idle");
				GetComponent<Collider2D>().enabled = true;
				;
			}
		}
	}

	#region Events
	#region OnSendCommand
	public delegate void GoblinDie();
	private event GoblinDie OnGoblinDie;
	public void AddOnGoblinDie(GoblinDie func)
	{
		OnGoblinDie += func;
	}

	public void RemoveOnGoblinDie(GoblinDie func)
	{
		OnGoblinDie -= func;
	}

	private void ResetOnGoblinDie()
	{
		OnGoblinDie = null;
	}

	public void InvokeOnGoblinDie()
	{
		if (OnGoblinDie != null)
			OnGoblinDie();
	}
	#endregion
	#endregion
}