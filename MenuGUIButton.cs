using UnityEngine;
using System.Collections;

public class MenuGUIButton : MonoBehaviour {

	public TextMesh textMesh;
	public Texture2D button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button (new Rect (10, 70, 100, 20), "Start")) {
			textMesh.text = "Started!";
		}
	}
}
