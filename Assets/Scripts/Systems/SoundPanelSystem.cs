using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundPanelSystem : MonoBehaviour {

	public Slider sfxSlider;
	public Slider musicSlider;
	 	
	void Start() {
		sfxSlider.value = VolumeComponent.fxVolume;
		musicSlider.value = VolumeComponent.musicVolume;
	}
	// Update is called once per frame
	void Update () {
		VolumeComponent.fxVolume = sfxSlider.value;
		VolumeComponent.musicVolume = musicSlider.value;
	}
}
