using UnityEngine;
using System.Collections;

public class LiquidumSnowEmitter : MonoBehaviour {

	
	 Liquidum LiquidumScript;
	 Transform thisTransform;
	 ParticleEmitter Ps;
	 ParticleAnimator Pa;
	 Camera cam;
	 [W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to false for best performance.")]
	 public bool ChangeAtRunTime =false;
	
	void Start () {
	transform.name="Liquidum (SnowEmitter)";
	thisTransform=transform;
	Ps=thisTransform.GetComponent<ParticleEmitter>();
	Pa=thisTransform.GetComponent<ParticleAnimator>();
	LiquidumScript=Liquidum.LiquidumScript;
	//Set The Heigth
	transform.localPosition=new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z-LiquidumScript.SnowEmitterHeigth);
    ConfigurationSet ();
		
	}
	
	
		
		void ConfigurationSet () {

        Ps.GetComponent<Renderer>().material.SetColor("_TintColor",LiquidumScript.SnowColor);
		Ps.maxSize=LiquidumScript.SnowSize/5;
		Ps.minEmission=LiquidumScript.SnowEmissionRate/8;
		Ps.maxEmission=LiquidumScript.SnowEmissionRate/5;
		Ps.maxEnergy=LiquidumScript.SnowLifeTime;
		Ps.worldVelocity=new Vector3(LiquidumScript.SnowConstantAngle/80,-LiquidumScript.SnowSpeed/4,LiquidumScript.SnowConstantAngle/80);
	    Pa.force=new Vector3(0,-LiquidumScript.SnowSpeed/5,0);
		Pa.worldRotationAxis=new Vector3(LiquidumScript.SnowConstantAngle/100,0,LiquidumScript.SnowConstantAngle/100);
	}
	
	


	
	void Update () {
	Ps.emit=LiquidumScript.SnowEmit;
	
		if(ChangeAtRunTime)
         ConfigurationSet () ;
	}
}
