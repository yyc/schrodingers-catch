using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedSoundComponent : MonoBehaviour {

	public enum Type
	{
		Effect,
		Music
	}

	public Type type;
	AudioSource audiosource;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(type == Type.Effect){ 
			audiosource.volume = VolumeComponent.fxVolume;
		} else {
			audiosource.volume = VolumeComponent.musicVolume;
		}
	}
}
