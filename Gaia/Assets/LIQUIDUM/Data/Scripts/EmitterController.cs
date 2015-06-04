using UnityEngine;
using System.Collections;

public class EmitterController : MonoBehaviour {
	
	
	[W_Header("Liquidum Emitter Controller Configuration","With this button you can turn On/Off Drain and Cascade",14,"GreenColor")]
	public string InspectorSpace1;
	
	public KeyCode OnKey;
	public KeyCode OffKey;
	/// <summary>
	/// Emitters how control this Controller
	/// </summary>/// <summary>
	public GameObject []Emitters;
	
	void Start () {  
	
	GetComponent<Renderer>().material.color=Color.green;
		}
	
	
		void OnTriggerStay(Collider other) {
	 if(other.gameObject.GetComponentInChildren<Liquidum>()){
			
		if(Input.GetKeyDown(OnKey)){//On press Key 
				for (var i = 0; i < Emitters.Length; i++)
        {
					if(Emitters[i]){
					LiquidumDrainEmitter Drain =Emitters[i].transform.GetComponent<LiquidumDrainEmitter>();
					Liquidum_Cascade Cascade = Emitters[i].transform.GetComponent<Liquidum_Cascade>();
					
					
				if(Drain)
			       if(Drain.ThisIsActive)Drain.ThisIsActive=false;
				
				if(Cascade)
			        if(Cascade.ThisIsActive)Cascade.ThisIsActive=false;
					
						
						GetComponent<Renderer>().material.color=Color.black;
					}
	}		
		}		
			
					if(Input.GetKeyDown(OffKey)){//On press Key 
				for (var i = 0; i < Emitters.Length; i++)
        {
					if(Emitters[i]){
					LiquidumDrainEmitter Drain =Emitters[i].transform.GetComponent<LiquidumDrainEmitter>();
					Liquidum_Cascade Cascade = Emitters[i].transform.GetComponent<Liquidum_Cascade>();
					
				if(Drain)
			       if(!Drain.ThisIsActive)Drain.ThisIsActive=true;
				
				if(Cascade)
			      if(!Cascade.ThisIsActive)Cascade.ThisIsActive=true;
					}
					
					GetComponent<Renderer>().material.color=Color.green;
	}	
				
}

			
		}
	}
	
	
	
	    void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
            
		for (var i = 0; i < Emitters.Length; i++)
        {
			if(Emitters[i])Gizmos.DrawLine(transform.position, Emitters[i].transform.position);
}
		
		
    }
	

	void Update () {

	}
}
