using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public Control control;

	public int ancho, alto;

	void Awake(){
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("size:" + ancho + "," + alto);
		for (int i = (int)(transform.position.x - ancho / 2); i < (int)(transform.position.x + ancho / 2); i++) {
			for (int j = (int)(transform.position.y - alto / 2); j <(int)(transform.position.y + alto / 2); j++) {
				control.grid [i,j].bloqueado = true;
			}
		}

	}

}
