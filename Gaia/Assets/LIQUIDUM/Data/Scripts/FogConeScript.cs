using UnityEngine;
using System.Collections;

public class FogConeScript : MonoBehaviour {
		
	[HideInInspector]
	public Color StartColor;
		
	[HideInInspector]
	public bool fadeI,fadeO;
		
	[HideInInspector]
	Color TargetColor;
	
	Liquidum LiquidumScript;
	
	
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
		
	GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.RainFogColor);
	}
	
	
		 void FadeIN () {
		fadeO=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),StartColor,2*Time.deltaTime);
		if(TargetColor.a>=StartColor.a-0.01f)fadeI=false;//End of FadeIn
	}
	
		 void FadeOUT () {
		fadeI=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),2*Time.deltaTime);
		if(TargetColor.a<0.01f){fadeO=false; }//End of FadeOut
	}
	
	
	void Update () {
	if(fadeI)
			FadeIN();
		
	if(fadeO)
			FadeOUT();
		
		
		
			if(fadeI||fadeO){
	    GetComponent<Renderer>().material.SetColor("_Color",TargetColor);	
		}else{
			if(!LiquidumScript.UnderOcclusion&&LiquidumScript.RainEmit)
			GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.RainFogColor);	}
		
		if(!LiquidumScript.RainEmit){
			fadeI=false;
			fadeO=false;
			GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),3*Time.deltaTime));
	}
	}
	
}
