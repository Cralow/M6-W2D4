using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepController : MonoBehaviour
{
    [SerializeField] private GroundChecker groundChecker;

    private void Awake()
    {
        groundChecker = GetComponent<GroundChecker>();
    }
    public void PlayFootStepSound()
    {
        int terrainLayer = groundChecker.standingLayer;
       // Debug.Log($"layer del terreno : {terrainLayer}");
        if (terrainLayer != -1)
        {           
            AudioManager.Instance.RandomTerrainSound(terrainLayer);
        }
        else
        {
        }
    }



}
