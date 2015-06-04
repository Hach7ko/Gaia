using UnityEngine;
using System.Collections;

public class LiquidumStatic : MonoBehaviour {


	bool fadeO;
	bool fadeI;
	Color TargetColor;
	Liquidum LiquidumScript;

	
	void Start () {
		
	LiquidumScript=Liquidum.LiquidumScript;
    TargetColor=LiquidumScript.StaticColor;
	GetComponent<Renderer>().material.SetColor("_Color",new Color(0,0,0,0));
   
	}
		public void FadeOut(){
		fadeO=true;
		fadeI=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),LiquidumScript.StaticFadeOutSpeed*Time.deltaTime);
		if(TargetColor.a<0.01f){fadeO=false; }//End of FadeOut
		if(Time.timeSinceLevelLoad<5) Clear();
	}
	
	
		public void FadeIn(){
		fadeI=true;
		fadeO=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),LiquidumScript.StaticColor,LiquidumScript.StaticFadeInSpeed*Time.deltaTime);
		if(TargetColor.a>=LiquidumScript.StaticColor.a-0.01f)fadeI=false;//End of FadeIn
	}
	
	public void Clear(){
        fadeO=false;
		fadeI=false;
		GetComponent<Renderer>().material.SetColor("_Color",Color.clear);
		
	}
	
	void UpdateScalePosition () {
	 transform.localScale=new Vector3(LiquidumScript.StaticScale.x,
			LiquidumScript.StaticScale.y,
			LiquidumScript.StaticScale.y);
	}
	
	
	void Update () {
		if(fadeI||fadeO)
	GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		else
			if(TargetColor.a<0.01f)
	GetComponent<Renderer>().material.SetColor("_Color",new Color(0,0,0,0));
		else
			GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.StaticColor);
		
	GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.StaticDistortion);
		
		

		
		//Out
		if(fadeO)
		FadeOut();
		//In
		if(fadeI)
		FadeIn();
		
		
				if(LiquidumScript.Static_RainDependence&&!LiquidumScript.UnderOcclusion){
			fadeI=LiquidumScript.RainEmit;
			fadeO=!LiquidumScript.RainEmit;
			
				if(LiquidumScript.EmissionRate<10)fadeO=true;
			}
		
	
	}
}
