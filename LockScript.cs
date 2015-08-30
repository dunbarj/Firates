using UnityEngine;
using System.Collections;

public class LockScript : MonoBehaviour {

	public Camera cam;
	public GameObject player;
	public GameObject enemyPrefab;
	public int killCap = 0;
	public int spawnCap = 5;
	public float areaLimit = 1.8f;
	public float spawnHeight = 1f;

	private bool triggered = false;
	private bool done = false;
	private float spawnTime = 4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered && !done) {
			spawnTime += Time.deltaTime;
			if (Mathf.Abs(player.transform.position.x - transform.position.x) > 1.8f) {
				if (player.transform.position.x > transform.position.x) {
					player.transform.position = (new Vector3(transform.position.x + 1.8f, player.transform.position.y, 0));
				}
				else {
					player.transform.position = (new Vector3(transform.position.x - 1.8f, player.transform.position.y, 0));
				}
			}
			if (player.GetComponent<PirateController> ().kills < killCap && spawnTime > spawnCap) {
				spawnTime = 0;
				spawnPirate ();
			}
			else if (player.GetComponent<PirateController>().kills >= killCap){
				done = true;
				cam.gameObject.GetComponent<CameraScript>().locked = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player" && !triggered) {
			triggered = true;
			cam.gameObject.GetComponent<CameraScript>().locked = true;
		}
	}

	void spawnPirate() {
		int randx = Random.Range (-1, 1);
		Vector3 pos = new Vector3 (transform.position.x + randx, spawnHeight, 0);
		GameObject newEnemy = (GameObject) Instantiate(enemyPrefab, pos, Quaternion.identity);
		newEnemy.GetComponent<EnemyController> ().player = player;
	}
}
