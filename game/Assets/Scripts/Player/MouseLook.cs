using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{
    /// <summary>
    /// Sensitivity of the mouse
    /// </summary>
    [SerializeField] private float mouseSensitivity = 1000;

    /// <summary>
    /// X-rotation
    /// </summary>
    private float xRotation = 0f;

    /// <summary>
    /// Transform of the player
    /// </summary>
    [SerializeField] private Transform player;

    /// <summary>
    /// Start is called before the first frame update.
    /// Locks the cursor
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Update is called once per frame.
    /// Controls the view of the player.
    /// </summary>
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
    }
}
