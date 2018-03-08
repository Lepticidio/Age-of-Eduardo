﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : Entity{

	public Decoration(string nombreEnglish ){
		nombre [0] = nombreEnglish;
		type = 3;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);

	}
}
