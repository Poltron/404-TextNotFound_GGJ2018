using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region isMovingRight
	private bool isMovingRight = false;

	public bool GetIsMovingRight()
	{
		return isMovingRight;
	}
	#endregion
	#region isMovingLeft
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
	#region Key
	[Header("Keys")]
	[SerializeField]
	private KeyCode keyRight;
	[SerializeField]
	private KeyCode keyLeft;
	[SerializeField]
	private KeyCode keyJump;
	[SerializeField]
	private KeyCode keyAttack;
	#endregion
	#region Jump
	[Header("Jump")]
	[SerializeField]
	private float jumpHeight;
	[SerializeField]
	private float jumpTime;
	#endregion
	#region Parallax
	[Header("Parallax")]
	[SerializeField]
	private List<RectTransform> backgroundRect;
	[SerializeField]
	private List<float> backgroundSpeed;
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
	private GameObject particleSystemShotgunRight;
	[SerializeField]
	private GameObject particleSystemShotgunLeft;

	[SerializeField]
	private AudioSource jumpSound;
	[SerializeField]
	private AudioSource jumpLandSound;

	private float defaulGravity = 9.81f;
	private Transform myTransform;
	private Rigidbody2D myRigidBody;
	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private HeartContainer myHeartContainer;
	private float gravity;
	private bool justGrounded = false;
	private bool isOnGround;

	public GameObject cameraPointRight;
	public GameObject cameraPointLeft;

	private ConsoleWriter console;

	[SerializeField]
	private float gapAtkCol;
	[SerializeField]
	private int life;
	public bool isDead;

	[SerializeField]
	private float cooldownAttack;
	private float timerAttackCooldown;

	[Header("isInvincible")]
	[SerializeField]
	private float cooldownInvincibility;
	private float timerInvincibilityCooldown;
	[SerializeField]
	private bool isInvincible;

	[Header("FSX")]
	[SerializeField]
	private GameObject fx_death;
	[SerializeField]
	private GameObject fx_hurth;
	[SerializeField]
	private ParticleSystem fx_durth;

	private bool isInputEnabled = true;

	private void Awake()
	{
		myTransform = transform;
		myRigidBody = gameObject.GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		gravity = defaulGravity;
	}

	private void Start()
	{
		console = FindObjectOfType<ConsoleWriter>();
		console.AddOnSendCommand(SetAlive);
		weapon.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;

		myHeartContainer = HeartContainer.GetHeartContainer();

		attackCollider.tag = "Attack";
		fx_durth.Stop();

		justGrounded = true;
				timerInvincibilityCooldown = cooldownInvincibility;
	}

	private void OnDestroy()
	{
		if (console)
			console.RemoveOnSendCommand(SetAlive);
	}

	private void HitBodyColor()
	{
		GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.white, Time.deltaTime * 5.0f);
	}

	private void Update()
	{
		HitBodyColor();

		if (timerAttackCooldown > 0)
			timerAttackCooldown -= Time.deltaTime;
		if (isInvincible &&timerInvincibilityCooldown > 0.0f)
			timerInvincibilityCooldown -= Time.deltaTime;

		isInvincible = timerInvincibilityCooldown > 0.0f;

		myHeartContainer.SetHeart(life);

		if (isDead)
		{
			if (fx_durth.isPlaying)
				fx_durth.Stop();
			return;
		}


		KeyUpdate();
		Attack();
		HasJustGrounded();
		Animation();


		if (!isOnGround)
			fx_durth.Stop();
		else if (fx_durth.isPlaying == false && myRigidBody.velocity.x != 0.0f)
			fx_durth.Play();
		if (myRigidBody.velocity.x == 0.0f)
			fx_durth.Stop();

	}

	private void FixedUpdate()
	{
		if (isDead)
		{
			if (myRigidBody)
				myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);

			return;
		}

		Gravity();
		Move();
		Jump();
		ClampPlayerInCam();
	}

	public void KeyUpdate()
	{
		if (!isInputEnabled)
			return;

		if (Input.GetKey(keyRight))
			isMovingRight = true;
		else
			isMovingRight = false;

		if (Input.GetKey(keyLeft))
			isMovingLeft = true;
		else
			isMovingLeft = false;

		if (Input.GetKeyDown(keyJump))
			isKeyJump = true;
		else
			isKeyJump = false;

		if (Input.GetKeyDown(keyAttack) || Input.GetKeyDown(KeyCode.LeftControl))
			isKeyAttack = true;
		else
			isKeyAttack = false;
	}

	public void Gravity()
	{
		Vector3 moveDirection = myRigidBody.velocity;

		moveDirection.y -= gravity * Time.deltaTime;
		myRigidBody.velocity = moveDirection;
	}

	public void Move()
	{
		if (isMovingLeft)
		{
			Vector3 moveDirection = myRigidBody.velocity;

			moveDirection.x = -1.0f * speed * 100f * Time.deltaTime;

			myRigidBody.velocity = moveDirection;

			moveDirection = weapon.transform.localPosition;
			moveDirection.x = -Mathf.Abs(moveDirection.x);
			weapon.transform.localPosition = moveDirection;

			cameraPointRight.SetActive(false);
			cameraPointLeft.SetActive(true);


			/*backgroundRect[0].transform.Translate(new Vector2(-backgroundSpeed[0] * Time.deltaTime, 0));
			backgroundRect[1].transform.Translate(new Vector2(-backgroundSpeed[1] * Time.deltaTime, 0));
			backgroundRect[2].transform.Translate(new Vector2(-backgroundSpeed[2] * Time.deltaTime, 0)); */

			return;
		}
		else if (isMovingRight)
		{
			Vector3 moveDirection = myRigidBody.velocity;

			moveDirection.x = 1.0f * speed * 100f * Time.deltaTime;

			myRigidBody.velocity = moveDirection;

			moveDirection = weapon.transform.localPosition;
			moveDirection.x = Mathf.Abs(moveDirection.x);
			weapon.transform.localPosition = moveDirection;

			cameraPointRight.SetActive(true);
			cameraPointLeft.SetActive(false);

			/*backgroundRect[0].transform.Translate(new Vector2(backgroundSpeed[0] * Time.deltaTime, 0));
			backgroundRect[1].transform.Translate(new Vector2(backgroundSpeed[1] * Time.deltaTime, 0));
			backgroundRect[2].transform.Translate(new Vector2(backgroundSpeed[2] * Time.deltaTime, 0));*/

			return;
		}
		else
		{
			myRigidBody.velocity = new Vector2(0.0f, myRigidBody.velocity.y);
			/*cameraPointRight.SetActive(false);
			cameraPointLeft.SetActive(false);*/
		}
	}

	public void Jump()
	{
		if (IsOnGround())
		{
			if (isKeyJump)
			{
				Vector3 moveDirection = myRigidBody.velocity;

				Instantiate(jumpSound, myTransform.position, Quaternion.identity);
				moveDirection.y = JumpForce(jumpHeight, jumpTime);
				myRigidBody.velocity = moveDirection;
			}
		}
	}

	public void Attack()
	{
		if (isKeyAttack && timerAttackCooldown <= 0)
		{
			attackCollider.SetActive(true);
			StartCoroutine(WaitForFrame());
			timerAttackCooldown = cooldownAttack;
			weapon.GetComponent<Animator>().SetTrigger("Attack");
			weapon.GetComponent<PlayerWeapon>().Attack();
			var PlayerWeapon = GetComponentInChildren<PlayerWeapon>();
			if (PlayerWeapon.GetCurrentWeapon().code == 3)
			{
				if (!mySpriteRenderer.flipX)
				{
					particleSystemShotgunRight.GetComponent<ParticleSystem>().Stop();
					particleSystemShotgunRight.GetComponent<ParticleSystem>().Play();
				}
				else
				{
					particleSystemShotgunLeft.GetComponent<ParticleSystem>().Stop();
					particleSystemShotgunLeft.GetComponent<ParticleSystem>().Play();
				}
			}
		}
	}

	public void ClampPlayerInCam()
	{
		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
		var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

		Vector3 playerSize = GetComponent<SpriteRenderer>().bounds.size;

		if (this.transform.position.y > bottomBorder - playerSize.y / 2)
		{
			gravity = defaulGravity;
			myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -1);
		}

		this.transform.position = new Vector3(
		Mathf.Clamp(this.transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
		Mathf.Clamp(this.transform.position.y, topBorder + playerSize.y / 2, bottomBorder - playerSize.y / 2),
		0
		);
	}

	IEnumerator WaitForFrame()
	{
		yield return null;
		attackCollider.SetActive(false);
	}

	public float JumpForce(float height, float time)
	{
		gravity = CalculeGravity(height, time);
		return (4.0f * height) / time;
	}

	public bool HasJustGrounded()
	{
		foreach (var trans in feets)
		{
			RaycastHit2D[] ray = Physics2D.RaycastAll(trans.position, -Vector2.up, 0.1f);

			foreach (RaycastHit2D r in ray)
			{
				if (r.transform.tag == "Platform")
				{
					if (justGrounded == false && isOnGround == false)
					{
						gravity = defaulGravity;
						justGrounded = true;
						Instantiate(jumpLandSound, myTransform.position, Quaternion.identity);
					}
					else
					{
						justGrounded = false;
					}
					isOnGround = true;
					return true;
				}
			}
		}
		isOnGround = false;
		return false;
	}

	public bool IsOnGround()
	{
		foreach (var trans in feets)
		{
			RaycastHit2D[] ray = Physics2D.RaycastAll(trans.position, -Vector2.up, 0.2f);

			foreach (RaycastHit2D r in ray)
			{
				if (r.transform.tag == "Platform")
				{
					return true;
				}
			}
		}
		return false;
	}

	private float CalculeGravity(float height, float time)
	{
		return (8.0f * height) / (time * time);
	}

	public void EnableInput()
	{
		isInputEnabled = true;
	}

	public void DisableInput()
	{
		isInputEnabled = false;
	}

	private void Animation()
	{
		if (!isInputEnabled)
			return;

		if (Input.GetKeyDown(keyJump))
		{
			myAnimator.SetTrigger("Jump");
		}
		if (((Input.GetKeyUp(keyRight) || Input.GetKeyUp(keyLeft)) && IsOnGround() && (!isMovingLeft && !isMovingRight))
			|| justGrounded && (!isMovingLeft && !isMovingRight))
		{
			myAnimator.SetTrigger("Idle");
		}
		if (((Input.GetKeyDown(keyRight) || Input.GetKeyDown(keyLeft)) && IsOnGround() && (isMovingLeft || isMovingRight))
			|| justGrounded && (isMovingLeft || isMovingRight))
		{
			myAnimator.SetTrigger("Run");
		}

		if (isMovingLeft)
		{
			mySpriteRenderer.flipX = true;
			weapon.GetComponent<SpriteRenderer>().flipX = true;
			attackCollider.transform.localPosition = new Vector3(-gapAtkCol, 0, 0);
		}
		else if (isMovingRight)
		{
			mySpriteRenderer.flipX = false;
			weapon.GetComponent<SpriteRenderer>().flipX = false;
			attackCollider.transform.localPosition = new Vector3(gapAtkCol, 0, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag != "Attack" || isInvincible)
			return;

		isInvincible = false;
		--life;

		life -= collision.transform.parent.GetComponent<GoblinController>().GetComponentInChildren<PlayerWeapon>().GetCurrentWeapon().damage;

		AudioSource source = GetComponent<AudioSource>();
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
		}
		source.clip = collision.transform.parent.GetComponent<GoblinController>().GetComponentInChildren<PlayerWeapon>().GetCurrentWeapon().touch;
		source.loop = false;
		source.volume = 0.1f;
		if (source.clip != null)
			source.Play();



		GetComponent<SpriteRenderer>().color = Color.red;

		if (life <= 0)
		{
			isDead = true;
			isInvincible = true;
			myAnimator.SetTrigger("Death");
			Instantiate(fx_death, transform.position, Quaternion.identity);
			GetComponent<ObjectEntity>().SetValue("ISALIVE", "FALSE");
		}
		else
			Instantiate(fx_hurth, transform.position, Quaternion.identity);
	}

	private IEnumerator FlashPlayer()
	{
		while (timerInvincibilityCooldown > 0.0f)
		{
			mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
			yield return new WaitForSeconds(0.25f);
		}
		mySpriteRenderer.enabled = true;
	}

	public void SetAlive(string cmd, string[] args)
	{
		if (cmd != "SET" || args.Length != 2)
			return;
		if (args[0] == "ISALIVE" && args[1] == "TRUE")
		{
			if (isDead)
			{
				timerInvincibilityCooldown = cooldownInvincibility;
				StartCoroutine(FlashPlayer());
				myAnimator.SetTrigger("Revive");
			}
			else
				myAnimator.SetTrigger("Idle");
			isDead = false;
			life = 5;
		}
		if (args[0] == "ISALIVE" && args[1] == "FALSE")
		{
			isDead = true;
			life = 0;
			myAnimator.SetTrigger("Death");
		}
		else if (args[0] == "JUMP")
		{
			float h = 0.0f;
			if (float.TryParse(args[1], out h))
			{
				if (h > 10.0f)
					h = 10.0f;
				jumpTime = jumpTime * h / jumpHeight;
				jumpHeight = h;
			}
		}
		else if (args[0] == "WEAPON")
		{
			var playerWeapon = GetComponentInChildren<PlayerWeapon>();
			foreach (Weapon w in playerWeapon.GetListWeapon())
			{
				if (string.Equals(w.name, args[1], System.StringComparison.InvariantCultureIgnoreCase))
				{
					GetComponent<ObjectEntity>().SetValue("WEAPON", args[1]);
					playerWeapon.GetComponent<Animator>().SetTrigger(w.trigger);
				}
			}
		}
	}
}
