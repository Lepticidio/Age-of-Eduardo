using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPathfinding{
public class Nodo {

		public int G, x, y;
		public Nodo parent;
		public bool comprobado = false;
		public bool bloqueado = false;


		public Nodo () {
			G = 9999999;
		}


	}
}
