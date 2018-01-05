using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Habilidad {

	GameObject projectionGO, unitGO;


	public string[] nombre = new string[2];

	public Sprite icono;

	public Habilidad(string nombreEnglish){
		nombre [0] = nombreEnglish;
		icono = Resources.Load<Sprite> ("Sprites/Icons/" + nombreEnglish);
		projectionGO = Resources.Load<GameObject> ("Projection");
		unitGO = Resources.Load<GameObject> ("Unit");
	}


	public void Action(Seleccionable selec){
		if (nombre[0] == "Build") {
			Projection projection = MonoBehaviour.Instantiate (projectionGO).GetComponent<Projection>();
			projection.edificio = projection.control.gameObject.GetComponent<BaseDatos> ().searchObject ("Town Center") as Edificio;
			projection.selec = selec;
			selec.ocupado = true;
			projection.Actualizar ();
		}else if(nombre[0] == "Create Citizen"){
			selec.GetFronterizos ();
			if (selec.fronterizos.Count > 0) {
				GameObject aldeano = MonoBehaviour.Instantiate (unitGO, new Vector3 (selec.fronterizos [0].x, selec.fronterizos [0].y, 0), Quaternion.identity);
			}
		}
	}



}
