using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : Ability {

	public float value;

	public Active(string nombreEnglish){
		type = 1;
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
	}
	public void Action(Seleccionable selec){
		if (nombre [0] == "Build") {
			if (selec.buildAmount < selec.maxBuildAmount - value) {
				selec.buildAmount += value;
				Building building = selec.entity as Building;
				selec.gameObject.GetComponent<SpriteRenderer> ().sprite = building.sprites [(int)(1 + (selec.buildAmount / selec.maxBuildAmount) * (building.sprites.Count - 1))];
			} else {
				selec.buildAmount = selec.maxBuildAmount;
				selec.construido = true;
				Building building = selec.entity as Building;
				selec.gameObject.GetComponent<SpriteRenderer> ().sprite = building.sprites [0];
			}
		}
		if (nombre [0] == "Create") {
			selec.productionAmount += value;
		}

	}
}
