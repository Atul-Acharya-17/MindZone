using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// CharacterController of the player
    /// </summary>
    public CharacterController controller;

    /// <summary>
    /// Speed of the player
    /// </summary>
    [SerializeField] private float speed = 15f;

    /// <summary>
    /// Velocity of the player
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    /// Gravity of the world
    /// </summary>
    private const float gravity = -9.81f;

    /// <summary>
    /// Update is called once per frame.
    /// Controls the movement of the player.
    /// </summary>
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;   
        controller.Move(move * speed * Time.deltaTime); 

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
