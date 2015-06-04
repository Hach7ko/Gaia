using UnityEngine;
using System.Collections;

public class RainFogPlane : MonoBehaviour {

	Liquidum LiquidumScript;
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
	}
	
	// Update is called once per frame
	void Update () {
			float Yoffset =  Time.time * LiquidumScript.RainFogOrizzontalSpeed/30;
			GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(0, -Yoffset);
	}
}
