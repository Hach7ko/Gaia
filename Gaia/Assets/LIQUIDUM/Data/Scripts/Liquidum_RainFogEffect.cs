using UnityEngine;
using System.Collections;

public class Liquidum_RainFogEffect : MonoBehaviour {

	 Liquidum LiquidumScript;
	 public bool ChangeAtRuntime=false;
	 Transform thisTransform;
	 GameObject cam;
     bool tmpIn;
     bool tmpOut;
	 public Transform[]ChilderCones;
	 public ParticleSystem GraundFog;
	 float []RanRot=new float[5];
	
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
		GraundFog.transform.localPosition=new Vector3(GraundFog.transform.localPosition.x,GraundFog.transform.localPosition.y+LiquidumScript.GroundFogHeigth,GraundFog.transform.localPosition.z);
		
		
		if(!LiquidumScript.UseRainFog){
			for (var i = 0; i < ChilderCones.Length; i++) {
			GameObject.Destroy(ChilderCones[i].gameObject);		
			
		}  
			
		}	
			
		
		cam=LiquidumScript.TheCam;	
		thisTransform=transform;
		for (var i = 0; i < ChilderCones.Length; i++) {
			RanRot[i]=Random.Range(0.8f,1.2f);
			ChilderCones[i].GetComponent<Renderer>().material.SetTextureScale("_MainTex",new Vector2(1f, 0.5f/RanRot[i]));		
			
		}
		
		 UpdateGroundFog();

	}
	
	
	
	void MoveTexture(){
		
		        float ThisXoffset = Time.time * LiquidumScript.RainFogOrizzontalSpeed/30;
		        float ThisYoffset = Time.time * LiquidumScript.RainFogVerticalSpeed/10;
			
			//Vector3 CostRot= new Vector3(180+LiquidumScript.ConstantAngle,LiquidumScript.ConstantAngle,LiquidumScript.ConstantAngle);
		   // thisTransform.eulerAngles =CostRot;
		
		for (var i = 0; i < ChilderCones.Length; i++) {
		ChilderCones[i].GetComponent<Renderer>().material.SetTextureOffset("_MainTex",(new Vector2(ThisXoffset, ThisYoffset)*RanRot[i]));
		ChilderCones[i].GetComponent<FogConeScript>().StartColor=LiquidumScript.RainFogColor;
}

	    thisTransform.position=new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z);
		thisTransform.localScale=new Vector3(LiquidumScript.RainFogDistance,LiquidumScript.RainFogDistance,LiquidumScript.RainFogDistance);
	
	}
	
	
	
	void UpdateGroundFog(){
		
		///////////////GroundFog////////////
		
			if(LiquidumScript.UseGroundFog){
		GraundFog.gameObject.SetActive(true);
		GraundFog.startColor=LiquidumScript.GroundFogColor;
		  }else{
		GraundFog.gameObject.SetActive(false);}
		
		//////////////////////////////////////
		}
	
	
	
	
	void Update () {
		
		if(!LiquidumScript.TheCam){
			Debug.LogError("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller");
			return; }
		
		
		if(LiquidumScript.UseRainFog)
		MoveTexture();
		
		
		if(ChangeAtRuntime&&GraundFog)
			UpdateGroundFog();
		
		
		
		if(!LiquidumScript.UseRainFog&&!LiquidumScript.UseGroundFog)GameObject.Destroy(this);
		
		if(tmpIn&&LiquidumScript.RainEmit&&LiquidumScript.UseRainFog&&!LiquidumScript.UnderOcclusion){
			
			
		for (var i = 0; i < ChilderCones.Length; i++) {
		ChilderCones[i].GetComponent<FogConeScript>().fadeI=true;
		
		}
			tmpIn=false;

	}

		
		if(tmpOut&&!LiquidumScript.RainEmit){
			
		for (var i = 0; i < ChilderCones.Length; i++) {
		ChilderCones[i].GetComponent<FogConeScript>().fadeO=true;	
}
			tmpOut=false;
			
		}
	
		
	}
}
