using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;
    private GameManager gameManagerSc;
    private Rigidbody targetRb;
    private float minForce = 12f;
    private float maxForce = 16f;
    private float maxTorque = 10f;
    private float xBorder = 4f;
    private float yBorder = -2f;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManagerSc = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = (RandomSpawnPos());
        
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManagerSc.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameManagerSc.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gameManagerSc.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xBorder, xBorder), yBorder);
    }
}
