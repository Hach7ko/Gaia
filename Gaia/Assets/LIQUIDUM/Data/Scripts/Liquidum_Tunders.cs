using UnityEngine;
using System.Collections;

public class Liquidum_Tunders : MonoBehaviour {
	
	Liquidum LiquidumScript;
	AudioSource ThundersSound;
	float TheTimer;
	bool ThunderEmit;
	bool FadeLight;
	Light Lamp;
	public GameObject PreLampPrefab;
	GameObject W_ThunderBolts;
    GameObject ThunderScreenPlane;
	GameObject EcoLight;
	bool EcoLightExist=false;
	string Distance;
	float fadeSpeed;
	Quaternion OrMainLightRotation;
    float ranAngle;
	
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
		transform.name="_Thunders_effect(Sound&Light)";
		EcoLight=null;	
		
		//Find the prefabs under the main LIQUIDUM_Emitter
		W_ThunderBolts=GameObject.Find("W_ThunderBolts");	
		//Find the prefabs under the main LIQUIDUM_Emitter
	//	ThunderScreenPlane=GameObject.Find("_ThunderLightPlane");
    //    ThunderScreenPlane.renderer.material.SetColor("_TintColor",new Color(0,0,0,0));
		
		//If use the Main light
		if(LiquidumScript.SceneDirectionalLight){
		OrMainLightRotation=LiquidumScript.SceneDirectionalLight.transform.rotation;

		}
		
		//Find the prefabs under the main LIQUIDUM_Emitter
		ThunderScreenPlane=GameObject.Find("_ThunderLightPlane");
		ThunderScreenPlane.GetComponent<Renderer>().material.SetColor("_TintColor",new Color(0,0,0,0));
		
	if(LiquidumScript.AwayThundersSoundsClips.Length>0) {
	ThundersSound = gameObject.AddComponent<AudioSource>() as AudioSource;
	ThundersSound.volume=LiquidumScript.ThundersSoundVolume;
	ThundersSound.clip = LiquidumScript.AwayThundersSoundsClips[0];
	ThundersSound.Stop();
		}
		
	Lamp=GetComponentInChildren<Light>();
		
	
			
	}

	
	void SoundEmit () {
	
		
		AudioClip[] ThundersSoundsClips;
		
		
		//Select the thunder sound from a list (Near,Middle,Away) relative to lamp/sound delay time 
		if(Distance=="Away") {
		ThundersSoundsClips= LiquidumScript.AwayThundersSoundsClips;
		ThundersSound.volume=LiquidumScript.ThundersSoundVolume*0.8f;
		}else if(Distance=="Middle") {
			ThundersSound.volume=LiquidumScript.ThundersSoundVolume*0.9f;
			ThundersSoundsClips= LiquidumScript.MiddleThundersSoundsClips;
			}else{
			ThundersSound.volume=LiquidumScript.ThundersSoundVolume;
			ThundersSoundsClips= LiquidumScript.NearThundersSoundsClips;
		}
		
		
	int ran=Random.Range(0,ThundersSoundsClips.Length);
	if(LiquidumScript.AwayThundersSoundsClips.Length>0){
	ThundersSound.clip = ThundersSoundsClips[ran];	
	ThundersSound.PlayDelayed(LiquidumScript.ThundersDistance);
	}
	}
	
	
	
		void LampEmit () {
	
		//Get the distance of Thunder
		if(LiquidumScript.ThundersDistance>3.5f) {
		Distance="Away";
		}else if(LiquidumScript.ThundersDistance>1f) {
		Distance="Middle";
			}else{
		Distance="Near";
		}

		
		//Create another ligth (EcoLight)
		if(!EcoLightExist){
		    EcoLight = (GameObject)GameObject.Instantiate(PreLampPrefab, transform.GetChild(0).position, Quaternion.identity); //Create prefab 
			SoundEmit();//Emit the sound
			EcoLightExist=true;//Now the EcoLight exist
			
		}

		
		//If use the Main light
		if(LiquidumScript.SceneDirectionalLight){
			
			if(Lamp.intensity>=1f){
			
        ranAngle=Random.Range(1-LiquidumScript.ShadowInfluencing/100f, 1+LiquidumScript.ShadowInfluencing/100f);
		LiquidumScript.SceneDirectionalLight.transform.rotation=new Quaternion(LiquidumScript.SceneDirectionalLight.transform.rotation.x*ranAngle,LiquidumScript.SceneDirectionalLight.transform.rotation.y*ranAngle,LiquidumScript.SceneDirectionalLight.transform.rotation.z,LiquidumScript.SceneDirectionalLight.transform.rotation.w);

		}else{
			LiquidumScript.SceneDirectionalLight.transform.rotation=Quaternion.Lerp(LiquidumScript.SceneDirectionalLight.transform.rotation,OrMainLightRotation, LiquidumScript.ThundersFadeOutSpeed*LiquidumScript.LightEcos*Time.deltaTime);
			}
		}
		
		//Main Lamp (no update, once time)
		if(!FadeLight){				
			
		//If thunder is away, light is more low and fade speed is more fast
		if(Distance=="Away") {
        Lamp.intensity=LiquidumScript.ThundersLightPower/2;
		fadeSpeed=LiquidumScript.ThundersFadeOutSpeed;}
		else if(Distance=="Middle") {
        Lamp.intensity=LiquidumScript.ThundersLightPower;
		fadeSpeed=LiquidumScript.ThundersFadeOutSpeed/1.2f;	
			}
			else{
		Lamp.intensity=LiquidumScript.ThundersLightPower*1.5f;
		fadeSpeed=LiquidumScript.ThundersFadeOutSpeed/1.5f;
			}
			
		//If use the Main light
		if(LiquidumScript.SceneDirectionalLight){
            
			LiquidumScript.SceneDirectionalLight.transform.rotation=
			new Quaternion(LiquidumScript.SceneDirectionalLight.transform.rotation.x,
				LiquidumScript.SceneDirectionalLight.transform.rotation.y*LiquidumScript.ShadowInfluencing,
				LiquidumScript.SceneDirectionalLight.transform.rotation.z,
				LiquidumScript.SceneDirectionalLight.transform.rotation.w);	
		}
		
		

		FadeLight=true;}
		else{				
		Lamp.intensity=Mathf.Lerp(Lamp.intensity,0,fadeSpeed*Time.deltaTime);
		ThunderScreenPlane.GetComponent<Renderer>().material.SetColor("_TintColor",LiquidumScript.ThundersLightColor*new Color(Lamp.intensity,Lamp.intensity,Lamp.intensity,Lamp.intensity)/10);

	    W_ThunderBolts.gameObject.GetComponent<Liquidum_Thunderbolt>().BoltEmit(Lamp.intensity);
		}
		
		
		//End of Effect
		if(Lamp.intensity<=0.01f){
			LiquidumScript.ThunderEmitNow=false;
			FadeLight=false;
			EcoLightExist=false;
			//Reset the original main light orientation
			if(LiquidumScript.SceneDirectionalLight)LiquidumScript.SceneDirectionalLight.transform.rotation=OrMainLightRotation;
			
	 
			
		}
	
		
	}
	
	void Update () {
		if(!LiquidumScript.ThundersActive)return;
		
		if(LiquidumScript.ThunderEmitNow){
		  LampEmit ();
		}
		
		

	}
}
