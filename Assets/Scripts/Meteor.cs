using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : EffectsSoundDevice
{
    [SerializeField] float damage = 0;
    [SerializeField] GameObject impactVFX;


    [SerializeField]
    private AudioClip[] impactSoundAray;
    [SerializeField]
    private AudioClip[] impactSoundArayRock;
    [SerializeField]
    private float impactVolume;

    private AudioSource impactSource;

    [SerializeField]
    private AudioClip[] explosionSoundArray;
    [SerializeField]
    private float explosionVolume;

    private AudioSource explosionSource;

    private void Awake()
    {
        impactSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, impactVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f,4,14);
        explosionSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, explosionVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f, 4, 14);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        var health = collision.GetComponent<Health>();
        var entity = collision.GetComponent<Entity>(); ;

        if (entity && health)
        {
            impactVolume += damage / 10;
            if (collision.tag == "Meteor")
            {
                
                playSound(impactSoundArayRock, impactSource);
            }
            else
            {
                playSound(impactSoundAray, impactSource);
            }
            playSound(explosionSoundArray, explosionSource, damage==1?0:damage==3?1:2);
            impactVolume -= damage / 10;
            health.DealDamage(damage);
            var vfx = Instantiate(impactVFX, transform.position, transform.rotation);
            Destroy(vfx, 0.4f);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }
    private void playSound(AudioClip[] array, AudioSource source,int index = -1)
    {
        if(index==-1)
            source.clip = array[Random.Range(0, array.Length)];
        else
            source.clip = array[index];
        source.Play();
    }

    override public void updateSound()
    {
        impactSource.volume = impactVolume * PlayerPrefs.GetFloat("EffectsVolume");
        explosionSource.volume = explosionVolume * PlayerPrefs.GetFloat("EffectsVolume");
    }
}
