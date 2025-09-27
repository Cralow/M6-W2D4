using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private float checkDistance = 0.2f;
    [SerializeField] private float checkRadious = 0.2f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public UnityEvent<bool> onIsGroundedChanged;
    public int standingLayer;

    public bool IsGrounded { get; private set; }
    private bool prevGrounded;
    
    private void FixedUpdate()
    {
        // Punto di lancio del raycast appena sopra il bordo inferiore del collider
        Vector3 origin = new Vector3(
            transform.position.x,
            GetComponent<Collider>().bounds.min.y + 0.05f,
            transform.position.z);

        bool groundedR = Physics.Raycast(origin, Vector3.down, checkDistance, groundLayers);
        bool groundedS = Physics.CheckSphere(origin, checkRadious, groundLayers);
        
        bool grounded = groundedR ||  groundedS;
       standingLayer = GetTerrainLayerIndex(origin);

        if (grounded != prevGrounded)
            onIsGroundedChanged?.Invoke(grounded);

        IsGrounded = grounded;
        prevGrounded = grounded;
    }
    private int GetTerrainLayerIndex(Vector3 origin)
    {
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit ray, checkDistance))
        {

            return ray.collider.gameObject.layer;
        }



        return -1;
    }
}