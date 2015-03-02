using UnityEngine;
using System.Collections;

public static class Initiate {

	public static void Fade(string scene, Color col, float damp)
	{
		GameObject init = new GameObject();
		init.name = "Fader";
		init.AddComponent<Fader> ();
		Fader afade = init.GetComponent<Fader>();
		afade.fadeDamp = damp;
		afade.fadeScene = scene;
		afade.fadeColor = col;
		afade.start = true;

	}
}