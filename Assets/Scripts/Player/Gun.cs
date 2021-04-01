using UnityEngine;

public class Gun : MonoBehaviour
{

    /// <summary>
    /// Damage dealth by the gun.
    /// </summary>
    [SerializeField] private float damage = 10f; // Taken from Emotiv More attention implies greater damage

    /// <summary>
    /// First Person Camera
    /// </summary>
    [SerializeField] private Camera fpsCam;
    
    /// <summary>
    /// Muzzle flash GameObject
    /// </summary>
    [SerializeField] private GameObject muzzleFlash;

    /// <summary>
    /// Impact Effect GameObject
    /// </summary>
    [SerializeField] private GameObject impactFlash;

    /// <summary> 
    /// Location of the Barrel
    /// </summary>
    [SerializeField] private Transform barrelLocation;

    /// <summary>
    /// Sound effect when the player shoots the gun
    /// </summary>
    private AudioSource audioData;

    /// <summary>
    /// Update is called once per frame.
    /// Calls Shoot if the user presses "Fire1"
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Controls the shooting mechanism of the game using Raycasts
    /// </summary>
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject tempFlash;
            GameObject impactEffect;
            tempFlash = Instantiate(muzzleFlash, barrelLocation.position, barrelLocation.rotation);
            impactEffect = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            audioData = GetComponent<AudioSource>();
            if (audioData != null)
            {
                audioData.Play(0);
            }
            Destroy(tempFlash, 2);
            Destroy(impactEffect, 2);
        }
    }
}
