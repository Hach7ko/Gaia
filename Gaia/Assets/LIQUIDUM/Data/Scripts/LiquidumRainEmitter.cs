using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif	
using System.Collections;

public class LiquidumRainEmitter : MonoBehaviour {

	 Liquidum LiquidumScript;
	 Transform thisTransform;
	 ParticleSystem Ps;
	 public ParticleSystem CollisionPs;
	 AudioSource RainSound;
	 float OrVolume;
	 float RanTurbolence;
	 [HideInInspector]
	 public float RainTimer;	
	#if UNITY_EDITOR
//	 SerializedObject so ;
//	 SerializedProperty it;
	#endif	
	 public Material SimpleRain;
	 public Material DistortRain;
	 GameObject RainFogEmitter;
	 public GameObject RainFogPrefab;
     bool StartFadeInVolume,StartFadeOutVolume=false;
	 Camera cam;

	
	
	
	void Awake () {
	transform.name="Liquidum (Rain&Hail&Snow Emitters)";
	thisTransform=transform;
	Ps=thisTransform.GetComponent<ParticleSystem>();
	//CollisionPs=Ps.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
	LiquidumScript=Liquidum.LiquidumScript;
#if UNITY_EDITOR
/*	so = new SerializedObject(Ps);
	it = so.GetIterator();
	it = so.GetIterator();
		
		while (it.Next(true)) {
			
  Debug.Log (it.propertyPath);}
*/  		
#endif	
	cam=Camera.main;	
	if(LiquidumScript.UseRainFog||LiquidumScript.UseCloudinesslEffect||LiquidumScript.UseThunderBolts||LiquidumScript.UseGroundFog){
		RainFogEmitter=(GameObject)GameObject.Instantiate(RainFogPrefab,Vector3.zero,RainFogPrefab.transform.rotation);	

			RainFogEmitter.name="Liquidum (Fog&Clouds&Bolts)";

		}
	}
	
    void Start() {		
		
	RainSound = gameObject.AddComponent<AudioSource>() as AudioSource;
	OrVolume = LiquidumScript.RainSoundVolume;
	RainSound.clip = LiquidumScript.RainSoundClip;
	RainSound.loop = true;
	RainSound.volume=0;
		
		
			if(LiquidumScript.UseDistortionRain){
				Ps.GetComponent<Renderer>().material=DistortRain;
			Ps.GetComponent<Renderer>().material.SetColor("_Color", LiquidumScript.RainColor);}
			else{
				Ps.GetComponent<Renderer>().material=SimpleRain;  
			Ps.GetComponent<Renderer>().material.SetColor("_TintColor", LiquidumScript.RainColor);
			}	
		
		
    }
	

		public void PlaySound () {
		
		if(!RainSound.isPlaying){
		   StartFadeOutVolume=false;
		   StartFadeInVolume=true;
		   RainSound.Play();}
	}

	
			public void StopSound () {
		
		if(RainSound.isPlaying){
		   StartFadeInVolume=false;
		   StartFadeOutVolume=true;
			
		   }
	}
	
	

			public void SoundFadeIn () {
		
		RainSound.volume=Mathf.Lerp(RainSound.volume,OrVolume,LiquidumScript.RainVolumeFadeSpeed*Time.deltaTime);
		if(RainSound.volume>=OrVolume/1.05f)StartFadeInVolume=false;
	}

		   public void SoundFadeOut () {
		
		RainSound.volume=Mathf.Lerp(RainSound.volume,0,LiquidumScript.RainVolumeFadeSpeed*Time.deltaTime*10);
		if(RainSound.volume<=0.01){StartFadeOutVolume=false; RainSound.Stop();}
	}
	
	
	
	
		void NOCurveUse () {
		
			if(!LiquidumScript.TheCam){
			Debug.LogError("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller");
			return; }
		
		if(StartFadeInVolume) SoundFadeIn ();
		if(StartFadeOutVolume) SoundFadeOut ();
		
		Ps.enableEmission= LiquidumScript.RainEmit;
		
		
		if(LiquidumScript.RainEmit){
			RainTimer+=Time.deltaTime;//Useful to delay the start of drops on screen after rain start
			
	//if(LiquidumScript.AddRainTurbolence>0)
				
				
	#if UNITY_EDITOR	
//This property change is active only under editor 
//  so.FindProperty("ShapeModule.angle").floatValue = LiquidumScript.AddRainTurbolence;		
//	so.FindProperty("CollisionModule.enabled").boolValue = LiquidumScript.RainCheckCollision;
//	so.FindProperty("ShapeModule.radius").floatValue = LiquidumScript.RainEmitterArea;
//	so.FindProperty("CollisionModule.collidesWith").intValue = LiquidumScript.CollideWith;
//	so.FindProperty("InitialModule.maxNumParticles").intValue =LiquidumScript.MaxParticles;	
//   so.ApplyModifiedProperties();
	#endif	

        PlaySound (); 
		Ps.startSpeed=LiquidumScript.RainSpeed/1.5f;
		Ps.startSize=LiquidumScript.RainSize/10;		
		Ps.emissionRate=LiquidumScript.EmissionRate;
		Ps.startLifetime=LiquidumScript.RainLifeTime;
		Ps.gravityModifier=LiquidumScript.RainGravity;
		CollisionPs.startSize=LiquidumScript.RainCollisionSpriteSize/10;
		CollisionPs.startSpeed=LiquidumScript.RainCollisionSplashPower*3;
			

			
		Ps.startColor=LiquidumScript.RainColor;
			
		if(LiquidumScript.AddRainTurbolence>0){
			 RanTurbolence=Random.Range(-LiquidumScript.AddRainTurbolence*15,LiquidumScript.AddRainTurbolence*15);
			}else{
			 RanTurbolence=0;
			}
			
			
			
			Vector3 CostRot= new Vector3(90+LiquidumScript.ConstantAngle+RanTurbolence,LiquidumScript.ConstantAngle+RanTurbolence,LiquidumScript.ConstantAngle+RanTurbolence);
		    thisTransform.eulerAngles =CostRot;


	}
		else{
			StopSound ();
		    RainTimer=0;
		
		
		
		}
	}
	
	
	
	
	
	
	
		void CurveUse () {
		
			if(!LiquidumScript.TheCam){
			Debug.LogError("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller");
			return; }
		

		
		Ps.enableEmission= LiquidumScript.RainEmit;
		
		
		if(LiquidumScript.RainEmit){
			RainTimer+=Time.deltaTime;//Useful to delay the start of drops on screen after rain start
			
		RainSound.volume=LiquidumScript.RainSoundVolume;

        PlaySound (); 
		Ps.startSpeed=LiquidumScript.RainSpeed/1.5f;
		Ps.startSize=LiquidumScript.RainSize/10;		
		Ps.emissionRate=LiquidumScript.EmissionRate;
		Ps.startLifetime=LiquidumScript.RainLifeTime;
		Ps.gravityModifier=LiquidumScript.RainGravity;
		CollisionPs.startSize=LiquidumScript.RainCollisionSpriteSize/10;
		CollisionPs.startSpeed=LiquidumScript.RainCollisionSplashPower*3;
		
		Ps.startColor=LiquidumScript.RainColor;
			
		if(LiquidumScript.AddRainTurbolence>0){
			 RanTurbolence=Random.Range(-LiquidumScript.AddRainTurbolence*10,LiquidumScript.AddRainTurbolence*10);
			
			}else{
			 LiquidumScript.AddRainTurbolence=0;	
			 RanTurbolence=0;
			}
			
			
			
			Vector3 CostRot= new Vector3(90+LiquidumScript.ConstantAngle+RanTurbolence,LiquidumScript.ConstantAngle+RanTurbolence,LiquidumScript.ConstantAngle+RanTurbolence);
		   if(CostRot.magnitude<0)CostRot=Vector3.zero;
			thisTransform.eulerAngles =CostRot;


	
	}
		else{
			StopSound ();
		    RainTimer=0;

		}
	}
	
	
	
	
	
	
	void Update () {
		
		if(!LiquidumScript.WeatherEventInUse)
		NOCurveUse ();
		else
		CurveUse ();
		
		//Follow the camera
		thisTransform.position=new Vector3(cam.transform.position.x,cam.transform.position.y+LiquidumScript.RainEmitterHeigth,cam.transform.position.z)+LiquidumScript.TheCam.transform.forward*1.5f;
	}
}
