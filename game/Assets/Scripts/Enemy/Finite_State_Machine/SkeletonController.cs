using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    /// <summary>
    /// Player to attack
    /// </summary>
    private Transform player;

    /// <summary>
    /// Movement speed of the skeleton
    /// </summary>
    [SerializeField] private float movementSpeed;

    /// <summary>
    /// Time delay between attacks
    /// </summary>
    private float timeToAttack;

    /// <summary>
    /// Attack damage of the skeleton
    /// </summary>
    [SerializeField] private float attackPower;

    /// <summary>
    /// Attack sound effect of the skeleton
    /// </summary>
    private AudioSource audioData;

    /// <summary>
    /// Start is called before the first frame update
    /// Initialises data members
    /// </summary>
    void Start()
    {
        timeToAttack = 0.0f;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    /// <summary>
    /// Turns towards the player
    /// </summary>
    public void FacePlayer()
    {
        transform.LookAt(player);
    }

    /// <summary>
    /// Chases the player
    /// </summary>
    public void Chase()
    { 
        transform.position += (transform.forward * Time.deltaTime * movementSpeed);
    }

    /// <summary>
    /// Attacks the player
    /// </summary>
    public void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 10.0f && Time.time >= timeToAttack)
        {
            transform.position += (transform.forward * Time.deltaTime * movementSpeed * 0.25f);
            timeToAttack = (Time.time + 1f);
            GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            if (gm != null)
            {
                gm.DecreaseHealth(attackPower);
            }
            PlayAudio();
        }
    }

    /// <summary>
    /// Destroys the gameObject
    /// </summary>
    public void Die()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// Plays the attack sound effect
    /// </summary>
    void PlayAudio()
    {
        audioData = GetComponent<AudioSource>();
        if (audioData != null)
        {
            audioData.Play(0);
        }
    }
}
