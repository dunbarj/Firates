using UnityEngine;
using System.Collections;

public class PirateController : MonoBehaviour {

	[HideInInspector] Rigidbody2D rb2d;
	[HideInInspector] Animator anim;
	public Camera cam;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.05f;
	public LayerMask whatIsGround;

	public float maxSpeed = 2f;
	public float jumpForce = 150f;
	public GameObject swordCollider;
	public int health = 1000;
	public GameObject winText;

	private bool respawn = false;
	private float respawnTime = 0f;
	private bool facingRight = true;
	private Vector2 initialPosition;
	private Vector2 spawnPoint;
	private Vector2 spawnPointBackup;
	private float spawnPointTime = 0f;
	private bool swords = false;
	private float attackTime;
	private float sheathTime;
	private float waitAttack = 0.3f;
	private float waitSheath = 1f;
	[HideInInspector] public int kills = 0;
	[HideInInspector] public bool immaculate = true;
	[HideInInspector] public float timeSinceLastHit = 1;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		initialPosition = transform.position;
		spawnPoint = initialPosition;
		attackTime = 0.3f;
		sheathTime = 1f;
		swordCollider.SetActive (false);
		winText.SetActive (false);
	}

	void Update() {
		if (!immaculate && !respawn) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.LoadLevel("Main Menu");
			}
			/*
			if (Input.GetKeyDown (KeyCode.R) || Input.GetButtonDown ("Reset")) {
				transform.position = initialPosition;
				rb2d.velocity = new Vector2 (0, 0);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				if (maxSpeed != 20f) {
					maxSpeed = 20f;
				} else {
					maxSpeed = 2f;
				}
			}
			*/
			if (grounded && (Input.GetKeyDown (KeyCode.Space) || (Input.GetButtonDown ("A")))) {
				anim.SetBool ("Ground", false);
				rb2d.AddForce (new Vector2 (0, jumpForce));
			}

			if (grounded && anim.GetBool ("Swords") && (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Attack"))) {
				anim.SetTrigger ("Attack");
				waitAttack = 0;
			}

			if (grounded && (Input.GetKeyDown (KeyCode.W) || Input.GetButtonDown ("Unsheath"))) {
				anim.SetTrigger ("Unsheath");
				waitSheath = 0;
				if (swords) {
					swords = false;
					anim.SetBool ("Swords", swords);
				} else {
					swords = true;
					anim.SetBool ("Swords", swords);
				}
			}
		} else if (immaculate) {
			updateImmaculate ();
		} else if (respawn) {
			respawnTime += Time.deltaTime;
			if (respawnTime > 2) {
				respawnTime = 0;
				respawn = false;
				rb2d.velocity = new Vector2 (0, 0);
				if (spawnPointTime > 3) {
					transform.position = spawnPoint;
				} else {
					transform.position = spawnPointBackup;
				}
			}
		}
		if (kills >= 50) {
			winText.SetActive(true);
		}
		health = 1000;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		timeSinceLastHit += Time.deltaTime;
		waitAttack += Time.deltaTime;
		waitSheath += Time.deltaTime;
		if (!respawn) {
			spawnPointTime += Time.deltaTime;
		}

		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rb2d.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		if (move > 0) {
			anim.SetBool ("Right", true);
		} else if (move < 0) {
			anim.SetBool ("Right", false);
		}

		anim.SetFloat ("Speed", move);
		anim.SetFloat ("AbsSpeed", Mathf.Abs (move));
	 
		if ((waitAttack > attackTime) && (waitSheath > sheathTime) && !immaculate) {
			rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		}

		if (spawnPointTime > 5) {
			spawnPointTime = 0;
			spawnPointBackup = spawnPoint;
			spawnPoint = new Vector2 (transform.position.x, transform.position.y + 0.2f);
		}
	}

	public void respawnPlayer() {
		respawn = true;
	}

	void updateImmaculate() {
		transform.Translate(new Vector2(0,-.01f));
		if (cam.transform.position.y != transform.position.y) {
			if (Mathf.Abs(transform.position.y - cam.transform.position.y) < .03f) {
				cam.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
			}
		}
		if (grounded) {
			immaculate = false;
			rb2d.isKinematic = false;
		}
	}

	void setColliderOn() {
		swordCollider.SetActive (true);
	}

	void setColliderOff() {
		swordCollider.SetActive (false);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
