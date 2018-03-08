using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSource : Entity {

	public Gathering gathering;

	public ResourceSource(string nombreEnglish, Gathering gather){
		gathering = gather;
		nombre [0] = nombreEnglish;
		type = 4;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}
}
