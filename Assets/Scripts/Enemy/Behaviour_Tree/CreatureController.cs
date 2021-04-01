using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    /// <summary>
    /// Player to attack
    /// </summary>
    private Transform player;

    /// <summary>
    /// Walking speed of the creature
    /// </summary>
    [SerializeField] private float walkSpeed = 5;

    /// <summary>
    /// Running speed of the creature
    /// </summary>
    [SerializeField] private float runSpeed = 10;

    /// <summary>
    /// Attack damage of the creature
    /// </summary>
    [SerializeField] private float attackPower;

    /// <summary>
    /// Attack sound effect of the creature
    /// </summary>
    private AudioSource audioData;

    // Start is called before the first frame update
    /// <summary>
    /// Initialises data members
    /// </summary>
    void Start()
    {
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
    /// Idle action
    /// </summary>
    public void Idle()
    {
        FacePlayer();
    }

    /// <summary>
    /// Walking action
    /// </summary>
    public void Walk()
    {
        FacePlayer();
        transform.position += (transform.forward * Time.deltaTime * walkSpeed);
    }

    /// <summary>
    /// Running action
    /// </summary>
    public void Run()
    {
        FacePlayer();
        transform.position += (transform.forward * Time.deltaTime * runSpeed);
    }

    /// <summary>
    /// Death action
    /// </summary>
    public void Die()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 2.5f);
    }

    /// <summary>
    /// Howl action
    /// </summary>
    public void Howl()
    {
        FacePlayer();
    }

    /// <summary>
    /// Attack action
    /// </summary>
    public void Attack()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gm != null)
        {
            gm.DecreaseHealth(attackPower);
        }
        PlayAudio();
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
