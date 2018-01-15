using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour {

	public Camera myCamera;
	bool posible;
	public Edificio edificio;
	SpriteRenderer sr;
	public Control control;
	public GameObject edificioGO;
	public Seleccionable selec;
	public Creativa creativa;
	public Jugador jugador;

	void Awake(){
		control = GameObject.Find ("Controlador").GetComponent<Control> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		myCamera = Camera.main;
	}


	// Update is called once per frame
	void Update () {


		transform.position = myCamera.ScreenToWorldPoint (Input.mousePosition);

		if (posible && !CheckPosition ()) {
			mostrarImposible ();
		} else if (!posible && CheckPosition ()) {
			mostrarPosible ();
		} else if (!posible && !CheckPosition ()) {
			mostrarImposible ();
		}

		if(Input.GetMouseButtonUp(0)||Input.GetKey(KeyCode.Escape)){
			selec.ocupado = false;
			Destroy(gameObject);
		}

		if(Input.GetMouseButtonUp(1)&&posible){
			selec.ocupado = false;
			GameObject edi = Instantiate (control.edificio, transform.position,Quaternion.identity);
			edi.GetComponent<Obstacle> ().objeto = edificio;
			Seleccionable sel = edi.GetComponent<Seleccionable> ();
			sel.objeto = edificio;
			sel.maxBuildAmount = edificio.productionTime;
			sel.construido = false;
			sel.jugador = jugador;
			sel.GetComponent<SpriteRenderer> ().sprite = edificio.sprites [1];
			creativa.GastarRecursos (selec);
			selec.Invoke ("Destinar", 0.05f);
			Destroy(gameObject);
		}



	}


	bool CheckPosition(){
		bool resultado = true;
		for (int i = (int)(transform.position.x - edificio.ancho / 2); i < (int)(transform.position.x + edificio.ancho / 2); i++) {
			for (int j = (int)(transform.position.y - edificio.alto / 2); j <(int)(transform.position.y + edificio.alto / 2); j++) {
				if (i<0||j<0||i>=control.ancho||j>=control.alto ||control.grid [i, j].bloqueado) {
					resultado = false;
				}
			}
		}
		return resultado;
	}


	void mostrarPosible(){
		sr.color = new Color(0,1,0,0.5f);
		posible = true;
	}

	void mostrarImposible(){
		sr.color = new Color(1,0,0,0.5f);
		posible = false;
	}

	public void Actualizar (){
		sr.sprite = edificio.sprites[0];
	}
}
