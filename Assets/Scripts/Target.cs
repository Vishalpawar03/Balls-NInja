using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -5f;
    private Rigidbody targetRb;
    private GameManager gameManagerScript;

    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        // It gets rigidbody component of the gameObject
        targetRb = GetComponent<Rigidbody>();

        // It applies random force to the gameObject in upward direction 
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        // It applies random torque to the gameObject
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // It spawns the gameObject at random x-value
        transform.position = RandomSpawnPosition();
    }

    public void DestroyTarget()
    {
        if(gameManagerScript.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManagerScript.UpdateScore(pointValue);
        }

        if(gameObject.CompareTag("Bad") && gameManagerScript.isGameActive)
        {
            gameManagerScript.isClickedOnBad = true;
            gameManagerScript.lives = 0;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if(gameObject.CompareTag("Good"))
        {
            gameManagerScript.lives--;
        }
    }
    

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
