using UnityEngine;
using System.Collections;

public class Liquidum_Clouds : MonoBehaviour {

	Liquidum LiquidumScript;

	public static Color CloudsColor;

	public static bool fadeI,fadeO;
		

	public static Color OrColor;
	[W_ToolTip("-Use this bool if you want change this effect at runtime.(Fading and Heigth)\n-Set to false if clouds are static.\n-Set to false for best performance.")]
	public bool ChangeAtRunTime =false;

	
	void Awake () {
	LiquidumScript=Liquidum.LiquidumScript;
transform.localScale*=LiquidumScript.CloudsDomeScale;
	if(Liquidum.LiquidumScript.UseCloudinesslEffect)
	SetClouds();
		else
	GameObject.Destroy(gameObject);
	}
	

	void SetClouds () {
		
	if(LiquidumScript.WeatherEventInUse){
	LiquidumScript.CloudsColor=Liquidum.LiquidumScript.WeatherEventInUse.CloudsColor;
		
		}else fadeI=true;
		
	OrColor=LiquidumScript.CloudsColor;	
	transform.localPosition=new Vector3(transform.localPosition.x,-15+LiquidumScript.CloudinessHeigth,transform.localPosition.z);
	GetComponent<Renderer>().material.SetTextureScale("_MainTex",(new Vector2(10/LiquidumScript.CloudinessScale, 10/LiquidumScript.CloudinessScale)));
		
	
	}
	
	void MoveTexture(){
		
		        float ThisXoffset = Time.time * LiquidumScript.CloudinessSpeed.x/500;
		        float ThisYoffset = Time.time * LiquidumScript.CloudinessSpeed.y/500;

	GetComponent<Renderer>().material.SetTextureOffset("_MainTex",(new Vector2(ThisXoffset, ThisYoffset)));
	}
	
	
		void FadeIN () {
		fadeO=false;
		if(CloudsColor.a<1)
		CloudsColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),OrColor,LiquidumScript.CloudinessFadeSpeed/5*Time.deltaTime);
		if(CloudsColor.a>=OrColor.a-0.01f)fadeI=false;//End of FadeIn
	}
	
		void FadeOUT () {
		fadeI=false;
		if(CloudsColor.a>0)
		CloudsColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),new Color(0,0,0,0),LiquidumScript.CloudinessFadeSpeed/5*Time.deltaTime);
		if(CloudsColor.a<0.01f){fadeO=false; }//End of FadeOut
	}
	
	
	
	
    void Update () {		
	if(ChangeAtRunTime){
		SetClouds();
		MoveTexture();
		
	if(fadeI)
			FadeIN();
		
	if(fadeO)
			FadeOUT();
		}
		
		GetComponent<Renderer>().material.SetColor("_Color",CloudsColor);
		
	
	}
	
}
