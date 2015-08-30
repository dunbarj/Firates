using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	[HideInInspector] public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter () {
		anim.SetBool ("Hover", true);
	}

	void OnMouseExit () {
		anim.SetBool ("Hover", false);
	}
}
