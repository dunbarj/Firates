using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;
	private GUITexture tex;

	void Awake() {
		tex = GetComponent<GUITexture> ();
		tex.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}

	void Update() {
		if (sceneStarting) {
			StartScene ();
		}
	}

	void FadeToClear() {
		tex.color = Color.Lerp (tex.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void StartScene() {
		FadeToClear ();

		if (tex.color.a < 0.01f) {
			tex.color = Color.clear;
			tex.enabled = false;

			sceneStarting = false;
		}
	}
}
