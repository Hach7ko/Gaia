using UnityEngine;
using System.Collections;

public class Liquidum_Drops : MonoBehaviour {



	Liquidum LiquidumScript;
	private float FadeSpeed;
	private int NumDropFrames;
	private float frame;
	private Vector3 DropsScale;
	private float RandomS;
	private Color DropsColor;
	private float Distortion;
	private bool ClearingNow;
	
	
	void Awake()
	{
		//QualitySettings.vSyncCount = 0;//ForDebug
	}



	void Start()
	{

		LiquidumScript=Liquidum.LiquidumScript;

		NumDropFrames=LiquidumScript.NumDropFrames;

		DropsScale=new Vector3((LiquidumScript.DropsScale.x*10),1,(LiquidumScript.DropsScale.y)*10);

		DropsColor=LiquidumScript.DropsColor;
		Distortion=LiquidumScript.Distortion*5;
		
		frame=Random.Range(0,NumDropFrames);
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(frame/10,0.1f));
		GetComponent<Renderer>().material.SetTextureOffset("_BumpMap",new Vector2(frame/10,0.1f));
		
		GetComponent<Renderer>().material.SetColor("_Color",DropsColor);
		GetComponent<Renderer>().material.SetFloat("_BumpAmt",Distortion);
		
		transform.localScale=DropsScale;
		
		if(LiquidumScript.UseRandomScale){
			float Rscale=Random.Range(LiquidumScript.RandomScale.y,LiquidumScript.RandomScale.x);
			transform.localScale=DropsScale*Rscale;
	    }
	
			if(LiquidumScript.RandomSpeed){
			 RandomS=Random.Range(LiquidumScript.DropSlipSpeed*0.5f,LiquidumScript.DropSlipSpeed*5);
	    }else
			{
			 RandomS=1;
	    }
		
		
	}
		
    void OnDrawGizmosSelected() {		
		if(LiquidumScript.ShowGizmoInEditor){
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position,0.001f+(transform.localScale.magnitude/200));
			 }
    }
	
	    void Randomization() {		

			 transform.localScale=new Vector3((transform.localScale.x-Time.deltaTime),transform.localScale.y,(transform.localScale.z+Time.deltaTime));
    }
	
	
	void Update()
	{
		

		//Use Fade to trasparent and destroy
		FadeDrops();
		
		
		//Slip down of Drops
		if(LiquidumScript.DropSlipSpeed>0)
		SlipDrops();
		
		//Quick Fade and destroy
		if(ClearingNow)
		ClearImmediate();
	}
	
	
	void FadeDrops(){
		//FadeDrops
		Color TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),
			Color.clear,LiquidumScript.DropFadeSpeed*Time.deltaTime+0.005f);
		
		GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		if(TargetColor.a<0.001||transform.localPosition.y<-1.2f)Destroy(gameObject);
	}
	
	
	
		void SlipDrops(){
		transform.position= Vector3.Lerp(transform.position,new Vector3(transform.position.x,-0.1f,transform.position.z),(RandomS*LiquidumScript.DropSlipSpeed*Time.deltaTime)/20000);
	}
	
	
	
	
	public void ClearImmediate(){
		ClearingNow=true;
			Color TargetColor=Color.Lerp(GetComponent<Renderer>().material.GetColor("_Color"),Color.clear,2*Time.deltaTime);
		GetComponent<Renderer>().material.SetColor("_Color",TargetColor);
		if(TargetColor.a<0.001)Destroy(gameObject); 
	}
	
	

	
	
	
	
}


