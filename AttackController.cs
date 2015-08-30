using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

	[HideInInspector] BoxCollider2D collider;

	public GameObject player;

	private Rigidbody2D enemyrb2d;

	// Use this for initialization
	void Start () {
		collider = GetComponent <BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy" && col.gameObject.GetComponent<EnemyController>().timeSinceLastHit > 0.2f) {
			enemyrb2d = col.GetComponent <Rigidbody2D> ();
			col.gameObject.GetComponent<EnemyController>().timeSinceLastHit = 0;
			col.gameObject.GetComponent<EnemyController>().health -= 20;
			int health = col.gameObject.GetComponent<EnemyController>().health;
			if (col.transform.position.x > player.transform.position.x) { 
				enemyrb2d.AddForce(new Vector2(50f, 50f));
				if (health <= 0) {
					enemyrb2d.rotation = -30;
				}
			}
			else if (col.transform.position.x < player.transform.position.x) {
				enemyrb2d.AddForce(new Vector2(-50f, 50f));
				if (health <= 0) {
					enemyrb2d.rotation = 30;
				}
			}
			if (col.gameObject.GetComponent<EnemyController>().health <= 0) {
				enemyrb2d.freezeRotation = false;
				col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				col.gameObject.GetComponent<CircleCollider2D>().enabled = false;
				player.GetComponent<PirateController>().kills++;
			}
		}
	}
}
