using UnityEngine;
using System.Collections;

public class FistAttackController : MonoBehaviour {
	
	[HideInInspector] BoxCollider2D collider;
	
	public GameObject enemy;
	
	private Rigidbody2D playerrb2d;
	
	// Use this for initialization
	void Start () {
		collider = GetComponent <BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && col.gameObject.GetComponent<PirateController>().timeSinceLastHit > 0.2f) {
			playerrb2d = col.GetComponent <Rigidbody2D> ();
			col.gameObject.GetComponent<PirateController>().timeSinceLastHit = 0;
			col.gameObject.GetComponent<PirateController>().health -= 20;
			int health = col.gameObject.GetComponent<PirateController>().health;
			if (col.transform.position.x > enemy.transform.position.x) { 
				playerrb2d.AddForce(new Vector2(100f, 100f));
				if (health <= 0) {
					playerrb2d.rotation = -30;
				}
			}
			else if (col.transform.position.x < enemy.transform.position.x) {
				playerrb2d.AddForce(new Vector2(-100f, 100f));
				if (health <= 0) {
					playerrb2d.rotation = 30;
				}
			}
			if (col.gameObject.GetComponent<PirateController>().health <= 0) {
				playerrb2d.freezeRotation = false;
				col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				col.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			}
		}
	}
}