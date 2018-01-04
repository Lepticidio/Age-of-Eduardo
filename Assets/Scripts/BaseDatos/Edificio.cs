using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edificio : Ficha {

	public Sprite sprite;


	public Edificio(string nombreEnglish ){
		nombre [0] = nombreEnglish;
		type = 2;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		sprite = Resources.Load<Sprite> ("Sprites/Buildings/" + nombreEnglish);
	}
}
