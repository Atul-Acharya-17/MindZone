using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Array that stores the GameObjects to be spawned.
    /// </summary>
    public GameObject[] spawnees;

    /// <summary>
    /// The time delay between each spawn
    /// </summary>
    [SerializeField] private float spawnDelay = 5.0f;

    /// <summary>
    /// The time to start spawning
    /// </summary>
    [SerializeField] private float spawnTime = 1.0f;

    /// <summary>
    /// Revolution speed of the Spawner
    /// </summary>
    public float motionSpeed;

    /// <summary>
    /// Object to rotate around
    /// </summary>
    public Transform axisObject;

    [SerializeField] private float threshold = 0.15f;

    /// <summary>
    /// Start is called before the first frame update
    /// Repeatedly invokes the spawnObject method with a delay.
    /// </summary>
    void Start()
    {
        if (spawnees[0] != null && spawnees[1] != null && spawnDelay > 0.0f && spawnTime >= 0.0f)
        {
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// Cancels the spawnObject method from being invoked when the game is over.
    /// </summary>
    void Update ()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gm != null)
        {
            if (gm.IsGameOver())
            {
                CancelInvoke();
            }
        }

        Movement();
    }

    /// <summary>
    /// Instantiates the spawnee.
    /// </summary>
    void SpawnObject()
    {
        float probability = Random.Range(0.0f, 1.0f);
        int spawneeIndex = 0;
        if (probability > threshold)
        {
            spawneeIndex = 1;
        }
        Instantiate(spawnees[spawneeIndex], transform.position, transform.rotation);
        
        if (threshold <= 0.75f) 
        {
            threshold += 0.01f;
        }
    }


    /// <summary>
    /// Moves the spawner around the axisObject to create random position.
    /// </summary>
    void Movement()
    {
        transform.RotateAround(axisObject.transform.position, axisObject.transform.up, motionSpeed * Time.deltaTime);
    }

}
