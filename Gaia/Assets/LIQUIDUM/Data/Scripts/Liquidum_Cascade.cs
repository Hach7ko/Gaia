using UnityEngine;
using System.Collections;

public class Liquidum_Cascade : MonoBehaviour {
	
	
	
    [W_Header("Liquidum Cascade Unique Configuration","If you want make unique this effect, check \"MakeUnique\" to true and change below settings\nBelow settings work only if \"MakeUnique\" is true, or else use the \"Cascade Global Setting\" in the Main Liquidum_Effect script",14,"GreenColor")]
	public string InspectorSpace1;

	/// <summary>
	/// If true, this effect no follow the "Cascade Global Setting" in the Main Liquidum_Effect script
	/// </summary>
	[W_BoxBGr("",17,78,12,16)]public string space1;
	public bool MakeUnique=true;
	/// <summary>
	/// If this Cascade Effect effect is active or not.
	/// </summary>
	public bool ThisIsActive=true;
	/// <summary>
	/// If this Cascade Effect effect is rain dependent.
	/// </summary>
	public bool ThisIsRainDependent=false;
	/// <summary>
	/// material for this specific cascade effect
	/// Usefull you want use Liquidum shader with mask
	/// Usefull for Puddles
	/// </summary>
	public Material ThisCascadeMaterial;
	public AudioClip ThisSoundClip;
	[Range(0f,1f)]
	public float ThisSoundVolume=1;
	AudioSource Sound;
	[W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to true if you want controll this with a LiquidumController\n-Set to false for best performance.")]
	public bool ChangeAtRunTime =false;
	
	/// <summary>
	/// Color scrolling liquid for this specific Cascade Effect.
	/// Alpha channel also manages the overall transparency 
	/// </summary>/// <summary>
	public Color ThisMainColor=new Color(0.6f,0.6f,0.7f,0.4f);
	/// <summary>
	/// Distortion for this specific Cascade Effect.
	/// </summary>/// <summary>
	[Range(0f,5f)]
	public float ThisDistortion=3f;
	/// <summary>
	/// The X scroll speed for this specific Cascade Effect.
	/// </summary>
    public float ThisXscrollSpeed = 0F;
	/// <summary>
	/// The Y scroll speed for this specific Cascade Effect.
	/// </summary>
	public float ThisYscrollSpeed = 0.2F;	
	/// <summary>
	/// The X scroll textures tile for this specific Cascade Effect.
	/// </summary>
    public float ThisBumpXTile = 2F;
	/// <summary>
	/// The Y scroll textures tile for this specific Cascade Effect.
	/// </summary>
	public float ThisBumpYTile = 0.8F;
		/// <summary>
	/// The X scroll textures tile for this specific Cascade Effect.
	/// </summary>
    public float ThisDiffuseXTile = 2F;
	/// <summary>
	/// The Y scroll textures tile for this specific Cascade Effect.
	/// </summary>
	public float ThisDiffuseYTile = 0.8F;
	
	public Mesh UseSpecificMesh;
	Material thisMaterial;
	bool materialSet=false;
	string cloneName="_CascadeMesh";
	GameObject ClonedObject;
	float fadeSpeed=0.1f;
	Liquidum LiquidumScript;
	
	void Start () {
	LiquidumScript=Liquidum.LiquidumScript;
		
		if(!LiquidumScript){Debug.LogError("WARNING:Liquidum Main Script not found!\nPlease Drag&Drop Liquidum_Effect.prefab under your camera/player/character controller gameobject. Found it in /LIQUIDUM main directory\n");GameObject.Destroy(this);}
		
	if(transform.childCount>0&&CheckClone()){//If mesh to applicate effect exist
			   SetMaterial();
	}else{
			
			if(UseSpecificMesh)
				AddUserMesh();
				else
			    DuplicateMesh(); //Create the clone mesh
		}
		
		

		
	if(MakeUnique)	{
	if(ThisSoundClip){
	Sound = gameObject.AddComponent<AudioSource>() as AudioSource;
	Sound.volume=ThisSoundVolume;
    Sound.clip = ThisSoundClip;}
	thisMaterial.color=ThisMainColor;
	UseUniqueSetting();
		}else{
	if(LiquidumScript.CascadeSoundClip){
	Sound = gameObject.AddComponent<AudioSource>() as AudioSource;
	Sound.volume=LiquidumScript.CascadeSoundVolume;
	Sound.clip = LiquidumScript.CascadeSoundClip;}
	thisMaterial.color=LiquidumScript.CascadeMainColor;
	UseGlobalSetting();
		}
		
		
	if(Sound)Sound.maxDistance=2;
	}
	
	
	
			public void PlaySound () {
		if(Sound)
		if(!Sound.isPlaying){
		   Sound.Play();}
	}

	
			public void StopSound () {
		if(Sound)
		if(Sound.isPlaying){
	       Sound.Stop();
		   }
	}
	
	
	
	
	
	//True if a children with name "cloneName" exist
		bool CheckClone(){	
		for (int i = 0; i < transform.childCount; ++i)
        {
         if(transform.GetChild(i).name==cloneName){//If a children have the "cloneName" name
				ClonedObject=transform.GetChild(i).gameObject; //Assign the cloned Object
			return true;
			}			
		
			
        }
return false;

	}

	
	
    void DuplicateMesh() {
		ClonedObject=(GameObject)GameObject.Instantiate(gameObject,transform.position,transform.rotation);
		Destroy(ClonedObject.GetComponent<Liquidum_Cascade>());//Delete the script from childen
		Destroy(ClonedObject.GetComponent<Collider>());//Delete the collider (if exist) from childen
		ClonedObject.name=cloneName;//Set a neme for set it first children
		ClonedObject.transform.parent=transform;//Parent the clone
		ClonedObject.transform.localScale=new Vector3(1.0f,1.0f,1.0f);
		SetMaterial() ;
	}
	
	
    void AddUserMesh() {

        
		ClonedObject=new GameObject(cloneName);//Set a neme for set it first children
		ClonedObject.AddComponent<MeshRenderer>();
		ClonedObject.AddComponent<MeshFilter>();
		ClonedObject.GetComponent<MeshFilter>().mesh = UseSpecificMesh;		
		ClonedObject.transform.parent=transform;//Parent the new mesh
		ClonedObject.transform.localScale=new Vector3(1.0f,1.0f,1.0f);
		ClonedObject.transform.localPosition=Vector3.zero;
		ClonedObject.transform.localRotation=Quaternion.identity;
		SetMaterial() ;
	}
	
	
	
	
	
	
	
	    void SetMaterial() {
		CheckRenderer();

		
		if(MakeUnique) {
			ClonedObject.transform.GetComponent<Renderer>().material=ThisCascadeMaterial;		
		}else {
			ClonedObject.transform.GetComponent<Renderer>().material=LiquidumScript.CascadeMaterial;
		}
		
		
		thisMaterial= ClonedObject.transform.GetComponent<Renderer>().material;
		materialSet=true;
		
	}
	
	
	 void CheckRenderer() {

			if(!ClonedObject.GetComponent<Renderer>())//Check if have a renderer
			ClonedObject.AddComponent<MeshRenderer>(); //If no have, add one
 
	}

	    void UseUniqueSetting() {

	
		if(materialSet){
		if(ThisIsActive){
	    FadeIn(ThisMainColor);
				
				
        float ThisXoffset = Time.time * ThisXscrollSpeed;
		float ThisYoffset = Time.time * ThisYscrollSpeed;
		
        thisMaterial.SetTextureScale("_MainTex",new Vector2(ThisDiffuseXTile, ThisDiffuseYTile));
		thisMaterial.SetTextureScale("_BumpMap",new Vector2(ThisBumpXTile, ThisBumpYTile));
      //  thisMaterial.mainTextureOffset = new Vector2(ThisXoffset, ThisYoffset);

		thisMaterial.SetTextureOffset("_MainTex",new Vector2(ThisXoffset, ThisYoffset));
		thisMaterial.SetTextureOffset("_BumpMap",new Vector2(ThisXoffset, ThisYoffset));
		
		//thisMaterial.SetColor("_Color",ThisMainColor);
		thisMaterial.SetFloat("_BumpAmt",ThisDistortion*25);
			}else {
				
			FadeOut(ThisXscrollSpeed,ThisYscrollSpeed);
			}
		}
		
		
    }
	
	
	 void UseGlobalSetting() {

	
		if(materialSet&&ThisIsActive){
	if(LiquidumScript.CascadeActive){
	   FadeIn(LiquidumScript.CascadeMainColor);
			
        float ThisXoffset = Time.time * LiquidumScript.CascadeXscrollSpeed;
		float ThisYoffset = Time.time * LiquidumScript.CascadeYscrollSpeed;
		
        thisMaterial.SetTextureScale("_MainTex",new Vector2(LiquidumScript.CascadeXTile, LiquidumScript.CascadeYTile));
		thisMaterial.SetTextureScale("_BumpMap",new Vector2(LiquidumScript.CascadeXTile, LiquidumScript.CascadeYTile));


		thisMaterial.SetTextureOffset("_MainTex",new Vector2(ThisXoffset, ThisYoffset));
		thisMaterial.SetTextureOffset("_BumpMap",new Vector2(ThisXoffset, ThisYoffset));
		
		//thisMaterial.SetColor("_Color",LiquidumScript.CascadeMainColor);
		thisMaterial.SetFloat("_BumpAmt",LiquidumScript.CascadeDistortion*25);
			}else {
				
			FadeOut(LiquidumScript.CascadeYscrollSpeed,LiquidumScript.CascadeXscrollSpeed);
			}
		}
    }
	
	void FadeOut(float speedY,float speedX) {
		if(thisMaterial.color.a>0.0001f){

		thisMaterial.color=Color.Lerp(thisMaterial.color,new Color(0,0,0,0),Time.deltaTime*1.5f);
		float ThisXoffset = Time.time * speedY;
		float ThisYoffset = Time.time * speedX;
		thisMaterial.SetTextureOffset("_MainTex",new Vector2(ThisXoffset, ThisYoffset));
		thisMaterial.SetTextureOffset("_BumpMap",new Vector2(ThisXoffset, ThisYoffset));
		}else{ClonedObject.transform.GetComponent<Renderer>().enabled = false;}
			
	}
	

	
		
	
	
	void FadeIn(Color targColor) {
		ClonedObject.transform.GetComponent<Renderer>().enabled = true;
		thisMaterial.color=Color.Lerp(thisMaterial.color,targColor,fadeSpeed*Time.deltaTime*1.5f);
	}
	
	
    void Update() {

		
		if(MakeUnique) {
				
			if(ChangeAtRunTime){	
				
				
			if(ThisIsRainDependent){
				ThisIsActive=LiquidumScript.RainEmit;
				fadeSpeed=LiquidumScript.EmissionRate/300;
			}
			
			if(ThisIsActive)PlaySound();else StopSound();
	      UseUniqueSetting();
		
		}else {
		
        float ThisXoffset = Time.time * ThisXscrollSpeed;
		float ThisYoffset = Time.time * ThisYscrollSpeed;
		thisMaterial.SetTextureScale("_MainTex",new Vector2(ThisDiffuseXTile, ThisDiffuseYTile));
		thisMaterial.SetTextureScale("_BumpMap",new Vector2(ThisBumpXTile, ThisBumpYTile));

		thisMaterial.SetTextureOffset("_MainTex",new Vector2(ThisXoffset, ThisYoffset));
		thisMaterial.SetTextureOffset("_BumpMap",new Vector2(ThisXoffset, ThisYoffset));	

		}

			
		}else {
				
				
			if(ThisIsActive&&LiquidumScript.CascadeActive)PlaySound();else StopSound();
		  UseGlobalSetting();
		}
			
			

		
    }
	
	
	
}
