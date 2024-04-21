using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private ParticleSystem explosionParticle;
  
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float minSpeed = 13;
    private float maxSpeed = 18;
    private float maxTorque = 15;
    private float xRange = 4;
    private float ySpawnPos = -6;

    

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();  
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()  // toss objects
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()  //rotate objects
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()  //random spawn position
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    public void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }     
    }

    private void OnTriggerEnter(Collider other)
    {     
        Destroy(gameObject);
        
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
            
        }
    }
}

