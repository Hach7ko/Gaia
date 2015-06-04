using UnityEngine;
using System.Collections;

public class Liquidum_TriggerDistortion : MonoBehaviour {

	
	Liquidum LiquidumScript;
	bool fadeO=true;
	bool fadeI;
	Color TargetColor;
//	bool active=false;
	
	void Start () {
			
	LiquidumScript=Liquidum.LiquidumScript;
    TargetColor=new Color(0,0,0,0);
	fadeO=true;
	if(!LiquidumScript.TriggerDistortionActive)	
		GameObject.Destroy(gameObject);
	}
	
	
		
	public void FadeOut(){
		fadeO=true;
		fadeI=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),LiquidumScript.TriggerDistortionFadeOutSpeed*Time.deltaTime*2);
		if(TargetColor.a<0.01f){fadeO=false; }//End of FadeOut
	}
	
	
	public void FadeIn(){
		fadeI=true;
		fadeO=false;
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),LiquidumScript.TriggerDistortionColor,LiquidumScript.TriggerDistortionFadeInSpeed*Time.deltaTime*2);
		if(TargetColor.a>=LiquidumScript.TriggerDistortionColor.a-0.01f)fadeI=false;//End of FadeIn
	}
	
	
	
	
	
	
	
	
	

	void Update () {
	
			
		float Yoffset = Time.time /3;
			GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(0, -Yoffset);
	GetComponent<Renderer>().materials[0].SetTextureOffset("_BumpMap",new Vector2(0, -Yoffset));
		
		
		
		
		
		
		
	if(fadeI||fadeO)
	GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		else
			if(TargetColor.a<0.01f)
	GetComponent<Renderer>().material.SetColor("_Color",new Color(0,0,0,0));
		else
			GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.TriggerDistortionColor);
		
		  //  UpdateScalePosition ();
		
	GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.TriggerDistortionPower);

	GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(0, -Yoffset);
	GetComponent<Renderer>().materials[0].SetTextureOffset("_BumpMap",new Vector2(0, -Yoffset));
		
		//Out
		if(fadeO)
		FadeOut();
		//In
		if(fadeI)
		FadeIn();
	
		
		
		
		
		
		
	}
}
