using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {

	public List<float> recursos = new List<float>();
	BaseDatos baseDatos;


	void Awake(){	
		baseDatos = GameObject.Find ("Controlador").GetComponent<BaseDatos> ();
	}

	void Start(){

		for (int i = 0; i < baseDatos.recursos.Count; i++) {
			recursos.Add (0f);
		}

	}
}
