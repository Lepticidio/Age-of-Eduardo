using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

	bool iniciada;
	public bool cleared = true;
	public int language;
	public float sizeButtonX, sizeButtonY, margenX, margenY, sizePanRecursoX, sizePanRecursoY, margenXRecurso, margenYRecurso, interfaceFraction;
	public Image icon, healthBar;
	public Text nombre;
	public Transform PanelHabilidades, PanelRecursos;
	public GameObject genPanRecurso;
	public Button DefaultButton;
	public List<Seleccionable> selecs = new List<Seleccionable>();
	public Jugador jugador;
	public BaseDatos baseDatos;
	public List<Text> textosRecursos = new List<Text>();


	/* 
	 * CHARLA SERIA: 
	 * Para la selección múltiple tengo que ordenar los selecs por "prioridad".
	 * Esta será mayor en unidades especiales con habilidades, como ciudadanos,
	 * y entre las unidades normales cuanto más fuerte sea más "prioridad".	 * 
	 * 
	 *
	 * */

	void Awake(){
		baseDatos= gameObject.GetComponent<BaseDatos> ();
	}

	void Start(){

		CreateResourcePanels ();
	}

	void Update(){
		ActualizarRecursos ();
		if (!cleared && selecs.Count == 0) {
			ClearButtons ();
		}
		if (selecs.Count > 0) {
			UpdateHealth ();
		}
	}




	public void CreateHabilityButtons(){
		ClearButtons ();
		nombre.text = selecs[0].objeto.nombre[language];
		icon.sprite = selecs[0].objeto.icono;
		float xMax = sizeButtonX;
		float xMin= margenX;
		float yMax = margenY;
		float yMin = margenY - sizeButtonY;
		for (int i = 0; i < selecs[0].objeto.creativas.Count; i++) {
			Creativa creativa = selecs[0].objeto.creativas [i];
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizeButtonX;
			xMin+= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;
			
			}
			boton.image.sprite = creativa.icono;
			boton.onClick.AddListener(delegate{creativa.Action(selecs[0]);});


		}
		for (int i = 0; i < selecs[0].objeto.activas.Count; i++) {
			Activa activa = selecs[0].objeto.activas [i];
			Button boton = (Button)Instantiate (DefaultButton,PanelHabilidades);
			RectTransform rtrans = boton.transform as RectTransform;
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizeButtonX;
			xMin+= sizeButtonX;
			if (xMax > 1) {
				xMax = sizeButtonX;
				xMin= margenX;
				yMax -= sizeButtonY;
				yMin -= sizeButtonY;

			}
			boton.image.sprite = activa.icono;
			boton.onClick.AddListener(delegate{activa.Action(selecs[0]);});


		}
		cleared = false;

	}

	public void CreateResourcePanels(){		
		float xMax = sizePanRecursoX;
		float xMin= margenXRecurso;
		float yMax = margenYRecurso;
		float yMin = margenYRecurso - sizePanRecursoY;
		for (int i = 0; i < jugador.recursos.Count; i++) {
			GameObject panRecurso = Instantiate (genPanRecurso,PanelRecursos);
			RectTransform rtrans = panRecurso.transform as RectTransform;
			Recurso recurso = baseDatos.recursos [i];
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizePanRecursoX;
			xMin+= sizePanRecursoX;
			if (xMax > 1) {
				xMax = sizePanRecursoX;
				xMin= margenXRecurso;
				yMax -= sizePanRecursoY;
				yMin -= sizePanRecursoY;

			}
			panRecurso.GetComponentsInChildren<Image>()[1].sprite = recurso.icono;
			Text textoRecurso = panRecurso.GetComponentInChildren<Text> ();
			textoRecurso.text = ((int)jugador.recursos[i]).ToString();
			textosRecursos.Add (textoRecurso);
		}
	}
	public void ClearButtons(){
		nombre.text = "";
		icon.sprite = null;
		foreach (GameObject boton in GameObject.FindGameObjectsWithTag("Button")) {
			Destroy (boton);
		}
		cleared = true;
	}
	void ActualizarRecursos(){
		for (int i = 0; i< textosRecursos.Count; i++) {
			textosRecursos[i].text = ((int)jugador.recursos[i]).ToString();
		}
	}
	void UpdateHealth(){
		if (selecs [0].objeto.type == 1 || selecs [0].objeto.type == 2) {
			healthBar.rectTransform.anchorMax = new Vector2( selecs [0].health / (selecs [0].objeto as Ficha).health,healthBar.rectTransform.anchorMax.y);
			healthBar.rectTransform.offsetMax = new Vector2 (0, 0);
		}
	}
}
