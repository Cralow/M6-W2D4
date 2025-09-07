using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarouMaterialController : MonoBehaviour
{
    [SerializeField] Material materialToAnimate;
    [SerializeField] Vector2 offsetSpeed = new Vector2(1f, 3f);

     Vector2 currentOffset = Vector2.zero;

    void Update()
    {
        currentOffset += offsetSpeed * Time.deltaTime;

        if (materialToAnimate != null)
        {
            materialToAnimate.mainTextureOffset = currentOffset;
        }

        // Loop dei valori per evitare overflow
        currentOffset.x %= 3f;
        currentOffset.y %= 3f;      
    }
}