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

    private void Awake()
    {
        impactSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, impactVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        var health = collision.GetComponent<Health>();
        var entity = collision.GetComponent<Entity>(); ;

        if (entity && health)
        {
            if (collision.tag == "Meteor")
            {
                playSound(impactSoundArayRock, impactSource);
            }
            else
            {
                playSound(impactSoundAray, impactSource);
            }
            health.DealDamage(damage);
            var vfx = Instantiate(impactVFX, transform.position, transform.rotation);
            Destroy(vfx, 0.4f);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }
    private void playSound(AudioClip[] array, AudioSource source)
    {
        source.clip = array[Random.Range(0, array.Length)];
        source.Play();
    }

    override public void updateSound()
    {
        impactSource.volume = impactVolume * PlayerPrefs.GetFloat("EffectsVolume");
    }
}
