using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : EffectsSoundDevice
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField]
    private AudioClip[] breakSoundAray;
    [SerializeField]
    private float breakVolume;

    private AudioSource breakSource;


    [SerializeField] float health = 100f;
    float startHealth;

    private void Awake()
    {
        breakSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, breakVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f, 4, 14);
    }

    private void Start()
    {
        startHealth = health;
    }

    public void DealDamage(float damage)
    {
        health = health - damage;
        
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health <= 0)
        {
            Die();
        }
        UpdateSprite();


    }

    private void PlaySound(AudioClip[] audioClips, AudioSource source)
    {
        if (audioClips.Length > 0)
        {
            source.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            source.Play();
        }
        else
        {
            Debug.LogWarning("Tried to play audio clips but none exist on gameobject: " + this.name);
        }
    }

    private void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
        var entity = gameObject.GetComponent<Entity>();
        if (entity)
        {
            var death = entity.GetDeathAnimation();
            var vfx = Instantiate(death, transform.position, transform.rotation);
            PlaySound(breakSoundAray, breakSource);
            Debug.Log("DESTROYING");
            Destroy(vfx, 0.6f);
            Destroy(gameObject, 0.8f);
        }

        foreach (SpriteRenderer sprite in GetComponents<SpriteRenderer>())
        {
            sprite.enabled = false;
        }        
       
        Collider2D collider2D = GetComponent<Collider2D>();
        
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }
        else
        {
            Debug.LogWarning("No collider2D on: " + this.name);
        }
        Destroy(gameObject, 0.5f);
    }

    public float GetHealth()
    {
        return health;
    }

    private void UpdateSprite()
    {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = new Color(health / startHealth, health / startHealth, health / startHealth, 1f);
        }
    }

    override public void updateSound()
    {
        breakSource.volume = breakVolume * PlayerPrefs.GetFloat("EffectsVolume");
    }
}
