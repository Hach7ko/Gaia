using UnityEngine;
using System.Collections;

public class Liquidum_AutomaticWeatherEvents : MonoBehaviour {
  
	 Liquidum LiquidumScript;
	 public float StormPower = 0;
	 public float StormTimer = 0;
	 float theTimer;
     float Or_LightIntensity;
     float StormPower_Limit10;
	 float Clouds_GloblalLight;
	
    [W_Header("Automatic Weather Events","Create your own custom storm. \nChoose duration, intensity and more...",15,"YellowColor")]
	public string InspectorSpace;		
	[Range(1f,10)] 
	public float TimeScale=1;	
	[Range(1f,10)] 
	public float EventFadeSpeed=2;
	[Range(0f,5f)] 
	public float RainMultiplier=3;
	[Range(0f,2f)] 
	public float RainSoundVolumeMultipler  = 1;
	[Range(0f,5f)] 
	public float RainSpeedMultiplier=3;
	[Range(0f,5f)] 
	public float HailMultiplier=3;
	[Range(0f,5f)] 
	public float SnowMultiplier=3;
	[Range(0f,10)] 
	public float DropOnScreenMultiplier  = 1;
	[Range(0f,10)] 
	public float TrailOnScreenMultiplier  = 5;
	[Range(0f,10)] 
	public float AdditionalEffectMultiplier  = 5;
	[Range(0f,10)] 
	public float StaticEffectMultiplier  = 5;
	[Range(0f,10)] 
	public float RainFogEffectMultiplier  = 5;
	[Range(0f,10)] 
	public float GroundFogEffectMultiplier  = 5;
	[Range(0f,10)] 
	public float CloudinessEffectMultiplier  = 5;	
	public Color CloudsColor;
	[Range(0f,10)] 
	public int ThundesFrequency  = 5;
	[W_ToolTip("If true, the Directional Light intensisy resent of the storm power.\nSelect your Directional Light in the Liquidum Main Script under Thunder Setting")]
	public bool UseDirectionalLightAttenuation;
	public bool active;
	
	
	[SerializeField]
	public AnimationCurve WeatherCurve = AnimationCurve.Linear(0, 0, 10, 10);
	
		void Awake () {
			

		
	}
	
	
	void Start () {

		LiquidumScript=Liquidum.LiquidumScript;
		if(LiquidumScript.SceneDirectionalLight)
			Or_LightIntensity=LiquidumScript.SceneDirectionalLight.intensity;
		
		if(LiquidumScript){
	StormManage ();
		}else{
		Debug.LogError("WARNING:Liquidum Main Script not found!\nPlease Drag&Drop Liquidum_Effect.prefab under your camera/player/character controller gameobject. Found it in /LIQUIDUM main directory\n");GameObject.Destroy(this);
	}
	
	
	}
	
	
	void StormManage ()
{
		
		if(StormTimer>0&&ThundesFrequency>0)
		ThundersAutomaticEmit ();
		

		#region Under StormPower 10, manage the Rain/Thunder/ScreenEffects
	if(StormPower<=10){	
		
		Clouds_GloblalLight=StormPower*CloudinessEffectMultiplier/5;
			
		if(StormPower>0f&&LiquidumScript.UseCloudinesslEffect){
		LiquidumScript.CloudinessFadeSpeed=Clouds_GloblalLight;
		if(active)
		Liquidum_Clouds.fadeI=true;Liquidum_Clouds.fadeO=false;
		}
		


	

if(StormPower<1f)LiquidumScript.RainEmit=false;else LiquidumScript.RainEmit=true;
			
if(LiquidumScript.RainEmit&&LiquidumScript.EmissionRate>1){
LiquidumScript.RainGravity=StormPower/8f+2f;		
LiquidumScript.RainSpeed=(StormPower/2f)*RainSpeedMultiplier+5f;
LiquidumScript.AddRainTurbolence=StormPower/5;
LiquidumScript.ConstantAngle=StormPower*2;
			}
			
LiquidumScript.EmissionRate=((int)StormPower*(int)StormPower*(int)RainMultiplier*(int)RainMultiplier)*5;
LiquidumScript.RainFogColor=new Color(LiquidumScript.RainFogColor.r,LiquidumScript.RainFogColor.g,LiquidumScript.RainFogColor.b,StormPower/50*RainFogEffectMultiplier);
LiquidumScript.GroundFogColor=new Color(LiquidumScript.GroundFogColor.r,LiquidumScript.GroundFogColor.g,LiquidumScript.GroundFogColor.b,StormPower/50*GroundFogEffectMultiplier);
LiquidumScript.StaticColor=new Color(LiquidumScript.StaticColor.r,LiquidumScript.StaticColor.g,LiquidumScript.StaticColor.b,StormPower/100*StaticEffectMultiplier);
LiquidumScript.AdditionaDistortionColor=new Color(LiquidumScript.AdditionaDistortionColor.r,LiquidumScript.AdditionaDistortionColor.g,LiquidumScript.AdditionaDistortionColor.b,StormPower/100*AdditionalEffectMultiplier);
LiquidumScript.RainSoundVolume=(StormPower/10*RainSoundVolumeMultipler)-0.1f;
if(DropOnScreenMultiplier>1f){LiquidumDropOnScreenEmitter.OrDelay=((21f-DropOnScreenMultiplier)/10)-(StormPower/9);}else{ LiquidumDropOnScreenEmitter.OrDelay=1000;}//
if(TrailOnScreenMultiplier>1f){LiquidumScript.TrailCreationDelay=(10f-StormPower)-((int)TrailOnScreenMultiplier/2);} else{ LiquidumScript.TrailCreationDelay=100;}
			
			
			LiquidumScript.TrailCreationDelay=Mathf.Clamp(LiquidumScript.TrailCreationDelay,(10-TrailOnScreenMultiplier)+0.5f,1000);
					
		    LiquidumScript.DropCreationDelay=Mathf.Max(LiquidumScript.DropCreationDelay,0.1f);
			LiquidumDropOnScreenEmitter.OrDelay=Mathf.Max(LiquidumDropOnScreenEmitter.OrDelay,0.1f);
			LiquidumScript.EmissionRate=Mathf.Clamp(LiquidumScript.EmissionRate,0,10000);
			LiquidumScript.HailEmit=false;
			LiquidumScript.SnowEmit=false;			
			LiquidumScript.HailEmissionRate=0;
			LiquidumScript.SnowEmissionRate=0;
#endregion     
 
		}else{//10~20
			
			if(StormPower<=15f&&HailMultiplier>0){//Hail 10 ~ 15
			LiquidumScript.SnowEmit=false;
			LiquidumScript.HailEmit=true;
			LiquidumScript.HailSpeed=StormPower/2f;
			LiquidumScript.HailSize=(StormPower-10)/10;
			LiquidumScript.HailEmissionRate=(int)StormPower*150*(int)HailMultiplier;
			LiquidumScript.HailEmissionRate=Mathf.Clamp(LiquidumScript.HailEmissionRate,0,10000);	
			}
			
			if(StormPower>15f&&SnowMultiplier>0){//Snow 15 ~ 20
			LiquidumScript.HailEmit=false;	
			LiquidumScript.SnowEmit=true;
			LiquidumScript.SnowSpeed=((StormPower-14f)*5+(SnowMultiplier/2)/2)+2;
			LiquidumScript.SnowEmissionRate=(((int)StormPower-16)*800)+((int)SnowMultiplier*1000);
			LiquidumScript.SnowEmissionRate=Mathf.Clamp(LiquidumScript.SnowEmissionRate,0,10000);	
			}
			
		}
		if(!LiquidumScript.RainEmit||LiquidumScript.EmissionRate<1){
		LiquidumScript.AddRainTurbolence=0;
        LiquidumScript.ConstantAngle=0;
		}	
		
		
}	
	
void ThundersAutomaticEmit ()
{
		
	
		
	theTimer+=Time.deltaTime;	
	float ran=Random.Range(1f,5f);
		if(theTimer>(25-(ThundesFrequency/ran)-(StormPower_Limit10/10))){
			 
			 LiquidumScript.ThundersDistance=5-(StormPower_Limit10/2);
			 LiquidumScript.ThunderEmitNow=true;
		     theTimer=0;
		}
	}	
	
	
	
	
	
void Update ()
{	

StormPower_Limit10=Mathf.Clamp(StormPower,0,10);
		

if(LiquidumScript.WeatherEventInUse&&LiquidumScript.WeatherEventInUse.name!=this.name){gameObject.SetActive(false);return;}
		
if(active){	
LiquidumScript.WeatherEventInUse=this;
			
LiquidumScript.SnowSpeed=Mathf.Min(LiquidumScript.SnowSpeed,25);
			
StormTimer += Time.deltaTime/TimeScale;
StormPower = Mathf.Lerp(StormPower, WeatherCurve.Evaluate(StormTimer),EventFadeSpeed/8*Time.deltaTime);	
			
if(StormTimer>WeatherCurve.keys[WeatherCurve.length-1].time)active=false;
			
		}else{ 
			StormPower-=EventFadeSpeed/5*Time.deltaTime;
			StormTimer=0;
            if(LiquidumScript.UseCloudinesslEffect){LiquidumScript.CloudinessFadeSpeed=EventFadeSpeed/20f;  Liquidum_Clouds.fadeI=false; Liquidum_Clouds.fadeO=true;}
		}
		StormManage ();
		StormPower=Mathf.Clamp(StormPower,0,20);
		Clouds_GloblalLight=Mathf.Clamp(Clouds_GloblalLight,0,10);
		
	if(UseDirectionalLightAttenuation&&LiquidumScript.SceneDirectionalLight){	
	LiquidumScript.SceneDirectionalLight.intensity=Or_LightIntensity-(Clouds_GloblalLight/30f);
			}
}
}
