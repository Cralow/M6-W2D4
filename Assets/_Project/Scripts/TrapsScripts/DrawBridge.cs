using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    [SerializeField] private List<GameObject> bridgeObj = new List<GameObject>();
    [SerializeField] private Quaternion bridgeRotationDx;
    [SerializeField] private Quaternion bridgeRotationSx;
    [SerializeField] private float duration;
    [SerializeField] private float bridgeSpeed;
    [SerializeField] private bool isRotZ;


    private void Awake()
    {
        StartCoroutine(RotationTime());
    }

    private IEnumerator RotationTime()
    {
        foreach (GameObject obj in bridgeObj)
        {
            StartCoroutine(RotateBridge(obj));
            isRotZ = !isRotZ;
        }
        yield return new WaitForSeconds(bridgeSpeed);

        foreach (GameObject obj in bridgeObj)
        {
            StartCoroutine(ReturnToBaseRotation(obj));
        }
        yield return new WaitForSeconds(bridgeSpeed);
        StartCoroutine(RotationTime());
    } 

    public IEnumerator RotateBridge(GameObject target)
    {
        Quaternion bridgeRotation = isRotZ? bridgeRotationDx : bridgeRotationSx;

        float timer = 0;
        float rate = 0;

        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            rate = timer / duration;
            target.transform.rotation = Quaternion.Slerp(Quaternion.identity, bridgeRotation, rate);
        }
        target.transform.rotation = bridgeRotation;
        

    }
    public IEnumerator ReturnToBaseRotation(GameObject target)
    {
        Quaternion baseRotation = target.transform.rotation;

        float timer = 0;
        float rate = 0;

        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            rate = timer / duration;
            target.transform.rotation = Quaternion.Slerp(baseRotation, Quaternion.identity, rate);
        }
    }
}
