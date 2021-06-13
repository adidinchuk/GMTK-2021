using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;


    [SerializeField] float health = 100f;
    float startHealth;

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

    private void Die()
    {
        Debug.Log("Died");
        OnDied?.Invoke(this, EventArgs.Empty);
        var entity = gameObject.GetComponent<Entity>();
        if (entity)
        {
            var death = entity.GetDeathAnimation();
            var vfx = Instantiate(death, transform.position, transform.rotation);
            Destroy(vfx, 0.4f);
        }
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
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
}
