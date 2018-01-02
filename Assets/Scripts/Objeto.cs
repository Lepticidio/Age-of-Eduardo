using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto  {

	public int type = 0; // Indica si el objeto es una unidad, estructura o decoración
	public float vida;
	public string nombreSpanish, nombreEnglish;

	public List<Habilidad> habilidades = new List<Habilidad>();

	public Sprite icono;


}
