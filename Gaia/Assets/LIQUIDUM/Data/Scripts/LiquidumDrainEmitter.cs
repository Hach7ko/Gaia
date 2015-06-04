using UnityEngine;
using System.Collections;

public class LiquidumDrainEmitter : MonoBehaviour {
	
[W_Header("Liquidum Drain Unique Configuration","If you want make unique this effect, check \"MakeUnique\" to true and change below settings\nBelow settings work only if \"MakeUnique\" is true, or else use the \"Drains Global Setting\" in the Main Liquidum_Effect script",14,"GreenColor")]
	public string InspectorSpace1;
	
	Liquidum LiquidumScript;
    public Material[] Mat=new Material[2];
		/// <summary>
	/// If true, this effect no follow the "Drains Global Setting" in the Main Liquidum_Effect script
	/// </summary>
	[W_BoxBGr("",17,78,12,16)]public string space1;
	public bool MakeUnique=true;
	/// <summary>
	/// If this Drain Effect effect is active or not.
	/// </summary>
	public bool ThisIsActive=true;
	/// <summary>
	/// If this Drain Effect effect is rain dependent.
	/// </summary>
	public bool ThisIsRainDependent=false;
   
	public enum DrainTypeE {Normal,Distrot,Fire} 
	[W_ToolTip("-Tipe of Drop material.\n\n*Fire Drain not use ThisMainColor,Collision Sprite\n*Distort drops use more resource.")]
	public DrainTypeE DrainType = DrainTypeE.Normal;

	
	public AudioClip ThisDrainSoundClip;
	[Range(0f,1f)]
	public float ThisDrainSoundVolume=1;
	AudioSource DrainSound;
	
	/// <summary>
	/// Color scrolling liquid for this specific Drain Effect.
	/// Alpha channel also manages the overall transparency 
	/// </summary>/// <summary>
	public Color ThisMainColor=new Color(0.6f,0.6f,0.7f,0.4f);
		/// <summary>
	/// Speed of the Drain drop
	/// </summary>/// <summary>
	[Range(0.1f,5)]
	public float ThisDrainSpeed=1f;
	/// <summary>
	/// Forward force of Drain flow 
	/// </summary>/// <summary>
	[Range(0f,5f)]
	public float ThisDrainForwardForce=1f;
	/// <summary>
	/// The scale of single Drop.
	/// </summary>/// <summary>
	[Range(0.01f,3f)]
	public float ThisDrainDropSize=0.1f;
		/// <summary>
	/// Global scale of single CollisionEffect.
	/// </summary>/// <summary>
	[Range(0.01f,2f)]
	public float ThisDrainCollisionSpriteSize=0.1f;
	/// <summary>
	/// Power of CollisionEffect .
	/// </summary>/// <summary>
	[Range(0.01f,2f)]
	public float ThisDrainCollisionSplashPower=0.01f;
		/// <summary>
	/// Drop particles emitted per second
	/// </summary>/// <summary>
	[Range(1f,10000f)] 
	public int ThisDrainEmissionRate=3000;	
	/// <summary>
	/// Drop particles emitted per second
	/// </summary>/// <summary>
	[Range(0f,5f)] 
	public float ThisDrainLifeTime=2;	
	[W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to true if you want controll this with a LiquidumController\n-Set to false for best performance.")]
	public bool ChangeAtRunTime =false;
	
	
	
	
	
	
	
	

	ParticleSystem Ps,CollisionPs;

	
	void Start () {
	LiquidumScript=Liquidum.LiquidumScript;
		if(!LiquidumScript){Debug.LogError("WARNING:Liquidum Main Script not found!\nPlease Drag&Drop Liquidum_Effect.prefab under your camera/player/character controller gameobject. Found it in /LIQUIDUM main directory\n");GameObject.Destroy(this);}
	Ps=transform.GetComponent<ParticleSystem>();	
		
		//Get the children named "Drain Collision"
		for (int i = 0; i < Ps.transform.childCount; ++i)
        {
         if(transform.GetChild(i).name=="DrainCollision"){//If a children have the "DrainCollision" name
				CollisionPs=transform.GetChild(i).GetComponentInChildren<ParticleSystem>(); //Get the ColliderEffect
			break;}	
		}	
		
	
	DrainSound = gameObject.AddComponent<AudioSource>() as AudioSource;
		
	if(MakeUnique)	{
	DrainSound.volume=ThisDrainSoundVolume;
    DrainSound.clip = ThisDrainSoundClip;
	UseUniqueSetting();
	}else{
	DrainSound.volume=LiquidumScript.DrainSoundVolume;
	DrainSound.clip = LiquidumScript.DrainSoundClip;
	UseGlobalSetting();
		}
	DrainSound.maxDistance=2;
	DrainSound.loop = true;

	}
	
			public void PlaySound () {
		
		if(!DrainSound.isPlaying){
		   DrainSound.Play();}
	}

	
			public void StopSound () {
		
		if(DrainSound.isPlaying){
	       DrainSound.Stop();
		   }
	}
	
	
		void UseGlobalSetting () {
		Ps.enableEmission= LiquidumScript.DrainsEmit;
		Ps.startSpeed=LiquidumScript.DrainsForwardForce;
		Ps.gravityModifier=LiquidumScript.DrainsSpeed;
		Ps.startSize=LiquidumScript.DrainsDropSize/10;		
		Ps.emissionRate=LiquidumScript.DrainsEmissionRate;
		Ps.startLifetime=LiquidumScript.DrainsLifeTime;

		CollisionPs.startSize=LiquidumScript.DrainsCollisionSpriteSize/5;
		CollisionPs.startSpeed=LiquidumScript.DrainsCollisionSplashPower*2;
		
		
		
			if(LiquidumScript.UseDistrotDrains){
		Ps.GetComponent<Renderer>().material=Mat[0];
		Ps.GetComponent<Renderer>().material.SetColor("_Color", LiquidumScript.DrainsColor);}
			else{
		Ps.GetComponent<Renderer>().material=Mat[1];
		Ps.GetComponent<Renderer>().material.SetColor("_TintColor", LiquidumScript.DrainsColor);}
	}
	
	
	
			void UseUniqueSetting () {
		
		Ps.enableEmission=ThisIsActive;
		Ps.startSpeed=ThisDrainForwardForce;
		Ps.gravityModifier=ThisDrainSpeed+Random.Range(0,0.1f);
		Ps.startSize=ThisDrainDropSize/10;
		if(DrainType != DrainTypeE.Fire)
		Ps.emissionRate=ThisDrainEmissionRate+Random.Range(0,0.1f);
		else
		Ps.emissionRate=((float)ThisDrainEmissionRate+Random.Range(-1f,2f)+0.1f)/10;
		Ps.startLifetime=ThisDrainLifeTime;
		if(CollisionPs){CollisionPs.startSize=ThisDrainCollisionSpriteSize/5;
		CollisionPs.startSpeed=ThisDrainCollisionSplashPower*2;}
		
		
			if(DrainType == DrainTypeE.Distrot){
		Ps.GetComponent<Renderer>().material=Mat[0];
		Ps.GetComponent<Renderer>().material.SetColor("_Color", ThisMainColor);}
			else if(DrainType == DrainTypeE.Normal){
		Ps.GetComponent<Renderer>().material=Mat[1];
		Ps.GetComponent<Renderer>().material.SetColor("_TintColor", ThisMainColor);}
			else if(DrainType == DrainTypeE.Fire){
		Ps.GetComponent<Renderer>().material=Mat[2];
		}
	}
	
	
	
	
	
	
	
	void Update () {
	
		if(MakeUnique){
				if(ThisIsRainDependent){
				ThisIsActive=LiquidumScript.RainEmit;
				if(LiquidumScript.EmissionRate<50)ThisIsActive=false;
				Ps.enableEmission=ThisIsActive;
			}
	     if(ChangeAtRunTime)UseUniqueSetting();}
		else{
		 if(ChangeAtRunTime)UseGlobalSetting();
		}
		
		
		if(Ps.enableEmission)PlaySound();else StopSound();
	}
	
	

}