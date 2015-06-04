using UnityEngine;
using System.Collections;

public class WeatherEventArea : MonoBehaviour {


	[W_Header("Liquidum Weather Event Area","When the Main Camera enter on this area\nthe selected AutomaticWeatherEvents start ",14,"GreenColor")]
	public string InspectorSpace1;
	
	 Liquidum LiquidumScript;
	public Liquidum_AutomaticWeatherEvents WeatherEvent;
	
	public enum Check {OnEnter,OnExit,Both}
	public Check check = Check.Both;
	
	void Start () {
	LiquidumScript=Liquidum.LiquidumScript;
	
	}
	
	
	    void OnTriggerEnter(Collider other) {
		
		
		if(check== Check.Both||check== Check.OnEnter) {

		
       if(other.gameObject.GetComponent<Liquidum>()){
	            LiquidumScript.WeatherEventInUse=WeatherEvent;
				LiquidumScript.SetWeatherEvent(WeatherEvent);
				WeatherEvent.active=true;

				
	
				
			}
		}
    }
	

	    void OnTriggerExit(Collider other) {
		if(check== Check.Both||check== Check.OnExit) {
		
		
        if(other.gameObject.GetComponent<Liquidum>()){
	

				WeatherEvent.active=false;
				
				
			}
		}
    }
}
