using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private float checkDistance = 0.2f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public UnityEvent<bool> onIsGroundedChanged;

    public bool IsGrounded { get; private set; }
    private bool prevGrounded;

    private void FixedUpdate()
    {
        // Punto di lancio del raycast appena sopra il bordo inferiore del collider
        Vector3 origin = new Vector3(
            transform.position.x,
            GetComponent<Collider>().bounds.min.y + 0.05f,
            transform.position.z);

        bool grounded = Physics.Raycast(origin, Vector3.down, checkDistance, groundLayers);
        if (grounded != prevGrounded)
            onIsGroundedChanged?.Invoke(grounded);

        IsGrounded = grounded;
        prevGrounded = grounded;
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.CompareTag("PiattaformaRotante"))
        {
            transform.SetParent(hit.collider.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("PiattaformaRotante"))
        {
            transform.SetParent(null);
        }
    }
}