using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : EffectsSoundDevice {

	public AudioClip[] breakingSoundAray;
	public float volume;
	private AudioSource source;

    private void Awake()
    {
        Utils.AddAudioNoFalloff(gameObject, null, false, false, volume * PlayerPrefs.GetFloat("EffectsVolume"), 1f);

    }

    // Use this for initialization
    void Start () {
        Utils.AddAudioNoFalloff(gameObject, null, false, false, 0.5f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playAudio()
    {
        source.clip = breakingSoundAray[Random.Range(0, breakingSoundAray.Length)];
        source.Play();
    }
	
	override public void updateSound(){
		source.volume = volume * PlayerPrefs.GetFloat("EffectsVolume");
	}
}
