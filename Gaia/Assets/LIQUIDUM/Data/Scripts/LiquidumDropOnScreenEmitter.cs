using UnityEngine;
using System.Collections;

public class LiquidumDropOnScreenEmitter : MonoBehaviour {
	
	
	Liquidum LiquidumScript;
	//private int OrDropsNumberLimit;	
	public static float OrDelay;	
	private int ActualNumberOfDropOnScreen;
	private GameObject SelectedPrefab;
	private GameObject Drop;
	private AudioSource DropsSound;
	private float MyTimer;
	
	void Start () {
	
	LiquidumScript=Liquidum.LiquidumScript;
		
		//OrDropsNumberLimit=LiquidumScript.DropsNumberLimit;
        OrDelay=LiquidumScript.DropCreationDelay;
		
		ChangeDrops(0);//Default prefab, the first of array (in case you want have more Drops Prefabs)
		
		//Get and set the audio data for the drops on screen
		if(LiquidumScript.DropsSoundClip){
		DropsSound = gameObject.AddComponent<AudioSource>() as AudioSource;
		DropsSound.volume=LiquidumScript.DropsSoundVolume;
        DropsSound.clip = LiquidumScript.DropsSoundClip;
        DropsSound.loop=true;
			 }	
		

		
	}

	
	
	void DropOnScreen(){
		
		if(!LiquidumScript.UnderOcclusion)
		LiquidumScript.Emit=LiquidumScript.RainEmit;
				//Drops on screen emit
		if(LiquidumScript.RainEmitter.GetComponent<LiquidumRainEmitter>().RainTimer>1&&LiquidumScript.Emit&&MyTimer>=LiquidumScript.DropCreationDelay){//&&ActualNumberOfDropOnScreen<LiquidumScript.DropsNumberLimit
	    	
			float Max=LiquidumScript.MaxDistanceFromCenter;
			
	    	Vector3 position = new Vector3(
				Random.Range(-Max*1.5f, Max*1.5f),
				Random.Range(-Max, Max),
				1+Random.Range(-Max/150,Max/150));
			
	
			if(position.magnitude>(LiquidumScript.MinDistanceFromCenter*2.5f)){
			Drop=(GameObject)GameObject.Instantiate(SelectedPrefab, position, SelectedPrefab.transform.rotation); //Crea il prefab 
		    Drop.transform.parent=transform;
		    Drop.transform.localPosition=position;
		    MyTimer=0;

				if(DropsSound != null) {
				if(!DropsSound.isPlaying)
					DropsSound.Play();
				}
				
			}
		}
		
		//StopThe sound of drop on screen if Cam Look down
	//	if(DropsSound.clip)
	//	if(!LiquidumScript.Emit||LiquidumScript.DropsNumberLimit<=0){DropsSound.Stop();}
		
				//StopThe sound of drop on screen if Cam Look down
		if(DropsSound != null) {
		if(DropsSound.clip)
		if(!LiquidumScript.Emit||LiquidumScript.DropCreationDelay>1){
				DropsSound.Stop();
			}
		}
	
	}
	
	
	
	void DropOnScreenNoDipendence(){
				//Drops on screen emit
		if(LiquidumScript.Emit&&MyTimer>=LiquidumScript.DropCreationDelay){	//&&ActualNumberOfDropOnScreen<LiquidumScript.DropsNumberLimit
			
			
	    	Vector3 position = new Vector3(
				Random.Range(-LiquidumScript.MaxDistanceFromCenter*1.5f, LiquidumScript.MaxDistanceFromCenter*1.5f),
				Random.Range(-LiquidumScript.MaxDistanceFromCenter, LiquidumScript.MaxDistanceFromCenter),
				1+Random.Range(-LiquidumScript.MaxDistanceFromCenter,LiquidumScript.MaxDistanceFromCenter));
			
			
			if(position.magnitude>LiquidumScript.MinDistanceFromCenter*2f){
			Drop=(GameObject)GameObject.Instantiate(SelectedPrefab, position, SelectedPrefab.transform.rotation); //Crea il prefab 
		    Drop.transform.parent=transform;
		    Drop.transform.localPosition=position;
		    MyTimer=0;
				
				if(!DropsSound.isPlaying)
				DropsSound.Play();
				
			}
		}
		
		//Stop sound of drop on screen if emmission=0
	//	if(!LiquidumScript.Emit||LiquidumScript.DropsNumberLimit<=0||LiquidumScript.DropCreationDelay>1){DropsSound.Stop();}
				//Stop sound of drop on screen if emmission=0
		if(!LiquidumScript.Emit||LiquidumScript.DropCreationDelay>1){DropsSound.Stop();}
	

	}
	

	
	//If you have a more of DropPrefab
		public void ChangeDrops(int n){
         SelectedPrefab=LiquidumScript.DropPrefab[n];//change prefab

    }
	
	
	void Update () {
		MyTimer+=Time.deltaTime;
	//	ActualNumberOfDropOnScreen=GetComponentsInChildren<Liquidum_Drops>().Length;
		
		if(!LiquidumScript.TheCam){
			Debug.LogError("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller");
			return; }
	
		//Use the camera angle to manage the number of drops on screen
	/*	if(LiquidumScript.UseCameraAngleAdjustment){
			LiquidumScript.DropsNumberLimit+=(int)(LiquidumScript.TheCam.transform.forward.y*5);
		    LiquidumScript.DropsNumberLimit=Mathf.Clamp(LiquidumScript.DropsNumberLimit,0,OrDropsNumberLimit);
			if(LiquidumScript.TheCam.transform.forward.y<0)LiquidumScript.DropsNumberLimit=0;
	
		}*/
		
			

		
		
		
		//Drops on screen emit
	     if(LiquidumScript.Drops_RainDependence)DropOnScreen();else DropOnScreenNoDipendence();
		

		//Clear all drops on screen
		if(LiquidumScript.ClearAllDropsImmediately){       
		for (var i = 0; i < GetComponentsInChildren<Liquidum_Drops>().Length; i++)
        {GetComponentsInChildren<Liquidum_Drops>()[i].ClearImmediate();
		}LiquidumScript.ClearAllDropsImmediately=false;
		}
		
		
		
		//Use the camera angle to manage the number of drops on screen (use 5 angles)
		if(LiquidumScript.UseCameraAngleAdjustment){
			
			if(LiquidumScript.TheCam.transform.forward.y<-0.05)LiquidumScript.DropCreationDelay=10;
	           else if (LiquidumScript.TheCam.transform.forward.y<0.25f)LiquidumScript.DropCreationDelay=OrDelay*5f;
				  else if (LiquidumScript.TheCam.transform.forward.y<0.5f)LiquidumScript.DropCreationDelay=OrDelay*2f;
		        	else if (LiquidumScript.TheCam.transform.forward.y<0.75f)LiquidumScript.DropCreationDelay=OrDelay*1.5f;
			          else if (LiquidumScript.TheCam.transform.forward.y<0.9f)LiquidumScript.DropCreationDelay=OrDelay;
			    else LiquidumScript.DropCreationDelay=OrDelay/2f;
			
		}
		
		
		
		
		
		
		
		//Limit for performance
		LiquidumScript.DropCreationDelay=Mathf.Max(LiquidumScript.DropCreationDelay,0f);
		OrDelay=Mathf.Max(OrDelay,0f);
		
	}
}
