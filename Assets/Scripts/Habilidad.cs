using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Habilidad {

	public string[] nombre = new string[2];

	public Sprite icono;

	public Habilidad(string nombreEnglish){
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}


	public void Action(string action){
		if (action == "Build") {
			Debug.Log ("Construiría");
		}
	}
}
