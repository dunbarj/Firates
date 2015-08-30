using UnityEngine;
using System.Collections;

public class MapBottom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Enemy" || col.tag == "Box") {
			Destroy(col.gameObject);
		}
		if (col.tag == "Player") {
			col.gameObject.GetComponent<PirateController>().respawnPlayer();
		}
	}
}
