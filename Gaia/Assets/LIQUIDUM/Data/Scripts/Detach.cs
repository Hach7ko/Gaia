using UnityEngine;
using System.Collections;

public class Detach : MonoBehaviour {


	void Start () {
	if (transform.parent != null)
			transform.parent=null;
	}

}
