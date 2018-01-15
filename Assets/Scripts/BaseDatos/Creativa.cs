using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creativa : Habilidad {

	GameObject projectionGO, unitGO;
	Objeto objeto;
	public List<float> amounts = new List<float>();
	public List<Recurso> recursos = new List<Recurso>();

	public Creativa(string nombreEnglish, Objeto obj){
		objeto = obj;
		type = 2;
		nombre [0] = nombreEnglish;
		icono = obj.icono;
		projectionGO = Resources.Load<GameObject> ("Projection");
		unitGO = Resources.Load<GameObject> ("Unit");
	}
	public void Action(Seleccionable selec){
		if (RecursosRequeridos (selec.jugador)) {
			if (nombre [0] == "Build Town Center") {
				Projection projection = MonoBehaviour.Instantiate (projectionGO).GetComponent<Projection> ();
				projection.edificio = objeto as Edificio;
				projection.selec = selec;
				projection.creativa = this;
				selec.ocupado = true;
				projection.Actualizar ();
			} else if (nombre [0] == "Create Citizen") {
				selec.productionQueue.Add (this);
				if (selec.productionQueue.Count == 1) {
					selec.maxProductionAmount = (objeto as Ficha).productionTime;
				}
			}
		}
	}
	public void Work(Seleccionable selec){
		Ficha ficha = selec.objeto as Ficha;
		if (selec.productionAmount < selec.maxProductionAmount - ficha.productionRate) {
			selec.productionAmount += ficha.productionRate;

		} else {
			selec.GetFronterizos ();
			if (selec.fronterizos.Count > 0) {
				Seleccionable sele = MonoBehaviour.Instantiate (unitGO, new Vector3 (selec.fronterizos [0].x, selec.fronterizos [0].y, 0), Quaternion.identity).GetComponent<Seleccionable>();
				sele.jugador = selec.jugador;
				selec.productionQueue.RemoveAt (0);
				selec.productionAmount = 0;
				if (selec.productionQueue.Count > 0) {
					selec.maxProductionAmount = (selec.productionQueue[0].objeto as Ficha).productionTime;
				}
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
	public void GastarRecursos(Seleccionable selec){
		for (int i = 0; i < recursos.Count; i++) {
			selec.jugador.recursos [recursos [i].ID] -= amounts [i];
		}
	}
}
