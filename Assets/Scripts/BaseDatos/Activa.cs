using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activa : Habilidad {

	public float value;

	public Activa(string nombreEnglish){
		type = 1;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
	}
	public void Action(Seleccionable selec){
		if (nombre [0] == "Build") {
			if (selec.buildAmount < selec.maxBuildAmount - value) {
				selec.buildAmount += value;
				Edificio edificio = selec.objeto as Edificio;
				selec.gameObject.GetComponent<SpriteRenderer> ().sprite = edificio.sprites [(int)(1 + (selec.buildAmount / selec.maxBuildAmount) * (edificio.sprites.Count - 1))];
			} else {
				selec.buildAmount = selec.maxBuildAmount;
				selec.construido = true;
				Edificio edificio = selec.objeto as Edificio;
				selec.gameObject.GetComponent<SpriteRenderer> ().sprite = edificio.sprites [0];
			}
		}
		if (nombre [0] == "Create") {
			selec.productionAmount += value;
		}

	}
}
