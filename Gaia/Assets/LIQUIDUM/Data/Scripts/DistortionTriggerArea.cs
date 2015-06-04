using UnityEngine;
using System.Collections;

public class DistortionTriggerArea : MonoBehaviour {

	[W_Header("Liquidum Distortion Area","When the Main Camera enter on this area, activate the \"Distortion Trigger Effect\" ",14,"GreenColor")]
	public string InspectorSpace1;
	Liquidum LiquidumScript;
	LiquidumDrainEmitter DrainParent;
	Liquidum_Cascade CascadeParent;
	bool OnEnterEnabled=true;
	/// <summary>
	/// If true, this trigger area is enabled only when the parent (Cascade or Drain) is active.
	/// </summary>
	[W_ToolTip("If true, this trigger area is enabled only when the parent (Cascade or Drain) is active.")] 
	public bool IsParentDependent=false;
	
	void Start () {
	LiquidumScript=Liquidum.LiquidumScript;
		//Check if this TriggerArea is under a Drain effect
		if(transform.parent.GetComponent<LiquidumDrainEmitter>())
			DrainParent=transform.parent.GetComponent<LiquidumDrainEmitter>();

		
		//Check if this TriggerArea is under a Cascade effect
		if(transform.parent.GetComponent<Liquidum_Cascade>())
			CascadeParent=transform.parent.GetComponent<Liquidum_Cascade>();
	}

	void OnTriggerEnter(Collider other) {
	 if(other.gameObject.GetComponent<Liquidum>()&&OnEnterEnabled){
			
			
			LiquidumScript.FadeInTriggerDistortionNow =true;//Fade in the  Additional Distortion Effect
	
		}
	}
	
		void OnTriggerExit(Collider other) {
	 if(other.gameObject.GetComponentInChildren<Liquidum>()){
			
			LiquidumScript.FadeOutTriggerDistortionNow =true;//Fade in the  Additional Distortion Effect
		
		}
	}
	
	
		void Update () {
		    //If DrainParent exist, link this trigger with parent emitter
		if(IsParentDependent){
			if(DrainParent)OnEnterEnabled=DrainParent.ThisIsActive;
		    if(CascadeParent)OnEnterEnabled=CascadeParent.ThisIsActive;
		}
	}
	
	
	
	
}
