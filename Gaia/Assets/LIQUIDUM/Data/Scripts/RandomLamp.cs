using UnityEngine;
using System.Collections;

public class RandomLamp : MonoBehaviour {
	
		
	Liquidum LiquidumScript;
	Color Original;
	float MyTimer;
	Color SkyBoxOriginalColor;
	Color targetTmpSkyColor;
	float addY;
	float RanSkyLamp;
	Skybox skyBox;
	
	void Start () {
		LiquidumScript=Liquidum.LiquidumScript;
		GetComponent<Light>().intensity=LiquidumScript.ThundersLightPower/30;
		GetComponent<Light>().color=LiquidumScript.ThundersLightColor/10;
		skyBox=LiquidumScript.TheCam.gameObject.GetComponent<Skybox>();
		//Get the original Sky Color 
		if(skyBox&&LiquidumScript.LampSkyBox){			
		SkyBoxOriginalColor=skyBox.material.GetColor("_Tint");
		if(Camera.main.gameObject.GetComponent<PreserveSkyBoxColor>()&&
		Camera.main.gameObject.GetComponent<PreserveSkyBoxColor>().enabled)
		SkyBoxOriginalColor=PreserveSkyBoxColor.SkyBoxOriginalColor;
		}
		
		
	}

	
	void Update () {
		MyTimer+=Time.deltaTime;
		addY+=(6-LiquidumScript.LightEcos)*Time.deltaTime;
		transform.position=new Vector3(transform.position.x,transform.position.y+addY,transform.position.z);
		
		if(MyTimer<LiquidumScript.LightEcos/2) {			
		
			if(skyBox&&SkyBoxOriginalColor.a<=0.9){
		 RanSkyLamp=Random.Range(LiquidumScript.ThundersLightPower,SkyBoxOriginalColor.a);
					}else{
		 RanSkyLamp=Random.Range(LiquidumScript.ThundersLightPower,SkyBoxOriginalColor.a/2);
			}
		GetComponent<Light>().intensity=Random.Range(LiquidumScript.ThundersLightPower,0);
			
			
		//Set the Sky color of lamp color
		if(MyTimer<LiquidumScript.LightEcos/2.2f)
			if(skyBox&&LiquidumScript.LampSkyBox)LiquidumScript.TheCam.gameObject.GetComponent<Skybox>().material.SetColor("_Tint",new Color(SkyBoxOriginalColor.r,SkyBoxOriginalColor.g,SkyBoxOriginalColor.b,SkyBoxOriginalColor.a*RanSkyLamp));
		}
		else
		{	
			if(skyBox&&LiquidumScript.LampSkyBox)LiquidumScript.TheCam.gameObject.GetComponent<Skybox>().material.SetColor("_Tint",SkyBoxOriginalColor);
			Destroy(gameObject);
		}
		

	}
}
