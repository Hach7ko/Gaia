using UnityEngine;
using System.Collections;

public class Liquidum_Splash : MonoBehaviour {

	
	Liquidum LiquidumScript;
	float SplasSpeed;
	Vector3 FinalScale;
	float SplashSlipSpeed;
	Color TargetColor;
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
		GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.SplashColor);
		SplasSpeed=LiquidumScript.SplashAnimationSpeed;
		FinalScale=new Vector3(LiquidumScript.SplashScale.x,LiquidumScript.SplashScale.y,1)*400;
		transform.localScale=new Vector3(0.001f,0.001f,0.001f);
		SplashSlipSpeed=LiquidumScript.SplashSlipSpeed*10;
	}
	

	
	void Update () {
		//Scale
	transform.localScale= Liquidum.LerpAndStop(transform.localScale,FinalScale,SplasSpeed*Time.deltaTime*50);
		//SlipDown
	transform.localPosition= Vector3.Lerp(transform.localPosition,new Vector3(transform.localPosition.x,-1,transform.localPosition.z),(SplashSlipSpeed*Time.deltaTime)/1000);
	
		TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),
			new Color(GetComponent<Renderer>().material.GetColor("_Color").r,
			GetComponent<Renderer>().material.GetColor("_Color").g,
			GetComponent<Renderer>().material.GetColor("_Color").b,0),LiquidumScript.SplashFadeOutSpeed*Time.deltaTime);
		
		if(TargetColor.a<0.02f){GameObject.Destroy(gameObject);} 
		
		
	GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		
	GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.SplashDistortion*20);
		
	
	}
}
