using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creativa : Habilidad {


	GameObject projectionGO, unitGO;
	Objeto objeto;
	public List<float> amounts = new List<float>();
	public List<Recurso> recursos = new List<Recurso>();

	public Creativa(string nombreEnglish, Objeto obj){
		type = 2;
		nombre [0] = nombreEnglish;
		icono = obj.icono;
		projectionGO = Resources.Load<GameObject> ("Projection");
		unitGO = Resources.Load<GameObject> ("Unit");
	}
	public void Action(Seleccionable selec){
		if (RecursosRequeridos (selec.jugador)) {
			if (nombre [0] == "Build") {
				Projection projection = MonoBehaviour.Instantiate (projectionGO).GetComponent<Projection> ();
				projection.edificio = projection.control.gameObject.GetComponent<BaseDatos> ().searchObject ("Town Center") as Edificio;
				projection.selec = selec;
				selec.ocupado = true;
				projection.Actualizar ();
			} else if (nombre [0] == "Create Citizen") {
				selec.GetFronterizos ();
				if (selec.fronterizos.Count > 0) {
					Seleccionable sele = MonoBehaviour.Instantiate (unitGO, new Vector3 (selec.fronterizos [0].x, selec.fronterizos [0].y, 0), Quaternion.identity).GetComponent<Seleccionable>();
					sele.jugador = selec.jugador;
				}
			}

			for (int i = 0; i < recursos.Count; i++) {
				selec.jugador.recursos [recursos [i].ID] -= amounts [i];
			}
		}
	}
	public bool RecursosRequeridos(Jugador jug){
		bool resultado = true;
		for (int i = 0; i < recursos.Count; i++) {
			if (jug.recursos [recursos [i].ID] < amounts [i]) {
				resultado = false;
			}
		}
		return resultado;
	}
}
