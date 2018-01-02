using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad {

	public string nombreSpanish, nombreEnglish;

	public Sprite icono;

	public Habilidad(string nombreEnglish){

		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}

	public void Action(){
		
	}
}
