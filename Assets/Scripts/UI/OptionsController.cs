using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectsSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
    }

    public void updateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        MusicManager.getInstance().updateSound();
    }

    public void updateEffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.value);
        EffectsSoundDevice[] gameSounds = (EffectsSoundDevice[])GameObject.FindObjectsOfType(typeof(EffectsSoundDevice));
        for (int i = 0; i < gameSounds.Length; i++)
        {
            gameSounds[i].updateSound();
        }
    }
}
