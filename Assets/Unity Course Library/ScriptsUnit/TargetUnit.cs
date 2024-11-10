using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private Rigidbody targetRb;
    private const string _goodLayer = "Good Layer";
    private const string _badLayer = "Bad Layer";

    private float minSpeed = 12f, maxSpeed = 16f;

    private float maxTorque = 10f;

    private float xRange = 4;

    private float ySpawnPos = -2;

    private GameManager gameManager;

    private const string _badObject = "Bad Layer";

    public ParticleSystem boomxplosion;

    
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        
        targetRb.AddTorque(RandomTorque(), RandomTorque() , RandomTorque(),ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("SystemsManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void  OnMouseUp()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);

            gameManager.UpdateScore(5);
            Explode();
        }
        
    }

    /* private void OnMouseDrag()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);

            gameManager.UpdateScore(5);
            Explode();
        }
    } */

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque,maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    
    }

    void Explode()
    {
        Instantiate(boomxplosion,transform.position, boomxplosion.transform.rotation);
    }
}
