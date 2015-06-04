using UnityEngine;
using System.Collections;

public class AdditionalDistortion : MonoBehaviour {

	Liquidum LiquidumScript;
	public bool fadeO;
	public bool fadeI;
	Color TargetColor;
	float FieldOfViewCompensation;

	
	void Start () {
		
	LiquidumScript=Liquidum.LiquidumScript;
    TargetColor=LiquidumScript.AdditionaDistortionColor;
    

	}
	

	public void FadeOut(){
		fadeO=true;
		fadeI=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),LiquidumScript.AdditionalDistortionFadeOutSpeed*Time.deltaTime);
		if(TargetColor.a<0.01f){fadeO=false; }//End of FadeOut
		if(Time.timeSinceLevelLoad<4) Clear();
	}
	
	public void Clear(){
        fadeO=false;
		fadeI=false;
		GetComponent<Renderer>().material.SetColor("_Color",Color.clear);
		
	}
	
	public void FadeIn(){
		fadeI=true;
		fadeO=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),LiquidumScript.AdditionaDistortionColor,LiquidumScript.AdditionalDistortionFadeInSpeed*Time.deltaTime);
		if(TargetColor.a>=LiquidumScript.AdditionaDistortionColor.a-0.01f)fadeI=false;//End of FadeIn
	}
	
	
	
	
	void Update () {


		float Yoffset = Time.time * LiquidumScript.AdditionalDistortionSlipSpeed/10;


	if(fadeI||fadeO){
	GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		}else{
			if(TargetColor.a<0.01f)
	GetComponent<Renderer>().material.SetColor("_Color",new Color(0,0,0,0));
		else
	GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.AdditionaDistortionColor);
	}
		
	GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.AdditionalDistortionStrength);

	GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(0, -Yoffset);
	GetComponent<Renderer>().materials[0].SetTextureOffset("_BumpMap",new Vector2(0, -Yoffset));
		
		//Out
		if(fadeO)
		FadeOut();
		//In
		if(fadeI)
		FadeIn();
		
		
		if(LiquidumScript.Additional_RainDependence&&!LiquidumScript.UnderOcclusion){
			fadeI=LiquidumScript.RainEmit;
			fadeO=!LiquidumScript.RainEmit;
			
			if(LiquidumScript.EmissionRate<50)fadeO=true;
			}
	
	}

	
	
	
}
