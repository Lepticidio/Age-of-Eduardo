using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad: Ficha {



	public Unidad(string nombreEnglish ){
		nombre [0] = nombreEnglish;
		type = 1;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}

}
