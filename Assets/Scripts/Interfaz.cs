using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour {

	bool iniciada;
	public bool cleared = true;
	public int language;
	public float interfaceFraction, sizeButtonX, sizeButtonY, margenX, margenY, sizePanResourceX, sizePanResourceY, margenXResource, margenYResource, 
		sizePanSelectionX, sizePanSelectionY, margenXSelection, margenYSelection;
	public Image icon, healthBar, LowerProductionBar, UpperProductionBar;
	public Text nombre, ProductionDescription;
	public Transform PanelHabilidades, Panelresources, PanelSelections, PanelProduction;
	public GameObject genPanResource, genPanSelection;
	public Button DefaultButton;
	public List<Seleccionable> selecs = new List<Seleccionable>();
	public Jugador jugador;
	public Database database;
	public List<Text> textosresources = new List<Text>();
	public List<Image> healthBars = new List<Image>();
	public List<Image> productionIcons = new List<Image> ();

	/* 
	 * CHARLA SERIA: 
	 * Para la selección múltiple tengo que ordenar los selecs por "prioridad".
	 * Esta será mayor en unidades especiales con habilidades, como ciudadanos,
	 * y entre las unidades normales cuanto más fuerte sea más "prioridad".	 * 
	 * 
	 *
	 * */

	void Awake(){
		database= gameObject.GetComponent<Database> ();
	}

	void Start(){

		CreateResourcePanels ();
	}

	void Update(){
		Actualizarresources ();
		if (!cleared && selecs.Count == 0) {
			ClearButtons ();
		}
		if (selecs.Count > 0) {
			UpdateHealthBars ();
			if (selecs [0].entity.type == 2 && selecs [0].productionQueue.Count > 0) {
				UpdateProductionBar ();
			}
		}

	}




	public void CreateHabilityButtons(){
		nombre.text = selecs[0].entity.nombre[language];
		icon.sprite = selecs[0].entity.icono;
		float xMax = sizeButtonX;
		float xMin= margenX;
		float yMax = margenY;
		float yMin = margenY - sizeButtonY;
		for (int i = 0; i < selecs[0].entity.creatives.Count; i++) {
			Creative creative = selecs[0].entity.creatives [i];
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
			boton.image.sprite = creative.icono;
			boton.onClick.AddListener(delegate{creative.Action(selecs[0]);});


		}
		for (int i = 0; i < selecs[0].entity.activas.Count; i++) {
			Active activa = selecs[0].entity.activas [i];
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
		float xMax = sizePanResourceX;
		float xMin= margenXResource;
		float yMax = margenYResource;
		float yMin = margenYResource - sizePanResourceY;
		for (int i = 0; i < jugador.resources.Count; i++) {
			GameObject panResource = Instantiate (genPanResource,Panelresources);
			RectTransform rtrans = panResource.transform as RectTransform;
			Resource recurso = database.resources [i];
			rtrans.anchorMax = new Vector2(xMax, yMax);
			rtrans.anchorMin = new Vector2(xMin, yMin);
			rtrans.offsetMax = new Vector2(0,0);
			rtrans.offsetMin = new Vector2(0,0);
			xMax+= sizePanResourceX;
			xMin+= sizePanResourceX;
			if (xMax > 1) {
				xMax = sizePanResourceX;
				xMin= margenXResource;
				yMax -= sizePanResourceY;
				yMin -= sizePanResourceY;

			}
			panResource.GetComponentsInChildren<Image>()[1].sprite = recurso.icono;
			Text textoResource = panResource.GetComponentInChildren<Text> ();
			textoResource.text = ((int)jugador.resources[i]).ToString();
			textosresources.Add (textoResource);
		}
	}
	public void CreateSelectionPanels(){
		ClearButtons ();
		healthBars.Clear ();
		if (selecs.Count > 0) {
			float xMax = sizePanSelectionX;
			float xMin = margenXSelection;
			float yMax = margenYSelection;
			float yMin = margenYSelection - sizePanSelectionY;
			for (int i = 0; i < selecs.Count; i++) {
				Seleccionable sel = selecs [i];
				GameObject panSelection = Instantiate (genPanSelection, PanelSelections);
				RectTransform rtrans = panSelection.transform as RectTransform;
				Entity entity = sel.entity;
				rtrans.anchorMax = new Vector2 (xMax, yMax);
				rtrans.anchorMin = new Vector2 (xMin, yMin);
				rtrans.offsetMax = new Vector2 (0, 0);
				rtrans.offsetMin = new Vector2 (0, 0);
				xMax += sizePanSelectionX;
				xMin += sizePanSelectionX;
				if (xMax > 1) {
					xMax = sizePanSelectionX;
					xMin = margenXSelection;
					yMax -= sizePanSelectionY;
					yMin -= sizePanSelectionY;

				}
				panSelection.GetComponentsInChildren<Image> () [1].sprite = entity.icono;
				healthBars.Add (rtrans.GetChild (1).GetChild (1).GetComponent<Image>());
				panSelection.GetComponentInChildren<Button> ().onClick.AddListener(delegate {
					ChangeMainSelection(sel);
				});
			}
		}
	}

	void CreateTooltip(){
		Debug.Log ("Ratón arriba");
	}

	public void CreateProductionPanels(){

		PanelProduction.gameObject.SetActive (true);
		UpdateProductionIcons ();

	}
	public void ClearButtons(){
		nombre.text = "";
		icon.sprite = null;
		foreach (GameObject boton in GameObject.FindGameObjectsWithTag("Button")) {
			Destroy (boton);
		}
		cleared = true;
	}
	void Actualizarresources(){
		for (int i = 0; i< textosresources.Count; i++) {
			textosresources[i].text = ((int)jugador.resources[i]).ToString();
		}
	}
	void UpdateHealthBar(Image image ,Seleccionable sel){
		if (image != null) {
			if (selecs [0].entity.type == 1 || selecs [0].entity.type == 2) {
				image.rectTransform.anchorMax = new Vector2 (sel.health / (sel.entity as Token).health, image.rectTransform.anchorMax.y);
				image.rectTransform.offsetMax = new Vector2 (0, 0);
			}
		}
	}
	void UpdateHealthBars(){
		UpdateHealthBar (healthBar, selecs[0]);
		if (selecs.Count== healthBars.Count) {
			for (int i = 0; i < healthBars.Count; i++) {
				UpdateHealthBar (healthBars [i], selecs [i]);
			}
		}
	}

	void ChangeMainSelection(Seleccionable se){
		selecs [selecs.IndexOf (se)] = selecs [0];
		selecs [0] = se;
		for (int i = 1; i < selecs.Count; i++) {
			selecs [i].Invoke ("Deseleccion", 0.04f);
		}
		Invoke("UpdateSelection", 0.05f);
	}
	public void UpdateProductionBar(){
		UpperProductionBar.rectTransform.anchorMax = new Vector2 (selecs[0].productionAmount / selecs[0].maxProductionAmount, UpperProductionBar.rectTransform.anchorMax.y);
		UpperProductionBar.rectTransform.offsetMax = new Vector2 (0, 0);
	}
	public void UpdateProductionIcons(){
		if (selecs [0].productionQueue.Count > 0) {
			for (int i = 0; i < productionIcons.Count; i++) {
				if (i < selecs [0].productionQueue.Count) {
					productionIcons [i].sprite = selecs [0].productionQueue [i].entity.icono;
				} else {
					productionIcons [i].sprite = null;
				}
			}
			UpdateProductionBar ();
			if (language == 0) {
				ProductionDescription.text = "Training...";
			} else if (language == 1) {
				ProductionDescription.text = "Entrenando...";
			}
		} else {
			PanelProduction.gameObject.SetActive (false);
		
		}
	
	}
	public void UpdateSelection(){
		PanelProduction.gameObject.SetActive (false);
		if (selecs.Count > 0) {
			if (selecs [0].entity.type == 1) {
				CreateSelectionPanels ();
			} else if (selecs [0].entity.type == 2 && selecs[0].productionQueue.Count>0) {
				CreateProductionPanels ();
			}		
			CreateHabilityButtons ();
		}
	}
}
