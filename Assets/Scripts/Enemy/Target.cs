using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /// <summary>
    /// Enemy health
    /// </summary>
    [SerializeField] private float health;

    /// <summary>
    /// Accessor for the health
    /// </summary>
    public float Health => health;

    /// <summary>
    /// Decreases health based on the damage taken
    /// </summary>
    /// <param name="damage"> Damage received by the Enemy.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}