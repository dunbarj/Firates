using UnityEngine;
using System.Collections;

public class LogoScreen : MonoBehaviour {

	public float playTime = 6f;
	private float waitTime = 0f;
	private AudioSource audio;
	private float pan = -1f;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		waitTime += Time.deltaTime;
		if (pan < 1) {
			pan += (Time.deltaTime);
			audio.panStereo = pan;
		}
		if (waitTime > playTime) {
			Application.LoadLevel("Main Menu");
		}
	}
}
