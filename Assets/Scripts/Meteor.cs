using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float damage = 0;
    [SerializeField] GameObject impactVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        var health = collision.GetComponent<Health>();
        var entity = collision.GetComponent<Entity>(); ;

        if (entity && health)
        {
            health.DealDamage(damage);
            var vfx = Instantiate(impactVFX, transform.position, transform.rotation);
            Destroy(vfx, 0.3f);
            Destroy(gameObject);
        }
    }
}
