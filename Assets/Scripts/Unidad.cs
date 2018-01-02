using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad: Ficha {



	public Unidad(string nombreEnglish ){

		type = 1;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		if (icono == null)
			Debug.Log ("ha fracasado");
	}

}
