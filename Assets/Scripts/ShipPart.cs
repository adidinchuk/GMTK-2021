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
    public int score = 100;

    [SerializeField]
    private AudioClip[] fuseSoundAray;
    [SerializeField]
    private float fuseVolume;

    private AudioSource fuseSource;

    [SerializeField]
    private AudioClip[] breakSoundAray;
    [SerializeField]
    private float breakVolume;

    private AudioSource breakSource;


    private void Awake()
    {
        collider2d = gameObject.GetComponent<Collider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        fuseSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, fuseVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f);
        breakSource = Utils.AddAudioNoFalloff(gameObject, null, false, false, breakVolume * PlayerPrefs.GetFloat("EffectsVolume"), 1f);

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
        if (mouseOver && Input.GetMouseButtonDown(1))
        {
            Explode();
        }

        if (!collider2d.enabled && allowJointsTimer <= 0f)
        {
            allowJoints = true;
        }

        allowJointsTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!allowJoints) return;

        ShipPart otherShipPart = col.gameObject.GetComponent<ShipPart>();
        if (otherShipPart == null) return;

        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = col.rigidbody;
        joint.breakForce = 100;
        joint.breakTorque = 100;

        playSound(fuseSoundAray, fuseSource);
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

        playSound(breakSoundAray, breakSource);

        // Send in random direction with collider2d disabled for a few seconds
        Vector2 direction = Utils.GetRandomDirection() ;
        float force = Random.Range(explosionForce.x, explosionForce.y) * rigidbody2d.mass;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        allowJoints = false;
        allowJointsTimer = allowJointsTimerMax;
    }

    private void playSound(AudioClip[] array, AudioSource source)
    {
        source.clip = array[Random.Range(0, array.Length)];
        source.Play();
    }

    override public void updateSound()
    {
        fuseSource.volume = fuseVolume * PlayerPrefs.GetFloat("EffectsVolume");
        breakSource.volume = breakVolume * PlayerPrefs.GetFloat("EffectsVolume");
    }

    public int GetScore(ShipPart shipPart)
    {
        return score;
    }
    public IEnumerable<ShipPart> Neighbors(ShipPart shipPart)
    {
        FixedJoint2D[] fixedJoints = shipPart.GetComponents<FixedJoint2D>();


        foreach (FixedJoint2D fixedJoint in fixedJoints)
        {
            ShipPart neighoringShipPart = fixedJoint.connectedBody.gameObject.GetComponent<ShipPart>();
            yield return neighoringShipPart;
        }

    }
}
