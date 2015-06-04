using UnityEngine;
using System.Collections;

public class LiquidumHailEmitter : MonoBehaviour {


	
	 Liquidum LiquidumScript;
	 Transform thisTransform;
	 ParticleEmitter Ps;
	 ParticleAnimator Pa;
	 Camera cam;
	 [W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to false for best performance.")]
	 public bool ChangeAtRunTime =false;
	
	void Start () {
	transform.name="Liquidum (HailEmitter)";
	thisTransform=transform;
	Ps=thisTransform.GetComponent<ParticleEmitter>();
	Pa=thisTransform.GetComponent<ParticleAnimator>();
	LiquidumScript=Liquidum.LiquidumScript;
	//Set The Heigth
	transform.localPosition=new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z-LiquidumScript.HailEmitterHeigth/10);
    ConfigurationSet ();
		
	}
	
	
		
		void ConfigurationSet () {

        Ps.GetComponent<Renderer>().material.SetColor("_TintColor",LiquidumScript.HailColor);
		Ps.maxSize=LiquidumScript.HailSize/5;
		Ps.minEmission=LiquidumScript.HailEmissionRate/2;
		Ps.maxEmission=LiquidumScript.HailEmissionRate;
		Ps.maxEnergy=LiquidumScript.HailLifeTime;
		Ps.worldVelocity=new Vector3(LiquidumScript.HailConstantAngle/50,-LiquidumScript.HailSpeed,LiquidumScript.HailConstantAngle/150);
	    Pa.force=new Vector3(LiquidumScript.HailConstantAngle/90,-LiquidumScript.HailSpeed,0);
		Pa.worldRotationAxis=new Vector3(LiquidumScript.HailConstantAngle/90,0,LiquidumScript.HailConstantAngle/120);
	}
	
	


	
	void Update () {
	Ps.emit=LiquidumScript.HailEmit;
	
		if(ChangeAtRunTime)
         ConfigurationSet () ;
	}
}
