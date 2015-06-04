using UnityEngine;
using System.Collections;

public class LiquidumOcclusionArea : MonoBehaviour {

	[W_Header("Liquidum Occlusion Area","When the Main Camera enter on this area, the selected effect below start automatic fade out",14,"GreenColor")]
	public string InspectorSpace1;
	public bool FadeStaticEffect;
	public bool FadeAdditionaEffect;
	public bool FadeDropsOnScreenEffect;
	public bool StopTrailDropsEmission;
	GameObject Liquidum_RainFog;
	 Liquidum LiquidumScript;
	
	public enum Check {OnEnter,OnExit,Both}
	public Check check = Check.Both;
	
	void Start () {
	LiquidumScript=Liquidum.LiquidumScript;
	Liquidum_RainFog=GameObject.Find("Liquidum (Fog&Clouds&Bolts)");
	}
	
	
	    void OnTriggerEnter(Collider other) {
		
		
		if(check== Check.Both||check== Check.OnEnter) {

		
       if(other.gameObject.GetComponent<Liquidum>()){
	
				LiquidumScript.UnderOcclusion=true;
				
			if(Liquidum_RainFog){
					Liquidum_RainFog.GetComponent<Liquidum_RainFogEffect>().ChilderCones[0].GetComponent<FogConeScript>().fadeO=true;
				    Liquidum_RainFog.GetComponent<Liquidum_RainFogEffect>().ChilderCones[1].GetComponent<FogConeScript>().fadeO=true;
			}
				
				
			if(FadeDropsOnScreenEffect){
			LiquidumScript.ClearAllDropsImmediately=true;
			LiquidumScript.Emit=false;
			}
			
			if(FadeAdditionaEffect){
				LiquidumScript.FadeOutAdditionalDistortionNow=true;//Fade in the  Additional Distortion Effect
			}
			
			if(FadeStaticEffect){
				LiquidumScript.FadeOutStaticNow=true;//Dissolve the Static Effect
			}
			
				
				
			if(StopTrailDropsEmission){
				LiquidumScript.TrailEmit=false;//Dissolve the Trail Effect
			}
				
			}
		}
    }
	

	    void OnTriggerExit(Collider other) {
		if(check== Check.Both||check== Check.OnExit) {
		
		
        if(other.gameObject.GetComponent<Liquidum>()){
	
				
				
			LiquidumScript.UnderOcclusion=false;
				
								
			if(Liquidum_RainFog){
					Liquidum_RainFog.GetComponent<Liquidum_RainFogEffect>().ChilderCones[0].GetComponent<FogConeScript>().fadeI=true;
			    	Liquidum_RainFog.GetComponent<Liquidum_RainFogEffect>().ChilderCones[1].GetComponent<FogConeScript>().fadeI=true;
				}
			
			if(FadeDropsOnScreenEffect){
			LiquidumScript.Emit=true;
			}
			
			if(FadeAdditionaEffect){
				LiquidumScript.FadeInAdditionalDistortionNow=true;//Fade in the  Additional Distortion Effect
			}
			
			if(FadeStaticEffect){
				LiquidumScript.FadeInStaticNow=true;//Dissolve the Static Effect
			}
			
						if(StopTrailDropsEmission){
				LiquidumScript.TrailEmit=true;//Dissolve the Trail Effect
			}	
				
				
				
			}
		}
    }
}
