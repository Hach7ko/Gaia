using UnityEngine;
using System.Collections;
/// <summary>
/// Liquidum Main Script
/// WILEz Copyright.
/// </summary>//
[AddComponentMenu("WILEz/Liquidum")]
[System.Serializable]
public class Liquidum : MonoBehaviour {
	
	
	#region Private variables
///////////Private var////////////
	[HideInInspector]
	public GameObject TheCam; 
	public static Liquidum LiquidumScript;
	private SphereCollider CheckArea;
	private Rigidbody RB;
	private GameObject Ad;
    private GameObject StaticObj;
	private GameObject TriggerObj;
	[HideInInspector]
	public GameObject RainEmitter;
	private GameObject ThunderEffect;
	private GameObject LIQUIDUMEmitter;
	private GameObject Splash;
	[HideInInspector]
	public bool UnderOcclusion;
	private float AdjSplatY=0;
/////////////////////////////////	
	#endregion
	#region Global variables
	[W_BoxBGr("",5000,2000)]public string space1;
	[W_Header("General Configuration","",16)]public string space6="";
	[W_ToolTip("The main effect prefab\nNot change this var... if you want it to work :)")]
	public GameObject LIQUIDUM_GlobalEmitterPrefab;
		//[HideInInspector]
	[W_ToolTip("-Optional-\nWhen a Weather Event Curve is active appear here.\n\nIf have two or more events in scene you can drag here (or call SetWeatherEvent(myEvent) function), liquidum use this event and deactivate the others Wheather Events gameobject in the scene.")]
	public Liquidum_AutomaticWeatherEvents WeatherEventInUse;
//	#if UNITY_EDITOR
	public bool ShowGizmoInEditor=true;
    public bool showLiquidumStats=true;
//#endif	
	[W_ToolTip("How extensive is the camera area that detects collisions with Occlusion Area and Trigger Area in the scene")]
	public float CheckAreaRange=1.2f;//How big is the trigger area of the camera
	#endregion
	
	#region Drops On Screen Effect variables
    [W_Header("Drops On Screen Configuration","",16)]
	public string space;
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Start/Stop Drops On Screen effect.\n*N.B. Drops_RainDependence override this bool.")]
	public bool Emit=true;
	/// <summary>
	/// Drop prefab.
	/// You can switch from different prefabs at runtime
	/// calling ChangeDrops(n)function
	/// NB: All prefabs must have the same number of frames
	/// </summary>/// <summary>
	public GameObject []DropPrefab;
	[W_ToolTip("The Drops On Screen Effect is affected by the camera angle\nIt stops when the camera looks down.")]
	public bool UseCameraAngleAdjustment=true;//The "Drops On Screen Effect" stop if camera look down?
	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect with not LIQUIDUM rain effect")]
	public bool Drops_RainDependence=true;
	//Sound Variables 
	[W_ToolTip("The sound for this effect (when it is active).")]
	public AudioClip DropsSoundClip;
	[Range(0f,1f)]
	public float DropsSoundVolume=1;
	/// <summary>
	/// Color of drops.
	/// Alpha channel also manages the overall transparency of the drops
	/// </summary>/// <summary>
	[W_ToolTip("Color of drops.\nAlpha channel also manages the overall transparency of the drops")] 
	public Color DropsColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// The distortion of drops.
	/// </summary>/// <summary>
	[Range(0f,50f)]
	public float Distortion=30f;
	/// <summary>
	/// The pause time from a drop creation and the successive drop creation 
	/// How slow is the drops creation.
	/// N.B. Low number lower performance 
	/// </summary>/// <summary>
	[Range(0.1f,1f)]
	public float DropCreationDelay=0.1f;
	/// <summary>
	/// The number of particles in the system will be
	/// limited by this number. Emission will be
	/// temporarily healted if this in reached.
	/// </summary>/// <summary>
	//[Range(0f,100f)]
	//public int DropsNumberLimit=50;
	/// <summary>
	/// Maximum creation distance of the drops from screen center 
	/// (Must be higher than the Minimum Distance!)
	/// </summary>/// <summary>
	[Range(0f,1)]
	public float MaxDistanceFromCenter=0.6f;
	/// <summary>
	/// Minimum creation distance of the drops from screen center 
	/// Use it if you do not want to draw the drops on center of the screen
	/// (Must be lowest than the Maximum Distance!)
	/// </summary>/// <summary>
	[Range(0f,1f)]
	public float MinDistanceFromCenter=0.0f;
	/// <summary>
	/// How fast the drops vanish
	/// </summary>/// <summary>
	[Range(1f,20f)]
	public float DropFadeSpeed=4f;
	/// <summary>
	/// How fast the drops slide down
	/// </summary>/// <summary>
	[Range(0f,1f)]
	public float DropSlipSpeed=0.5f;
	/// <summary>
	/// The scale of drops.
	/// (Use a X low value to "slim" the drops)
	/// </summary>/// <summary>
	public Vector2 DropsScale=new Vector2(0.6f,0.7f);
	/// <summary>
	/// Drops size change randomly?
	/// </summary>/// <summary>
	public bool UseRandomScale=true;
	/// <summary>
	/// Min and max Randomscale of drops.
	/// </summary>/// <summary>
	public Vector2 RandomScale=new Vector2(1f,5f);
	/// <summary>
	/// Single Drop Speed change randomly?
	/// </summary>/// <summary>
	public bool RandomSpeed;
	/// <summary>
	/// When true clear immediately all the drops on the screen
	/// </summary>/// <summary>
	[W_ToolTip("When true clear immediately all the drops on the screen")] 
	public bool ClearAllDropsImmediately=false;
	/// <summary>
	/// The number drop frames.
	/// NB: If you use multiple dorps prefabs, all of thems must have the same number of frames
	/// </summary>/
	[W_ToolTip("The number drop frames.\nNB: If you use multiple dorps prefabs, all of thems must have the same number of frames")]  
	public int NumDropFrames=10;
	#endregion
	
	
	
	#region Trail On Screen Effect variables
	 [W_Header("Trail On Screen Configuration","",16)]
	public string space12;	
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Start/Stop this effect.\n*N.B. TrialRain_Dependence override this bool.")]
	public bool TrailEmit=true;
	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect without LIQUIDUM rain effect")]
	public bool TrialRain_Dependence=true;
	/// <summary>
	/// Color of drops.
	/// Alpha channel also manages the overall transparency of the drops
	/// </summary>/// <summary>
	public Color TrailsColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// The distortion of drops.
	/// </summary>/// <summary>
	[Range(0f,50f)]
	public float TrailDistortion=30f;
	/// <summary>
	/// The pause time from a Trail Drop creation and the successive drop creation 
	/// How slow is the drops creation.
	/// N.B. Low number lessen performance!
	/// </summary>/// <summary>
	[Range(0.5f,10f)]
	public float TrailCreationDelay=1f;
	/// <summary>
	/// The number of particles in the system will be
	/// limited by this number. Emission will be
	/// temporarily healted if this in reached.
	/// </summary>/// <summary>
	//[Range(0f,150f)]
	//public int TrailsNumberLimit=15;
	/// <summary>
	/// Maximum creation distance of the drops from screen center 
	/// (Must be higher than the Minimum Distance!)
	/// </summary>/// <summary>
	[Range(0f,1f)]
	public float TrailMaxDistanceFromCenter=0.6f;
	/// <summary>
	/// Minimum creation distance of the drops from screen center 
	/// Use it if you do not want to draw the drops on center of the screen
	/// (Must be lowest than the Maximum Distance!)
	/// </summary>/// <summary>
	[Range(0f,1f)]
	public float TrailMinDistanceFromCenter=0.0f;
	/// <summary>
	/// How fast the Trail Drops slip down
	/// </summary>/// <summary>
	[Range(1f,200f)]
	public float TrailSlipSpeed=50f;
	/// <summary>
	/// Add a irregular speed to drops
	/// </summary>/// <summary>
	[Range(0.1f,1f)]
	public float TrailDropsFriction=0.2f;
		/// <summary>
	/// How long is the tail af trial
	/// </summary>/// <summary>
	[Range(0.1f,2f)]
	public float TrialTail=0.4f;
	/// <summary>
	/// Angle of single trail drop.
	/// </summary>/// <summary>
	 [Range(-1f,1f)]
	public float TrailConstantAngle=0.1f;
	/// <summary>
	/// The gloabal scale of trail.
	/// </summary>/// <summary>
	 [Range(0.5f,10f)]
	public float TrailScale=1f;
     #endregion
	
	
	
	#region Rain Effect variables
	[W_Header("Rain Configuration","",16)]
	public string space5;
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Stop/Start Rain effect and all effects that depend on it")]
	public bool RainEmit=true;			
	/// <summary>
	/// Use simple or distrort rain prefab
	/// Warning, the distort rain have some problem over another particle effect
	/// </summary>/// <summary>
	[W_ToolTip("Use simple or distrort rain prefab\nNo change at runtime\n\n - Warning! The distort rain have some graphic problem over another particle effect\nUse it only if you can not do without it")] 
	public bool UseDistortionRain;
	/// <summary>
	/// Rain Emitter prefab 
	/// </summary>/// <summary>
	public GameObject RainEmitterPrefab;
	//Sound Variables
	public AudioClip RainSoundClip;
	[Range(0f,1f)]
	public float RainSoundVolume=1;
	[Range(0.01f,1f)]
	public float RainVolumeFadeSpeed=0.1f;
	/////////////////	
	/// <summary>
	/// Color of Rain.
	/// Alpha channel also manages the overall transparency of the Rain
	/// </summary>/// <summary>
	public Color RainColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Speed of the rain drop
	/// </summary>/// <summary>
	[Range(1f,20f)]
	public float RainSpeed=20f;
	/// <summary>
	/// Global scale of single RainDrop.
	/// </summary>/// <summary>
	[Range(0.01f,1f)]
	public float RainSize=0.1f;
	/// <summary>
	/// Global scale of single CollisionEffect.
	/// </summary>/// <summary>
	[Range(0.01f,1f)]
	public float RainCollisionSpriteSize=0.1f;
	/// <summary>
	/// Power of CollisionEffect Splash.
	/// </summary>/// <summary>
	[Range(0.01f,1f)]
	public float RainCollisionSplashPower=0.01f;
	/// <summary>
	/// Rain particles emitted per second
	/// </summary>/// <summary>
	[Range(1f,10000f)] 
	public int EmissionRate=1000;	
    /// <summary>
	/// RainDrops Costant direction Angle for all rain drops
	/// </summary>/// <summary>
	[Range(-40f,40f)] 
	public float ConstantAngle;
	[Range(0f,3f)]
	public float AddRainTurbolence;
	/// <summary>
	/// The heigth of emitter (from MainCamera) 
	/// </summary>/// <summary>
	[Range(1f,100f)]
	public float RainEmitterHeigth=10f;
	/// <summary>
	/// Rains Life Time
	/// </summary>/// <summary>
	[Range(0f,5f)] 
	public float RainLifeTime=2;
	/// <summary>
	/// Add Rain gravity
	/// </summary>/// <summary>
	[Range(0.1f,3f)] 
	public float RainGravity=1;	
		#endregion
	
	
	#region Hailstone Effect variables
	[W_Header("Hail Configuration","",16)]
	public string space52;
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Stop/Start Hail effect")]
	public bool HailEmit=true;			

	public AudioClip HailSoundClip;
	[Range(0f,1f)]
	public float HailSoundVolume=1;

	/////////////////	
	/// <summary>
	/// Color of Hail.
	/// Alpha channel also manages the overall transparency of the Hailstone
	/// </summary>/// <summary>
	public Color HailColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Speed of the Hailstone
	/// </summary>/// <summary>
	[Range(1f,20f)]
	public float HailSpeed=20f;
	/// <summary>
	/// Global scale of single Hailstone.
	/// </summary>/// <summary>
	[Range(0.1f,1f)]
	public float HailSize=0.1f;
	/// <summary>
	/// Hailstones particles emitted per second
	/// </summary>/// <summary>
	[Range(1f,10000f)] 
	public int HailEmissionRate=1000;	
    /// <summary>
	/// Hail Costant direction Angle
	/// </summary>/// <summary>
	[Range(-40f,40f)] 
	public float HailConstantAngle;
	/// <summary>
	/// The heigth of emitter (from MainCamera) 
	/// </summary>/// <summary>
	[Range(1f,100f)]
	public float HailEmitterHeigth=10f;
	/// <summary>
	/// Hail Life Time
	/// </summary>/// <summary>
	[Range(0f,5f)] 
	public float HailLifeTime=2;	
		#endregion
	
	
		
		#region Snow Effect variables
	[W_Header("Snow Configuration","",16)]
	public string space522;
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Stop/Start Snow effect")]
	public bool SnowEmit=true;			
	/////////////////	
	/// <summary>
	/// Color of Snow.
	/// Alpha channel also manages the overall transparency of the Snow
	/// </summary>/// <summary>
	public Color SnowColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Speed of the Snow
	/// </summary>/// <summary>
	[Range(1f,20f)]
	public float SnowSpeed=20f;
	/// <summary>
	/// Global scale of single Snow.
	/// </summary>/// <summary>
	[Range(0.1f,1f)]
	public float SnowSize=0.1f;
	/// <summary>
	/// Snow particles emitted per second
	/// </summary>/// <summary>
	[Range(1f,10000f)] 
	public int SnowEmissionRate=1000;	
    /// <summary>
	/// Snow Costant direction Angle 
	/// </summary>/// <summary>
	[Range(-40f,40f)] 
	public float SnowConstantAngle;
	/// <summary>
	/// The heigth of emitter (from MainCamera) 
	/// </summary>/// <summary>
	[Range(1f,100f)]
	public float SnowEmitterHeigth=10f;
	/// <summary>
	/// Snow Life Time
	/// </summary>/// <summary>
	[Range(0f,5f)] 
	public float SnowLifeTime=2;	
		#endregion
	
	
	
	
	#region Fog Effect variables
    [W_Header("Fog Configuration","",16)]
	public string space75;
	/// <summary>
	/// Add a fog effect to rain
	/// </summary>/// <summary>
	public bool UseRainFog;
	[Range(1f,3f)] 
	public float RainFogVerticalSpeed=2f;
	[Range(1f,3f)] 
	public float RainFogOrizzontalSpeed=0.5f;
	/// <summary>
	/// Color of RainFog.
	/// Alpha channel also manages the overall transparency of the RainFog
	/// </summary>/// <summary>
	public Color RainFogColor=new Color(0.6f,0.6f,0.7f,0.9f);
	/// <summary>
	/// Distance from RainFog and Camera
	/// </summary>/// <summary>
	[Range(0.5f,3f)] 
	public float RainFogDistance=1;
	/// <summary>
	/// Add a ground fog effect to rain
	/// </summary>/// <summary>
	public bool UseGroundFog;
	/// <summary>
	/// Color of GroundFog.
	/// Alpha channel also manages the overall transparency of the GroundFog
	/// </summary>/// <summary>
	public Color GroundFogColor=new Color(0.6f,0.6f,0.7f,0.9f);
	/// <summary>
	/// The heigth of emitter (from MainCamera) 
	/// </summary>/// <summary>
	[Range(-10f,10f)]
	public float GroundFogHeigth=10f;
	#endregion
	
	
	
	
		#region Cloudiness Effect variables
    [W_Header("Cloudiness Configuration","",16)]
	public string space23;
	public bool UseCloudinesslEffect;
	/// <summary>
	/// The heigth of emitter (from MainCamera) 
	/// </summary>/// <summary>
	[Range(-100f,1000f)]
	public float CloudinessHeigth=10f;
	[Range(1f,100f)]
	public int CloudsDomeScale=2;
	public Color CloudsColor=new Color(0.5f,0.5f,0.5f,0.5f);
	[Range(1f,10f)]
	public float CloudinessScale=1f;
	/// <summary>
	/// Clouds Scroll Speed.
	/// </summary>/
	public Vector2 CloudinessSpeed=new Vector2(1f,1f);
	[Range(0.1f,2f)]
	public float CloudinessFadeSpeed=0.5f;
	#endregion

	
	
	
	
	
	
	
	
	//Old version var
	/*/// <summary> 
	/// Maximum creation distance of the RainDrops from MainCamera 
	/// </summary>/// <summary>
    //	[Range(1f,50f)]
    //public float RainEmitterArea=15f;
	/// <summary>
	/// Use rain collision.
	/// </summary>/// <summary>
	//public bool RainCheckCollision=true;
	/// <summary>
	/// If "RainCheckCollision" is true
	/// the rain collide only with this layers .
	/// /// </summary>/// <summary>
    //	public LayerMask CollideWith
	/// <summary>
	/// The number of particles in the system will be
	/// limited by this number. Emission will be
	/// temporarily healted if this in reached.
	/// </summary>/// <summary>
    //	[Range(1f,50000f)]
	//public int MaxParticles=8000;	
	*/
	
	#region Additional Distrortion Effect variables
    [W_Header("Additional Distrortion Configuration","",16)]
	public string space2;
	[W_ToolTip("Start/Stop (no fade) this effect.")]
	public bool UseAdditionalDistortion;
	/// <summary>
	/// If this effect stop when rain stop.
	/// </summary>
	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect with not LIQUIDUM rain effect")]
	public bool Additional_RainDependence;
	/// <summary>
	/// The additional distortion scale.
	/// Change only on Start
	/// </summary>/
	public Vector2 AdditionalDistortionScale=new Vector2(1f,1f);
	public Color AdditionaDistortionColor=new Color(0.6f,0.6f,0.7f,0.35f);
	[Range(0f,100f)]
	public float AdditionalDistortionStrength=80f;
	[Range(0f,10f)]
	public float AdditionalDistortionSlipSpeed=3f;
	/// <summary>
	/// When true and if AdditionalDistortion is trasparent, fade to StaticColor (use the AdditionalDistortionInFadeSpeed)
	/// </summary>/// <summary>
	public bool FadeInAdditionalDistortionNow=false;
    /// <summary>
	/// When true fade to trasparent the AdditionalDistortion Effect (use the AdditionalDistortionOutFadeSpeed)
	/// </summary>/// <summary>
	public bool FadeOutAdditionalDistortionNow=false;
	[Range(0.1f,5f)]
	public float AdditionalDistortionFadeOutSpeed=0.5f;	
	[Range(0.1f,5f)]
	public float AdditionalDistortionFadeInSpeed=0.5f;
	#endregion

	
	
	#region Static Liquid Effect variables
    [W_Header("Static Liquid Configuration","",16)]
	public string space3;
	[W_ToolTip("Start/Stop (no fade) this effect.")]
	public bool UseStatic;
	/// <summary>
	/// If this effect stop when rain stop.
	/// </summary>
	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect with not LIQUIDUM rain effect")]
	public bool Static_RainDependence;
	public Vector2 StaticScale=new Vector2(1f,1f);
	public Color StaticColor=new Color(0.6f,0.6f,0.7f,0.3f);
	[Range(0f,100f)]
	public float StaticDistortion=20f;	
	/// <summary>
	/// When true and if Static is trasparent, fade to StaticColor (use the StaticInFadeSpeed)
	/// </summary>/// <summary>
	public bool FadeInStaticNow=false;
    /// <summary>
	/// When true fade to trasparent the Static Effect (use the StaticOutFadeSpeed)
	/// </summary>/// <summary>
	public bool FadeOutStaticNow=false;
	[Range(0.1f,5f)]
	public float StaticFadeOutSpeed=0.5f;	
	[Range(0.1f,5f)]
	public float StaticFadeInSpeed=0.5f;
	#endregion

	#region Splash Liquid Effect variables
	 [W_Header("Splash Liquid Configuration","",16)]
	public string space7;
	public GameObject SplashPrefab;
	public float PosOffSet=0.5f;
	public Color SplashColor=new Color(0.6f,0.6f,0.7f,0.8f);
	public Vector2 SplashScale=new Vector2(2f,1f);
	[Range(0f,5f)]
	public float SplashDistortion=0.5f;
	[Range(0.1f,2f)]
	public float SplashFadeOutSpeed=0.5f;
	[Range(10f,100f)]
	public float SplashAnimationSpeed=3f;
	[Range(0f,10f)]
	public float  SplashSlipSpeed=2f;
	#endregion
	
	#region Drains Global Effect variables
	[W_Header("Drains Global Setting","",16)]
	public string space8;
	/// <summary>
	/// Start or stop the effect.
	/// </summary>/// <summary>
	[W_ToolTip("Start/Stop (no fade) this effect.")]
	public bool DrainsEmit=true;	
	/// <summary>
	/// Use simple or distrort rain prefab
	/// Warning, the distort rain have some problem over another particle effect
	/// </summary>/// <summary>
	public bool UseDistrotDrains;	
	//Sound variables
	public AudioClip DrainSoundClip;
	[Range(0f,1f)]
	public float DrainSoundVolume=1;
	///////////////	
	/// <summary>
	/// Color of Drains.
	/// Alpha channel also manages the overall transparency of the Drains
	/// </summary>/// <summary>
	public Color DrainsColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Speed of the Drain drop
	/// </summary>/// <summary>
	[Range(0.1f,5)]
	public float DrainsSpeed=1f;
	/// <summary>
	/// Global Forward force of Drains flow in scene 
	/// </summary>/// <summary>
	[Range(0f,5f)]
	public float DrainsForwardForce=1f;
	/// <summary>
	/// Global scale of single RainDrop.
	/// </summary>/// <summary>
	[Range(0.01f,3f)]
	public float DrainsDropSize=0.1f;
	/// <summary>
	/// Global scale of single SplashCollisionEffect.
	/// </summary>/// <summary>
	[Range(0.01f,2f)]
	public float DrainsCollisionSpriteSize=0.1f;
	/// <summary>
	/// Power of CollisionEffect SplashCollisionEffect.
	/// </summary>/// <summary>
	[Range(0.01f,2f)]
	public float DrainsCollisionSplashPower=0.01f;
	/// <summary>
	/// Rain particles emitted per second
	/// </summary>/// <summary>
	[Range(1f,1000f)] 
	public int DrainsEmissionRate=100;	
	/// <summary>
	/// Drains Life Time
	/// </summary>/// <summary>
	[Range(0f,5f)] 
	public float DrainsLifeTime=2;	
	#endregion
	
	#region Cascade Global Effect variables
	[W_Header("Cascade Global Setting","",16)]
	public string space975;
	/// <summary>
	/// If All WallCascade Effects in the scene are active or not.
	/// </summary>
	public bool CascadeActive=true;
	//Sound variables
	public AudioClip CascadeSoundClip;
	[Range(0f,1f)]
	public float CascadeSoundVolume=1;
	///////////////
	/// <summary>
	/// Global Material for WallCascade effect.
	/// </summary>
	public Material CascadeMaterial;
	/// <summary>
	/// Color scrolling liquid for All WallCascade Effect in the scene.
	/// Alpha channel also manages the overall transparency 
	/// </summary>/// <summary>
	public Color CascadeMainColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Distortion for All WallCascade Effect in the scene.
	/// </summary>/// <summary>
	[Range(0f,5f)]
	public float CascadeDistortion=0.5f;
	/// <summary>
	/// The X scroll speed for All WallCascade Effect in the scene.
	/// </summary>
    public float CascadeXscrollSpeed = 0F;
	/// <summary>
	/// The Y scroll speed for All WallCascade Effect in the scene.
	/// </summary>
	public float CascadeYscrollSpeed = 0.4F;	
	/// <summary>
	/// The X scroll textures tile for All WallCascade Effect in the scene.
	/// </summary>
    [W_ToolTip("How many times the texture is repeated on the X axis")]
	public float CascadeXTile = 1.5F;
	/// <summary>
	/// The Y scroll textures tile for All WallCascade Effect in the scene.
	/// </summary>
	[W_ToolTip("How many times the texture is repeated on the Y axis")]
	public float CascadeYTile = 0.2F;
	#endregion
	
	
	#region Trigger Distortion Global Effect variables
	 [W_Header("Trigger Distortion Global Setting","",16)]
	public string space65;
	/// <summary>
	/// If this Effects is active or not.
	/// </summary>
	[W_ToolTip("Start/Stop (no fade) this effect.")]
	public bool TriggerDistortionActive=true;
	/// <summary>
	/// Color of this effect.
	/// Alpha channel also manages the overall transparency 
	/// </summary>/// <summary>
	public Color TriggerDistortionColor=new Color(0.6f,0.6f,0.7f,0.5f);
	/// <summary>
	/// Distortion this effect.
	/// </summary>/// <summary>
	[Range(0f,500f)]
	public float TriggerDistortionPower=100f;
	/// <summary>
	/// The X scroll speed for All TriggerDistortion Effect in the scene.
	/// </summary>
   	[W_ToolTip("The X scroll speed for All TriggerDistortion Effect in the scene")]
	public float TriggerDistortionXscrollSpeed = 0F;
	/// <summary>
	/// The Y scroll speed for All TriggerDistortion Effect in the scene.
	/// </summary>
	[W_ToolTip("The Y scroll speed for All TriggerDistortion Effect in the scene")]
	public float TriggerDistortionYscrollSpeed = 0.4F;	
	[Range(1f,5f)]
	public float TriggerDistortionFadeOutSpeed=3f;	
	[Range(1f,5f)]
	public float TriggerDistortionFadeInSpeed=3f;
	/// <summary>
	/// When true and if TriggerDistortion is trasparent, fade to TriggerDistortionColor (use the TriggerDistortionFadeInSpeed)
	/// </summary>/// <summary>
	public bool FadeInTriggerDistortionNow=false;
    /// <summary>
	/// When true fade to trasparent the TriggerDistortion Effect (use the TriggerDistortionFadeOutSpeed)
	/// </summary>/// <summary>
	public bool FadeOutTriggerDistortionNow=false;
	#endregion
	
	#region Thunders Effect variables
	[W_Header("Thunders Setting","",16)]
	public string space85;
	/// <summary>
	/// If Thunders Effects are active or not.
	/// </summary>
	[W_ToolTip("If you want use thunder effect in your scene")]
	public bool ThundersActive=true;
	[W_ToolTip("If you want lamp your SkyBox when the thunder emit")]
	public bool LampSkyBox=true;
	[W_ToolTip("If you want see the thunderbolts")]
	public bool UseThunderBolts=true;
	public GameObject ThunderPrefab;
		/// <summary>
	/// Distance from RainFog and Camera
	/// </summary>/// <summary>
	[Range(1f,5f)] 
	public float ThundersBoltDistance=1;
	/// <summary>
	/// Delay (seconds) from lamp light and thunder sound.
	/// N.B. This variable manage the slection of sound list (Away, Middle,Near)
	/// Low delay thunder is near the cam.
	/// </summary>/
	[Range(0,5)]
	public float ThundersDistance=1;
	[Range(1f,5f)]
	public float ThundersLightPower=3f;
	/// <summary>
	/// Color scrolling Thunders lamps Effect in the scene.
	/// </summary>/// <summary>	
	public Color ThundersLightColor=new Color(0.6f,0.6f,0.7f,0.5f);
	[Range(1f,10f)]
	public float ThundersFadeOutSpeed=3f;	
	/// <summary>
	/// Additional flash after the main lamp
	/// </summary>/
	[Range(0,5)]
	public int LightEcos=3;
	/// <summary>
	/// Optional.
	/// Drag here directional light that casts the shadows in your scene.
	/// If you use it, the thunder light will affect the scene shadows
	/// </summary>/// <summary>
	[W_ToolTip("- Optional.\nDrag here the directional light that casts the shadows in your scene\nIf you use it, the thunder light will affect the scene shadows")]
	public Light SceneDirectionalLight;
	[Range(1f,5)]
	public float ShadowInfluencing;
	//Sound variables
	public AudioClip[] AwayThundersSoundsClips;
	public AudioClip[] MiddleThundersSoundsClips;
	public AudioClip[] NearThundersSoundsClips;
	[Range(0f,1f)]
	public float ThundersSoundVolume=1;
     ///////////////
	public bool ThunderEmitNow;
	#endregion

	
	    void Awake() {
		
		
		
		transform.name="Liquidum (Main Script)";
		//Set this static variable for fast acces from other scripts
		LiquidumScript=this;
		TheCam=transform.parent.gameObject;//Get the parent of main liquidum script (this)
		DropCreationDelay*=2;//Performance Adjustment
		//We need a trigger collider over the Liquidum_Effect to check OnEnter in Occlusion or Trigger Areas
        CheckArea=gameObject.AddComponent<SphereCollider>() as SphereCollider;
		CheckArea.radius=CheckAreaRange;
		CheckArea.isTrigger=true;
		RB=gameObject.AddComponent<Rigidbody>() as Rigidbody;
		RB.isKinematic=true;
		RB.useGravity=false;
		//////////////////////////////


		
		//Create the Rain Emitter
	      if(TheCam) RainEmitter=(GameObject)GameObject.Instantiate(RainEmitterPrefab, 
				new Vector3(TheCam.transform.position.x,TheCam.transform.position.y,TheCam.transform.position.z), RainEmitterPrefab.transform.rotation); //Crea il prefab 
		else
			Debug.LogError("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller");
		
		if(ThundersActive){
		//Create the Tunder Emitter
	       ThunderEffect=(GameObject)GameObject.Instantiate(ThunderPrefab, 
				Vector3.zero, Quaternion.identity); //Crea il prefab 
		    ThunderEffect.transform.parent=RainEmitter.transform;
			ThunderEffect.transform.localPosition=new Vector3(0,10,0);
			ThunderEffect.GetComponentInChildren<Light>().color=ThundersLightColor;
			ThunderEffect.GetComponentInChildren<Light>().intensity=0;
		}
		
		
		//Create the Main emitter for all effect   
		   LIQUIDUMEmitter=(GameObject)GameObject.Instantiate(LIQUIDUM_GlobalEmitterPrefab, 
				new Vector3(1000,1000,1000), LIQUIDUM_GlobalEmitterPrefab.transform.rotation); //Create prefab and position it away
		LIQUIDUMEmitter.name="Liquidum (On-Screen Main Emitters)";
		
		//Find the prefabs under the main LIQUIDUMEmitter
		    Ad=GameObject.Find("_AdditionalDistortion");
		    StaticObj=GameObject.Find("_StaticDropsPrefab");
	    	TriggerObj=GameObject.Find("_TriggerDistortionPlane");
		////////////////////////////////////////////////
		if(!UseAdditionalDistortion)GameObject.Destroy(Ad);
		if(!UseStatic)GameObject.Destroy(StaticObj);
		
		
		if(WeatherEventInUse){WeatherEventInUse.gameObject.SetActive(true);
		if(!GameObject.Find(WeatherEventInUse.gameObject.name))   
		       Debug.LogError("Please, use a weather event already in the scene, not a prefab!");
		}
	 }
	
	void Start(){
		{
			if(!ThundersActive)GameObject.Destroy(GameObject.Find("_ThunderLightPlane"));}
		}
	
	//If you want see the drops in editor
    void OnDrawGizmosSelected() {		
		if(ShowGizmoInEditor){
		if(MinDistanceFromCenter>MaxDistanceFromCenter)
			Gizmos.color = Color.red;
		    else
            Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,MaxDistanceFromCenter*2f);
		Gizmos.color = Color.grey;
		Gizmos.DrawWireSphere(transform.position,MinDistanceFromCenter*2f);
		 }

    }

	//My lerp metod
		public static Vector3 LerpAndStop(Vector3 currentPos,Vector3 targetPos,float t){
		       float dist=Vector3.Distance(currentPos,targetPos);
	           return dist <= t ? targetPos:
			Vector3.Lerp(currentPos,targetPos,(t/dist)*10);
	}
	
	
	
	
	//Metod to set a weather event on run-time
		public void SetWeatherEvent(Liquidum_AutomaticWeatherEvents SetWeatherEvent){
		if(SetWeatherEvent){SetWeatherEvent.gameObject.SetActive(true);
		
		if(GameObject.Find(SetWeatherEvent.gameObject.name))       
		WeatherEventInUse=SetWeatherEvent;
		else{
       Debug.LogError("Use a weather event already in the scene, not a prefab!");
				}
		}else{ Debug.LogError("You've tried to change weather event at runtime but no event was chosen.");}
	}
	
	
	
	
	//Spash function (string SplashPos: Central,Left,Right,Up,Down)
			public void SplashNow(string SplashPos){ 
			if(LIQUIDUMEmitter.GetComponentsInChildren<Liquidum_Splash>().Length>0)
			AdjSplatY+=0.15f;else AdjSplatY=0;		
		
			if(AdjSplatY>1f)AdjSplatY=0;
		
			Vector3 position=Vector3.zero;
			
			if(SplashPos=="Central")
			position = new Vector3(0,0,0.45f+AdjSplatY);
			
			if(SplashPos=="Up")//Up
			position = new Vector3(0,PosOffSet,0.41f+AdjSplatY);
			
			if(SplashPos=="Down")//Down
			position = new Vector3(0,-PosOffSet,0.42f+AdjSplatY);
			
			if(SplashPos=="Left")//Left
			position = new Vector3(-PosOffSet,0,0.43f+AdjSplatY);
			
			if(SplashPos=="Right")//Right
			position = new Vector3(PosOffSet,0,0.44f+AdjSplatY);
			
		if(LIQUIDUMEmitter.GetComponentsInChildren<Liquidum_Splash>().Length<4){//4 for time
			Splash=(GameObject)GameObject.Instantiate(SplashPrefab, position, SplashPrefab.transform.rotation); //Crea il prefab 
		    Splash.transform.parent=LIQUIDUMEmitter.transform;
		    Splash.transform.localPosition=position;
			Splash.transform.localRotation=SplashPrefab.transform.rotation;	
			}
		}
		
		

	
	void UpdateStatic(){
			
				//Call the Fade IN/OUT metods on Static and Additional scripts 
		if(FadeOutStaticNow){ 
			StaticObj.GetComponent<LiquidumStatic>().FadeOut();
		    FadeOutStaticNow=false;
		}
		
		if(FadeInStaticNow){ 
			StaticObj.GetComponent<LiquidumStatic>().FadeIn();
		    FadeInStaticNow=false;
		}
	}
	
	
		void UpdateAdditional(){
			
					
		if(FadeOutAdditionalDistortionNow){ 
			Ad.GetComponent<AdditionalDistortion>().FadeOut();
		    FadeOutAdditionalDistortionNow=false;
		}
		
		if(FadeInAdditionalDistortionNow){ 
			Ad.GetComponent<AdditionalDistortion>().FadeIn();
		    FadeInAdditionalDistortionNow=false;
		}
	}
	
	
	    void UpdateTrigger(){
			
		
		////Call the Fade IN/OUT metods for TriggerDistortion
		if(FadeInTriggerDistortionNow){ 
			TriggerObj.GetComponent<Liquidum_TriggerDistortion>().FadeIn();
		    FadeInTriggerDistortionNow=false;
		}
		
				
		if(FadeOutTriggerDistortionNow){ 
			TriggerObj.GetComponent<Liquidum_TriggerDistortion>().FadeOut();
		    FadeOutTriggerDistortionNow=false;
		}
		/////////////////////////////////////////////////////
		
	}
	
	
	
	void Update() {

		
		if(UseStatic)UpdateStatic();
		if(UseAdditionalDistortion)UpdateAdditional();
        if(TriggerDistortionActive)UpdateTrigger();

		
        
		if(WeatherEventInUse)WeatherEventInUse.gameObject.SetActive(true);
}
	
		void OnGUI() {
		
		if(showLiquidumStats){

			
	     GUILayout.BeginArea(new Rect(Screen.width-400, 2, 500, 1000));
	     GUI.color = Color.green;
   		 
			
			 string CloudsInt=Liquidum_Clouds.CloudsColor.a.ToString();
			 if(CloudsInt.Length>5)CloudsInt=CloudsInt.Remove(5);	
							
		  	 string StaticInt=StaticColor.a.ToString();
			 if(StaticInt.Length>5)StaticInt=StaticInt.Remove(5);
				
			 string AdditInt=AdditionaDistortionColor.a.ToString();
			 if(AdditInt.Length>5)AdditInt=AdditInt.Remove(5);
			
			
			 string DropsInt="0";
		     if(RainEmit){				
			  DropsInt=(9.9f-DropCreationDelay).ToString();
			 if(DropsInt.Length>4)DropsInt=DropsInt.Remove(4);
			 if(DropCreationDelay>10) DropsInt="0";	
			}else{ DropsInt="0";}
			
			
			string TrailInt="0";
		     if(RainEmit){
			  TrailInt=(10f-TrailCreationDelay).ToString();
			 if(TrailCreationDelay>50) TrailInt="0";
			 if(TrailInt.Length>4)TrailInt=TrailInt.Remove(4);
			}else{ TrailInt="0";}
			
			
			
			
         GUILayout.Label("LIQUIDUM Stats"); GUI.color = Color.grey;
		 if(!WeatherEventInUse){GUILayout.Label("WeatherEventInUse: NONE");}
			else{
			
			 string Sp=WeatherEventInUse.StormPower.ToString();
			 if(Sp.Length>4)Sp=Sp.Remove(4);
								
			 string Lt=WeatherEventInUse.StormTimer.ToString();
			 if(Lt.Length>4)Lt=Lt.Remove(4);
				
			 string TotLt=WeatherEventInUse.WeatherCurve.keys[WeatherEventInUse.WeatherCurve.length-1].time.ToString();
		     if(TotLt.Length>4)TotLt=TotLt.Remove(4);
				
			 string RealT=(WeatherEventInUse.WeatherCurve.keys[WeatherEventInUse.WeatherCurve.length-1].time*WeatherEventInUse.TimeScale).ToString();
			 if(RealT.Length>5)RealT=RealT.Remove(5);
				

			GUI.color = Color.blue;	
			GUILayout.Label("WeatherEventInUse: "+WeatherEventInUse.name);
			if(WeatherEventInUse.active){
				GUI.color = Color.green;
				GUILayout.Label("Active: ON");}else{
				GUI.color = Color.red;
				GUILayout.Label("Active: OFF");		
					}
			GUI.color = Color.blue;
			GUILayout.Label("Storm Power: "+Sp);GUI.color = Color.grey;
			GUILayout.Label("Line timer: "+Lt+" (Total: "+TotLt+")");
			GUILayout.Label("Storm time scale: "+WeatherEventInUse.TimeScale);
			GUILayout.Label("Real storm duration: "+RealT+" s");}
           	GUILayout.Label("Clouds effect intesity: "+CloudsInt+" (0~1)");
			GUILayout.Label("Static effect intesity: "+ StaticInt +" (0~1)");
			GUILayout.Label("Additional distortion effect intesity: "+AdditInt+" (0~1)");
			GUILayout.Label("Rain intesity: "+EmissionRate.ToString()+" (0~10000)");
			GUILayout.Label("Drops on screen intesity: "+DropsInt+" (0~10)");
			GUILayout.Label("Trail drops intesity: "+TrailInt+" (0~10)");
			GUILayout.Label("Hail intesity: "+HailEmissionRate.ToString()+" (0~10000)");
			GUILayout.EndArea();
	}
	
		}	
	
}