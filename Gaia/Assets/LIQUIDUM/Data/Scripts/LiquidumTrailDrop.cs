using UnityEngine;
using System.Collections;

public class LiquidumTrailDrop : MonoBehaviour {

	 float speed;

	float MyTimer;
	float YSpeed,XSpeed,TmpYSpeed;
	Liquidum LiquidumScript;
	float Ran;
	float updateInterval=0.2f;
	TrailRenderer ThisTrailRender;
	TrailRenderer coda;
	TrailRenderer sfondo;
	float scale;
	float RanScale=1;
	float GoYSpeed=1;
	float GoXSpeed=1;
	
	[W_ToolTip("-Use this bool if you want view changing at runtime.\n-Set to false for best performance.")]
	public bool ChangeAtRunTime =false; 
	[W_ToolTip("-Use this bool to add a additional background trial.\n-Set to false for best performance.")]
	public bool UsebackgroundTrial =false; 
	
	void Start () {
	
		LiquidumScript=Liquidum.LiquidumScript;
		ThisTrailRender=GetComponent<TrailRenderer>();
		scale=LiquidumScript.TrailScale/100;
		Color MainColor=LiquidumScript.TrailsColor/5;
		Color sfondoColor=(MainColor/30)+new Color(0.02f,0.02f,0.02f,0.02f);	
		coda=transform.GetChild(0).GetComponentInChildren<TrailRenderer>();
		coda.GetComponent<Renderer>().material.SetColor("_Color",MainColor);
		coda.GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.TrailDistortion*8f);
		if(sfondo)sfondo=transform.GetChild(1).GetComponentInChildren<TrailRenderer>();
		if(sfondo)sfondo.GetComponent<Renderer>().material.SetColor("_Color",sfondoColor);
		if(sfondo)sfondo.GetComponent<Renderer>().material.SetFloat("_BumpAmt",LiquidumScript.TrailDistortion*5f);
		
		
		TrialUpdate();
	}
	
	void TrialUpdate(){	
		

		
	ThisTrailRender.startWidth=scale*RanScale;
	coda.startWidth=scale*RanScale*0.6f;
    if(sfondo)sfondo.startWidth=scale*RanScale*3f;
	ThisTrailRender.time=LiquidumScript.TrialTail/5f;
	coda.time=LiquidumScript.TrialTail*1.5f;
	if(sfondo)sfondo.time=LiquidumScript.TrialTail*3f;
		
		
			 
    }
	
	    void OnDrawGizmosSelected() {		

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,0.001f+(transform.localScale.magnitude/25));
			 
    }

	void Update () {
		
		speed=LiquidumScript.TrailSlipSpeed;

		
		MyTimer+=Time.deltaTime;	
		if(MyTimer>updateInterval){
			
		     TmpYSpeed=Random.Range(1f,speed)* Time.deltaTime;
			
			
			
		 XSpeed=Random.Range(-0.05f,0.05f);
		 Ran=Random.Range(0f,1f);
			
			if(YSpeed<0.001){
			RanScale+=0.5f* Time.deltaTime;
			XSpeed+=1.5f* Time.deltaTime;
			}else{		RanScale-=0.02f* Time.deltaTime;
			XSpeed-=1.5f* Time.deltaTime;
			}		
			

			RanScale=Mathf.Clamp(RanScale,1f,2);

		if(ChangeAtRunTime)	
       TrialUpdate();
			
	MyTimer=0;
			
	}		
			YSpeed=Mathf.Lerp(YSpeed,TmpYSpeed, 0.02f* Time.deltaTime);
		
			if(Ran<LiquidumScript.TrailDropsFriction){
		     GoYSpeed=YSpeed/2f;
           GoXSpeed=XSpeed* Time.deltaTime;
	        }else{
		     GoYSpeed=YSpeed*3f;
           GoXSpeed=XSpeed/1.5f* Time.deltaTime;
	        }
		
		
		if(Time.timeScale>0.01f){
		transform.position -= transform.up * GoYSpeed;
		transform.position += transform.right *(GoXSpeed+LiquidumScript.TrailConstantAngle/100);
		}

		
	if(transform.localPosition.y<-3f*LiquidumScript.TrialTail)
	Destroy(gameObject);
	
	}
	



}