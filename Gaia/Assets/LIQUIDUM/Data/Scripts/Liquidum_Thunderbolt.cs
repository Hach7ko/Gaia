using UnityEngine;
using System.Collections;

public class Liquidum_Thunderbolt  : MonoBehaviour {

	public Transform [] bolts;
	bool BoltNow=false;
	int RanBolt;
	int Oframe;
	int Vframe;
	void Start () {
		if(!Liquidum.LiquidumScript.UseThunderBolts||!Liquidum.LiquidumScript.ThundersActive)GameObject.Destroy(gameObject);
		
		transform.name="W_ThunderBolts";
		transform.localScale=new Vector3(Liquidum.LiquidumScript.ThundersBoltDistance,1+(Liquidum.LiquidumScript.ThundersBoltDistance/3),Liquidum.LiquidumScript.ThundersBoltDistance); 
		 bolts =new Transform[transform.GetChildCount()];
		for (var i = 0; i < transform.GetChildCount(); i++) {
			bolts[i]=transform.GetChild(i);	
			bolts[i].GetComponent<Renderer>().enabled=false;
		}  
	
	}
	
	public void  BoltEmit(float LightIntensity){
		
		if(!BoltNow){//OneTime
		RanBolt= Random.Range(0,transform.GetChildCount()-1);
		Oframe=Random.Range(0,4);
		Vframe=Random.Range(1,2);
		bolts[RanBolt].GetComponent<Renderer>().enabled=true;
		BoltNow=true;}

				
		bolts[RanBolt].GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(0.25f*Oframe,0.5f*Vframe));
	
		bolts[RanBolt].GetComponent<Renderer>().material.SetColor("_TintColor",new Color(LightIntensity,LightIntensity,LightIntensity,LightIntensity)*Random.Range(1,3));
		if(LightIntensity<=0.01f){bolts[RanBolt].GetComponent<Renderer>().enabled=false;BoltNow=false;}
		
	}

	
}
