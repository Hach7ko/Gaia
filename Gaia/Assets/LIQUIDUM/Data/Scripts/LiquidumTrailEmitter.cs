using UnityEngine;
using System.Collections;

public class LiquidumTrailEmitter : MonoBehaviour {
	
	
	Liquidum LiquidumScript;
	public GameObject TrailDropPrefab;
	GameObject TrailDrop;
	public Material DefaultTrialMaterial;
	TrailRenderer trailRender;
	public bool Emit=true;
	public bool ChangeMaterialAtRunTime=false;
	float MyTimer;
	bool randomize;
	
	void Start () {
	
	LiquidumScript=Liquidum.LiquidumScript;
		
	if(!LiquidumScript){Debug.LogError("WARNING:Liquidum Main Script not found!\nPlease Drag&Drop Liquidum_Effect.prefab under your camera/player/character controller gameobject.\nFound it in /LIQUIDUM main directory\n");return;}
		
		TrailDropPrefab.GetComponent<Renderer>().material=DefaultTrialMaterial;
			
		if(!ChangeMaterialAtRunTime){
			TrailDropPrefab.GetComponent<TrailRenderer>().GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.TrailsColor/2);;
		    TrailDropPrefab.GetComponent<TrailRenderer>().GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.TrailDistortion*4);
				}
		
		
	
	}
	
	
		void TrialEmit () {
				//Trial on screen emit
	     if(LiquidumScript.TrialRain_Dependence&&!LiquidumScript.UnderOcclusion)Emit=LiquidumScript.RainEmit;else Emit=LiquidumScript.TrailEmit;
		
		
		MyTimer+=Time.deltaTime;
		
			if(Emit&&MyTimer>=LiquidumScript.TrailCreationDelay){ //if(Emit&&MyTimer>=LiquidumScript.TrailCreationDelay&&GetComponentsInChildren<LiquidumTrailDrop>().Length<LiquidumScript.TrailsNumberLimit){
			
				Vector3 position = new Vector3(
				Random.Range((-LiquidumScript.TrailMaxDistanceFromCenter-LiquidumScript.TrailMinDistanceFromCenter)*2.2f, (LiquidumScript.TrailMaxDistanceFromCenter+LiquidumScript.TrailMinDistanceFromCenter)*2f),
				1.2f,
				2 );
			if(position.x>-2&&position.x<1.5f)
			if(position.magnitude>=(LiquidumScript.TrailMinDistanceFromCenter)*4.5f){
			TrailDrop=(GameObject)GameObject.Instantiate(TrailDropPrefab, transform.position, transform.rotation); //Crea il prefab 
		    TrailDrop.transform.parent=transform;
		    TrailDrop.transform.localPosition=position; 
			
			if(ChangeMaterialAtRunTime){
			trailRender=TrailDrop.GetComponent<TrailRenderer>();
			trailRender.GetComponent<Renderer>().material.SetColor("_Color",LiquidumScript.TrailsColor/2);
		    trailRender.GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.TrailDistortion*4);
				}
				
				
		    MyTimer=0;
			}
	  }
	}
	
	
	
	
	
	void Update () {

			TrialEmit();
	}
}
