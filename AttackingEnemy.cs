using UnityEngine;
using System.Collections;

public class AttackingEnemy : MonoBehaviour {
	[HideInInspector] Rigidbody2D rb2d;
	[HideInInspector] Animator anim;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.05f;
	public LayerMask whatIsGround;
	
	public GameObject player;
	
	public float maxSpeed = 2f;
	public float jumpForce = 150f;
	public int health = 60;
	[HideInInspector] public float timeSinceLastHit = 1;
	
	private bool surprised = false;
	private bool facingRight = true;
	private float move = 0;
	private float timeSinceLastSurprise = 1;
	
	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("surprised", false);
		anim.SetBool ("facingRight", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("grounded", grounded);
		
		timeSinceLastHit += Time.deltaTime;
		timeSinceLastSurprise += Time.deltaTime;
		
		if (Mathf.Abs (player.transform.position.x - this.transform.position.x) < 1.5f) {
			Surprised ();
		} else {
			surprised = false;
			anim.SetBool("surprised", false);
			move = 0f;
			anim.SetFloat ("Speed", 0);
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}
		
		if (move > 0) {
			anim.SetBool ("Right", true);
		} else if (move < 0) {
			anim.SetBool ("Right", false);
		}
		
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}
	
	void Surprised () {
		if (!surprised && (timeSinceLastSurprise > 1f)) {
			surprised = true;
			timeSinceLastSurprise = 0;
			anim.SetBool("surprised", true);
			rb2d.AddForce(new Vector2 (0, 150f));
		} else if (grounded) {
			if (this.transform.position.x > player.transform.position.x) {
				move = -.25f;
			}
			else {
				move = .25f;
			}
			anim.SetFloat ("Speed", Mathf.Abs (move));
			rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		}
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
