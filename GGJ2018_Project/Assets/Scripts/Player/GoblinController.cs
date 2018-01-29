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
	[SerializeField]
	private List<GameObject> hearts;

    [SerializeField]
    private AudioSource goblinDeathSound;

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

	[SerializeField]
	private GameObject fx_death;
	[SerializeField]
	private GameObject fx_hurth;
	[SerializeField]
	private ParticleSystem fx_durth;

	private bool isInputEnabled = true;

	private ConsoleWriter console;

	private void Start()
	{
		myTransform = transform;
		myRigidBody = gameObject.GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		gravity = defaulGravity;
		myAnimator.SetTrigger("Idle");

		NameSelector nameSelector = FindObjectOfType<NameSelector>();
		GetComponent<ObjectEntity>().SetName(nameSelector.GetRandom("GOBLIN"));

		console = FindObjectOfType<ConsoleWriter>();
		console.AddOnSendCommand(SetAlive);

		attackCollider.tag = "Attack";

		int result = Random.Range(0, 3);
		switch (result)
		{
			case 0:
				GetComponent<ObjectEntity>().SetValue("WEAPON", "FIST");
				weapon.GetComponent<Animator>().SetTrigger("SwitchToFist");
				GetComponentInChildren<PlayerWeapon>().SetWeapon(0);
				break;
			case 1:
				GetComponent<ObjectEntity>().SetValue("WEAPON", "SWORD");
				weapon.GetComponent<Animator>().SetTrigger("SwitchToSword");
				GetComponentInChildren<PlayerWeapon>().SetWeapon(1);
				break;
			case 2:
				GetComponent<ObjectEntity>().SetValue("WEAPON", "MACE");
				weapon.GetComponent<Animator>().SetTrigger("SwitchToMace");
				GetComponentInChildren<PlayerWeapon>().SetWeapon(2);
				break;
		}
	}

	private void OnDestroy()
	{
		console = FindObjectOfType<ConsoleWriter>();
		if (console)
			console.RemoveOnSendCommand(SetAlive);
		ResetOnGoblinDie();
	}

	private void HitBodyColor()
	{
		GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.white, Time.deltaTime * 5.0f);
	}


	private void Update()
	{
		HitBodyColor();

        if (isDead)
        {
            if (myRigidBody)
                myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);

			fx_durth.Stop();
            return;
        }


		Gravity();

		if (GameManager.Instance.Player.isDead)
			return;

		CheckFollowing();

		if (!isFollowing)
			return;
		else
			myAnimator.SetTrigger("Run");

		if (timerCooldown > 0)
			timerCooldown -= Time.deltaTime;

		Jump();
		Move();
		CheckAttack();
		Attack();

		if (fx_durth.isPlaying == false && myRigidBody.velocity.x != 0.0f)
			fx_durth.Play();
		if (myRigidBody.velocity.x == 0.0f)
			fx_durth.Stop();
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

			Vector3 moveDirection = weapon.transform.localPosition;
			moveDirection.x = -Mathf.Abs(moveDirection.x);
			weapon.transform.localPosition = moveDirection;


			attackCollider.transform.localPosition = new Vector3(gapAtkCol, 0, 0);

		}
		else
		{
			isMovingLeft = true;
			isMovingRight = false;
			mySpriteRenderer.flipX = true;
			weapon.GetComponent<SpriteRenderer>().flipX = true;
			Vector3 moveDirection = weapon.transform.localPosition;
			moveDirection.x = Mathf.Abs(moveDirection.x);
			weapon.transform.localPosition = moveDirection;
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
		{
			if (collision.transform.GetComponentInParent<GoblinController>())
				return;
			life -= collision.transform.parent.GetComponent<PlayerController>().GetComponentInChildren<PlayerWeapon>().GetCurrentWeapon().damage;

			AudioSource source = GetComponent<AudioSource>();
			if (source == null)
			{
				source = gameObject.AddComponent<AudioSource>();
			}
			source.clip = collision.transform.parent.GetComponent<PlayerController>().GetComponentInChildren<PlayerWeapon>().GetCurrentWeapon().touch;
			source.loop = false;
            source.volume = 0.1f;
            if (source.clip != null)
				source.Play();

			UpdateHearth();
		}

		if (life <= 0)
		{
            Instantiate(goblinDeathSound, transform.position, Quaternion.identity);
			isDead = true;
			myAnimator.SetTrigger("Dead");
			GetComponent<ObjectEntity>().SetValue("ISALIVE", "FALSE");
			GetComponent<Collider2D>().enabled = false;
			InvokeOnGoblinDie();
			Instantiate(fx_death, transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(fx_hurth, transform.position, Quaternion.identity);
		}
	}

	private void UpdateHearth()
	{
		int i = 0;
		foreach (GameObject heart in hearts)
		{
			if (i < life)
			{
				heart.SetActive(true);
			}
			else
			{
				heart.SetActive(false);
			}
			i++;
		}
	}

	public void SetAlive(string cmd, string[] args)
	{
		if (cmd != "SET" || args.Length != 2)
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
				UpdateHearth();
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