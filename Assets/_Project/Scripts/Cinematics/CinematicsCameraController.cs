using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsCameraController : MonoBehaviour
{
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject cameraObj1;
    [SerializeField] private GameObject cameraObj2;

    private void Awake()
    {
        StartCoroutine(AnimationRoutine());
    }
    public IEnumerator AnimationRoutine()
    {

        yield return new WaitForSeconds(animationTime);
        cameraObj1.SetActive(false);
        cameraObj2.SetActive(true);
        StartCoroutine(AnimationRoutine2());
    }
    public IEnumerator AnimationRoutine2()
    {

        yield return new WaitForSeconds(animationTime);
        //carica scena
    }
}
