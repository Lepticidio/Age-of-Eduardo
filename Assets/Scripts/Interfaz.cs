using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

	public float sizeButtonX, sizeButtonY, margenX, margenY, interfaceFraction;
	public Image icon;
	public Text nombre;
	public string idioma;
	public Transform PanelHabilidades;
	public Button DefaultButton;



	public void CreateHabilityButtons(Objeto obj){
		float xMax = sizeButtonX;
		float xMin= margenX;
		float yMax = margenY;
		float yMin = margenY - sizeButtonY;



		for (int i = 0; i < obj.habilidades.Count; i++) {
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax-= sizeButtonX;
			xMin-= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;
			
			}
			if (obj.habilidades [i] == null) {
				Debug.Log ("No hay habilidad");
			}
			boton.image.sprite = obj.habilidades [i].icono;


		}

	}
}
