using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : EffectsSoundDevice, Graph<ShipPart>
{
    public int jointBreakingForce = 100;
    public float allowJointsTimerMax = 2f;
    public Vector2 explosionForce = new Vector2(30f,70f);

    private bool mouseOver = false;
    private float allowJointsTimer = 0f;
    private bool allowJoints = true;
    private Collider2D collider2d;    
    private Rigidbody2D rigidbody2d;
    
    [SerializeField]
    private AudioClip[] fuseSoundAray;
    [SerializeField]
    private float fuseVolume;

    private AudioSource fuseSource;


    private void Awake()
    {
        collider2d = gameObject.GetComponent<Collider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        fuseSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, fuseVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f, 4, 14);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }



    private void Update()
    {
        //if (mouseOver && Input.GetMouseButtonDown(1))
        //{
        //    Explode();
        //}

        //if (!collider2d.enabled && allowJointsTimer <= 0f)
        //{
        //    allowJoints = true;
        //}

        //allowJointsTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!allowJoints) return;

        ShipPart otherShipPart = col.gameObject.GetComponent<ShipPart>();
        if (otherShipPart == null) return;

        PlaySound(fuseSoundAray, fuseSource);

        FixedJoint2D jointA = gameObject.AddComponent<FixedJoint2D>();
        jointA.connectedBody = col.rigidbody;
        jointA.enableCollision = false;
        jointA.breakForce = 100;
        jointA.breakTorque = 100;


        FixedJoint2D jointB = col.gameObject.AddComponent<FixedJoint2D>();
        jointB.connectedBody = this.rigidbody2d;
        jointB.enableCollision = false;
        jointB.breakForce = 100;
        jointB.breakTorque = 100;
        // Check if connected to main ship, if so add points

    }

    public void removeJointsConnectedTo(GameObject other)
    {

        FixedJoint2D[] fixedJoints = gameObject.GetComponents<FixedJoint2D>();

        for (int i = 0; i < fixedJoints.Length; i++)
        {
            if (fixedJoints[i].connectedBody.gameObject.GetInstanceID() == other.GetInstanceID())
            {
                Destroy(fixedJoints[i]);
            }
        }
    }

    private void Explode()
    {

        // Find connected objects
        FixedJoint2D[] fixedJoints = gameObject.GetComponents<FixedJoint2D>();

        for (int i = 0; i < fixedJoints.Length; i++)
        {
            ShipPart shipPart = fixedJoints[i].connectedBody.gameObject.GetComponent<ShipPart>();

            if (shipPart != null)
            {
                shipPart.removeJointsConnectedTo(this.gameObject);
            }

            Destroy(fixedJoints[i]);
        }        

        // Send in random direction with collider2d disabled for a few seconds
        Vector2 direction = Utils.GetRandomDirection() ;
        float force = Random.Range(explosionForce.x, explosionForce.y) * rigidbody2d.mass;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        allowJoints = false;
        allowJointsTimer = allowJointsTimerMax;
    }

    private void PlaySound(AudioClip[] audioClips, AudioSource source)
    {
        if (audioClips.Length > 0)
        {
            source.clip = audioClips[Random.Range(0, audioClips.Length)];
            source.Play();
        } else
        {
            Debug.LogWarning("Tried to play audio clips but none exist on gameobject: " + this.name);
        }
    }

    override public void updateSound()
    {
        fuseSource.volume = fuseVolume * PlayerPrefs.GetFloat("EffectsVolume");
    }

    public int GetWeight(ShipPart shipPart)
    {
        return (int)rigidbody2d.mass * 10;
    }

    public IEnumerable<ShipPart> Neighbors(ShipPart shipPart)
    {
        FixedJoint2D[] fixedJoints = shipPart.GetComponents<FixedJoint2D>();


        foreach (FixedJoint2D fixedJoint in fixedJoints) { 

            Rigidbody2D connectedBody = fixedJoint.connectedBody;

            if (!connectedBody)
            {
                Destroy(fixedJoint);
                continue;
            }

            ShipPart neighoringShipPart = connectedBody.GetComponent<ShipPart>();
            if (!neighoringShipPart)
            {
                Destroy(fixedJoint);
                continue;
            }
            
            yield return neighoringShipPart;
        }

    }
}
