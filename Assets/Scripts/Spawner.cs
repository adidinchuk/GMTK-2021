using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] bool enabled = true;
    [SerializeField] int spawnRadius = 100;
    [SerializeField] GameObject[] spawnObjectPrefab;
    [SerializeField] float minVelocity = 1f;
    [SerializeField] float maxVelocity = 2f;    
    [SerializeField] public float minSpawnDelay = 2f;
    [SerializeField] public float maxSpawnDelay = 1f;
    [SerializeField] bool zAxisRandomization = true;
    [SerializeField] bool zAxisRotation = true;    
    [SerializeField] float maxZAxisRotationSpeed = 20f;
    [SerializeField] float angleRandomization = 20f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnObject();
        }
    }

    public void StopSpawning()
    {
        enabled = false;
    }

    private void SpawnObject()
    {
        
        int spawnIndex = Random.Range(0, spawnObjectPrefab.Length); //random object from prefab list
        
        GameObject newObject = Instantiate(spawnObjectPrefab[spawnIndex], getRandomCirclePosition(), transform.rotation) as GameObject;
        
        //z rotation
        if (zAxisRandomization)
            newObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));

        Vector3 unitDirection = new Vector3(transform.position.x - newObject.transform.position.x, transform.position.y - newObject.transform.position.y).normalized;

        //randomize direction by the provided angleRandomization factor
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-angleRandomization, angleRandomization));        

        //Apply random force
        newObject.GetComponent<Rigidbody2D>().AddForce((rotation * unitDirection * Random.Range(minVelocity, maxVelocity)));

        if (zAxisRotation)        
            newObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-maxZAxisRotationSpeed, maxZAxisRotationSpeed);

    }

    private Vector2 getRandomCirclePosition()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized;
        spawnPosition.x = (spawnPosition.x  * spawnRadius) + transform.position.x;
        spawnPosition.y = (spawnPosition.y  * spawnRadius) + transform.position.y;
        return spawnPosition ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
