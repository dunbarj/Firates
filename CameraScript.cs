using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject playerObject;
	public float smooth = 1.5f;

	private Transform player;
	[HideInInspector] public bool locked = false;

	// Use this for initialization
	void Awake () {
		player = playerObject.GetComponent <Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerObject.GetComponent<PirateController> ().immaculate && !locked) {
			transform.position = new Vector3 (playerObject.transform.position.x, playerObject.transform.position.y, -5);
			if (transform.position.y < -.55f) {
				transform.position = new Vector3 (transform.position.x, -1, -5);
			}
		}
	}
}
