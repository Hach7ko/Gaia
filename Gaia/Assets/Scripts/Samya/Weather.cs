using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour {

	public Liquidum _liquid;
	public Light sun;

	public GameObject[] shark;
	public GameObject[] panda;
	public GameObject[] human;
	public void SetWeatherEffects(string effectToActive) {

		_liquid.RainEmit = true;
		switch(effectToActive)
		{
			case "rain": 
			_liquid.RainEmit = true;
			_liquid.SnowEmit = false;
			sun.intensity = 1f;

			SharkActivate(true);
			PandaActivate(false);
			HumanActivate(false);
			break;

			case "sun": 
			_liquid.RainEmit = false;
			_liquid.SnowEmit = false;
			sun.intensity = 3f;
			
			SharkActivate(false);
			PandaActivate(false);
			HumanActivate(false);
			break;

			case "snow": 
			_liquid.RainEmit = false;
			_liquid.SnowEmit = true;
			sun.intensity = 0f;

			
			
			SharkActivate(false);
			PandaActivate(true);
			HumanActivate(false);
			break;

			case "wind": 
			_liquid.RainEmit = false;
			_liquid.SnowEmit = false;
			sun.intensity = 1f;

			              
			              
              SharkActivate(false);
              PandaActivate(false);
              HumanActivate(true);
			break;
		}
	}

	public void SharkActivate(bool toto) {

		for(int i = 0; i < shark.Length; i++) {
			shark[i].SetActive(toto);
		}
	}

	public void PandaActivate(bool toto) {
		
		for(int i = 0; i < panda.Length; i++) {
			panda[i].SetActive(toto);
		}
	}

	public void HumanActivate(bool toto) {
		
		for(int i = 0; i < human.Length; i++) {
			human[i].SetActive(toto);
		}
	}
}
