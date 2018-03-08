using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Token {
	public List<Sprite> sprites = new List<Sprite>();

	public Building(string nombreEnglish, int nImage){
		nombre [0] = nombreEnglish;
		type = 2;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		for (int i = 0; i < nImage; i++) {
			sprites.Add(Resources.Load<Sprite> ("Sprites/Buildings/"+nombreEnglish+" "+i));
		}
	}
}
