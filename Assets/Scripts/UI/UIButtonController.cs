using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : EffectsSoundDevice
{
    private AudioSource mouseOverSource;
    private AudioSource clickSource;

    [SerializeField]
    private AudioClip mouseOverSound;
    [SerializeField]
    private AudioClip clickSound;

    private float mouseOverVol;
    private float clickVol;
    // Start is called before the first frame update
    void Start()
    {
        float globalVolume = PlayerPrefs.GetFloat("EffectsVolume");
        mouseOverSource = Utils.AddAudioNoFalloff(gameObject, mouseOverSound, false, false, mouseOverVol* globalVolume, 1f);
        clickSource = Utils.AddAudioNoFalloff(gameObject, clickSound, false, false, clickVol * globalVolume, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMouseOverSound()
    {
        if(!mouseOverSource.isPlaying)
            mouseOverSource.Play();
    }

    public void playClickSound()
    {
        if (!clickSource.isPlaying)
            clickSource.Play();
    }

    override public void updateSound()
    {
        float globalVolume = PlayerPrefs.GetFloat("EffectsVolume");
        mouseOverSource.volume = mouseOverVol * globalVolume;
        clickSource.volume = clickVol * globalVolume;
    }

}
