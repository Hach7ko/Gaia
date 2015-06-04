using UnityEngine;
using System.Collections;

/*
In most cases if quit the level when the Sky is under a thunderlight, the color stay of lap color because the SkyBox material 
is not instanced but is the original material in the asset.
Use this script if you want preseve your SkyBox color after application quit.
Drag PreserveSkyBoxColor script under your camera.
*/

[ExecuteInEditMode]
public class PreserveSkyBoxColor : MonoBehaviour {

	public static Color SkyBoxOriginalColor;
	
		void Start () {
		if (Application.isPlaying)	
		SkyBoxOriginalColor=GetComponent<Skybox>().material.GetColor("_Tint");
		else
		GetComponent<Skybox>().material.SetColor("_Tint",SkyBoxOriginalColor);

	}
	
	

}
